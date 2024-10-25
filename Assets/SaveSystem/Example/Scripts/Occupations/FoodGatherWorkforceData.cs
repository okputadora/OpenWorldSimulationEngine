using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;

public class FoodGatherWorkforceData : WorkforceData
{
  private bool isHuntingWorkforce;
  // private HashSet<SharedItemData> itemTargets;
  private HashSet<SharedPickableData> pickableTargets;
  private HashSet<SharedAnimalData> animalTargets;
  private List<VirtualPickable> pickables;
  private List<VirtualAnimal> animals;
  private List<VirtualItem> items;
  private List<VirtualStorage> toolContainers;
  private List<VirtualStorage> foodContainers;

  public FoodGatherWorkforceData(
    List<VirtualCitizen> citizens,
    HashSet<SharedItemData> itemTargets,
    HashSet<SharedPickableData> pickableTargets,
    List<VirtualStorage> foodContainers,
    List<VirtualStorage> toolContainers
  ) : base(citizens, itemTargets)
  {
    this.pickableTargets = pickableTargets;
    // this.itemTargets = itemTargets;
    this.foodContainers = foodContainers;
    this.toolContainers = toolContainers;
    isHuntingWorkforce = false;

  }

  public FoodGatherWorkforceData(
    List<VirtualCitizen> citizens,
    HashSet<SharedItemData> itemTargets,
    HashSet<SharedAnimalData> animalTargets,
    List<VirtualStorage> foodContainers,
    List<VirtualStorage> toolContainers
  ) : base(citizens, itemTargets)
  {
    this.animalTargets = animalTargets;
    this.itemTargets = itemTargets;
    this.foodContainers = foodContainers;
    this.toolContainers = toolContainers;
    isHuntingWorkforce = true;
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