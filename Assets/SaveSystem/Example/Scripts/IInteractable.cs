using UnityEngine;
public interface IInteractable : IHoverable
{
  void Interact();
}

public enum InteractionResult {
  SUCCESS,
  FAILURE,
  PENDING
}