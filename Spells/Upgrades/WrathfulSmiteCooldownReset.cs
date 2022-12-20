using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System.Collections.Generic;
using UnityEngine;

namespace Crusader
{
    public class WrathfulSmiteCooldownReset
    {
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Endless Wrath",
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.arbitraryPassiveSkillID,
                New_ItemID = IDs.wrathfulSmiteCooldownResetID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Divine Favour",
                Description = "Wrathful Smite instantly becomes available for another use if it kills its target.",
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
            return skill;
        }
    }
}
