using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTarget : BTCitizenNode
{
  public enum TargetType { PICKUP, DROPOFF, ATTACK, INTERACT, CRAFT, BUILDING, BUILDPIECE }
  [SerializeField] private TargetType m_targetType;
  private object m_target; // SaveableGameObject or GameObject
  private bool isTargetSet = false;

  protected override void OnEnter()
  {
    base.OnEnter();
    // Debug.Log("Entering GoToTarget");
    Debug.Log("ON ENTER: GoToTarget");
    context.citizen.SetCurrentTargetPosition(new Vector3(Random.Range(-12, 12), 0, Random.Range(-12, 12)));
  }
  public override BTResult OnEvaluate()
  {
    // if (m_target == null)
    // {
    //   // LOG ERROR or return success?
    // }
    // if target is still null return failure
    if (!context.citizen.IsTargetReached())
    {
      Debug.Log("BT RESULT = RUNNING");
      return BTResult.RUNNING;
    }
    Debug.Log("BT: reached target!");
    // m_target = null;
    // do we need to check if target was destroyed and return FAILED if so?
    Debug.Log("BT RESULT = SUCESS");
    return BTResult.SUCCESS;
  }

  protected override void OnExit()
  {
    base.OnExit();
    Debug.Log("ON EXIT: GoToTarget");
    // context.citizen.ClearCurrentTarget();
  }
}
