using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTargetSetAction : BTCitizenNode
{
  public override BTResult OnEvaluate()
  {
    bool isTargetSet = context.citizen.IsTargetSet();
    Debug.Log("isTargetSetAction: " + isTargetSet);
    if (isTargetSet)
    {
      return BTResult.SUCCESS;
    }
    return BTResult.FAILURE;
  }
}
