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
    public class Concecration
    {
        public static Skill Init()
        {
            var myitem = new SL_AttackSkill()
            {
                Name = "Concecration",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.perfectStrikeID, //perfect strike
                New_ItemID = IDs.concecrationID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Rebuking Smite",
                Description = "Expend your " + ModTheme.ImbueEffectName + " to concecrate the ground.",
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
                DurabilityCost = 3
            };

            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            //Set the correct animation
            WeaponSkillAnimationSelector.SetCustomAttackAnimation(skill, Weapon.WeaponType.Mace_1H);

            new SL_PlaySoundEffect()
            {
                Follow = true,
                OverrideCategory = EffectSynchronizer.EffectCategories.None,
                Delay = 0,
                MinPitch = 1,
                MaxPitch = 1,
                SyncType = Effect.SyncTypes.OwnerSync,
                Sounds = new List<GlobalAudioManager.Sounds>() { GlobalAudioManager.Sounds.SFX_SKILL_LeapAttack_Impact}
            }.ApplyToTransform(TinyGameObjectManager.MakeFreshObject("Effects", true, true, skill.transform).transform);

            new SL_PlaySoundEffect()
            {
                Follow = true,
                OverrideCategory = EffectSynchronizer.EffectCategories.None,
                Delay = 0,
                MinPitch = 1,
                MaxPitch = 1,
                SyncType = Effect.SyncTypes.OwnerSync,
                Sounds = new List<GlobalAudioManager.Sounds>() { GlobalAudioManager.Sounds.SFX_SKILL_FinishingBlow }
            }.ApplyToTransform(TinyGameObjectManager.MakeFreshObject("ActivationEffects", true, true, skill.transform).transform);

            //foreach (ParticleSystem particles in skill.gameObject.GetComponentsInChildren<ParticleSystem>())
            //{
            //    var m = particles.main;
            //    m.startColor = new Color() { r = 1f, g = 0.9f, b = 0.4f, a = 1 };
            //    var colorOverLifetime = particles.colorOverLifetime;

            //    var grad = new ParticleSystem.MinMaxGradient(new Color(1,0.9f,0.4f,1), new Color(1, 0.3f, 0.0f, 0));
            //    colorOverLifetime.color = grad;
            //    colorOverLifetime.enabled = true;
            //}

            //Transform hitEffects = TinyGameObjectManager.MakeFreshObject("HitEffects", true, true, skill.transform).transform;
            //var damage = hitEffects.gameObject.AddComponent<WeaponDamage>();
            //setDamage(damage);

            return skill;
        }
    }
}
