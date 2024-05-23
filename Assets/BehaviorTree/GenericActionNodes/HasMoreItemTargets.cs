using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rename has more pickup targets
public class HasMoreItemTargets : BTCitizenNode
{
  public List<SharedItemData.ItemType> itemTypes = new List<SharedItemData.ItemType>();
  public override BTResult OnEvaluate()
  {
    if (context.citizen.HasMoreInteractTargets())
    {
      return BTResult.SUCCESS;
    }
    return BTResult.FAILURE;
  }
}
