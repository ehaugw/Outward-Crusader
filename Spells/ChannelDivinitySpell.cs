using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TinyHelper;

namespace Crusader
{
    public class ChannelDivinitySpell
    {
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = ModTheme.ChannelDivinitySpellName,
                EffectBehaviour  = EditBehaviours.Override,
                Target_ItemID = IDs.blessID, //Bless
                New_ItemID = IDs.channelDivinityID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Channel Divinity",
                Description = "You channel your divinity, drastically increasing your " + ModTheme.BurstOfDivinityEffectName + " buildup, or produces combo effects when casted in combination with a Rune spell.",
                CastType = Character.SpellCastType.CallElements,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1,
                CastSheatheRequired = 1,

                EffectTransforms = new SL_EffectTransform[] {
                    //new SL_EffectTransform() {
                    //    TransformName = "ActivationEffects",
                    //    Effects = new List<SL_Effect>() {
                    //    }
                    //},

                    new SL_EffectTransform() {
                        TransformName = "Effects",
                        Effects = new SL_Effect[] {
                        }
                    }
                },

                Cooldown = 300,
                StaminaCost = 0,
                HealthCost = 0,
                ManaCost = 7,
            };

            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;
            //EmptyOffHandCondition.AddToSkill(skill, true, true);

            var myEffects = skill.transform.Find("Effects");
            myEffects.gameObject.AddComponent<ChannelDivinity>();
            GameObject.Destroy(skill.gameObject.GetComponentInChildren<AddStatusEffect>());


            new SL_PlaySoundEffect()
            {
                Follow = true,
                OverrideCategory = EffectSynchronizer.EffectCategories.None,
                Delay = 1.1f,
                MinPitch = 1,
                MaxPitch = 1,
                SyncType = Effect.SyncTypes.OwnerSync,
                Sounds = new List<GlobalAudioManager.Sounds>() { GlobalAudioManager.Sounds.SFX_SKILL_FinishingBlow}
            }.ApplyToTransform(TinyGameObjectManager.GetOrMake(skill.transform, "ActivationEffects", true, true));

            new SL_PlayVFX()
            {
                VFXPrefab = SL_PlayVFX.VFXPrefabs.VFXPreciseStrike,
                Delay = 1.1f
            }.ApplyToTransform(TinyGameObjectManager.GetOrMake(skill.transform, "ActivationEffects", true, true));



            return skill;
        }
    }
}
