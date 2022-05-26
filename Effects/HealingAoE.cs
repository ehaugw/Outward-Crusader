namespace Crusader
{
    using SideLoader;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;


    class HealingAoE : Healing
    {

        public float Range = 30;

        public static void StaticActivate(Character _centerCharacter, Character _sourceCharacter, float _range, float _restoredHealth, bool _canRevive, object[] _infos)
        {
            List<Character> charsInRange = new List<Character>();
            CharacterManager.Instance.FindCharactersInRange(_centerCharacter.CenterPosition, _range, ref charsInRange);

            foreach (Character character in charsInRange.Where(c => c.IsAlly(_centerCharacter)))
            {
                Healing.StaticActivate(character, _sourceCharacter, _restoredHealth, _canRevive, _infos);
            }
        }

        protected override void ActivateLocally(Character _centerCharacter, object[] _infos)
        {
            HealingAoE.StaticActivate(_centerCharacter, this.SourceCharacter, this.Range, this.RestoredHealth, this.CanRevive, _infos);
        }
    }
}
