using System.Collections.Generic;
using TinyHelper;
using InstanceIDs;
using SideLoader;
using UnityEngine;
using System.Linq;
using EffectSourceConditions;

namespace Crusader
{
    using EffectSourceConditions;

    class EffectInitializer
    {
        public static StatusEffect MakeAuraOfSmitingPrefab()
        {
                var statusEffect = TinyEffectManager.MakeStatusEffectPrefab(
                    effectName:         "Aura of Smiting",
                    familyName:         "Aura of Smiting",
                    description:        "Grants you and your nearby allies bonus " + HolyDamageManager.HolyDamageManager.GetDamageType().ToString() + " damage on your weapon attacks, but also drains your mana over time.",
                    lifespan:           -1,
                    refreshRate:        AuraOfSmitingEffect.UPDATERATE,
                    stackBehavior:      StatusEffectFamily.StackBehaviors.Override,
                    targetStatusName:   "Doom",
                    isMalusEffect:      false,
                    modGUID:            Crusader.GUID);

                var effectSignature = statusEffect.StatusEffectSignature;
                var effectComponent = TinyGameObjectManager.MakeFreshObject("Effects", true, true, effectSignature.transform).AddComponent<AuraOfSmitingEffect>();
                effectComponent.UseOnce = false;
                effectSignature.Effects = new List<Effect>() { effectComponent };

            statusEffect.OverrideIcon = CustomTexture.MakeSprite(CustomTexture.LoadTexture(Crusader.ModFolderName + @"\SideLoader\Texture2D\radiatingIcon.png", 0, true, FilterMode.Point), CustomTexture.SpriteBorderTypes.None);

                return statusEffect;
        }

        public static StatusEffect MakeMeditationPrefab()
        {
            var statusEffect = TinyEffectManager.MakeStatusEffectPrefab(
                effectName:             MeditateSpell.MEDITATION_EFFECT_NAME,
                familyName:             MeditateSpell.MEDITATION_EFFECT_NAME,
                description:            "You are meditating...",
                lifespan:               -1,
                refreshRate:            0.25f,
                stackBehavior:          StatusEffectFamily.StackBehaviors.Override,
                targetStatusName:       "Mana Ratio Recovery 3",
                isMalusEffect:          true,
                modGUID:                Crusader.GUID);

            var effectSignature = statusEffect.StatusEffectSignature;
            var effectComponent = TinyGameObjectManager.MakeFreshObject("Effects", true, true, effectSignature.transform).AddComponent<MeditationEffect>();
            effectComponent.UseOnce = false;
            effectSignature.Effects = new List<Effect>() { effectComponent };

            statusEffect.IsHidden = true;
            statusEffect.DisplayInHud = false;

            return statusEffect;
        }

        public static StatusEffect MakeMeditationCooldownPrefab()
        {
            var statusEffect = TinyEffectManager.MakeStatusEffectPrefab(
                effectName:             MeditateSpell.MEDITATION_COOLDOWN_EFFECT_NAME,
                familyName:             MeditateSpell.MEDITATION_COOLDOWN_EFFECT_NAME,
                description:            ModTheme.MeditationCooldownNotification,
                lifespan:               MeditateSpell.Cooldown,
                refreshRate:            -1,
                stackBehavior:          StatusEffectFamily.StackBehaviors.Override,
                targetStatusName:       "Mana Ratio Recovery 3",
                isMalusEffect:          true,
                modGUID:                Crusader.GUID);

            statusEffect.IsHidden =     true;
            statusEffect.DisplayInHud = false;

            return statusEffect;
        }

        public static StatusEffect MakeSurgeOfDivinityPrefab()
        {
            var statusEffect = TinyEffectManager.MakeStatusEffectPrefab(
                effectName:             "Surge of Divinity",
                familyName:             "Surge of Divinity",
                description:            "Greatly increases Burst of Divinity buildup.",
                lifespan:               90,
                refreshRate:            -1,
                stackBehavior:          StatusEffectFamily.StackBehaviors.Override,
                targetStatusName:       "Mana Ratio Recovery 3",
                isMalusEffect:          false,
                modGUID:                Crusader.GUID,
                iconFileName:           Crusader.ModFolderName + @"\SideLoader\Texture2D\surgeOfDivinityIcon.png");

            return statusEffect;
        }

