using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Crusader
{
    using TinyHelper;
    using EffectSourceConditions;

    public class CureWoundsSpell
    {
        [HarmonyPatch(typeof(Character), "SpellCastAnim")]
        public class Character_SpellCastAnim
        {
            [HarmonyPrefix]
            public static void Prefix(Character __instance, ref Character.SpellCastType _type, ref Character.SpellCastModifier _modifier, Animator ___m_animator,  int _sheatheRequired = 1)
            {
                //if (_type == Character.SpellCastType.ChakramArc && _modifier == Character.SpellCastModifier.Mobile)
                //    _type += 1000;
                //___m_animator.SetInteger("SpellType", (int)(_type + 2000));
                //___m_animator.SetTrigger("Spell");
            }
        }

        public const string ItemName = "Cure Wounds";

        public static void Prepare()
        {
            var myitem = new SL_Skill()
            {
                Name = ItemName,
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.blessID,
                New_ItemID = IDs.cureWoundsID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Cure Wounds",
                Description = "A swift spell that restores some health.",
                CastType = Character.SpellCastType.Fast,
                CastModifier = Character.SpellCastModifier.Mobile,
                CastLocomotionEnabled = true,
                MobileCastMovementMult = 0.7f,

                EffectTransforms = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "Effects",
                        Effects = new SL_Effect[] {
                        }
                    }
                },

                Cooldown = 2f,
                StaminaCost = 0,
                HealthCost = 0,
                ManaCost = 14,
            };
            myitem.ApplyTemplate();
        }

        public static Skill Init()
        {
            Prepare();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(IDs.cureWoundsID) as Skill;
            
            //EmptyOffHandCondition.AddToSkill(skill, false, false);

            Transform effects;

            effects = TinyGameObjectManager.MakeFreshObject(EffectSourceConditions.EFFECTS_CONTAINER, true, true, skill.transform).transform;
            var healingAoE = effects.gameObject.AddComponent<HealingAoE>();
            healingAoE.Range = 30;
            setHealing(healingAoE, 2);
            StatusEffectCondition condition;
            condition = effects.gameObject.AddComponent<StatusEffectCondition>();
            condition.StatusEffectPrefab = Crusader.Instance.healingSurgeInstance;

            effects = TinyGameObjectManager.MakeFreshObject(EffectSourceConditions.EFFECTS_CONTAINER, true, true, skill.transform).transform;
            var healing = effects.gameObject.AddComponent<HealingAoE>();
            healing.Range = 15;
            setHealing(healing);
            condition = effects.gameObject.AddComponent<StatusEffectCondition>();
            condition.Inverse = true;
            condition.StatusEffectPrefab = Crusader.Instance.healingSurgeInstance;

            return skill;
        }

        private static void setHealing(Healing healing, float multiplier = 1)
        {
            healing.RestoredHealth = 10 * multiplier;
            healing.AmplificationType = HolyDamageManager.HolyDamageManager.GetDamageType();
            healing.CanRevive = false;
        }
    }
}
