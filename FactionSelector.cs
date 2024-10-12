using EffectSourceConditions;
using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyHelper;
using UnityEngine;

namespace Crusader
{
    using EffectSourceConditions;
    public class SourceConditionCrusaderFaction : SourceCondition
    {
        public FactionSelector.CrusaderFaction Faction = FactionSelector.CrusaderFaction.None;
        public bool Invert = false;
        public override bool CharacterHasRequirement(Character character)
        {
            return (character.GetCrusaderFaction() == Faction) ^ Invert;
        }
    }

    public class ConditionCrusaderFaction : EffectCondition
    {
        public FactionSelector.CrusaderFaction Faction = FactionSelector.CrusaderFaction.None;
        protected override bool CheckIsValid(Character _affectedCharacter)
        {
            return (_affectedCharacter.GetCrusaderFaction() == Faction) ^ Invert;
        }
    }

    public static class CharacterCrusaderFactionExtension
    {
        public static FactionSelector.CrusaderFaction GetCrusaderFaction(this Character character)
        {
            return Crusader.Instance.FactionSelectorInstance.PlayerFactions[character.UID.ToString()];
        }
    }

    public class FactionSelector
    {
        public enum CrusaderFaction {
            HolyMission,
            BlueChamber,
            HeroicKingdom,
            None,
        }
        public Dictionary<string, CrusaderFaction> PlayerFactions = new Dictionary<string, CrusaderFaction>();

        public FactionSelector()
        {
            SL.OnSceneLoaded += OnSceneLoadedReportFaction;
        }
        private void OnSceneLoadedReportFaction()
        {
            var character = CharacterManager.Instance.GetFirstLocalCharacter();
            if (character.IsWorldHost)
            {
                var faction = CrusaderFaction.None;
                if (QuestRequirements.HasQuestKnowledge(character, new [] { IDs.questionsAndCorruptionID }, LogicType.Any))
                {
                    faction = CrusaderFaction.HolyMission;
                }
                if (QuestRequirements.HasQuestKnowledge(character, new [] { IDs.heroicPeacemakerID }, LogicType.Any))
                {
                    faction = CrusaderFaction.HeroicKingdom;
                }
                if (QuestRequirements.HasQuestKnowledge(character, new [] { IDs.mixedLegaciesID }, LogicType.Any))
                {
                    faction = CrusaderFaction.BlueChamber;
                }
                PlayerFactions[character.UID] = faction;
            }
            CrusaderRPCManager.Instance.photonView.RPC("AssignFaction", PhotonTargets.All, new object[] { character.UID.ToString(), PlayerFactions.ContainsKey(character.UID) ? PlayerFactions[character.UID] : CrusaderFaction.None });
        }

        public static ParticleSystem.MinMaxGradient holyMissionMinMaxGradient = new ParticleSystem.MinMaxGradient(new Color(1, 0.83f, 0.7f, 0.5f) / 2, new Color(1, 0.5f, 0.2f, 0.5f) / 2);
        public static ParticleSystem.MinMaxGradient blueChamberCollectiveMinMaxGradient = new ParticleSystem.MinMaxGradient(new Color(0.73f, 1f, 0.83f, 0.5f) / 2, new Color(0.3f, 1f, 0.5f, 0.5f) / 2);

        public static void SetWeaponTrailForFaction(Skill skill)
        {
            foreach(var tup in new Tuple<CrusaderFaction, ParticleSystem.MinMaxGradient>[]
            {
                new Tuple<CrusaderFaction, ParticleSystem.MinMaxGradient>(CrusaderFaction.HolyMission, holyMissionMinMaxGradient),
                new Tuple<CrusaderFaction, ParticleSystem.MinMaxGradient>(CrusaderFaction.HeroicKingdom, holyMissionMinMaxGradient),
                new Tuple<CrusaderFaction, ParticleSystem.MinMaxGradient>(CrusaderFaction.BlueChamber, blueChamberCollectiveMinMaxGradient),
            })
            {
                var particleTransform = TinyGameObjectManager.MakeFreshTransform(skill.transform, EffectSourceConditions.EFFECTS_CONTAINER_ACTIVATION, true, true);
                new SL_PlayVFX()
                {
                    VFXPrefab = SL_PlayVFX.VFXPrefabs.VFXMomentOfTruth,
                }.ApplyToTransform(particleTransform);

                foreach (var vfxSystem in particleTransform.gameObject.GetComponents<PlayVFX>())
                {
                    foreach (ParticleSystem particles in vfxSystem.VFX.gameObject.GetComponentsInChildren<ParticleSystem>())
                    {
                        var m = particles.main;
                        m.startColor = tup.Item2;
                    }
                }
                var requirementTransform = TinyGameObjectManager.GetOrMake(particleTransform, EffectSourceConditions.SOURCE_CONDITION_CONTAINER, true, true);
                requirementTransform.gameObject.AddComponent<SourceConditionCrusaderFaction>().Faction = tup.Item1;
            }
        }

        public static void SetCasterParticleForFaction(Skill skill, float delay)
        {
            foreach (var tup in new Tuple<CrusaderFaction, ParticleSystem.MinMaxGradient>[]
            {
                new Tuple<CrusaderFaction, ParticleSystem.MinMaxGradient>(CrusaderFaction.HeroicKingdom, holyMissionMinMaxGradient),
                new Tuple<CrusaderFaction, ParticleSystem.MinMaxGradient>(CrusaderFaction.HolyMission, holyMissionMinMaxGradient),
                new Tuple<CrusaderFaction, ParticleSystem.MinMaxGradient>(CrusaderFaction.BlueChamber, blueChamberCollectiveMinMaxGradient),
            })
            {
                var particleTransform = TinyGameObjectManager.MakeFreshTransform(skill.transform, EffectSourceConditions.EFFECTS_CONTAINER_ACTIVATION, true, true);
                new SL_PlayVFX()
                {
                    VFXPrefab = SL_PlayVFX.VFXPrefabs.VFXBoonEthereal,
                    Delay = delay
                }.ApplyToTransform(particleTransform);

                foreach (var vfxSystem in particleTransform.gameObject.GetComponents<PlayVFX>())
                {
                    foreach (ParticleSystem particles in vfxSystem.VFX.gameObject.GetComponentsInChildren<ParticleSystem>(true))
                    {
                        var m = particles.main;
                        m.startColor = tup.Item2;
                        var s = particles.colorBySpeed;
                        s.color = tup.Item2;
                        var l = particles.colorOverLifetime;
                        s.color = tup.Item2;
                    }
                }

                var requirementTransform = TinyGameObjectManager.GetOrMake(particleTransform, EffectSourceConditions.SOURCE_CONDITION_CONTAINER, true, true);
                requirementTransform.gameObject.AddComponent<SourceConditionCrusaderFaction>().Faction = tup.Item1;
            }
        }
    }
}
