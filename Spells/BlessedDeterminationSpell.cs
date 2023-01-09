using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Crusader
{
    public class BlessedDeterminationSpell
    {
        public const float BLESSED_DETERMINATION_EFFICIENCY = 0.3f;
        public const float BLESSED_DETERMINATION_STAMINA_REGEN = 1f;
        public const float FREECAST_PROVIDED_MANA = 7f;
        public const float FREECAST_LIFESPAN = 30f;

        public static float GetFreeCastBuildup(Character character)
        {
            //efficiency = providedmana/neededenergy
            //neededEnergy = 100/buildup
            //efficiency = buildup*providedmana/100
            //buildup = efficiency*100/providedmana
            return (
                1
                + (character?.StatusEffectMngr?.HasStatusEffect(Crusader.Instance.surgeOfDivinityInstance.IdentifierName) ?? false ? 1.0f : 0)
                + (character?.StatusEffectMngr?.HasStatusEffect(Crusader.Instance.consecrationAllyInstance.IdentifierName) ?? false ? 0.5f : 0)
            ) * 100 * BlessedDeterminationSpell.BLESSED_DETERMINATION_EFFICIENCY / FREECAST_PROVIDED_MANA;
        }

        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = ModTheme.BlessedDeterminationSpellName,
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.arbitraryPassiveSkillID,
                New_ItemID = IDs.blessedDeterminationID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Blessed Determination",
                Description = (ModTheme.BlessedDeterminationRequiredBoonName != null ? "While under the effect of the " + ModTheme.BlessedDeterminationRequiredBoonName + " boon, all" : "All") + 
                    " spent mana is regained as stamina, and spending stamina builds up a " + ModTheme.BurstOfDivinityEffectName + " effect that reduces the mana cost of the next spell you cast.",
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

            //var affectStat = skill.gameObject.GetComponentInChildren<AffectStat>();
            //affectStat.IsModifier = true;
            //affectStat.Value = 0.25f;
            return skill;
        }
    }

    [HarmonyPatch(typeof(CharacterStats), "UseStamina", new Type[] { typeof(float), typeof(float) })]
    public class CharacterStats_UseStamina
    {
        [HarmonyPostfix]
        public static void Postfix(CharacterStats __instance, float _staminaConsumed, Character ___m_character)
        {
            if (___m_character is Character character && character.Inventory.SkillKnowledge.IsItemLearned(IDs.blessedDeterminationID) && (ModTheme.BlessedDeterminationRequiredBoonName == null || character.StatusEffectMngr.HasStatusEffect(ModTheme.BlessedDeterminationRequiredBoonName) || character.StatusEffectMngr.HasStatusEffect(ModTheme.BlessedDeterminationRequiredBoonName + " Amplified")))
            {
                character.StatusEffectMngr.AddStatusEffectBuildUp(Crusader.Instance.burstOfDivinityInstance, _staminaConsumed * BlessedDeterminationSpell.GetFreeCastBuildup(character), character);
            }
        }
    }

    //[HarmonyPatch(typeof(Skill), "HasEnoughMana")]
    //public class Skill_HasEnoughMana
    //{
    //    [HarmonyPrefix]
    //    public static bool Prefix(Skill __instance, ref bool _tryingToActivate, ref bool __result)
    //    {
    //        if (__instance.ManaCost > 0)
    //        {
    //            int freecastingStacks = __instance.OwnerCharacter?.StatusEffectMngr?.GetStatusEffectOfName(Crusader.Instance.burstOfDivinityInstance.IdentifierName)?.StackCount ?? 0;
    //            var remainingCost = __instance.ManaCost;
    //            remainingCost -= freecastingStacks * BlessedDeterminationSpell.FREECAST_PROVIDED_MANA;
    //            remainingCost = __instance.OwnerCharacter?.Stats?.GetFinalManaConsumption(null, remainingCost) ?? remainingCost;

    //            if (__instance.OwnerCharacter.Mana >= remainingCost)
    //            {
    //                __result = true;
    //                return false; //do NOT call the original function
    //            }
    //        }
    //        return true;//call the original function
    //    }
    //}

    [HarmonyPatch(typeof(CharacterStats), "UseMana")]
    public class CharacterStats_UseMana
    {
        //[HarmonyPrefix]
        //public static void Prefix(CharacterStats __instance, ref float _amount, out Tuple<Character, float, int, StatusEffect> __state)
        //{
        //    __state = null;
        //    if (_amount <= 0) return;


        //    if (At.GetField<CharacterStats>(__instance, "m_character") is Character character && character.StatusEffectMngr != null)
        //    {
        //        if (character.StatusEffectMngr.GetStatusEffectOfName(Crusader.Instance.burstOfDivinityInstance.IdentifierName) is StatusEffect effect)
        //        {
        //            int freecastingStacks = effect.StackCount;
        //            if (freecastingStacks > 0)
        //            {
        //                //float remainingCost = _amount;
        //                //remainingCost = character?.Stats?.GetFinalManaConsumption(null, remainingCost) ?? remainingCost;

        //                freecastingStacks = Math.Min(Convert.ToInt32(Math.Ceiling(_amount / BlessedDeterminationSpell.FREECAST_PROVIDED_MANA)), freecastingStacks);

        //                float oldCost = character?.Stats?.GetFinalManaConsumption(null, _amount) ?? _amount;
        //                _amount = Mathf.Max(0, _amount - freecastingStacks * BlessedDeterminationSpell.FREECAST_PROVIDED_MANA);
        //                __state = new Tuple<Character, float, int, StatusEffect>(character, /*oldCost*/ _amount, freecastingStacks, effect);
        //            }
        //        }
        //    }
        //}

        //[HarmonyPostfix]
        //public static void Postfix(CharacterStats __instance, ref float _amount, ref Tuple<Character, float, int, StatusEffect> __state)
        //{
        //    if (__state != null)
        //    {
        //        //_amount = __state.Item2;
        //        for (int i = 0; i < __state.Item3; i++)
        //        {
        //            __state.Item4.RemoveOldestStack();
        //        }
        //        if (__state.Item3 > 0 && TinyHelper.SkillRequirements.SafeHasSkillKnowledge(__state.Item1, IDs.divineFavourID))
        //        {
        //            __state.Item1?.CurrentWeapon?.AddImbueEffect(Crusader.Instance.classInfusion, Judgement.ImbueDuration);
        //        }
        //    }

        //    if (At.GetField<CharacterStats>(__instance, "m_character") is Character character)
        //    {
        //if (character != null && character.Inventory.SkillKnowledge.IsItemLearned(IDs.blessedDeterminationID) && (ModTheme.BlessedDeterminationRequiredBoonName == null || character.StatusEffectMngr.HasStatusEffect(ModTheme.BlessedDeterminationRequiredBoonName) || character.StatusEffectMngr.HasStatusEffect(ModTheme.BlessedDeterminationRequiredBoonName + " Amplified")))
        //{
        //    character.Stats.AffectStamina(_amount* BlessedDeterminationSpell.BLESSED_DETERMINATION_STAMINA_REGEN);
        //}
        //    }
        //}
        [HarmonyPrefix]
        public static void Prefix(ref float _amount, out float __state)
        {
            __state = _amount;
        }

        [HarmonyPostfix]
        public static void Postfix(CharacterStats __instance, ref float _amount, ref float __state)
        {
            __instance.GetFinalManaConsumption(new Tag[] { Crusader.Instance.AfterUseManaTagInstance }, __state);
        }
    }

    /// <summary>
    /// Patch that is used to reduce mana costs for each free casting stacks
    /// If expendFreecastingStacks tag is provided, it will expend the required amount of stacks
    /// </summary>
    [HarmonyPatch(typeof(CharacterStats), "GetFinalManaConsumption")]
    public class CharacterStats_GetFinalManaConsumption
    {
        [HarmonyPostfix]
        public static void Postfix(CharacterStats __instance, Tag[] _tags, float _manaConsumption, ref float __result)
        {
            if (At.GetField<CharacterStats>(__instance, "m_character") is Character character && character.StatusEffectMngr != null)
            {
                bool AfterUseMana = _tags.Contains(Crusader.Instance.AfterUseManaTagInstance);

                
                if (AfterUseMana && character.Inventory.SkillKnowledge.IsItemLearned(IDs.blessedDeterminationID) && (ModTheme.BlessedDeterminationRequiredBoonName == null || character.StatusEffectMngr.HasStatusEffect(ModTheme.BlessedDeterminationRequiredBoonName) || character.StatusEffectMngr.HasStatusEffect(ModTheme.BlessedDeterminationRequiredBoonName + " Amplified")))
                {
                    character.Stats.AffectStamina(__result * BlessedDeterminationSpell.BLESSED_DETERMINATION_STAMINA_REGEN);
                }

                if (character.StatusEffectMngr.GetStatusEffectOfName(Crusader.Instance.burstOfDivinityInstance.IdentifierName) is StatusEffect effect)
                {
                    int freecastingStacks = effect.StackCount;
                    
                    if (freecastingStacks > 0)
                    {
                        if (AfterUseMana && TinyHelper.SkillRequirements.SafeHasSkillKnowledge(character, IDs.divineFavourID) && character.CurrentWeapon is Weapon weapon)
                        {
                            if (FactionSelector.IsHolyMission(character))
                            {
                                CrusaderRPCManager.Instance.photonView.RPC("ApplyAddImbueEffectRPC", PhotonTargets.All, new object[] { weapon.UID, Crusader.Instance.holyMissionInfusion.PresetID, Judgement.ImbueDuration });
                            }
                            else if (FactionSelector.IsBlueChamberCollective(character))
                            {
                                CrusaderRPCManager.Instance.photonView.RPC("ApplyAddImbueEffectRPC", PhotonTargets.All, new object[] { weapon.UID, Crusader.Instance.blueChamberInfusion.PresetID, Judgement.ImbueDuration });
                            }
                        }

                        freecastingStacks = Math.Min(Convert.ToInt32(Math.Ceiling(__result / BlessedDeterminationSpell.FREECAST_PROVIDED_MANA)), freecastingStacks);
                        __result = Mathf.Max(0, __result - freecastingStacks * BlessedDeterminationSpell.FREECAST_PROVIDED_MANA);

                        if (AfterUseMana)
                        {
                            for (int i = 0; i < freecastingStacks; i++)
                            {
                                effect.RemoveOldestStack();
                            }
                        }
                    }
                }
            }
        }
    }
}