        public static StatusEffect MakeBurstOfDivinityPrefab()
        {
            var statusEffect = TinyEffectManager.MakeStatusEffectPrefab(
                effectName:             ModTheme.BurstOfDivinityEffectIdentifierName,
                displayName:            ModTheme.BurstOfDivinityEffectName,
                familyName:             ModTheme.BurstOfDivinityEffectIdentifierName,
                description:            "Reduces the mana cost of spells, but stacks are expended when a spell is casted.",
                lifespan:               BlessedDeterminationSpell.FREECAST_LIFESPAN,
                refreshRate:            -1,
                stackBehavior:          StatusEffectFamily.StackBehaviors.StackAll,
                targetStatusName:       "Mana Ratio Recovery 3",
                isMalusEffect:          false,
                modGUID:                Crusader.GUID,
                iconFileName:           Crusader.ModFolderName + @"\SideLoader\Texture2D\burstOfDivinityIcon.png");;

            return statusEffect;
        }

        public static StatusEffect MakeHealingSurgePrefab()
        {
            var statusEffect = TinyEffectManager.MakeStatusEffectPrefab(
                effectName:             "Healing Surge",
                familyName:             "Healing Surge",
                description:            "Increases the range and potency of Cure Wounds.",
                lifespan:               120,
                refreshRate:            -1,
                stackBehavior:          StatusEffectFamily.StackBehaviors.Override,
                targetStatusName:       "Mana Ratio Recovery 3",
                isMalusEffect:          false,
                modGUID:                Crusader.GUID,
                iconFileName:           Crusader.ModFolderName + @"\SideLoader\Texture2D\healingSurgeIcon.png");

            return statusEffect;
        }

        public static StatusEffect MakeRadiatingPrefab()
        {
            var statusEffect = TinyEffectManager.MakeStatusEffectPrefab(
                effectName:             ModTheme.RadiatingEffectName,
                familyName:             ModTheme.RadiatingEffectName,
                description:            "Damages you and your nearby allies.",
                lifespan:               Radiating.LIFE_SPAN,
                refreshRate:            1,
                stackBehavior:          StatusEffectFamily.StackBehaviors.Override,
                targetStatusName:       "Doom",
                isMalusEffect:          true,
                modGUID:                Crusader.GUID,
                iconFileName:           Crusader.ModFolderName + @"\SideLoader\Texture2D\radiatingIcon.png"
            );

            var effectSignature = statusEffect.StatusEffectSignature;
            var effectComponent = TinyGameObjectManager.MakeFreshObject("Effects", true, true, effectSignature.transform).AddComponent<Radiating>();
            effectComponent.UseOnce = false;
            effectSignature.Effects = new List<Effect>() { effectComponent };

            return statusEffect;
        }

