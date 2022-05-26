using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Templar
{
    public class SharingIsCaringSpell
    {
        public static void ApplySideLoader()
        {
            var myitem = new SL_Skill()
            {
                Name = "Sharing is Caring",
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.arbitraryPassiveSkillID,
                New_ItemID = IDs.sharingIsCaringID,
                SLPackName = "Templar",
                SubfolderName = "Blessed Determination",
                Description = "Cure Wounds can heal your allies.",
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
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(IDs.sharingIsCaringID) as Skill;
            return skill;
        }
    }
}
