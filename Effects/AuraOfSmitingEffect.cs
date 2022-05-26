using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crusader
{
    using SideLoader;
    using UnityEngine;

    public class AuraOfSmitingEffect : Effect
    {
        public const float RANGE = 10f;
        public const float DAMAGE = 10f;
        public const float BASECOST = 0.35f;
        public const float UPDATERATE = 1;
        protected override void ActivateLocally(Character character, object[] _infos)
        {
            float cost = BASECOST * UPDATERATE;
            if (character.Mana >= cost)
            {
                character.Stats.UseMana(new Tag[] { }, cost);
            } else
            {
                character.StatusEffectMngr.CleanseStatusEffect(this.m_parentStatusEffect);
            }
        }
    }
}
