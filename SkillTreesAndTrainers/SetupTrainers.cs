using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Crusader
{
    public class SetupTrainers
    {
        public static void SetupRufusInteraction()
        {
            if (GameObject.Find(/*"_SNPC/_UNPC/UNPC_RufusA/"+ */"Dialogue_Rufus/NPC/DialogueTree_1_Initial") is GameObject rufusDialogueTree)
            {
                if (rufusDialogueTree.GetComponent<NodeCanvas.DialogueTrees.DialogueTreeController>() is NodeCanvas.DialogueTrees.DialogueTreeController dialogueTreeController)
                {
                    Graph graph = dialogueTreeController.graph;
                    List<Node> nodes = graph.allNodes;

                    if (nodes.FirstOrDefault(n => (n != null) && (n is StatementNodeExt) && ((StatementNodeExt)n).statement.text.Contains("Elatt however... I’ve had the honor to speak with him. He is very real. Having a god that was once man is comforting.")) is StatementNodeExt finalElattMention)
                    {

                        ActionNode actionNode = graph.AddNode<ActionNode>();
                        var reward = new GiveReward();
                        actionNode.action = reward;
                        reward.RewardReceiver = GiveReward.Receiver.Instigator;


                        var skillReward = new NodeCanvas.Tasks.Actions.ItemQuantity();
                        var skillReference = new ItemReference();
                        skillReference.ItemID = Crusader.Instance.meditationInstance.ItemID;
                        skillReward.Item = new BBParameter<ItemReference>(skillReference);
                        skillReward.Quantity = 1;
                        reward.ItemReward = new List<NodeCanvas.Tasks.Actions.ItemQuantity>() { skillReward };

                        //graph.ConnectNodes(finalElattMention, actionNode, 0);
                        finalElattMention.outConnections[0] = DTConnection.Create(finalElattMention, actionNode); //actionNode;

                        FinishNode exit = graph.AddNode<FinishNode>();
                        nodes.Add(exit);
                        graph.ConnectNodes(actionNode, exit);
                    }
                }
            }
        }
        //public static void SetupAltarInteraction(ref Trainer altarTrainer, ref SkillSchool skillTreeInstance)
        //{
        //    GameObject altarDialogueFull = GameObject.Find("DialogueAltar/NPC/InteractionActivatorSettings");
        //    if (altarDialogueFull != null)
        //    {
        //        NPCInteraction npcInteraction = altarDialogueFull.GetComponentInChildren<NPCInteraction>();
        //        if (npcInteraction != null)
        //        {
        //            if (npcInteraction.ActorLocKey == "name_unpc_altar_01")
        //            {
        //                DialogueTreeController dialogueTreeController = npcInteraction.NPCDialogue.DialogueController;
        //                //DialogueTreeExt dialogueTreeExt = npcInteraction.NPCDialogue.DialogueTree;
        //                Graph graph = dialogueTreeController.graph;

        //                List<Node> nodes = graph.allNodes;

        //                MultipleChoiceNodeExt multipleChoiceNodeExt = nodes.FirstOrDefault(n => n != null && n is MultipleChoiceNodeExt && (((MultipleChoiceNodeExt)n).availableChoices.Where(m => m.statement.text == "(Offer a prayer to Elatt)").ToList().Count > 0)) as MultipleChoiceNodeExt;
        //                if (multipleChoiceNodeExt == null) return;

        //                string oathString = "(Swear an oath.)";

        //                var choice = new MultipleChoiceNodeExt.Choice();

        //                choice.statement = new Statement();
        //                choice.statement.text = oathString;
        //                choice.isUnfolded = true;

        //                multipleChoiceNodeExt.availableChoices.Insert(0, choice);

        //                ActionNode actionNode = graph.AddNode<ActionNode>();
        //                nodes.Add(actionNode);

        //                //if (altarTrainer == null)
        //                //{
        //                altarTrainer = new Trainer();
        //                At.SetValue<UID>(UID.Generate(), typeof(Trainer), altarTrainer, "m_uid");
        //                At.SetValue<UID>(skillTreeInstance.UID, typeof(Trainer), altarTrainer, "m_skillTreeUID");
        //                //}

        //                actionNode.action = new TrainDialogueAction();
        //                ((TrainDialogueAction)actionNode.action).Trainer = new BBParameter<Trainer>(altarTrainer);
        //                var playerBP = new BBParameter<Character>();
        //                playerBP.name = "gInstigator";

        //                ((TrainDialogueAction)actionNode.action).PlayerCharacter = playerBP;
        //                graph.ConnectNodes(multipleChoiceNodeExt, actionNode, 0);


        //                FinishNode exit = graph.AddNode<FinishNode>();
        //                nodes.Add(exit);
        //                graph.ConnectNodes(actionNode, exit);
        //            }
        //        }
        //    }
        //}

    }
}
