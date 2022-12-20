using CustomWeaponBehaviour;
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
    public class RebukingSmiteSpell
    {
        public static Skill Init()
        {
            var myitem = new SL_AttackSkill()
            {
                Name = "Rebuking Swipe",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.punctureID,
                New_ItemID = IDs.rebukingSmiteID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Rebuking Smite",
                Description = "Attack in a wide arch.",
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
                

                Cooldown = 20,
                StaminaCost = 7,
                ManaCost = 7,
                HealthCost = 0,
                DurabilityCost = 3,
                
                EffectTransforms = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "Effects",
                        Effects = new SL_Effect[] {
                        }
                    },
                },
            };

            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            //Set the correct animation
            WeaponSkillAnimationSelector.SetCustomAttackAnimation(skill, Weapon.WeaponType.Sword_2H);

            new SL_PlaySoundEffect()
            {
                Follow = true,
                OverrideCategory = EffectSynchronizer.EffectCategories.None,
                Delay = 0,
                MinPitch = 1,
                MaxPitch = 1,
                SyncType = Effect.SyncTypes.OwnerSync,
                Sounds = new List<GlobalAudioManager.Sounds>() { GlobalAudioManager.Sounds.SFX_SKILL_SavageStrike }
            }.ApplyToTransform(TinyGameObjectManager.GetOrMake(skill.transform, "ActivationEffects", true, true));

            new SL_PlayVFX()
            {
                VFXPrefab = SL_PlayVFX.VFXPrefabs.VFXMomentOfTruth,
                
            }.ApplyToTransform(TinyGameObjectManager.GetOrMake(skill.transform, "ActivationEffects", true, true));

            foreach (var vfxSystem in skill.transform.Find("ActivationEffects").gameObject.GetComponents<PlayVFX>())
            {
                foreach (ParticleSystem particles in vfxSystem.VFX.gameObject.GetComponentsInChildren<ParticleSystem>())
                {
                    var m = particles.main;
                    m.startColor = new ParticleSystem.MinMaxGradient(new Color(1, 0.83f, 0.7f, 0.5f)/2, new Color(1, 0.5f, 0.2f, 0.5f)/2);
                }
            }


            Transform hitEffects = TinyGameObjectManager.MakeFreshObject("HitEffects", true, true, skill.transform).transform;
            var damage = hitEffects.gameObject.AddComponent<WeaponDamage>();
            setDamage(damage);
            var status = hitEffects.gameObject.AddComponent<AddStatusEffect>();
            status.SetChanceToContract(100);
            status.Status = ResourcesPrefabManager.Instance.GetStatusEffectPrefab(IDs.doomNameID);
            
            return skill;
        }

        private static void setDamage(WeaponDamage damage)
        {
            float minDamage = 1.5f;

            damage.WeaponDamageMult = minDamage;
            damage.WeaponDamageMultKDown = -1.0f;
            damage.WeaponKnockbackMult = 1.5f;

            damage.WeaponDurabilityLossPercent = 0;
            damage.WeaponDurabilityLoss = 5;
            damage.OverrideDType = DamageType.Types.Count;
            //damage.Damages = new DamageType[] { new DamageType(HolyDamageManager.HolyDamageManager.GetDamageType(), 30) };
            damage.Damages = new DamageType[] { };

        }
    }
}