        public static StatusEffect MakeImpendingDoomPrefab()
        {
            var statusEffect = TinyEffectManager.MakeStatusEffectPrefab(
                effectName:             ModTheme.ImpendingDoomEffectIdentifierName,
                displayName:            ModTheme.ImpendingDoomEffectName,
                familyName:             ModTheme.ImpendingDoomEffectIdentifierName,
                description:            "DESCRIPTION",
                lifespan:               ImpendingDoom.LIFE_SPAN,
                refreshRate:            ImpendingDoom.REFRESH_RATE,
                stackBehavior:          StatusEffectFamily.StackBehaviors.Override, 
                targetStatusName:       "HolyBlaze",
                isMalusEffect:          true,
                modGUID:                Crusader.GUID,
                iconFileName:           Crusader.ModFolderName + @"\SideLoader\Texture2D\impendingDoomIcon.png"
            );

            var effectSignature = statusEffect.StatusEffectSignature;
            Transform effectsContainer;

            //Impending Doom
            effectsContainer = TinyGameObjectManager.MakeFreshObject(EffectSourceConditions.EFFECTS_CONTAINER, true, true, effectSignature.transform).transform;
            var impendingDoom = effectsContainer.gameObject.AddComponent<ImpendingDoom>();
            impendingDoom.UseOnce = false;

            //Nova
            effectsContainer = TinyGameObjectManager.MakeFreshObject(EffectSourceConditions.EFFECTS_CONTAINER, true, true, effectSignature.transform).transform;
            WillDieFromImpendingDoom condition = effectsContainer.gameObject.AddComponent<WillDieFromImpendingDoom>();

            var shootBlast = effectsContainer.gameObject.AddComponent<TinyHelper.Effects.ShootBlastFromEffect>();

            shootBlast.UseOnce = true;
            shootBlast.enabled = true;
            shootBlast.transform.parent = effectsContainer;
            shootBlast.BaseBlast = SL_ShootBlast.GetBlastPrefab(SL_ShootBlast.BlastPrefabs.ForceRaiseLightning).GetComponent<Blast>();
            shootBlast.InstanstiatedAmount = 5;
            shootBlast.CastPosition = Shooter.CastPositionType.Local;
            shootBlast.TargetType = Shooter.TargetTypes.Enemies;
            shootBlast.TransformName = "ShooterTransform";

            shootBlast.UseTargetCharacterPositionType = false;

            shootBlast.SyncType = Effect.SyncTypes.OwnerSync;
            shootBlast.OverrideEffectCategory = EffectSynchronizer.EffectCategories.None;
            shootBlast.BasePotencyValue = 1f;
            shootBlast.Delay = 0.25f;
            shootBlast.LocalCastPositionAdd = new Vector3(0f, -1.0f, 0);
            shootBlast.BaseBlast.Radius = ImpendingDoom.RANGE;

            effectSignature.Effects = new List<Effect>() { impendingDoom, shootBlast};

            //thunderGameObject.GetComponentInChildren<PunctualDamage>().enabled = true;

            var blastEffects = shootBlast.BaseBlast.transform.Find("Effects");
            var damage = blastEffects.GetComponent<PunctualDamage>();
            GameObject.Destroy(damage);
            damage = blastEffects.gameObject.AddComponent<ImpendingDoomDamage>();
            damage.Delay = 0.25f;
            damage.Knockback = 40;

            return statusEffect;
        }

        public static void AddCondemnToDivineLightImbue()
        {
            //Dictionary<int, EffectPreset> dictionary = (Dictionary<int, EffectPreset>) TinyHelper.At.GetValue(typeof(ResourcesPrefabManager), null, "EFFECTPRESET_PREFABS");

            var effectPreset = TinyEffectManager.GetEffectPreset(IDs.divineLightImbueID);
            var effectTransform = TinyGameObjectManager.MakeFreshObject("Effects", true, true, effectPreset.transform).transform;

            TinyEffectManager.MakeStatusEffectChance(effectTransform, Crusader.Instance.impendingDoomInstance.IdentifierName, 100);
            //TinyEffectManager.AddWeaponDamage(effectTransform, 4, 0, HolyDamageManager.HolyDamageManager.GetDamageType(), 0);

            var requirementTransform = TinyGameObjectManager.GetOrMake(effectTransform, EffectSourceConditions.SOURCE_CONDITION_CONTAINER, true, true);
            var skillReq = requirementTransform.gameObject.AddComponent<SourceConditionSkill>();
            skillReq.RequiredSkillID = IDs.divineFavourID;// = Crusader.Instance.divineFavourInstance;

        }

        //public static void MakeRadiantLightInfusion()
        //{
        //    var infusion = TinyEffectManager.MakeImbuePreset(IDs.radiantLightImbueID, "Radiant Light Imbue", "Weapon deals some " + HolyDamageManager.HolyDamageManager.GetDamageType().ToString() + " Damage, applies Radiating and emits light.", null, IDs.divineLightImbueID, 6, 0.25f, HolyDamageManager.HolyDamageManager.GetDamageType(), 0, "Radiating", null, buildUp: 100);


        //    if (ResourcesPrefabManager.Instance.GetItemPrefab(IDs.elementalDischargeID)?.transform.Find("NormalBolt").gameObject is GameObject lightningEffectElemental)
        //    //if (gongStrike.transform.Find("ElementalEffect/NormalLight").gameObject is GameObject normalLightEffectObject)
        //    {
        //        if (lightningEffectElemental.GetComponent<ImbueEffectORCondition>() is ImbueEffectORCondition orCondition)
        //        {
        //            var listOfImubes = orCondition.ImbueEffectPresets.ToList();
        //            listOfImubes.Add(infusion);

        //            orCondition.ImbueEffectPresets = listOfImubes.ToArray();
        //        }
        //    }


