using CustomWeaponBehaviour;
using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TinyHelper;
using HarmonyLib;
using Epic.OnlineServices.Presence;

namespace Crusader
{
    public class Consecration
    {
        public static float range = 10;
        public static float duration = 30;
        public static float cooldown = 100;
        public static Skill Init()
        {
            var myitem = new SL_AttackSkill()
            {
                Name = "Consecration",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.perfectStrikeID, //perfect strike
                New_ItemID = IDs.consecrationID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Consecration",
                Description = "Slam your weapon into the ground to consecrate it.",
                CastType = Character.SpellCastType.WeaponSkill2,
                CastModifier = Character.SpellCastModifier.Attack,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1,
                CastSheatheRequired = 2,

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
                    Weapon.WeaponType.FistW_2H,
                    Weapon.WeaponType.FistW_2H
                },
                
                Cooldown = cooldown,
                StaminaCost = 7,
                ManaCost = 7,
                HealthCost = 0,
                DurabilityCost = 0
            };

            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            //Set the correct animation
            WeaponSkillAnimationSelector.SetCustomAttackAnimation(skill, Weapon.WeaponType.Mace_1H);

            var effects = TinyGameObjectManager.MakeFreshObject("Effects", true, true, skill.transform).transform;

            new SL_PlaySoundEffect()
            {
                Follow = true,
                OverrideCategory = EffectSynchronizer.EffectCategories.None,
                Delay = 0,
                MinPitch = 1,
                MaxPitch = 1,
                SyncType = Effect.SyncTypes.OwnerSync,
                Sounds = new List<GlobalAudioManager.Sounds>() { GlobalAudioManager.Sounds.SFX_SKILL_LeapAttack_Impact}
            }.ApplyToTransform(effects);


            //new SL_Summon()
            //{
            //    Delay = 0,
            //    SyncType = Effect.SyncTypes.OwnerSync,
            //    OverrideCategory = EffectSynchronizer.EffectCategories.None,
            //    Prefab = IDs.summonedConcecratedGroundID.ToString(),
            //    SummonPrefabType = SL_Summon.PrefabTypes.Item,
            //    BufferSize = 1,
            //    LimitOfOne = false,
            //    SummonMode = Summon.InstantiationManagement.CreateNew,
            //    PositionType = Summon.SummonPositionTypes.InFrontOfTarget,
            //    MinDistance = 0,
            //    MaxDistance = 0.2f,
            //    SameDirectionAsSummoner = true,
            //    SummonLocalForward = new Vector3(0, 0, 0),
            //    IgnoreOnDestroy = false

            //}.ApplyToTransform(effects);

            new SL_RemoveImbueEffects()
            {
                AffectSlot = Weapon.WeaponSlot.MainHand,
                SyncType = Effect.SyncTypes.OwnerSync

            }.ApplyToTransform(effects);

            SpecificImbueCondition.AddToSkill(skill, Crusader.Instance.classInfusion);

            new SL_PlayVFX()
            {
                VFXPrefab = SL_PlayVFX.VFXPrefabs.VFXPreciseStrike,
            }.ApplyToTransform(effects);

            var damageBlast = new SL_ShootBlast()
            {

                CastPosition = Shooter.CastPositionType.Local,
                TargetType = Shooter.TargetTypes.Allies,

                BaseBlast = SL_ShootBlast.BlastPrefabs.DispersionLight,
                Radius = range,
                RefreshTime = 0.25f,
                BlastLifespan = duration,
                InstantiatedAmount = 10,
                Interruptible = false,
                HitOnShoot = true,
                IgnoreShooter = false,
                ParentToShootTransform = false,
                ImpactSoundMaterial = EquipmentSoundMaterials.NONE,
                DontPlayHitSound = true,

                EffectBehaviour = EditBehaviours.Destroy,
                
                BlastEffects = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "Effects",
                        Effects = new SL_Effect[] {
                            new SL_AddStatusEffect()
                            {
                                StatusEffect = ModTheme.ConsecratedGroundEffectName,
                                ChanceToContract = 100,
                            }
                        }
                    }
                },

            }.ApplyToTransform(effects) as ShootBlast;

            //var prefab = UnityEngine.Object.Instantiate(SL.GetSLPack(Crusader.ModFolderName).AssetBundles["consecrated_ground"].LoadAsset<GameObject>("new_consecrated_ground_Prefab"));
            //prefab.transform.SetParent(damageBlast.BaseBlast.transform);

            //var staggerEffects = TinyGameObjectManager.MakeFreshObject("Effects", true, true, skill.transform).transform;
            //var staggerBlast = new SL_ShootBlast()
            //{

            //    CastPosition = Shooter.CastPositionType.Local,
            //    TargetType = Shooter.TargetTypes.Allies,

            //    BaseBlast = SL_ShootBlast.BlastPrefabs.DispersionLight,
            //    Radius = 2,
            //    RefreshTime = 0,
            //    BlastLifespan = -1,
            //    InstantiatedAmount = 5,
            //    Interruptible = false,
            //    HitOnShoot = true,
            //    IgnoreShooter = false,
            //    ParentToShootTransform = false,
            //    ImpactSoundMaterial = EquipmentSoundMaterials.NONE,
            //    DontPlayHitSound = true,

            //    EffectBehaviour = EditBehaviours.Destroy,

            //    BlastEffects = new SL_EffectTransform[] {
            //        new SL_EffectTransform() {
            //            TransformName = "Effects",
            //            Effects = new SL_Effect[] {
            //                new SL_AddStatusEffect()
            //                {
            //                    StatusEffect = ModTheme.ConsecratedGroundEffectName,
            //                    ChanceToContract = 100,
            //                }
            //            }
            //        }
            //    }
            //}.ApplyToTransform(staggerEffects) as ShootBlast;

            //staggerBlast.transform.Find("Effects").gameObject.AddComponent<CasualStagger>();

            return skill;
        }

        //[HarmonyPatch(typeof(CharacterStats), "GetDamageProtection")]
        //public class CharacterStats_GetDamageProtection
        //{
        //    [HarmonyPostfix]
        //    public static void Postfix(CharacterStats __instance, ref float __result, ref DamageType.Types _type)
        //    {
        //        if (_type == DamageType.Types.Physical && SideLoader.At.GetField<CharacterStats>(__instance, "m_character") is Character character)
        //        {
        //            List<Character> charsInRange = new List<Character>();
        //            //CharacterManager.Instance.FindCharactersInRange(character.CenterPosition, range, ref charsInRange);
        //        }
        //    }
        //}
    }
}
