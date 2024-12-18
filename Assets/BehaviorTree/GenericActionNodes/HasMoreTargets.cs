using UnityEngine;

public class HasMoreTargets : BTCitizenNode
{
  public GoToTarget.TargetType targetType;
  public override BTResult OnEvaluate()
  {
    Debug.Log("HasMoreTargets: " + targetType); 
    if (context.citizen.HasMoreInteractTargets(targetType))
    {
      return BTResult.SUCCESS;
    }
    return BTResult.FAILURE;
  }
}