        //    if (ResourcesPrefabManager.Instance.GetItemPrefab(IDs.gongStrikeID)?.transform.Find("ElementalEffect/NormalLightning").gameObject is GameObject lightningEffectGong)
        //    //if (gongStrike.transform.Find("ElementalEffect/NormalLight").gameObject is GameObject normalLightEffectObject)
        //    {
        //        if (lightningEffectGong.GetComponent<ImbueEffectORCondition>() is ImbueEffectORCondition orCondition)
        //        {
        //            var listOfImubes = orCondition.ImbueEffectPresets.ToList();
        //            listOfImubes.Add(infusion);

        //            orCondition.ImbueEffectPresets = listOfImubes.ToArray();
        //        }
        //    }
        //}

        public static ImbueEffectPreset MakeClassInfusion()
        {
            var requireDivineFavour = false;

            ImbueEffectPreset effectPreset = TinyEffectManager.MakeImbuePreset(
                imbueID:            IDs.burstOfLightImbueID,
                name:               ModTheme.ImbueEffectName,
                description:        "Weapon deals some " + HolyDamageManager.HolyDamageManager.GetDamageType().ToString() + " Damage" + /*", applies " + ModTheme.RadiatingEffectName + */ " and emits light.",
                iconFileName:       Crusader.ModFolderName + @"\SideLoader\Texture2D\impendingDoomImbueIcon.png",
                visualEffectID:     IDs.divineLightImbueID
            );

            Transform effectTransform;

            effectTransform = TinyGameObjectManager.MakeFreshObject("Effects", true, true, effectPreset.transform).transform;
            TinyEffectManager.MakeWeaponDamage(effectTransform, 5, 0.25f, HolyDamageManager.HolyDamageManager.GetDamageType(), 3);

            if (requireDivineFavour)
            {
                effectTransform = TinyGameObjectManager.MakeFreshObject("Effects", true, true, effectPreset.transform).transform;
            }
            TinyEffectManager.MakeStatusEffectChance(effectTransform, Crusader.Instance.impendingDoomInstance.IdentifierName, 100);

            if (requireDivineFavour)
            {
                var requirementTransform = TinyGameObjectManager.GetOrMake(effectTransform, EffectSourceConditions.SOURCE_CONDITION_CONTAINER, true, true);
                var skillReq = requirementTransform.gameObject.AddComponent<SourceConditionSkill>();
                skillReq.RequiredSkillID = IDs.divineFavourID;// = Crusader.Instance.divineFavourInstance;
            }

            //var prefab = UnityEngine.Object.Instantiate(SL.GetSLPack("Crusader").AssetBundles["divinesmite"].LoadAsset<GameObject>("divineinfusion_Prefab"));
            //effectPreset.ImbueFX = SL.GetSLPack("Crusader").AssetBundles["divinesmite"].LoadAsset<GameObject>("divineinfusion_Prefab").transform;
            //UnityEngine.Object.DontDestroyOnLoad(effectPreset.ImbueFX.gameObject);
            //prefab.transform.SetParent(effectTransform.transform);


            if (ResourcesPrefabManager.Instance.GetItemPrefab(IDs.elementalDischargeID)?.transform.Find("NormalBolt").gameObject is GameObject lightningEffectElemental)
            //if (gongStrike.transform.Find("ElementalEffect/NormalLight").gameObject is GameObject normalLightEffectObject)
            {
                if (lightningEffectElemental.GetComponent<ImbueEffectORCondition>() is ImbueEffectORCondition orCondition)
                {
                    var listOfImubes = orCondition.ImbueEffectPresets.ToList();
                    listOfImubes.Add(effectPreset);

                    orCondition.ImbueEffectPresets = listOfImubes.ToArray();
                }
            }


            if (ResourcesPrefabManager.Instance.GetItemPrefab(IDs.gongStrikeID)?.transform.Find("ElementalEffect/NormalLightning").gameObject is GameObject lightningEffectGong)
            //if (gongStrike.transform.Find("ElementalEffect/NormalLight").gameObject is GameObject normalLightEffectObject)
            {
                if (lightningEffectGong.GetComponent<ImbueEffectORCondition>() is ImbueEffectORCondition orCondition)
                {
                    var listOfImubes = orCondition.ImbueEffectPresets.ToList();
                    listOfImubes.Add(effectPreset);

                    orCondition.ImbueEffectPresets = listOfImubes.ToArray();
                }
            }

            return effectPreset;
        }
    }
}
