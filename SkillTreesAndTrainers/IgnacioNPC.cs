using InstanceIDs;
using SideLoader;
using UnityEngine;

namespace Crusader
{
    using SynchronizedWorldObjects;
    using System.Linq;
    using TinyHelper;

    public class IgnacioNPC : SynchronizedNPC
    {
        public static void Init()
        {
            var syncedNPC = new IgnacioNPC(
                identifierName: "Ignacio",
                rpcListenerID: IDs.NPCID_Ignacio,
                defaultEquipment: new int[] { IDs.krypteiaHoodID, IDs.krypteiaBootsID, IDs.krypteiaArmorID, IDs.wolfSwordID },
                visualData: new SL_Character.VisualData() { Gender = Character.Gender.Female}
            );

            syncedNPC.AddToScene(new SynchronizedNPCScene(
                scene: "Emercar_Dungeon5",
                position: new Vector3(27.5948f, 0.03f, 0.7649f),
                rotation: new Vector3(0, 90f, 0),
                pose: Character.SpellCastType.EnterInnBed
            ));
        }

        public IgnacioNPC(string identifierName, int rpcListenerID, int[] defaultEquipment = null, int[] moddedEquipment = null, Vector3? scale = null, Character.Factions? faction = null, SL_Character.VisualData visualData = null) : 
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

            var rootStatement = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "Have you came to honor the memories of our ancestors?");
            var characterIntroduction = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "That is classified. All I can tell you is that I am Ignacio.");

            NodeCanvas.DialogueTrees.DTNode openTrainer;
            if (FactionSelector.IsBlueChamberCollective(player))
            {
                openTrainer = TinyDialogueManager.MakeTrainDialogueAction(graph, trainerComp);
            } else
            {
                openTrainer = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "I am not allowed to train people not commited to the tribe. You should go back to town and talk to Rissa.");
            }

            var introMultipleChoice = TinyDialogueManager.MakeMultipleChoiceNode(graph, new string[] {
                "You mean the people responsible for my blood dept? Well, no... Who are you anyways?",
                "I am here to receive training."
            });

            graph.allNodes.Clear();
            graph.allNodes.Add(rootStatement);
            graph.allNodes.Add(introMultipleChoice);
            graph.allNodes.Add(characterIntroduction);
            graph.allNodes.Add(openTrainer);

            graph.primeNode = rootStatement;
            graph.ConnectNodes(rootStatement, introMultipleChoice);
            graph.ConnectNodes(introMultipleChoice, characterIntroduction, 0);
            graph.ConnectNodes(introMultipleChoice, openTrainer, 1);
            graph.ConnectNodes(characterIntroduction, rootStatement);

            var obj = instanceGameObject.transform.parent.gameObject;
            obj.SetActive(true);

            return instanceCharacter;
        }
    }
}