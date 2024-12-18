using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTarget : BTCitizenNode
{
  public enum TargetType { PICKUP, PICKABLE, ANIMAL, DROPOFF, ATTACK, INTERACT, CRAFT, BUILDING, BUILDPIECE }
  [SerializeField] private TargetType m_targetType;

  protected override void OnEnter()
  {
    base.OnEnter();

    // Debug.Log("Entering GoToTarget");
    // Debug.Log("ON ENTER: GoToTarget");
    // Debug.Log("Target Type: " + m_targetType);  
    // context.citizen.SetCurrentTargetPosition(new Vector3(Random.Range(-30, 30), 0, Random.Range(-30, 30)));
    context.citizen.SetCurrentTargetType(m_targetType);
  }
  public override BTResult OnEvaluate()
  {
    // if (m_target == null)
    // {
    //   // LOG ERROR or return success?
    // }
    // if target is still null return failure
    // Debug.Log("Go to target " + m_targetType);
    if (!context.citizen.IsTargetReached())
    {
      // Debug.Log("RUNNING");
      return BTResult.RUNNING;
    }
      // Debug.Log("target reached SUCCESS");
    // Debug.Log("BT: reached target!");
    // m_target = null;
    // do we need to check if target was destroyed and return FAILED if so?
    // Debug.Log("BT RESULT = SUCESS");
    return BTResult.SUCCESS;
  }

  protected override void OnExit()
  {
    base.OnExit();
    // Debug.Log("ON EXIT: GoToTarget");
    // context.citizen.ClearCurrentTarget();
  }
}
