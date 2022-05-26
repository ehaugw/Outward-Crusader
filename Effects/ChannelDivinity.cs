namespace Crusader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;


    class ChannelDivinity: Effect
    {
        protected override void ActivateLocally(Character castingCharacter, object[] _infos)
        {
            if (TryRemove(castingCharacter, "Dez Runes"))
                castingCharacter.StatusEffectMngr.AddStatusEffect(ResourcesPrefabManager.Instance.GetStatusEffectPrefab("Runic Protection Amplified"), castingCharacter);
            else if (TryRemove(castingCharacter, "Shim Runes"))
                CelestialSurge.StaticActivate(castingCharacter, _infos, this);
            else if (TryRemove(castingCharacter, "Egoth Runes"))
                castingCharacter.StatusEffectMngr.AddStatusEffect(Crusader.Instance.healingSurgeInstance, castingCharacter);
            else if (TryRemove(castingCharacter, "Fal Runes"))
                HealingAoE.StaticActivate(castingCharacter, castingCharacter, 40, 40, true, _infos);
            else
                castingCharacter.StatusEffectMngr.AddStatusEffect(Crusader.Instance.surgeOfDivinityInstance, castingCharacter);
        }

        private bool TryRemove(Character character, string spellIdentifierName)
        {
            if (character.StatusEffectMngr.HasStatusEffect(spellIdentifierName))
            {
                character.StatusEffectMngr.RemoveStatusWithIdentifierName(spellIdentifierName);
                return true;
            }
            return false;
        }
    }
}
