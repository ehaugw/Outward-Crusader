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

    public static class FactionSelector
    {
        public static ParticleSystem.MinMaxGradient holyMissionMinMaxGradient = new ParticleSystem.MinMaxGradient(new Color(1, 0.83f, 0.7f, 0.5f) / 2, new Color(1, 0.5f, 0.2f, 0.5f) / 2);
        public static ParticleSystem.MinMaxGradient blueChamberCollectiveMinMaxGradient = new ParticleSystem.MinMaxGradient(new Color(0.73f, 1f, 0.83f, 0.5f) / 2, new Color(0.3f, 1f, 0.5f, 0.5f) / 2);
        

        public static bool IsHolyMission(Character character)
        {
            return true;
            // return character?.Inventory?.QuestKnowledge?.IsItemLearned(IDs.questionsAndCorruptionID) ?? false;
        }

        public static bool IsBlueChamberCollective(Character character)
        {
            return false;
            //return character?.Inventory?.QuestKnowledge?.IsItemLearned(IDs.mixedLegaciesID) ?? false;
        }

        public static void SetWeaponTrailForFaction(Skill skill)
        {
            foreach(var tup in new Tuple<int, ParticleSystem.MinMaxGradient>[]
            {
                new Tuple<int, ParticleSystem.MinMaxGradient>(IDs.questionsAndCorruptionID, holyMissionMinMaxGradient),
                new Tuple<int, ParticleSystem.MinMaxGradient>(IDs.mixedLegaciesID, blueChamberCollectiveMinMaxGradient)
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
                requirementTransform.gameObject.AddComponent<SourceConditionQuest>().RequiredQuestID = tup.Item1;
            }
        }

        public static void SetCasterParticleForFaction(Skill skill, float delay)
        {
            foreach (var tup in new Tuple<int, ParticleSystem.MinMaxGradient>[]
            {
                new Tuple<int, ParticleSystem.MinMaxGradient>(IDs.questionsAndCorruptionID, holyMissionMinMaxGradient),
                new Tuple<int, ParticleSystem.MinMaxGradient>(IDs.mixedLegaciesID, blueChamberCollectiveMinMaxGradient)
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
                requirementTransform.gameObject.AddComponent<SourceConditionQuest>().RequiredQuestID = tup.Item1;
            }
        }
    }
}
