using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class VirtualPickable : VirtualSimulatable // Berry Bushes and Crops, etc.. should be simulatable to simulate regeneration...maybe
{
  public PickableData pickableData;
  public Action<VirtualPickable> onDepleted;

  // destructible data

  public VirtualPickable(SharedPickableData sharedPickableData) : base()
  {
    isStatic = true;
    pickableData = new PickableData(sharedPickableData);
  }

    public override void Simulate(float deltaTime)
    {
      if (pickableData.sharedPickableData.isRenawable && pickableData.dropsRemaining <= 0) {
        pickableData.timeSinceDepletion += deltaTime;
        if (pickableData.timeSinceDepletion >= pickableData.sharedPickableData.timeToRenew)
        {
          Replenish();
        }
      }
    }

    // do we need to return a bool, when would this be false
    public override InteractionResult Interact(Action<List<ItemData>> callback)
  {
    if (pickableData.dropsRemaining <= 0)
    {
      return InteractionResult.FAILURE;
    }
    StartInteraction(callback);
    return InteractionResult.PENDING;      
  }

  public async void StartInteraction(Action<List<ItemData>> onComplete)
  {
      await Task.Delay(pickableData.sharedPickableData.aiTimeToPick * 1000).ContinueWith(t => {});
      var drop = pickableData.sharedPickableData.CreateDrop();
      // check if onComplete, the interactor might have died before the interaction was completed
      onComplete(drop);
      pickableData.dropsRemaining--;
      if (pickableData.dropsRemaining <= 0)
      {
        if (pickableData.sharedPickableData.isRenawable)
        {
          pickableData.depletionTime = Time.time; // eventually need to change this to game time or time since start of game
          onDepleted?.Invoke(this);
        }
        else
        {
          // destroy pickable
          // we cant do this as part of the simulation, we need to wait to destroy anythign until this simulation tick is over.
          // instead we should register this object for destruction. This raises the question if ObjectSpawner and the simulation should 
          // be separate classes.
          Debug.Log("TRYING TO DESTROY PICKABLE IN SIMULATION LOOP"); 
          ObjectSpawner.instance.DestroyVirtualObject(this);
        }
      }
  }

  public void RegisterOnDepletedEvent(Action<VirtualPickable> onDepleted)
  {
    this.onDepleted += onDepleted;
  }

  public void UnRegisterOnDepletedEvent(Action<VirtualPickable> onDepleted)
    {
      this.onDepleted -= onDepleted;
    } 

  private void Replenish()
  {
    pickableData.dropsRemaining = pickableData.sharedPickableData.GetDropCount();
    pickableData.timeSinceDepletion = 0;
  } 
}