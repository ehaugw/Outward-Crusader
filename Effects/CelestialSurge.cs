namespace Crusader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using TinyHelper;
    using Discord;
    using ImpendingDoom;

    class CelestialSurge : Effect
    {
        public static void StaticActivate(Character character, object[] _infos, Effect instance)
        {
            List<Character> charsInRange = new List<Character>();
            CharacterManager.Instance.FindCharactersInRange(character.CenterPosition, range + Radiating.RANGE, ref charsInRange);
           
            
            foreach (var c in charsInRange.Where(c => !c.IsAlly(character)))
            {
                TinyEffectManager.AddStatusEffectForDuration(c, ImpendingDoomMod.Instance.impendingDoomInstance, 50, source: character);
            }
        }

        public static float range = 50f;
        protected override void ActivateLocally(Character character, object[] _infos)
        {
            StaticActivate(character, _infos, this);
        }
    }
}
