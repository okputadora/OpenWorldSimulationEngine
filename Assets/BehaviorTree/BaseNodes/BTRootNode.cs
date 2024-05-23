using XNode;
using UnityEngine;

public class BTRootNode : BTCitizenNode
{
  [Input] public BTResult inResult;

  public override object GetValue(NodePort port)
  {
    return Evaluate();
  }

  public override BTResult OnEvaluate()
  {
    // Debug.Log("evaluating root node");
    return GetInputValue("inResult", BTResult.FAILURE);
  }
}
