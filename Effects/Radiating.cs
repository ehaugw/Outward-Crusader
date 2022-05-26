using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crusader
{
    using HarmonyLib;
    using SideLoader;
    using UnityEngine;

    public class Radiating : Effect
    {
        public const float RANGE = 10f;
        public const float LIFE_SPAN = 15;
        public const float DECAY = 10f;
        public const float DAMAGE = 15f;

        protected override void ActivateLocally(Character character, object[] _infos)
        {
            float lifespan = LIFE_SPAN;
            
            if (At.GetField<Effect>(this, "m_parentStatusEffect") is StatusEffect parent && parent.SourceCharacter != null)
            {
                float x = (lifespan - parent.RemainingLifespan);
                float dmg = HolyDamageManager.HolyDamageManager.BuffHolyDamageOrHealing(parent.SourceCharacter, Radiating.DAMAGE * Convert.ToSingle(Math.Exp(-x / Radiating.DECAY)) / Radiating.normalize());

                List<Character> charsInRange = new List<Character>();
                CharacterManager.Instance.FindCharactersInRange(character.CenterPosition, RANGE, ref charsInRange);

                foreach (Character chr in charsInRange.Where(c => !c.IsAlly(parent.SourceCharacter)))
                {
                    if (!chr.Invincible)
                    {
                        //var damage = new PunctualDamage();
                        //damage.Damages = new DamageType[] { new DamageType(HolyDamageManager.HolyDamageManager.GetDamageType(), dmg)};
                        //damage.Knockback = dmg;
                        //damage.OnReceiveActivation(chr,null);

                        var damageList = new DamageList(HolyDamageManager.HolyDamageManager.GetDamageType(), dmg);
                        var knockback = dmg;
                        chr.Stats.GetMitigatedDamage(null, ref damageList, true);
                        chr.Stats.ReceiveDamage(damageList.TotalDamage);
                        //Debug.Log(chr.Name + " took " + damageList.TotalDamage + " damage.");
                    }
                }
            } else
            {
                Debug.Log("Radiating Effect had not parent Status Effect");
            }

        }


        public static float normalize()
        {
            double r = Math.Exp(-1 / Radiating.DECAY);
            double n = LIFE_SPAN;

            return Convert.ToSingle((1 - Math.Pow(r, n)) / (1 - r));
        }

        public static float sumTicks(float remain)
        {
            double r = Math.Exp(-1 / Radiating.DECAY);
            double n = LIFE_SPAN;

            return Convert.ToSingle((Math.Pow(r, (n - remain)) - Math.Pow(r, n)) / (1 - r));
        }

        public static float sumToRemain(float s)
        {
            var r = Math.Exp(-1 / Radiating.DECAY);
            double n = LIFE_SPAN;
            return Convert.ToSingle(n - (Math.Log(s * (1 - r) + Math.Pow(r, n))) / Math.Log(r));
        }
    }

    [HarmonyPatch(typeof(StatusEffectManager), "AddStatusEffect", new Type[] { typeof(StatusEffect), typeof(Character), typeof(string[]) })]
    public class StatusEffectManager_AddStatusEffect_Radiating
    {
        [HarmonyPrefix]
        public static void Prefix(StatusEffectManager __instance, StatusEffect _statusEffect, out StatusData __state)
        {
            __state = null;
            if (_statusEffect.IdentifierName == ModTheme.RadiatingEffectName && __instance.HasStatusEffect(_statusEffect.IdentifierName))
            {
                __state = _statusEffect.StatusData;
                _statusEffect.StatusData = new StatusData(__state);

                float xe = __instance.GetStatusEffectOfName(_statusEffect.IdentifierName).RemainingLifespan;
                float xn = _statusEffect.RemainingLifespan != 0 ? _statusEffect.RemainingLifespan : _statusEffect.StartLifespan;

                float dmgE = Radiating.sumTicks(xe);
                float dmgN = Radiating.sumTicks(xn);

                xn = Radiating.sumToRemain(dmgE + dmgN);

                _statusEffect.StatusData.LifeSpan = xn;
            }
        }

        [HarmonyPostfix]
        public static void Postfix(StatusEffectManager __instance, StatusEffect _statusEffect, StatusData __state)
        {
            if (_statusEffect?.StatusData != null && __state != null)
            {
                _statusEffect.StatusData = __state;
            }
        }
    }
}
