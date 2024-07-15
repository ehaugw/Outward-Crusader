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
                + (character?.StatusEffectMngr?.HasStatusEffect(Crusader.Instance.surgeOfMemoriesInstance.IdentifierName) ?? false ? 1.0f : 0)
                + (character?.StatusEffectMngr?.HasStatusEffect(Crusader.Instance.consecrationAllyInstance.IdentifierName) ?? false ? 0.5f : 0)
            ) * 100 * BlessedDeterminationSpell.BLESSED_DETERMINATION_EFFICIENCY / FREECAST_PROVIDED_MANA;
        }

        public static Skill Init()
        {
            TinyHelper.TinyHelper.OnDescriptionModified += delegate (Item item, ref string description) {
                if (item.ItemID == IDs.blessedDeterminationID)
                {
                    if (FactionSelector.IsBlueChamberCollective(CharacterManager.Instance.GetFirstLocalCharacter()))
                    {
                        description =
                            (ModTheme.BlessedDeterminationRequiredBoonName != null ? "While under the effect of the " + ModTheme.BlessedDeterminationRequiredBoonName + " boon, all" : "All") +
                            " spent mana is regained as stamina, and spending stamina builds up an " + ModTheme.AncestralMemoryEffectName + ", which reduces the mana cost of the next spell you cast.";
                    }
                    else if (FactionSelector.IsHolyMission(CharacterManager.Instance.GetFirstLocalCharacter()))
                    {
                        description =
                            (ModTheme.BlessedDeterminationRequiredBoonName != null ? "While under the effect of the " + ModTheme.BlessedDeterminationRequiredBoonName + " boon, all" : "All") +
                            " spent mana is regained as stamina, and spending stamina builds up an " + ModTheme.BurstOfDivinityEffectName + ", which reduces the mana cost of the next spell you cast.";
                    }
                }
            };

            var myitem = new SL_Skill()
            {
                Name = ModTheme.BlessedDeterminationSpellName,
                EffectBehaviour = EditBehaviours.Override,
                Target_ItemID = IDs.arbitraryPassiveSkillID,
                New_ItemID = IDs.blessedDeterminationID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Blessed Determination",
                Description =
                    (ModTheme.BlessedDeterminationRequiredBoonName != null ? "While under the effect of the " + ModTheme.BlessedDeterminationRequiredBoonName + " boon, all" : "All") + 
                    " spent mana is regained as stamina, and spending stamina builds up an effect that reduces the mana cost of the next spell you cast.",
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
            if (___m_character is Character character
                && character.Inventory.SkillKnowledge.IsItemLearned(IDs.blessedDeterminationID)
                && (
                    ModTheme.BlessedDeterminationRequiredBoonName == null
                    || character.StatusEffectMngr.HasStatusEffect(ModTheme.BlessedDeterminationRequiredBoonName)
                    || character.StatusEffectMngr.HasStatusEffect(ModTheme.BlessedDeterminationRequiredBoonName + " Amplified")
                )
            )
            {
                if (FactionSelector.IsBlueChamberCollective(character))
                {
                    character.StatusEffectMngr.AddStatusEffectBuildUp(Crusader.Instance.ancestralMemoryInstance, _staminaConsumed * BlessedDeterminationSpell.GetFreeCastBuildup(character), character);
                } else
                {
                    character.StatusEffectMngr.AddStatusEffectBuildUp(Crusader.Instance.burstOfDivinityInstance, _staminaConsumed * BlessedDeterminationSpell.GetFreeCastBuildup(character), character);
                }
            }
        }
    }

    [HarmonyPatch(typeof(CharacterStats), "UseMana")]
    public class CharacterStats_UseMana
    {
        [HarmonyPrefix]
        public static void Prefix(ref float _amount, out float __state)
        {
            __state = _amount;
        }

        [HarmonyPostfix]
        public static void Postfix(CharacterStats __instance, ref float _amount, ref float __state)
        {
            //UseMana includes GetFinalManaConsumption, and GetFinalManaConsumption includes mana providing effect stacks.
            //We tell GetFinalManaConsumption to calculate again using the pre mana cost reduction to prevent it from applying mana cost reduction twice. In this call, we pass the tag that makes it remove the stacks.
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

                
                if (AfterUseMana
                    && character.Inventory.SkillKnowledge.IsItemLearned(IDs.blessedDeterminationID)
                    && (
                        ModTheme.BlessedDeterminationRequiredBoonName == null
                        || character.StatusEffectMngr.HasStatusEffect(ModTheme.BlessedDeterminationRequiredBoonName)
                        || character.StatusEffectMngr.HasStatusEffect(ModTheme.BlessedDeterminationRequiredBoonName + " Amplified")
                    )
                )
                {
                    character.Stats.AffectStamina(__result * BlessedDeterminationSpell.BLESSED_DETERMINATION_STAMINA_REGEN);
                }

                foreach (var tup in new Tuple<string, int>[]
                {
                    new Tuple<string, int>(Crusader.Instance.burstOfDivinityInstance.IdentifierName, Crusader.Instance.holyMissionInfusion.PresetID),
                    new Tuple<string, int>(Crusader.Instance.ancestralMemoryInstance.IdentifierName, Crusader.Instance.blueChamberInfusion.PresetID)
                })
                {
                    if (character.StatusEffectMngr.GetStatusEffectOfName(tup.Item1) is StatusEffect effect)
                    {
                        int freecastingStacks = effect.StackCount;

                        if (freecastingStacks > 0)
                        {
                            if (AfterUseMana && TinyHelper.SkillRequirements.SafeHasSkillKnowledge(character, IDs.divineFavourID) && character.CurrentWeapon is Weapon weapon)
                            {
                                TinyHelper.TinyHelperRPCManager.Instance.photonView.RPC("ApplyAddImbueEffectRPC", PhotonTargets.All, new object[] { weapon.UID, tup.Item2, Judgement.ImbueDuration });
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
                            break;
                        }
                    }
                }
            }
        }
    }
}
