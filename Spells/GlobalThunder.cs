//using InstanceIDs;
//using SideLoader;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using TinyHelper;
//using UnityEngine;

//namespace Templar
//{
//    public class GlobalThunder
//    {
//        public static Skill Init()
//        {
//            var myitem = new SL_Skill()
//            {
//                Name = "TEMPLARGLOBALTHUNDER",
//                EffectBehaviour = EditBehaviours.Destroy,
//                Target_ItemID = IDs.pushKickID,
//                New_ItemID = IDs.globalThunderID,
//                SLPackName = "Templar",
//                SubfolderName = "Prayer",
//                Description = ModTheme.PrayerSpellDescription,
//                CastType = Character.SpellCastType.Fast,
//                CastModifier = Character.SpellCastModifier.Mobile,
//                CastLocomotionEnabled = false,
//                MobileCastMovementMult = -1f,
//                CastSheatheRequired = 1,

//                EffectTransforms = new SL_EffectTransform[] {
//                    new SL_EffectTransform() {
//                        TransformName = "ActivationEffects",
//                        Effects = new SL_Effect[] {
                        
//                        }
//                    }
//                },
//                Cooldown = 0,
//                StaminaCost = 0,
//                ManaCost = 0
//            };
//            var skill = (Skill)CustomItems.CreateCustomItem(myitem.Target_ItemID, myitem.New_ItemID, myitem.Name, myitem);

//            Transform effectsContainer;

//            //Nova
//            effectsContainer = TinyEffectManager.MakeFreshObject("Effects", true, true, skill.transform).transform;
//            var shootBlast = effectsContainer.gameObject.AddComponent<ShootBlast>();

//            shootBlast.UseOnce = true;
//            shootBlast.enabled = true;
//            shootBlast.transform.parent = effectsContainer;
//            shootBlast.BaseBlast = SL_ShootBlast.GetBlastPrefab(SL_ShootBlast.BlastPrefabs.ForceRaiseLightning).GetComponent<Blast>();
//            shootBlast.InstanstiatedAmount = 1;
//            shootBlast.CastPosition = Shooter.CastPositionType.Character;
//            shootBlast.TargetType = Shooter.TargetTypes.Any;
//            shootBlast.TransformName = "ShooterTransform";

//            shootBlast.UseTargetCharacterPositionType = false;

//            shootBlast.SyncType = Effect.SyncTypes.OwnerSync;
//            shootBlast.OverrideEffectCategory = EffectSynchronizer.EffectCategories.None;
//            shootBlast.BasePotencyValue = 1f;
//            shootBlast.Delay = 0f;
//            shootBlast.LocalCastPositionAdd = new Vector3(0f, 1.0f, 0);
//            shootBlast.BaseBlast.Radius = ImpendingDoom.RANGE;

//            var blastEffects = shootBlast.BaseBlast.transform.Find("Effects");
//            var damage = blastEffects.GetComponent<PunctualDamage>();
//            GameObject.Destroy(damage);
//            damage = blastEffects.gameObject.AddComponent<ImpendingDoomDamage>();
//            damage.Delay = 0.3f;
//            damage.Knockback = 40;

//            return skill;
//        }
//    }
//}
