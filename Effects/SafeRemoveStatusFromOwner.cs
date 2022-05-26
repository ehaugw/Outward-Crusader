using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crusader
{
    using HarmonyLib;
    using SideLoader;
    using UnityEngine;

    public class SafeRemoveStatusFromOwner : Effect
    {
        public string StatusToRemove;
     
        protected override void ActivateLocally(Character character, object[] _infos)
        {
            Debug.Log("character: " + character.name);
            if (character && character.StatusEffectMngr && character.StatusEffectMngr.GetStatusEffectOfName(StatusToRemove) is var statusEffect)
            {
                Debug.Log("how about now?");
                if (statusEffect.gameObject.GetComponentInChildren<ShootBlast>().LastCondSuccess)
                {
                    Debug.Log("remove effect pls");
                    character.StatusEffectMngr.CleanseStatusEffect(statusEffect);
                }
            }
        }
    }
}
