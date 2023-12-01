using CustomWeaponBehaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crusader
{
    public class AuraOfSmitingBonusDamage : IBaseDamageModifier
    {
        public void Apply(Weapon weapon, DamageList original, ref DamageList result)
        {
            result.Add(new DamageType(HolyDamageManager.HolyDamageManager.GetDamageType(), AuraOfSmitingEffect.DAMAGE));
        }

        public bool Eligible(Weapon weapon)
        {
            return weapon?.OwnerCharacter?.StatusEffectMngr?.HasStatusEffect(Crusader.Instance.auraOfSmitingEffectInstance.IdentifierName) ?? false;
        }
    }
}
