using XNode;
using UnityEngine;
public class SubTreeNode : BTCitizenNode
{
  [SerializeField] private BehaviorTreeGraph m_subTree;
  public enum SubTreeType
  {
    Occupation
  }
  [SerializeField] private SubTreeType m_subTreeType;
  public SubTreeType subTreeType { get { return m_subTreeType; } }
  public override BTResult OnEvaluate()
  {
    // return BTResult.SUCCESS;
    if (m_subTree == null)
    {
      return BTResult.FAILURE;
    }
    BTCitizenNode rootNode = m_subTree.nodes[0] as BTCitizenNode;
    rootNode.context = context;
    // Debug.Log("EVALUATING TREE: " + tickCount);
    // Debug.Log("calling evaluate on subtree root node");
    // BTResult result = rootNode.Evaluate();
    BTResult result = (BTResult)rootNode.GetValue(rootNode.GetPort("inResult"));
    // Debug.Log("sub tree result: " + result);
    return result;
  }

  public void SetSubTree(BehaviorTreeGraph subTree)
  {
    if (m_subTree != null) throw new System.Exception("Trying to set subTree of SubTreeNode but it's already set");
    m_subTree = (BehaviorTreeGraph)subTree.Copy();
  }
}