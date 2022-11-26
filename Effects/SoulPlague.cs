using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crusader
{
    using HarmonyLib;
    using SideLoader;
    using UnityEngine;

    public class SoulPlague : Effect
    {
        public const float LIFE_SPAN = 100;
        public const float DAMAGE = 20f;

        protected override void ActivateLocally(Character character, object[] _infos)
        {
            if (character.transform.Find("SoulSpot") is Transform soulSpot)
            {
                var damageList = new DamageList(HolyDamageManager.HolyDamageManager.GetDamageType(), DAMAGE/LIFE_SPAN);
                character.Stats.GetMitigatedDamage(null, ref damageList, true);
                character.Stats.ReceiveDamage(damageList.TotalDamage);
                if (At.GetField<Effect>(this, "m_parentStatusEffect") is StatusEffect parent && parent.SourceCharacter != null)
                {
                    parent.SourceCharacter.Stats.AffectHealth(damageList.TotalDamage);
                }
            }
        }
    }
}
