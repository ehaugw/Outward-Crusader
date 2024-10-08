﻿using InstanceIDs;
using SideLoader;
using System.Collections.Generic;
using UnityEngine;
using TinyHelper;

namespace Crusader
{
    public class WrathfulSmiteSpell
    {
        public static Skill Init()
        {
            var myitem = new SL_AttackSkill()
            {
                Name = "Wrathful Smite",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.punctureID,
                New_ItemID = IDs.wrathfulSmiteID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Wrathful Smite",
                Description = "A leaping attack that deals more damage to wounded enemies, and instantly becomes available for another use if it kills its target.",
                CastType = Character.SpellCastType.AxeLeap,
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
                    Weapon.WeaponType.FistW_2H
                },

                Cooldown = 100,
                StaminaCost = 10,
                ManaCost = 7,
                HealthCost = 0,
            };
            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            new SL_PlaySoundEffect()
            {
                Follow = true,
                OverrideCategory = EffectSynchronizer.EffectCategories.None,
                Delay = 0.22f,
                MinPitch = 1,
                MaxPitch = 1,
                SyncType = Effect.SyncTypes.OwnerSync,
                Sounds = new List<GlobalAudioManager.Sounds>() { GlobalAudioManager.Sounds.SFX_SKILL_PreciseStrike_WhooshImpact }
            }.ApplyToTransform(TinyGameObjectManager.GetOrMake(skill.transform, "ActivationEffects", true, true));

            FactionSelector.SetWeaponTrailForFaction(skill);
            TinyGameObjectManager.MakeFreshTransform(skill.transform, "ActivationEffects", true, true).gameObject.AddComponent<EnableHitDetection>().Delay = 0.5f;

            Transform hitEffects;
            hitEffects = TinyGameObjectManager.MakeFreshTransform(skill.transform, "HitEffects", true, true);
            var execDamage = hitEffects.gameObject.AddComponent<CooldownChangeWeaponDamageTargetHealth>();
            execDamage.ExecuteSetCooldown = 0f;
            setDamage(execDamage);

            //hitEffects = TinyGameObjectManager.MakeFreshObject("HitEffects", true, true, skill.transform).transform;
            //var damage = hitEffects.gameObject.AddComponent<WeaponDamageTargetHealth>();
            //setDamage(damage);
            //condition = TinyGameObjectManager.MakeFreshObject(EffectSourceConditions.SOURCE_CONDITION_CONTAINER, true, true, hitEffects).AddComponent<SourceConditionSkill>();
            //condition.RequiredSkillID = IDs.wrathfulSmiteCooldownResetID;
            //condition.Inverted = true;

            return skill;
        }
        private static void setDamage(WeaponDamageTargetHealth damage)
        {
            damage.MultiplierHighLowHP = new Vector2(2, 5);
            damage.WeaponDamageMult = 1;
            damage.WeaponDamageMultKDown = -1.0f;
            damage.WeaponKnockbackMult = 2.0f;
            
            damage.WeaponDurabilityLossPercent = 0;
            damage.WeaponDurabilityLoss = 5;
            damage.OverrideDType = DamageType.Types.Count;
            //damage.Damages = new DamageType[] { new DamageType(HolyDamageManager.HolyDamageManager.GetDamageType(), 30) };
            damage.Damages = new DamageType[] { };

        }
    }
}
