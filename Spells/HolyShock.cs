using InstanceIDs;
using SideLoader;
using System.Collections.Generic;
using UnityEngine;
using TinyHelper;
using ImpendingDoom;

namespace Crusader
{
    public class HolyShock
    {
        public static Skill Init()
        {
            var myitem = new SL_AttackSkill()
            {
                Name = "Holy Shock",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.blessID, //perfect strike
                New_ItemID = IDs.holyShockSkillID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Holy Shock",
                Description = "A blast that damages opponents and heals allies.",
                CastType = Character.SpellCastType.Bubble,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1,
                CastSheatheRequired = 0,

                Cooldown = 30,
                StaminaCost = 0,
                HealthCost = 0,
                ManaCost = 14,
            };

            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            //Set the correct animation
            new SL_PlaySoundEffect()
            {
                Follow = true,
                OverrideCategory = EffectSynchronizer.EffectCategories.None,
                Delay = 0.05f,
                MinPitch = 1,
                MaxPitch = 1,
                SyncType = Effect.SyncTypes.OwnerSync,
                Sounds = new List<GlobalAudioManager.Sounds>() { GlobalAudioManager.Sounds.SFX_SKILL_PreciseStrike_WhooshImpact }
            }.ApplyToTransform(TinyGameObjectManager.GetOrMake(skill.transform, "ActivationEffects", true, true));

            new SL_PlaySoundEffect()
            {
                Follow = true,
                OverrideCategory = EffectSynchronizer.EffectCategories.None,
                Delay = 0.4f,
                MinPitch = 1,
                MaxPitch = 1,
                SyncType = Effect.SyncTypes.OwnerSync,
                Sounds = new List<GlobalAudioManager.Sounds>() { GlobalAudioManager.Sounds.SFX_SKILL_LeapAttack_Impact }
            }.ApplyToTransform(TinyGameObjectManager.GetOrMake(skill.transform, "ActivationEffects", true, true));

            new SL_PlayVFX()
            {
                VFXPrefab = SL_PlayVFX.VFXPrefabs.VFXPreciseStrike,
            }.ApplyToTransform(TinyGameObjectManager.GetOrMake(skill.transform, "ActivationEffects", true, true));

            var effectsContainer = TinyGameObjectManager.GetOrMake(skill.transform, "Effects", true, true);


            var damageBlast = new SL_ShootBlast()
            {
                CastPosition = Shooter.CastPositionType.Local,
                TargetType = Shooter.TargetTypes.Enemies,

                BaseBlast = SL_ShootBlast.BlastPrefabs.DispersionLight,
                Radius = 7,
                BlastLifespan= 1,
                RefreshTime= -1,
                InstantiatedAmount = 5,
                Interruptible = false,
                HitOnShoot = true,
                IgnoreShooter = true,
                ParentToShootTransform = false,
                ImpactSoundMaterial = EquipmentSoundMaterials.NONE,
                DontPlayHitSound = true,
                EffectBehaviour = EditBehaviours.Destroy,
                Delay= 0,
                BlastEffects = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "Effects",
                        Effects = new SL_Effect[] {
                        }
                    }
                },
            }.ApplyToTransform(effectsContainer) as ShootBlast;


            var damageBlastEffect = damageBlast.BaseBlast.transform.Find("Effects");
            damageBlastEffect.gameObject.SetActive(true);

            var damage = damageBlastEffect.GetComponent<PunctualDamage>();
            GameObject.Destroy(damage);
            damage = damageBlastEffect.gameObject.AddComponent<PunctualDamage>();
            damage.Delay = 0f;
            damage.Damages = new DamageType[] { new DamageType(HolyDamageManager.HolyDamageManager.GetDamageType(), 40) };
            damage.Knockback = 40;
            var addThenSpread = damageBlastEffect.gameObject.AddComponent<AddThenSpreadStatus>();
            addThenSpread.Status = ImpendingDoomMod.Instance.impendingDoomInstance;
            addThenSpread.SetChanceToContract(100);
            addThenSpread.Range = 7;

            //Heal
            var healingAoE = effectsContainer.gameObject.AddComponent<HealingAoE>();
            healingAoE.Range = 7;
            healingAoE.RestoredHealth = 20;
            healingAoE.AmplificationType = HolyDamageManager.HolyDamageManager.GetDamageType();
            healingAoE.CanRevive = false;

            var prefab = UnityEngine.Object.Instantiate(SL.GetSLPack(Crusader.ModFolderName).AssetBundles["holy_shock"].LoadAsset<GameObject>("holy_shock_Prefab"));
            prefab.transform.SetParent(damageBlast.BaseBlast.transform);

            return skill;
        }
    }
}
