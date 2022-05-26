namespace Crusader
{
    using SideLoader;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;


    class Healing : Effect
    {

        public float RestoredHealth = 0;
        public bool CanRevive = false;
        public DamageType.Types AmplificationType = DamageType.Types.Count;

        public static void Revive(Character character)
        {
            if (character)
            {
                PlayerSaveData playerSaveData = null;
                if (!character.IsAI)
                {
                    playerSaveData = new PlayerSaveData(character);
                    playerSaveData.BurntHealth += character.ActiveMaxHealth * 0.5f;
                    playerSaveData.Health = character.ActiveMaxHealth;
                    playerSaveData.Stamina = character.ActiveMaxStamina;
                }

                character.Resurrect(null, true);
            }
        }

        public static void StaticActivate(Character _affectedCharacter, Character _sourceCharacter, float _restoredHealth, bool _canRevive, object[] _infos)
        {
            float sumHealing = HolyDamageManager.HolyDamageManager.BuffHolyDamageOrHealing(_sourceCharacter, _restoredHealth);
            if (_affectedCharacter.IsDead && _canRevive) Revive(_affectedCharacter);
            if (!_affectedCharacter.IsDead || _canRevive) _affectedCharacter.Stats.AffectHealth(sumHealing);
        }

        protected override void ActivateLocally(Character _affectedCharacter, object[] _infos)
        {
            StaticActivate(_affectedCharacter, this.SourceCharacter, this.RestoredHealth, this.CanRevive, _infos);
        }
    }
}
