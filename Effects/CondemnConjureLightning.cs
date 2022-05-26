using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crusader
{
    using SideLoader;
    using UnityEngine;

    class CondemnConjureLightning : Effect
    {
        protected override void ActivateLocally(Character character, object[] _infos)
        {
            Debug.Log("EHAUGE DETONATED LIGHTNING");
            List<Character> charsInRange = new List<Character>();
            CharacterManager.Instance.FindCharactersInRange(character.CenterPosition, 50, ref charsInRange);

            charsInRange = charsInRange.Where(c => c.StatusEffectMngr.HasStatusEffect("Doom") && !c.IsAlly(character)).ToList();

            //var otherBlast = otherSkill.transform.Find("Lightning").Find("ExtraEffects").GetComponentInChildren<ShootBlastsProximity>();

            //var myEffects = skill.transform.Find("Effects");
            //var myBlast = myEffects.gameObject.AddComponent<ShootBlast>();

            //myBlast.transform.parent = myEffects.transform;
            //myBlast.BaseBlast = otherBlast.BaseBlast;
            //myBlast.InstanstiatedAmount = 5;
            //myBlast.NoTargetForwardMultiplier = 5;
            //myBlast.CastPosition = Shooter.CastPositionType.Character;
            //myBlast.TargetType = Shooter.TargetTypes.Enemies;
            //myBlast.TransformName = "ShooterTransform";

            //myBlast.UseTargetCharacterPositionType = false;

            //myBlast.SyncType = Effect.SyncTypes.OwnerSync;
            //myBlast.OverrideEffectCategory = EffectSynchronizer.EffectCategories.None;
            //myBlast.BasePotencyValue = 1f;
            //myBlast.Delay = 0f;
            //myBlast.LocalCastPositionAdd = new Vector3(0, 1, 1.5f);
            //myBlast.BaseBlast.Radius = 3.5f;


            foreach (Character chr in charsInRange)
            {
                //chr.StatusEffectMngr.AddStatusEffect(Radiative);
                chr.Stats.ReceiveDamage(100);
            }
        }
    }
}
