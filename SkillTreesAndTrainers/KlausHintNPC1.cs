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

            var root                                    = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "Hail civillian. I implore you for aid, as a guard it is my duty to stay in Monsoon. Can you search for my old master. I've beseeched holy Elatt and in my request he showed me a vision of a great tree on a peninsula.");
            var rootReply0                              = TinyDialogueManager.MakeMultipleChoiceNode(graph, new string[] {"What is so important  about your teacher?",});
            var rootReply0Answer0                       = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "He was my teacher, nothing too special, though he was one of the few who beleived in me when I was young, and kept in touch until recently and now I worry for him. The marsh is no safe place after all.");
            var rootReply0Answer0Reply0                 = TinyDialogueManager.MakeMultipleChoiceNode(graph, new string[] {
                "I will do what i can.",
                "I am not suited for this. Take care.",
            });

            var locateTrainer = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "Thank you! He just left town, and is likely at his usual spot, at the North-Eastern shore of the Huge Tree in the marsh.");
            var closeDialogue = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "Good bye!");

            graph.allNodes.Clear();
            graph.allNodes.Add(root);
            graph.allNodes.Add(rootReply0);
            graph.allNodes.Add(rootReply0Answer0);
            graph.allNodes.Add(rootReply0Answer0Reply0);
            graph.allNodes.Add(locateTrainer);
            graph.allNodes.Add(closeDialogue);

            graph.primeNode = root;
            graph.ConnectNodes(root, rootReply0);
            graph.ConnectNodes(rootReply0, rootReply0Answer0, 0);
            graph.ConnectNodes(rootReply0Answer0, rootReply0Answer0Reply0);
            graph.ConnectNodes(rootReply0Answer0Reply0, locateTrainer, 0);
            graph.ConnectNodes(rootReply0Answer0Reply0, closeDialogue, 1);

            var obj = instanceGameObject.transform.parent.gameObject;
            obj.SetActive(true);

            return instanceCharacter;
        }
    }
}