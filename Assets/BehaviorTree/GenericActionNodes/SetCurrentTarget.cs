using UnityEngine;
public class SetCurrentTarget : BTCitizenNode
{
  [SerializeField] private GoToTarget.TargetType m_targetType;
  public override BTResult OnEvaluate()
  {
    bool didSet = context.citizen.SetCurrentTargetType(m_targetType);
    // check if target is null, if so return FAILURE
    return didSet ? BTResult.SUCCESS : BTResult.FAILURE;
  }
}