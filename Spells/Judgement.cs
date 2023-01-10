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
            var myitem = new SL_Skill()
            {
                Name = ModTheme.DivineFavorSpellName,
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.arbitraryPassiveSkillID,
                New_ItemID = IDs.divineFavourID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Judgement",
                Description = "When you expend " + ModTheme.BurstOfDivinityEffectName + " or " + ModTheme.BlueChamberImbueName + " to cast a spell, your primary weapon becomes infused with a powerful infusion for " + ImbueDuration + " seconds.",//ModTheme.ImbueEffectName +" and Spark to cause a " + ModTheme.ImpendingDoomEffectName + " effect. Spark will cause the " + ModTheme.ImpendingDoomEffectName + " effect to spread if the target is already affected by " + ModTheme.ImpendingDoomEffectName + ".\n\nCreatures affected by " + ModTheme.ImpendingDoomEffectName + " take dmage over time and may be struck by lightning.",
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
