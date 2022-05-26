using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crusader
{
    public class RestorationSpell
    {
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Restoration",
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.blessID,
                New_ItemID = IDs.restorationID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Restoration",
                Description = "Cures you from all hexes, bleeding and slowing effects.",
                CastType = Character.SpellCastType.CallElements,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = 0f,

                EffectTransforms = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "Effects",
                        Effects = new SL_Effect[] {//slow down, cripple, ele vulnerability and dizzy
                            new SL_RemoveStatusEffect() { Status_Tag = "Hex", CleanseType = RemoveStatusEffect.RemoveTypes.StatusType},
                            new SL_RemoveStatusEffect() { Status_Name = IDs.slowNameID, CleanseType = RemoveStatusEffect.RemoveTypes.StatusSpecific},
                            new SL_RemoveStatusEffect() { Status_Name = "Cripple", CleanseType = RemoveStatusEffect.RemoveTypes.StatusSpecific},
                            new SL_RemoveStatusEffect() { Status_Tag = "Bleeding", CleanseType = RemoveStatusEffect.RemoveTypes.StatusType},
                            //new SL_RemoveStatusEffect() { Status_Tag = "Bleeding", CleanseType = RemoveStatusEffect.RemoveTypes.StatusType},
                            //new SL_RemoveStatusEffect() { Status_Tag = "Disease", CleanseType = RemoveStatusEffect.RemoveTypes.StatusType},
                            //new SL_RemoveStatusEffect() { Status_Tag = "Infection", CleanseType = RemoveStatusEffect.RemoveTypes.StatusType}
                        }
                    }
                },

                Cooldown = 30,
                StaminaCost = 0,
                HealthCost = 0,
                ManaCost = 14,
            };
            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;
            return skill;
        }
    }
}
