using InstanceIDs;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
using SideLoader;
using System.Collections.Generic;
using UnityEngine;

namespace Crusader
{
    using SynchronizedWorldObjects;
    using TinyHelper;

    public class KlausNPC : SynchronizedNPC
    {
        public static void Init()
        {
            var syncedNPC = new KlausNPC(
                identifierName: "Klaus",
                rpcListenerID: IDs.NPCID_Klaus,
                defaultEquipment: new int[] { IDs.beardID, IDs.krypteiaArmorID, IDs.krypteiaBootsID, IDs.palladiumSwordID },
                visualData: new SL_Character.VisualData() { Gender = Character.Gender.Female}
            );

            syncedNPC.AddToScene(new SynchronizedNPCScene(
                scene: "HallowedMarshNewTerrain",
                position: new Vector3(523.9f, -62.3f, 512.2f),
                rotation: new Vector3(0, 21.5f, 0),
                pose: Character.SpellCastType.EnterInnBed
            ));
        }

        public KlausNPC(string identifierName, int rpcListenerID, int[] defaultEquipment = null, int[] moddedEquipment = null, Vector3? scale = null, Character.Factions? faction = null, SL_Character.VisualData visualData = null) : 
            base(identifierName, rpcListenerID, defaultEquipment: defaultEquipment, moddedEquipment: moddedEquipment, scale: scale, faction: faction, visualData: visualData)
        { }

        override public object SetupClientSide(int rpcListenerID, string instanceUID, int sceneViewID, int recursionCount, string rpcMeta)
        {
            Character instanceCharacter = base.SetupClientSide(rpcListenerID, instanceUID, sceneViewID, recursionCount, rpcMeta) as Character;
            if (instanceCharacter == null) return null;

            GameObject instanceGameObject = instanceCharacter.gameObject;
            var trainerTemplate = TinyDialogueManager.AssignTrainerTemplate(instanceGameObject.transform);
            var actor = TinyDialogueManager.SetDialogueActorName(trainerTemplate, IdentifierName);
            var trainerComp = TinyDialogueManager.SetTrainerSkillTree(trainerTemplate, CrusaderSkillTree.KlausSkillSchool.UID);
            var graph = TinyDialogueManager.GetDialogueGraph(trainerTemplate);
            TinyDialogueManager.SetActorReference(graph, actor);

            var openTrainer = TinyDialogueManager.MakeTrainDialogueAction(graph, trainerComp);
            var rootStatement = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "Hi there! What is a guy like you doing so far off the road? Are you all right?");
            var wantToLeavePrisonStatement = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "Ohh... Please forgive me. I am Klaus, a " + ModTheme.SkillTreeNameReadable + ". I have not felt in touch with myself lately, so  I went out here to the marsh looking for a place whare I could " + ModTheme.MeditationSkillName + ".");

            var introMultipleChoice = TinyDialogueManager.MakeMultipleChoiceNode(graph, new string[] {
                "I seem to be doing all right. Thanks for the concern. Who are you?",
                "I am here to receive some training. Would you be willing to help me with that?"
            });

            graph.allNodes.Clear();
            graph.allNodes.Add(rootStatement);
            graph.allNodes.Add(introMultipleChoice);
            graph.allNodes.Add(wantToLeavePrisonStatement);
            graph.allNodes.Add(openTrainer);

            graph.primeNode = rootStatement;
            graph.ConnectNodes(rootStatement, introMultipleChoice);
            graph.ConnectNodes(introMultipleChoice, wantToLeavePrisonStatement, 0);
            graph.ConnectNodes(introMultipleChoice, openTrainer, 1);
            graph.ConnectNodes(wantToLeavePrisonStatement, rootStatement);

            var obj = instanceGameObject.transform.parent.gameObject;
            obj.SetActive(true);

            return instanceCharacter;
        }
    }
}