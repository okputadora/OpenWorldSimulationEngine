using UnityEngine;
using UnityEngine.AI;
/// <summary>
///  InteractLoop will keep interacting with the current target until the interaction target cannot be interacted with.
/// </summary>
public class InteractLoop : BTCitizenNode
{
  private bool isInteracting = false;

  protected override void OnEnter()
  {
    isInteracting = context.citizen.InteractWithCurrentTarget();
    Debug.Log("ENTER INTERACT LOOP: " + isInteracting); 
  }
  public override BTResult OnEvaluate()
  {
    // CURRENT ISSUE: When sequence star comes back to this after dropping off stuff in storage, theres no more interaciton happenig 
    // so we just return running forever
    Debug.Log("Interact Loop: OnEvaluate" + context.citizen.citizenData.currentTarget);
      // check if interaction is complete
      if (context.citizen.IsInteractionComplete())
      {
        isInteracting = context.citizen.InteractWithCurrentTarget();
        if (!isInteracting)
        {
            return BTResult.SUCCESS;
        }
      }
      return BTResult.RUNNING;
  }
}