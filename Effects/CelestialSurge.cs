namespace Crusader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using TinyHelper;
    using Discord;

    class CelestialSurge : Effect
    {
        public static void StaticActivate(Character character, object[] _infos, Effect instance)
        {
            List<Character> charsInRange = new List<Character>();
            CharacterManager.Instance.FindCharactersInRange(character.CenterPosition, range + Radiating.RANGE, ref charsInRange);
            foreach (var c in charsInRange.Where(c => !c.IsAlly(character))) TinyEffectManager.AddStatusEffectForDuration(c, Crusader.Instance.impendingDoomInstance, 50);
        }

        public static float range = 50f;
        protected override void ActivateLocally(Character character, object[] _infos)
        {
            StaticActivate(character, _infos, this);
        }
    }
}
