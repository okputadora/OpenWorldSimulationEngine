using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugNode : BTCitizenNode
{
  public string message;
  public override BTResult OnEvaluate()
  {
    Debug.Log(context.citizen);
    return BTResult.SUCCESS;
  }
}
