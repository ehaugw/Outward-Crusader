using InstanceIDs;
using SideLoader;
using System.Collections.Generic;



namespace Crusader
{
    public class RetributiveSmiteSpell
    {
        public static Skill Init()
        {
            var myitem = new SL_AttackSkill()
            {
                Name = "Retributive Smite",
                EffectBehaviour = EditBehaviours.NONE,
                Target_ItemID = IDs.counterAttackID,
                New_ItemID = IDs.retributiveSmiteID,
                SLPackName = Crusader.ModFolderName,
                SubfolderName = "Retributive Smite",
                Description = "Completely block a physical attack, striking the attacker and dealing additional lightning damage.",
                CastType = Character.SpellCastType.Counter,
                CastModifier = Character.SpellCastModifier.Attack,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1,
                CastSheatheRequired = 2,
                RequiredWeaponTypes = new Weapon.WeaponType[] {
                    Weapon.WeaponType.FistW_2H,
                    Weapon.WeaponType.Axe_1H,
                    Weapon.WeaponType.Axe_2H,
                    Weapon.WeaponType.Sword_1H,
                    Weapon.WeaponType.Sword_2H,
                    Weapon.WeaponType.Mace_1H,
                    Weapon.WeaponType.Mace_2H,
                    Weapon.WeaponType.Halberd_2H,
                    Weapon.WeaponType.Spear_2H,
                    Weapon.WeaponType.FistW_2H
                },

                Cooldown = 20,
                StaminaCost = 7,
                ManaCost = 7,
                HealthCost = 0,
            };
            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            var damageEffect = skill.gameObject.GetComponentInChildren<WeaponDamage>();
            damageEffect.WeaponDamageMult = 1.5f;
            damageEffect.WeaponKnockbackMult = 1.5f;
            damageEffect.WeaponDamageMultKDown = -1f;
            damageEffect.WeaponDurabilityLossPercent = 0;
            damageEffect.WeaponDurabilityLoss = 1;

            
            
            
            
            damageEffect.OverrideDType = DamageType.Types.Count;
            damageEffect.Damages = new DamageType[] { new DamageType(HolyDamageManager.HolyDamageManager.GetDamageType(), 20) };
            return skill;
        }
    }
}
