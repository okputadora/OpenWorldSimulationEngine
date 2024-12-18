using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasInteractTarget : BTCitizenNode
{
  public override BTResult OnEvaluate()
  {
    if (context.citizen.HasInteractTarget())
    {
      return BTResult.SUCCESS;
    }
    return BTResult.FAILURE;
  }
}
