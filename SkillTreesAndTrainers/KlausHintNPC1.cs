using InstanceIDs;
using SideLoader;
using UnityEngine;

namespace Crusader
{
    using SynchronizedWorldObjects;
    using System.Linq;
    using TinyHelper;

    public class KlausHintNPC1 : SynchronizedNPC
    {
        public static void Init()
        {
            var syncedNPC = new KlausHintNPC1(
                identifierName: "Rafael",
                rpcListenerID: IDs.NPCID_KlausHint1,
                defaultEquipment: new int[] { IDs.palladiumBootsID, IDs.palladiumArmorID, IDs.wolfSwordID },
                visualData: new SL_Character.VisualData() {
                    Gender = Character.Gender.Male,
                    SkinIndex = (int)SL_Character.Ethnicities.Asian,
                    HeadVariationIndex = 1,
                    HairStyleIndex = (int) HairStyles.BraidsBack,
                    HairColorIndex = (int) HairColors.Black
                }
            );

            syncedNPC.AddToScene(new SynchronizedNPCScene(
                scene: "Monsoon",
                //position: new Vector3(523.9f, -62.3f, 512.2f),
                //rotation: new Vector3(0, 21.5f, 0),
                position: new Vector3(76.6395f, - 4.9488f, 213.0735f),
                rotation: new Vector3(0, 91.9002f, 0)
            ));
        }

        public KlausHintNPC1(string identifierName, int rpcListenerID, int[] defaultEquipment = null, int[] moddedEquipment = null, Vector3? scale = null, Character.Factions? faction = null, SL_Character.VisualData visualData = null) : 
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

            var rootStatement = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "Hello! How can I help?");
            var characterIntroduction = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "I am Rafael, a Holy Mission Missionary and a city guard.");
            var locateTrainer = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "Of course! He just left town, and is likely at his usual spot, at the North-Eastern shore of the Huge Tree in the marsh.");
            var closeDialogue = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "Good bye!");

            var introMultipleChoice = TinyDialogueManager.MakeMultipleChoiceNode(graph, new string[] {
                "You could present yourself.",
                "I have heard great things about a guy called Klaus. Do you know where I can find him?",
                "No, I am fine. Thank you."
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
            graph.ConnectNodes(introMultipleChoice, closeDialogue, 2);
            graph.ConnectNodes(characterIntroduction, rootStatement);

            var obj = instanceGameObject.transform.parent.gameObject;
            obj.SetActive(true);

            return instanceCharacter;
        }
    }
}