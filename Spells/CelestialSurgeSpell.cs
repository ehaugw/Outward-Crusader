using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Crusader
{
    public class CelestialSurgeSpell
    {
        public const float DAMAGE = 40;
        public const float RANGE = 35;
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Celestial Surge",
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.blessID,
                New_ItemID = IDs.celestialSurgeID,
                //SLPackName = Crusader.ModFolderName,
                //SubfolderName = "channel Divinity",
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
                ManaCost = 600,
            };

            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            var myEffects = skill.transform.Find("Effects");
            myEffects.gameObject.AddComponent<CelestialSurge>();

            GameObject.Destroy(skill.gameObject.GetComponentInChildren<AddStatusEffect>());

            return skill;
        } //PART OF CHANNEL DIVINITY
    }
}
