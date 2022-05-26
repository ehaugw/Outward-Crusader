using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Crusader
{
    public class PrayerOfHealingSpell
    {
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Prayer of Healing",
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.blessID,
                New_ItemID = IDs.prayerOfHealingID,
                //SLPackName = Crusader.ModFolderName,
                //SubfolderName = "Channel Divinity",
                Description = "WIP",
                CastType = Character.SpellCastType.CallElements,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1,
                CastSheatheRequired = 2,

                EffectTransforms = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "ActivationEffects",
                        Effects = new SL_Effect[] {
                        }
                    },

                    new SL_EffectTransform() {
                        TransformName = "Effects",
                        Effects = new SL_Effect[] {
                        }
                    }
                },

                Cooldown = 30,
                StaminaCost = 0,
                HealthCost = 0,
                ManaCost = 40,
            };

            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            var myEffects = skill.transform.Find("Effects");
            HealingAoE healing = myEffects.gameObject.AddComponent<HealingAoE>();
            healing.RestoredHealth  = 30;
            healing.Range           = 30;
            healing.CanRevive       = true;
            healing.AmplificationType = HolyDamageManager.HolyDamageManager.GetDamageType();

            GameObject.Destroy(skill.gameObject.GetComponentInChildren<AddStatusEffect>());

            return skill;
        } //PART OF CHANNEL DIVINITY
    }
}