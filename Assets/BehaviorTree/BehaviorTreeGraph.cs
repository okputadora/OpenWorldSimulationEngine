using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
[System.Serializable]
public class BehaviorTreeGraph : NodeGraph
{
  public bool ignoreNodeDebug;
  public SubTreeNode GetOccupationNode() // should probably move this to CitizenBehaviorTreeGraph :
  {
    foreach (Node node in nodes)
    {
      BTCitizenNode btNode = (BTCitizenNode)node;
      if (btNode.GetType().FullName == "SubTreeNode")
      {
        SubTreeNode subTreeNode = (SubTreeNode)btNode;
        if (subTreeNode.subTreeType == SubTreeNode.SubTreeType.Occupation)
        {
          return subTreeNode;
        }
      }
    }
    return null;
  }

  public void SetOccupationNode(BehaviorTreeGraph occupationTree)
  {
    // Debug.Log("setting occupation node: " + (occupationTree != null).ToString());
    SubTreeNode occupationNode = GetOccupationNode();
    occupationNode.SetSubTree(occupationTree);
  }

  public override NodeGraph Copy()
  {

    BehaviorTreeGraph graph = (BehaviorTreeGraph)base.Copy();
    SubTreeNode occupationNode = graph.GetOccupationNode();
    // Debug.Log("copying occupationNode: " + occupationNode);
    if (occupationNode)
    {
      occupationNode.SetSubTreeToCopy();
    }
    // copy occupation sub tree
    return graph;

  }

  // @TODO could do it like this instead
  // public void InitializeNodesFromWorkforce(WorkforceData workforceData)
  // {
  //   foreach (Node node in nodes)
  //   {
  //     BTCitizenNode btNode = (BTCitizenNode)node;
  //     if (btNode.GetType() == typeof(HasIngredients))
  //     {

  //     }
  //   }
  // }
}