using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System.Collections.Generic;
using UnityEngine;

namespace Templar
{
    public class WrathfulSmiteCooldownResetSkill
    {
        public static void ApplySideLoader()
        {
            var myitem = new SL_Skill()
            {
                Name = "Endless Wrath",
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.arbitraryPassiveSkillID,
                New_ItemID = IDs.wrathfulSmiteCooldownResetID,
                SLPackName = "Templar",
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
            myitem.Apply();
        }
        public static Skill Init() {
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(IDs.wrathfulSmiteCooldownResetID) as Skill;
            return skill;
        }
    }

    //[HarmonyPatch(typeof(Weapon), "AddImbueEffect")]
    //public class Weapon_AddImbueEffect
    //{
    //    [HarmonyPrefix]
    //    public static void Prefix(Weapon __instance, ref ImbueEffectPreset _effect)
    //    {
    //        var skillKnowledge = __instance?.OwnerCharacter?.Inventory?.SkillKnowledge;
    //        if (_effect.PresetID == IDs.divineLightImbueID && skillKnowledge != null && skillKnowledge.IsItemLearned(IDs.divineFavourID))
    //        {
    //            _effect = (ImbueEffectPreset)ResourcesPrefabManager.Instance.GetEffectPreset(IDs.radiantLightImbueID);
    //        }
    //    }
    //}
}
