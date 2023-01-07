using InstanceIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crusader
{
    public static class FactionSelector
    {
        public static bool IsHolyMission(Character character)
        {
            return character?.Inventory?.QuestKnowledge?.IsItemLearned(IDs.questionsAndCorruptionID) ?? false;
        }

        public static bool IsBlueChamberCollective(Character character)
        {
            return character?.Inventory?.QuestKnowledge?.IsItemLearned(IDs.mixedLegaciesID) ?? false;
        }
    }
}
