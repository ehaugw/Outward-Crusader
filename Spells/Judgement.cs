using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System.Collections.Generic;
using UnityEngine;

namespace Crusader
{
    public class Judgement
    {
        public static float ImbueDuration = 10;

        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = ModTheme.DivineFavorSpellName,
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.arbitraryPassiveSkillID,
                New_ItemID = IDs.divineFavourID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Judgement",
                Description = "When you expend " + ModTheme.BurstOfDivinityEffectName + " to cast a spell, your primary weapon becomes infused with " + ModTheme.ImbueEffectName + " for " + ImbueDuration + " seconds.",//ModTheme.ImbueEffectName +" and Spark to cause a " + ModTheme.ImpendingDoomEffectName + " effect. Spark will cause the " + ModTheme.ImpendingDoomEffectName + " effect to spread if the target is already affected by " + ModTheme.ImpendingDoomEffectName + ".\n\nCreatures affected by " + ModTheme.ImpendingDoomEffectName + " take dmage over time and may be struck by lightning.",
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
