using XNode;
using UnityEngine;
using System;

public class BTRootNode : BTCitizenNode
{
  [Input] public BTResult inResult;

  public override object GetValue(NodePort port)
  {
    return Evaluate();
  }

  protected override void OnEnter()
  {
    base.OnEnter();
  }

  public override BTResult OnEvaluate()
  {
    // Debug.Log("evaluating root node");
    return GetInputValue("inResult", BTResult.FAILURE);
  }
}
