using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;

public class FoodGatherWorkforceData : WorkforceData
{
  public bool isHuntingWorkforce { get; private set; }
  // private HashSet<SharedItemData> itemTargets;
  public HashSet<SharedPickableData> pickableTargets { get; private set; }
  public HashSet<SharedAnimalData> animalTargets { get; private set; }
  public List<VirtualPickable> pickables { get; private set; }
  public List<VirtualAnimal> animals { get; private set; }
  public List<VirtualItem> items { get; private set; }
  public List<VirtualStorage> toolContainers { get; private set; }
  public List<VirtualStorage> foodContainers { get; private set; }

  public FoodGatherWorkforceData(
    string workforceName,
    List<VirtualCitizen> citizens,
    List<Vector2Int> zones,
    HashSet<SharedItemData> itemTargets,
    HashSet<SharedPickableData> pickableTargets,
    List<VirtualStorage> foodContainers,
    List<VirtualStorage> toolContainers
  ) : base(citizens, workforceName, zones, itemTargets)
  {
    this.pickableTargets = pickableTargets;
    // this.itemTargets = itemTargets;
    this.foodContainers = foodContainers;
    this.toolContainers = toolContainers;
    isHuntingWorkforce = false;
    pickables = ObjectSpawner.instance.GetObjectsInZones<VirtualPickable>(zones);
    // Debug.Log("created food gather workforce, pickables in zones: " + pickables.Count);
    // find packables in zones? which zones

  }

  public FoodGatherWorkforceData(
    string workforceName,
    List<Vector2Int> zones,
    List<VirtualCitizen> citizens,
    HashSet<SharedItemData> itemTargets,
    HashSet<SharedAnimalData> animalTargets,
    List<VirtualStorage> foodContainers,
    List<VirtualStorage> toolContainers
  ) : base(citizens, workforceName, zones, itemTargets)
  {
    this.animalTargets = animalTargets;
    this.itemTargets = itemTargets;
    this.foodContainers = foodContainers;
    this.toolContainers = toolContainers;
    isHuntingWorkforce = true;

    // find animals in zones? which zones
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
}