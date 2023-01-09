using InstanceIDs;
using SideLoader;
using UnityEngine;

namespace Crusader
{
    using SynchronizedWorldObjects;
    using System.Linq;
    using TinyHelper;

    public class IgnacioHintNPC1 : SynchronizedNPC
    {
        public static void Init()
        {
            var syncedNPC = new IgnacioHintNPC1(
                identifierName: "Laura",
                rpcListenerID: IDs.NPCID_IgnacioHint1,
                defaultEquipment: new int[] { IDs.redClansageRobeID, IDs.desertBootsID },
                scale: new Vector3(0.9f, 0.8f, 0.9f),
                visualData: new SL_Character.VisualData() {
                    Gender = Character.Gender.Female,
                    SkinIndex = (int)SL_Character.Ethnicities.Asian,
                    HeadVariationIndex = 1,
                    HairStyleIndex = (int) HairStyles.BraidsBack,
                    HairColorIndex = (int) HairColors.Black
                }
            );

            syncedNPC.AddToScene(new SynchronizedNPCScene(
                scene: "Berg",
                //position: new Vector3(523.9f, -62.3f, 512.2f),
                //rotation: new Vector3(0, 21.5f, 0),
                position: new Vector3(1210.116f, - 13.7223f, 1375.415f),
                rotation: new Vector3(0, 284f, 0)
            ));
        }

        public IgnacioHintNPC1(string identifierName, int rpcListenerID, int[] defaultEquipment = null, int[] moddedEquipment = null, Vector3? scale = null, Character.Factions? faction = null, SL_Character.VisualData visualData = null) : 
            base(identifierName, rpcListenerID, defaultEquipment: defaultEquipment, moddedEquipment: moddedEquipment, scale: scale, faction: faction, visualData: visualData)
        { }

        override public object SetupClientSide(int rpcListenerID, string instanceUID, int sceneViewID, int recursionCount, string rpcMeta)
        {
            Character player = GameObject.FindObjectsOfType<Character>().Where(x => x.IsLocalPlayer).First();
            Character instanceCharacter = base.SetupClientSide(rpcListenerID, instanceUID, sceneViewID, recursionCount, rpcMeta) as Character;
            if (instanceCharacter == null) return null;

            GameObject instanceGameObject = instanceCharacter.gameObject;
            var trainerTemplate = TinyDialogueManager.AssignTrainerTemplate(instanceGameObject.transform);
            var actor = TinyDialogueManager.SetDialogueActorName(trainerTemplate, IdentifierName);
            var trainerComp = TinyDialogueManager.SetTrainerSkillTree(trainerTemplate, CrusaderSkillTree.KlausSkillSchool.UID);
            var graph = TinyDialogueManager.GetDialogueGraph(trainerTemplate);
            TinyDialogueManager.SetActorReference(graph, actor);

            var rootStatement = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "Hi! Have you seen Ignacio?");
            var characterIntroduction = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "Ignacio calls me Laura, and so can you!");
            var locateTrainer = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "Ignacio uses to play with me when he's not meditating in the Ancestor's Resting Place.");
            //var closeDialogue = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "Good bye!");

            var introMultipleChoice = TinyDialogueManager.MakeMultipleChoiceNode(graph, new string[] {
                "Who is asking?",
                "Who is Ignacio?",
                //"No, I am sorry."
            });

            graph.allNodes.Clear();
            graph.allNodes.Add(rootStatement);
            graph.allNodes.Add(introMultipleChoice);
            graph.allNodes.Add(characterIntroduction);
            graph.allNodes.Add(locateTrainer);

            graph.primeNode = rootStatement;
            graph.ConnectNodes(rootStatement, introMultipleChoice);
            graph.ConnectNodes(introMultipleChoice, characterIntroduction, 0);
            graph.ConnectNodes(introMultipleChoice, locateTrainer, 1);
            //graph.ConnectNodes(introMultipleChoice, closeDialogue, 2);
            graph.ConnectNodes(characterIntroduction, rootStatement);

            var obj = instanceGameObject.transform.parent.gameObject;
            obj.SetActive(true);

            return instanceCharacter;
        }
    }
}