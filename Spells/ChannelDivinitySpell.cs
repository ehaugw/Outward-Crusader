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
            TinyHelper.TinyHelper.OnDescriptionModified += delegate (Item item, ref string description) {
                if (item.ItemID == IDs.channelDivinityID)
                {
                    if (Crusader.Instance.FactionSelectorInstance.PlayerFactions[CharacterManager.Instance.GetFirstLocalCharacter().UID] == FactionSelector.CrusaderFaction.BlueChamber)
                    {
                        description = "You channel your powers, drastically increasing your " + ModTheme.AncestralMemoryEffectName + " buildup, or produces combo effects when casted in combination with Discipline or Rage.";
                    }
                    else if (Crusader.Instance.FactionSelectorInstance.PlayerFactions[CharacterManager.Instance.GetFirstLocalCharacter().UID] == FactionSelector.CrusaderFaction.HolyMission)
                    {
                        description = "You channel your powers, drastically increasing your " + ModTheme.BurstOfDivinityEffectName + " buildup, or produces combo effects when casted in combination with Discipline or Rage.";
                    }
                }
            };

            var myitem = new SL_Skill()
            {
                Name = ModTheme.ChannelDivinitySpellName,
                EffectBehaviour  = EditBehaviours.Destroy,
                Target_ItemID = IDs.sparkID,
                New_ItemID = IDs.channelDivinityID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Channel Divinity",
                Description = "You channel your powers, drastically increasing your " + ModTheme.BlessedDeterminationSpellName + " buildup, or produces combo effects when casted in combination with Discipline or Rage.",
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

            FactionSelector.SetCasterParticleForFaction(skill, 1.6f);

            foreach (var tup in new Tuple<FactionSelector.CrusaderFaction, SL_ShootBlast.BlastPrefabs>[]{
                new Tuple<FactionSelector.CrusaderFaction, SL_ShootBlast.BlastPrefabs>(FactionSelector.CrusaderFaction.HolyMission, SL_ShootBlast.BlastPrefabs.EliteSupremeShellSpecialLaser),
                new Tuple<FactionSelector.CrusaderFaction, SL_ShootBlast.BlastPrefabs>(FactionSelector.CrusaderFaction.HeroicKingdom, SL_ShootBlast.BlastPrefabs.EliteSupremeShellSpecialLaser),
                new Tuple<FactionSelector.CrusaderFaction, SL_ShootBlast.BlastPrefabs>(FactionSelector.CrusaderFaction.BlueChamber, SL_ShootBlast.BlastPrefabs.CrimsonEliteLaser)
            })
            {
                var blastTransform = TinyGameObjectManager.MakeFreshTransform(skill.transform, EffectSourceConditions.EFFECTS_CONTAINER, true, true);
                var damageBlast = new SL_ShootBlast()
                {
                    CastPosition = Shooter.CastPositionType.Local,
                    LocalPositionAdd = new Vector3(0, 0, 0),

                    TargetType = Shooter.TargetTypes.Enemies,

                    BaseBlast = tup.Item2,
                    Radius = 0.5f,
                    BlastLifespan = 1,
                    RefreshTime = 0.2f,
                    InstantiatedAmount = 5,
                    Interruptible = false,
                    HitOnShoot = false,
                    IgnoreShooter = true,
                    ParentToShootTransform = false,
                    ImpactSoundMaterial = EquipmentSoundMaterials.NONE,
                    DontPlayHitSound = true,
                    EffectBehaviour = EditBehaviours.Destroy,
                    Delay = 0,
                    BlastEffects = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "HitEffects",
                        Effects = new SL_Effect[] {
                            new SL_AutoKnock()
                            {
                                KnockDown = false
                            }
                        }
                    }
                },
                }.ApplyToTransform(blastTransform) as ShootBlast;
                damageBlast.transform.Rotate(-90, 0, 0);
                var requirementTransform = TinyGameObjectManager.GetOrMake(blastTransform, EffectSourceConditions.SOURCE_CONDITION_CONTAINER, true, true);
                requirementTransform.gameObject.AddComponent<SourceConditionCrusaderFaction>().Faction = tup.Item1;
            }
            
            StatusEffectsCondition conditions;
            Transform myEffects;
            ConditionCrusaderFaction qconditions;

            //SURGE OF DIVINITY
            myEffects = TinyGameObjectManager.MakeFreshTransform(skill.transform, EffectSourceConditions.EFFECTS_CONTAINER, true, true);
            conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            conditions.StatusEffectNames = new[] { IDs.disciplineNameID, IDs.disciplineAmplifiedNameID };
            conditions.Invert = true;
            conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            conditions.StatusEffectNames = new[] { IDs.rageNameID, IDs.rageAmplifiedNameID };
            conditions.Invert = true;
            qconditions = myEffects.gameObject.AddComponent<ConditionCrusaderFaction>();
            qconditions.Faction = FactionSelector.CrusaderFaction.HolyMission;
            qconditions.Invert = false;
            myEffects.gameObject.AddComponent<AddStatusEffect>().Status = Crusader.Instance.surgeOfDivinityInstance;
            
            //SURGE OF INNER FLAME
            myEffects = TinyGameObjectManager.MakeFreshTransform(skill.transform, EffectSourceConditions.EFFECTS_CONTAINER, true, true);
            conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            conditions.StatusEffectNames = new[] { IDs.disciplineNameID, IDs.disciplineAmplifiedNameID };
            conditions.Invert = true;
            conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            conditions.StatusEffectNames = new[] { IDs.rageNameID, IDs.rageAmplifiedNameID };
            conditions.Invert = true;
            qconditions = myEffects.gameObject.AddComponent<ConditionCrusaderFaction>();
            qconditions.Faction = FactionSelector.CrusaderFaction.HeroicKingdom;
            qconditions.Invert = false;
            myEffects.gameObject.AddComponent<AddStatusEffect>().Status = Crusader.Instance.surgeOfDivinityInstance;

            //SURGE OF MEMORIES
            myEffects = TinyGameObjectManager.MakeFreshTransform(skill.transform, EffectSourceConditions.EFFECTS_CONTAINER, true, true);
            conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            conditions.StatusEffectNames = new[] { IDs.disciplineNameID, IDs.disciplineAmplifiedNameID };
            conditions.Invert = true;
            conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            conditions.StatusEffectNames = new[] { IDs.rageNameID, IDs.rageAmplifiedNameID };
            conditions.Invert = true;
            qconditions = myEffects.gameObject.AddComponent<ConditionCrusaderFaction>();
            qconditions.Faction = FactionSelector.CrusaderFaction.BlueChamber;
            qconditions.Invert = false;
            myEffects.gameObject.AddComponent<AddStatusEffect>().Status = Crusader.Instance.surgeOfMemoriesInstance;

            //HEALING SURGE
            myEffects = TinyGameObjectManager.MakeFreshTransform(skill.transform, EffectSourceConditions.EFFECTS_CONTAINER, true, true);
            conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            conditions.StatusEffectNames = new[] { IDs.disciplineNameID, IDs.disciplineAmplifiedNameID };
            conditions.Invert = false;
            conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            conditions.StatusEffectNames = new[] { IDs.rageNameID, IDs.rageAmplifiedNameID };
            conditions.Invert = true;
            myEffects.gameObject.AddComponent<AddStatusEffect>().Status = Crusader.Instance.healingSurgeInstance;

            //CELESTIAL SURGE
            myEffects = TinyGameObjectManager.MakeFreshTransform(skill.transform, EffectSourceConditions.EFFECTS_CONTAINER, true, true);
            conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            conditions.StatusEffectNames = new[] { IDs.disciplineNameID, IDs.disciplineAmplifiedNameID };
            conditions.Invert = true;
            conditions = myEffects.gameObject.AddComponent<StatusEffectsCondition>();
            conditions.StatusEffectNames = new[] { IDs.rageNameID, IDs.rageAmplifiedNameID };
            conditions.Invert = false;
            myEffects.gameObject.AddComponent<CelestialSurge>();

            return skill;
        }
    }
}
