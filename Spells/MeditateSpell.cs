using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyHelper;

namespace Crusader
{
    public class MeditateSpell
    {
        public static float Cooldown = 100;
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = ModTheme.MeditationSkillName,
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.pushKickID,
                New_ItemID = IDs.meditationID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Meditate",
                Description = ModTheme.MeditationDescription,
                CastType = Character.SpellCastType.EnterInnBed,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1f,
                CastSheatheRequired = 1,
                EffectTransforms = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "ActivationEffects",
                        Effects = new SL_Effect[] {
                            new SL_AddStatusEffect() {StatusEffect = MEDITATION_EFFECT_NAME, ChanceToContract=100, Delay = 0},
                        }
                    }
                },
                Cooldown = 0,
                StaminaCost = 0,
                HealthCost = 0,
                ManaCost = 0,
            };
            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            ForbiddenWeaponTypeCondition.AddToSkill(skill, new List<Weapon.WeaponType> { Weapon.WeaponType.Shield });
            return skill;
        }

        public const string MEDITATION_EFFECT_NAME = "Meditation";
        public const string MEDITATION_COOLDOWN_EFFECT_NAME = "MeditationOnCooldownEffect";
    }
}
