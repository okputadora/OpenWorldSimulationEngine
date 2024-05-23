using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterNode : BTCitizenNode
{
  [Input] public BTResult inResult;
  public override BTResult OnEvaluate()
  {
    BTResult result = GetInputValue("inResult", BTResult.SUCCESS);
    if (result == BTResult.RUNNING)
    {
      // Debug.Log("inverter: RUNNING");
      return result;
    }
    result = result == BTResult.SUCCESS ? BTResult.FAILURE : BTResult.SUCCESS;
    // Debug.Log("inverter: " + result);
    return result;
  }
}
