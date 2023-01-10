using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System.Collections.Generic;
using UnityEngine;

namespace Crusader
{
    public class Judgement
    {
        public const float ImbueDuration = 10;

        public static Skill Init()
        {
            TinyHelper.TinyHelper.OnDescriptionModified += delegate (Item item, ref string description) {
                if (item.ItemID == IDs.blessedDeterminationID)
                {
                    if (FactionSelector.IsBlueChamberCollective(CharacterManager.Instance.GetFirstLocalCharacter()))
                    {
                        description = "When you expend " + ModTheme.BlueChamberImbueName + " to cast a spell, your primary weapon becomes infused with " + ModTheme.BlueChamberImbueName + " for " + ImbueDuration + " seconds.";
                    }
                    else if (FactionSelector.IsHolyMission(CharacterManager.Instance.GetFirstLocalCharacter()))
                    {
                        description = "When you expend " + ModTheme.BurstOfDivinityEffectName + " to cast a spell, your primary weapon becomes infused with " + ModTheme.HolyMissionImbueName + " for " + ImbueDuration + " seconds.";
                    }
                }
            };

            var myitem = new SL_Skill()
            {
                Name = ModTheme.DivineFavorSpellName,
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.arbitraryPassiveSkillID,
                New_ItemID = IDs.divineFavourID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Judgement",
                Description = "When you expend " + ModTheme.BurstOfDivinityEffectName + " or " + ModTheme.BlueChamberImbueName + " to cast a spell, your primary weapon becomes infused with a powerful infusion for " + ImbueDuration + " seconds.",
                IsUsable = false,
                CastType = Character.SpellCastType.NONE,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1f,

                Cooldown = 0,
                StaminaCost = 0,
                ManaCost = 0,
            };
            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;
            GameObject.Destroy(skill.gameObject.GetComponentInChildren<HasStatusEffectEffectCondition>());
            return skill;
        }
    }
}
