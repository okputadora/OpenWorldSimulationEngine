using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;
using System;
using System.Linq;
public class FoodGatherWorkforceData : WorkforceData
{
  public bool isHuntingWorkforce { get; private set; }
  // private HashSet<SharedItemData> itemTargets;
  public HashSet<SharedPickableData> pickableTargets { get; private set; }
  public HashSet<SharedAnimalData> animalTargets { get; private set; }
  public List<VirtualPickable> pickables { get; private set; }
  private List<VirtualPickable> assignedPickables = new List<VirtualPickable>(); // could make this a dict of pickables and number of citizens assigned 
  private HashSet<SharedItemData> pickableItems;
  public List<VirtualAnimal> animals { get; private set; }
  private List<VirtualAnimal> assignedAnimals = new List<VirtualAnimal>();
  public List<VirtualItem> items { get; private set; }
  public List<VirtualStorage> toolContainers { get; private set; }
  public List<VirtualStorage> foodContainers { get; private set; }

  public FoodGatherWorkforceData(SharedFoodGatherOccupationData sharedData, List<SharedPickableData> pickableItems, string workforceName, List<VirtualCitizen> citizens, List<Vector2Int> zones, List<VirtualStorage> foodContainers, List<VirtualStorage> toolContainers) : base(sharedData, workforceName, citizens, zones, foodContainers, toolContainers)
  {
    this.foodContainers = foodContainers;
    this.toolContainers = toolContainers;
    pickables = ObjectSpawner.instance.GetObjectsInZones<VirtualPickable>(zones);
     foreach (VirtualPickable virtualPickable in pickables)
    {
      if (virtualPickable.pickableData.sharedPickableData.isRenawable)
      {
        this.pickableItems = virtualPickable.pickableData.sharedPickableData.PreviewDrop();
        virtualPickable.RegisterOnDepletedEvent(OnPickableDepleted);
      }
    }
    animals = ObjectSpawner.instance.GetObjectsInZones<VirtualAnimal>(zones);
  }

  private void OnPickableDepleted(VirtualPickable virtualPickable) {
    virtualPickable.UnRegisterOnDepletedEvent(OnPickableDepleted);
    assignedPickables.Remove(virtualPickable);
    pickables.Add(virtualPickable);
  }

  public override void Simulate(float deltaTime)
  {
    // remove some pickables and animals from the available ones
    // add their items to the assigned storage bins
  }
  //@TODO UI Could interact with this (or the SharedData version) and call a filter method to get items allowed to be added in ui (in this case restricted to food)
  public int GetEstimatedCaloriesPerDay()
  {
    int estimate = 3000 * citizens.Count;
    // Dictionary<SharedItemData, int> targetResourcesPerDay = CalculateTargetResourcesPerDay();
    // foreach (KeyValuePair<SharedItemData, int> keyValue in targetResourcesPerDay)
    // {
    //   // estimate += keyValue.Key.calories * keyValue.Value;
    // }
    return estimate;
  }

  public override bool TryAddWorker(VirtualCitizen citizen)
  {
    bool didAddWorker = base.TryAddWorker(citizen);
    if (didAddWorker)
    {

    }
    return didAddWorker;
  }

  public override bool HasMoreTargets(GoToTarget.TargetType targetType)
  {
    if (targetType == GoToTarget.TargetType.PICKABLE)
    {
      return pickables.Count > 0;
    }
    else if (targetType == GoToTarget.TargetType.ANIMAL)
    {
      return animals.Count > 0;
    }
    // throw error invalid type?
    return false;
  }

