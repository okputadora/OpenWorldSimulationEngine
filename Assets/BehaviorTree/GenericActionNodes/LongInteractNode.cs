using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongInteractNode : BTCitizenNode
{
  [SerializeField] private float m_interactionTime = 3f;
  private float m_startTime = 0f;
  private bool m_isInteracting;


  protected override void OnEnter()
  {
    m_isInteracting = context.citizen.InteractWithCurrentTarget();
    m_startTime = Time.time;

  }
  public override BTResult OnEvaluate()
  {
    if (m_isInteracting)
    {
      if (Time.time < (m_startTime + m_interactionTime))
      {
        // @TODO should update progress UI
        return BTResult.RUNNING;
      }
      return BTResult.SUCCESS;
    }
    return BTResult.FAILURE;
  }
}
