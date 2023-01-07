using InstanceIDs;
using JetBrains.Annotations;
using SideLoader;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Crusader
{
    public class InfuseBurstOfLight
    {
        public static Skill Init()
        {
            var myitem = new SL_AttackSkill()
            {
                Name = ModTheme.InfusionSpellName,
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.infuseLightID, //Infuse Light
                New_ItemID = IDs.infuseBurstOfLightID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Infuse Burst of Light",
                Description = "Temporarly infuse your weapon to bring " + ModTheme.InfusionSpellDescriptionType + " upon your enemies.",
                IsUsable = true,
                CastType = 
                    //Character.SpellCastType.SpellBindLight,
                    Character.SpellCastType.Bubble,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1f,

                RequiredWeaponTypes = new Weapon.WeaponType[] {
                    Weapon.WeaponType.FistW_2H,
                    Weapon.WeaponType.Axe_1H,
                    Weapon.WeaponType.Axe_2H,
                    Weapon.WeaponType.Sword_1H,
                    Weapon.WeaponType.Sword_2H,
                    Weapon.WeaponType.Mace_1H,
                    Weapon.WeaponType.Mace_2H,
                    Weapon.WeaponType.Halberd_2H,
                    Weapon.WeaponType.Spear_2H,
                    Weapon.WeaponType.FistW_2H
                },

                EffectTransforms = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "Effects",
                        Effects = new SL_Effect[] {
                            new SL_ImbueWeapon() {
                                Lifespan = 60,
                                ImbueEffect_Preset_ID = IDs.holyMissionImbueID,
                                Imbue_Slot = Weapon.WeaponSlot.MainHand
                            },
                            //new SL_AddStatusEffect()
                            //{
                            //    StatusEffect = "Last Stand",
                            //    ChanceToContract = 100
                            //}
                        }
                    },
                },

                Cooldown = 100,
                StaminaCost = 0,
                HealthCost = 0,
                ManaCost = 7,
            };
            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;
            GameObject.Destroy(skill.gameObject.GetComponentInChildren<HasStatusEffectEffectCondition>());
            foreach (var playSound in skill.gameObject.GetComponentsInChildren<PlaySoundEffect>())
            {
                Object.Destroy(playSound);
            }

            //new SL_PlayVFX().
            new SL_PlaySoundEffect()
            {
                Follow = true,
                OverrideCategory = EffectSynchronizer.EffectCategories.None,
                Delay = 0,
                MinPitch = 1,
                MaxPitch = 1,
                SyncType = Effect.SyncTypes.OwnerSync,
                Sounds = new List<GlobalAudioManager.Sounds>() { GlobalAudioManager.Sounds.SFX_SKILL_PreciseStrike_WhooshImpact }
            }.ApplyToTransform(skill.transform.Find("ActivationEffects"));

            return skill;
        }
    }
}
