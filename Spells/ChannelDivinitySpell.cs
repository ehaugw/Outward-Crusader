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
    using EffectSourceConditions;
    public class ChannelDivinitySpell
    {
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = ModTheme.ChannelDivinitySpellName,
                EffectBehaviour  = EditBehaviours.Destroy,
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

                Cooldown = 300,
                StaminaCost = 0,
                HealthCost = 0,
                ManaCost = 7,
            };

            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;
            //EmptyOffHandCondition.AddToSkill(skill, true, true);

            new SL_PlaySoundEffect()
            {
                Follow = true,
                OverrideCategory = EffectSynchronizer.EffectCategories.None,
                Delay = 1.1f,
                MinPitch = 1,
                MaxPitch = 1,
                SyncType = Effect.SyncTypes.OwnerSync,
                Sounds = new List<GlobalAudioManager.Sounds>() { GlobalAudioManager.Sounds.SFX_SKILL_FinishingBlow}
            }.ApplyToTransform(TinyGameObjectManager.GetOrMake(skill.transform, EffectSourceConditions.EFFECTS_CONTAINER_ACTIVATION, true, true));

            new SL_PlayVFX()
            {
                VFXPrefab = SL_PlayVFX.VFXPrefabs.VFXPreciseStrike,
                Delay = 1.1f
            }.ApplyToTransform(TinyGameObjectManager.GetOrMake(skill.transform, EffectSourceConditions.EFFECTS_CONTAINER_ACTIVATION, true, true));


            StatusEffectsCondition conditions;
            Transform myEffects;

            myEffects = TinyGameObjectManager.MakeFreshTransform(skill.transform, EffectSourceConditions.EFFECTS_CONTAINER, true, true);
            conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            conditions.StatusEffectNames = new[] { IDs.disciplineNameID, IDs.disciplineAmplifiedNameID };
            conditions.Invert = false;
            conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            conditions.StatusEffectNames = new[] { IDs.rageNameID, IDs.rageAmplifiedNameID };
            conditions.Invert = true;
            myEffects.gameObject.AddComponent<AddStatusEffect>().Status = Crusader.Instance.healingSurgeInstance;

            myEffects = TinyGameObjectManager.MakeFreshTransform(skill.transform, EffectSourceConditions.EFFECTS_CONTAINER, true, true);
            conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            conditions.StatusEffectNames = new[] { IDs.disciplineNameID, IDs.disciplineAmplifiedNameID };
            conditions.Invert = true;
            conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            conditions.StatusEffectNames = new[] { IDs.rageNameID, IDs.rageAmplifiedNameID };
            conditions.Invert = true;
            myEffects.gameObject.AddComponent<AddStatusEffect>().Status = Crusader.Instance.burstOfDivinityInstance;

            myEffects = TinyGameObjectManager.MakeFreshTransform(skill.transform, EffectSourceConditions.EFFECTS_CONTAINER, true, true);
            conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            conditions.StatusEffectNames = new[] { IDs.disciplineNameID, IDs.disciplineAmplifiedNameID };
            conditions.Invert = true;
            conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            conditions.StatusEffectNames = new[] { IDs.rageNameID, IDs.rageAmplifiedNameID };
            conditions.Invert = false;
            myEffects.gameObject.AddComponent<CelestialSurge>();

            //myEffects = TinyGameObjectManager.MakeFreshTransform(skill.transform, EffectSourceConditions.EFFECTS_CONTAINER, true, true);
            //conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            //conditions.StatusEffectNames = new[] { IDs.disciplineNameID, IDs.disciplineAmplifiedNameID };
            //conditions.Invert = false;
            //conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            //conditions.StatusEffectNames = new[] { IDs.rageNameID, IDs.rageAmplifiedNameID };
            //conditions.Invert = false;
            //myEffects.gameObject.AddComponent<CelestialSurge>();

            return skill;
        }
    }
}
