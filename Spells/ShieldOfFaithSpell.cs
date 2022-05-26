using InstanceIDs;
using SideLoader;
using UnityEngine;
using TinyHelper;

namespace Crusader
{
    public class ShieldOfFaithSpell
    {
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Shield of Faith",
                EffectBehaviour = EditBehaviours.NONE,
                Target_ItemID = IDs.braceID,
                New_ItemID = IDs.shieldOfFaithID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Shield of Faith",
                Description = "Blocks an attack, restores your stability and protects you against damage for a brief moment.",
                CastType = Character.SpellCastType.Brace,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1,
                CastSheatheRequired = 0,

                Cooldown = 15,
                StaminaCost = 0,
                HealthCost = 0,
                ManaCost = 7,
            };
            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            GameObject.Destroy(skill.gameObject.GetComponentInChildren<AddBoonEffect>());
            GameObject.Destroy(skill.gameObject.GetComponentInChildren<AutoKnock>());

            var hitEffects = skill.transform.Find("HitEffects").gameObject;
            hitEffects.AddComponent<AddStatusEffect>().Status = ResourcesPrefabManager.Instance.GetStatusEffectPrefab("Force Bubble");

            var blockEffects = skill.transform.Find("BlockEffects").gameObject;
            blockEffects.AddComponent<CasualStagger>();

            return skill;
        }
    }
}