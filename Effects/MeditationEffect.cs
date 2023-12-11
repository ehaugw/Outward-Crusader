namespace Crusader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using SideLoader;
    using InstanceIDs;
    using TinyHelper;
    using At = SideLoader.At;

    class MeditationEffect : Effect
    {
        bool buffsWereReceived = false;
        bool trainerWasOpened = false;
        const float DELAY = 3;
        
        protected override void ActivateLocally(Character _affectedCharacter, object[] _infos)
        {
            //if ((_affectedCharacter?.Animator?.velocity != null) && (_affectedCharacter.Animator.velocity.sqrMagnitude > 0.1) && this.m_parentStatusEffect.Age > 1)
            bool cleanse = false;
            
            if (this.m_parentStatusEffect is StatusEffect parent)
            {
                if (parent.Age > DELAY+0.5 && !buffsWereReceived)
                {
                    if (!_affectedCharacter.StatusEffectMngr.HasStatusEffect(Crusader.Instance.meditationCooldownStatusEffectInstance.IdentifierName))
                    {
                        if (FactionSelector.IsBlueChamberCollective(_affectedCharacter))
                        {
                            _affectedCharacter.StatusEffectMngr.AddStatusEffect(Crusader.Instance.ancestralMemoryInstance, _affectedCharacter);
                        }
                        else
                        {
                            _affectedCharacter.StatusEffectMngr.AddStatusEffect(Crusader.Instance.burstOfDivinityInstance, _affectedCharacter);
                        }

                        _affectedCharacter.StatusEffectMngr.AddStatusEffect(Crusader.Instance.meditationCooldownStatusEffectInstance, _affectedCharacter);
                    } else if (_affectedCharacter.IsLocalPlayer && !trainerWasOpened)
                    {
                        //if (SceneManagerHelper.ActiveSceneName == "Chersonese_Dungeon4_CommonPath")
                        //{
                        //    _affectedCharacter.CharacterUI.ShowInfoNotification("I should try meditating in front of the altar.");

                        //} else
                        //{
                        _affectedCharacter.CharacterUI.ShowInfoNotification(Crusader.Instance.meditationCooldownStatusEffectInstance.Description);
                        //}
                    }
                    buffsWereReceived = true;
                }

                if (parent.Age > DELAY && !trainerWasOpened)
                {

                    if (_affectedCharacter.IsLocalPlayer)
                    {
                        Vector3 position = _affectedCharacter.transform.position;
                        Vector3 rotation = _affectedCharacter.transform.rotation.eulerAngles;

                        bool missingCondition = false;
                        foreach (var location in CrusaderSkillTree.TrainingLocations)
                        {
                            if (SceneManagerHelper.ActiveSceneName == location.scene && 
                                ((location.range == null) ? (
                                    position.x < location.positionUpper.x &&
                                    position.x > location.positionLower.x &&
                                    position.z < location.positionUpper.z &&
                                    position.z > location.positionLower.z
                                ) : (
                                    Vector3.SqrMagnitude(position - (location.positionLower != null ? location.positionLower : location.positionUpper)) < (location.range*location.range))
                                ) && (
                                    Math.Cos(Mathf.Deg2Rad * (rotation.y - location.direction)) > Math.Cos(Mathf.Deg2Rad * location.angle / 2)) &&
                                    location.skillSchool != null
                                )
                            {

                                if (location.requirement?.CharacterHasRequirement(_affectedCharacter) ?? true)
                                {
                                    trainerWasOpened = true;
                                    var skillTreeTrainer = new Trainer();
                                    At.SetField<Trainer>(skillTreeTrainer, "m_uid", UID.Generate());
                                    At.SetField<Trainer>(skillTreeTrainer, "m_skillTreeUID", location.skillSchool.UID);

                                    skillTreeTrainer.StartTraining(_affectedCharacter);
                                    break;
                                } else
                                {
                                    missingCondition = true;
                                }
                            }
                        }
                        if (missingCondition && !trainerWasOpened)
                        {
                            _affectedCharacter.CharacterUI.ShowInfoNotification(ModTheme.PrayForSkillTreeMissingCondition);
                        }
                    }
                }
                if ((_affectedCharacter?.AnimMoveSqMagnitude ?? 0) > 0.1 && parent.Age > 1f)
                {
                    cleanse = true;
                }
            } else
            {
                cleanse = true;
            }
            if (cleanse)
            {
                _affectedCharacter.StatusEffectMngr?.RemoveStatusWithIdentifierName(Crusader.Instance.meditationStatusEffectInstance.IdentifierName);
                //_affectedCharacter.ForceCancel(true, true);
                TinyHelperRPCManager.Instance.photonView.RPC("CharacterForceCancelRPC", PhotonTargets.All, new object[] { _affectedCharacter.UID.ToString(), true, true});
            }
        }
    }
}