  public override bool AssignTargetToCitizen(VirtualCitizen citizen, GoToTarget.TargetType targetType, out VirtualGameObject target)
  {
    target = null;
    Debug.Log("assigning new target");
    if (citizen.citizenData.currentTarget != null)
    {
      Debug.Log("current target: " + citizen.citizenData.currentTarget);
      if (citizen.citizenData.currentTarget is VirtualPickable)
      {
        Debug.Log("current target is pickable, removing it");
        bool didRemove = assignedPickables.Remove(citizen.citizenData.currentTarget as VirtualPickable);
        Debug.Log("Did remove: " + didRemove);
        pickables.Add(citizen.citizenData.currentTarget as VirtualPickable);
      }
      else if (citizen.citizenData.currentTarget is VirtualAnimal)
      {
        assignedAnimals.Remove(citizen.citizenData.currentTarget as VirtualAnimal);
        animals.Add(citizen.citizenData.currentTarget as VirtualAnimal);
      }
    }
    if (targetType == GoToTarget.TargetType.PICKABLE)
    {
      VirtualPickable nearestPickable = FindNearestTarget<VirtualPickable>(citizen.worldPosition);
      if (nearestPickable != null)
      {
        target = nearestPickable;
        pickables.Remove(nearestPickable);
        assignedPickables.Add(nearestPickable);
        return true;
      }
      return false;
    }
    else if (targetType == GoToTarget.TargetType.ANIMAL)
    {
      if (animals.Count > 0)
      {
        VirtualAnimal animal = animals[0];
        animals.RemoveAt(0);
        target = animal;
        return true;
      }
    } else if (targetType == GoToTarget.TargetType.DROPOFF) {
      if (foodContainers.Count > 0) {
        // could find nearest food container
        VirtualStorage foodContainer = foodContainers[0];
        // foodContainers.RemoveAt(0);
        target = foodContainer;
        return true;
      }
    }
    return false;
  }

  private T FindNearestTarget<T>(Vector3 sourcePosition) {
    if (typeof(T) == typeof(VirtualPickable)) {
      VirtualPickable nearestPickable = null;
      foreach(VirtualPickable pickable in pickables) {
        if (nearestPickable == null) {
          nearestPickable = pickable;
        } else {
          if (Vector3.Distance(sourcePosition, pickable.worldPosition) < Vector3.Distance(sourcePosition, nearestPickable.worldPosition) && pickable.pickableData.dropsRemaining > 0) {
            nearestPickable = pickable;
          }
        }
      }
      return (T)(object)nearestPickable;
      // find nearest pickable
    } 
    // else if (T is VirtualAnimal) {
    //   // find nearest animal
    // } else if (T is VirtualItem) {
    //   // find nearest item
    // } else if (T is VirtualStorage) {
    //   // find nearest storage
    // }
    return (T)(object)null;
  }

  
  public override InteractionResult InteractWithCurrentTarget(VirtualCitizen citizen, VirtualGameObject target, GoToTarget.TargetType targetType, Action<List<ItemData>> onComplete)
  {
    // return itemTargets[0];
    if (targetType == GoToTarget.TargetType.PICKABLE)
    {
      VirtualPickable pickable = target as VirtualPickable;
      if (pickable != null)
      {
        return pickable.Interact(onComplete);
      }
    }
    else if (targetType == GoToTarget.TargetType.ANIMAL)
    {
      VirtualAnimal animal = target as VirtualAnimal;
      if (animal != null)
      {
        return animal.Interact(onComplete);
      }
    }
    else if (targetType == GoToTarget.TargetType.DROPOFF)
    {
      VirtualStorage storage = target as VirtualStorage;
      if (storage != null)
      {
        InventoryData.TransferItemsOfType(citizen.citizenData.inventory, storage.inventoryData, pickableItems);
        // onComplete.Invoke(null); // BUG: this needs to happen after the return, threading is not the way top go here
        return InteractionResult.SUCCESS;
      }
    }
    return InteractionResult.FAILURE;
  }
  
  public override void DrawWorkforce()
  {
    foreach (VirtualPickable pickable in pickables)
    {
      Gizmos.color = new Color(1, 0.5f, 0);
      if (pickable.pickableData.dropsRemaining <= 0) {
        Gizmos.color = new Color(0.5f, 0.5f, 0.5f);
      }
      Gizmos.DrawSphere(ZoneSystem.instance.WorldToGamePosition(pickable.worldPosition), 1);

    }
    foreach (VirtualPickable pickable in assignedPickables)
    {
      Gizmos.color = new Color(0, 1, 0);
      Gizmos.DrawSphere(ZoneSystem.instance.WorldToGamePosition(pickable.worldPosition), 1);
    }

    foreach (VirtualStorage storage in foodContainers)
    {
      Gizmos.color = new Color(0, 0, 1);
      Gizmos.DrawSphere(ZoneSystem.instance.WorldToGamePosition(storage.worldPosition), 1);
    }
  }
}