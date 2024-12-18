using UnityEngine;
public class Interact : BTCitizenNode
{
  private bool didInteract = false;

  protected override void OnEnter()
  {
    didInteract = context.citizen.InteractWithCurrentTarget();
  }
  public override BTResult OnEvaluate()
  {
    if (didInteract)
    {
      // check if interaction is complete
      if (context.citizen.IsInteractionComplete())
      {
        return BTResult.SUCCESS;
      }
      return BTResult.RUNNING;
    }
    return BTResult.SUCCESS;
  }
}