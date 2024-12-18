using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInventoryFull : BTCitizenNode
{
  public override BTResult OnEvaluate()
  {
    if (!context.citizen.IsInteractionComplete()) {
      return BTResult.FAILURE;
    }
    if (context.citizen.IsInventoryFull())
    {
      return BTResult.SUCCESS;
    }
    return BTResult.FAILURE;
  }
}
