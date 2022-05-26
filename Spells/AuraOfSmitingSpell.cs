using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TinyHelper;
using HarmonyLib;
using CustomWeaponBehaviour;

namespace Crusader
{
    public class AuraOfSmitingSpell
    {
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Aura of Smiting",
                EffectBehaviour  = EditBehaviours.Override,
                Target_ItemID = IDs.blessID, //Bless
                New_ItemID = IDs.auraOfSmitingID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Aura of Smiting",
                Description = "Grants you and your nearby allies bonus " + HolyDamageManager.HolyDamageManager.GetDamageType().ToString() + " damage on your weapon attacks.\n\nCasting this spell toggles the effect, which cost 0.3 mana per second while active." ,
                CastType = Character.SpellCastType.Fast,
                CastModifier = Character.SpellCastModifier.Mobile,
                CastLocomotionEnabled = true,
                MobileCastMovementMult = 0.8f,
                CastSheatheRequired = 1,

                EffectTransforms = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "Effects",
                        Effects = new SL_Effect[] { }
                    }
                },

                Cooldown = 0,
                StaminaCost = 0,
                HealthCost = 0,
                ManaCost = 0,
            };

            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;
            GameObject.Destroy(skill.gameObject.GetComponentInChildren<AddStatusEffect>());

            var myEffects = skill.transform.Find("Effects");
            myEffects.gameObject.AddComponent<ToggleEffect>().StatusEffectInstance = Crusader.Instance.auraOfSmitingEffectInstance;
            return skill;
        }
    }

    [HarmonyPatch(typeof(CustomBehaviourFormulas), "PostAmplifyWeaponDamage")]
    public class CustomBehaviourFormulas_PostAmplifyWeaponDamage
    {
        [HarmonyPostfix]
        public static void Postfix(ref Weapon _weapon, ref DamageList _damageList)
        {
            if (_weapon.OwnerCharacter is Character character)
            {
                bool qualifiesForAuraOfSmiting = character.StatusEffectMngr?.HasStatusEffect(Crusader.Instance.auraOfSmitingEffectInstance.IdentifierName) ?? false;

                if (!qualifiesForAuraOfSmiting)
                {
                    List<Character> charsInRange = new List<Character>();
                    CharacterManager.Instance.FindCharactersInRange(character.CenterPosition, AuraOfSmitingEffect.RANGE, ref charsInRange);
                    qualifiesForAuraOfSmiting = charsInRange.FirstOrDefault(c => c.IsAlly(character) && (c.StatusEffectMngr?.HasStatusEffect(Crusader.Instance.auraOfSmitingEffectInstance.IdentifierName) ?? false)) != null;
                }

                if (qualifiesForAuraOfSmiting)
                {
                    _damageList.Add(new DamageType(HolyDamageManager.HolyDamageManager.GetDamageType(), AuraOfSmitingEffect.DAMAGE));
                }
            }
        }
    }
}
