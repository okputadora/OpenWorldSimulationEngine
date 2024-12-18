using System.Collections.Generic;
using UnityEngine;
using System;
public class WorkforceData : ISaveableData
{
  public Guid id;
  public string workforceName;
  public List<VirtualCitizen> citizens = new List<VirtualCitizen>();
  public HashSet<SharedItemData> itemTargets = new HashSet<SharedItemData>();
  public List<Vector2Int> zones;
  public int maxWorkers = 6;
  public bool isBlocked;
  public int priority = 10; // lower numbers = higher priority
  public SharedOccupationData sharedOccupationData;
  public WorkforceData(SharedOccupationData sharedData, string workforceName, List<VirtualCitizen> citizens, List<Vector2Int> zones, List<VirtualStorage> dropoff, List<VirtualStorage> pickup)
  {
    id = Guid.NewGuid();
    this.workforceName = workforceName;
    this.citizens = citizens;
    itemTargets = new HashSet<SharedItemData>(sharedData.itemTargets);
    sharedOccupationData = sharedData;
    this.zones = zones;
    foreach (VirtualCitizen citizen in citizens)
    {
      citizen.AssignWorkforce(this);
    }

    // Debug.Log("WorkforceData constructor");
    // Debug.Log("itemTargets: " + itemTargets);
  }
  public WorkforceData(List<VirtualCitizen> citizens, string workforceName, List<Vector2Int> zones, SharedOccupationData sharedData)
  {
    id = Guid.NewGuid();
    this.workforceName = workforceName;
    this.citizens = citizens;
    itemTargets = new HashSet<SharedItemData>(sharedData.itemTargets);
    this.zones = zones;
    foreach (VirtualCitizen citizen in citizens)
    {
      citizen.AssignWorkforce(this);
    }

    // Debug.Log("WorkforceData constructor");
    // Debug.Log("itemTargets: " + itemTargets);
  }

  public WorkforceData(List<VirtualCitizen> citizens, string workforceName, List<Vector2Int> zones, HashSet<SharedItemData> targetResources = null)
  {
    id = Guid.NewGuid();
    this.workforceName = workforceName;
    this.citizens = citizens;
    itemTargets = targetResources;
    this.zones = zones;
    foreach (VirtualCitizen citizen in citizens)
    {
      citizen.AssignWorkforce(this);
    }

    // Debug.Log("WorkforceData constructor");
    // Debug.Log("itemTargets: " + itemTargets);
  }
  // settlementId
  // civilizationId
  // maybe implement a schedule
  // requirements (tools, food, etc)

  public virtual bool TryAddWorker(VirtualCitizen citizen)
  {
    if (citizens.Count < maxWorkers)
    {
      citizens.Add(citizen);
      return true;
    }
    return false;
  }

  public virtual void Simulate(float deltaTime)
  {

  }

  public virtual bool HasMoreTargets(GoToTarget.TargetType targetType)
  {
    return itemTargets.Count > 0;
  }

  // consider making these abstract
  public virtual bool AssignTargetToCitizen(VirtualCitizen citizen, GoToTarget.TargetType targetType, out VirtualGameObject target)
  {
    // return itemTargets[0];
    target = null;
    return false;
  }

  public virtual InteractionResult InteractWithCurrentTarget(VirtualCitizen citizen, VirtualGameObject target, GoToTarget.TargetType targetType, Action<List<ItemData>> onComplete)
  {
    // return itemTargets[0];
    return InteractionResult.FAILURE;
  }
  public virtual void Load(SaveData dataToSave)
  {
    throw new System.NotImplementedException();
  }

  public virtual void Save(SaveData dataToLoad)
  {
    throw new System.NotImplementedException();
  }

  public virtual void DrawWorkforce()
  {
    // Debug.Log("draw workforce");
  }
}

// Tree felling, mining, gathering non food resources, 
public class GatherWorkforceData : WorkforceData
{
  // targetResources (wood, stone, ore, hides, meat, etc)
  public List<SharedItemData> targetResources = new List<SharedItemData>();
  public List<SharedDestructibleData> attackTargets; // change to SharedDestructibleData
  public bool needsToAttackToPickup = false;

  // Actual targets
  private List<VirtualItem> itemsTargetsInRange = new List<VirtualItem>();
  private List<IDestructible> attackTargetsInRange = new List<IDestructible>();
  // attackToGather = true/false 
  // acctackTargets (trees, rocks, ore deposits, animals, etc)
  // settlementId
  // \
  public GatherWorkforceData(
    string workforceName,
    List<Vector2Int> zones,
    List<VirtualCitizen> citizens,
    List<SharedItemData> targetResources,
    bool needsToAttackToPickup,
    List<SharedDestructibleData> attackTargets
  ) : base(citizens, workforceName, zones)
  {
    this.targetResources = targetResources;
    this.needsToAttackToPickup = needsToAttackToPickup;
    this.attackTargets = attackTargets;

    // calculate itemTargetsInRange and conditionally attackTargetsInRange
  }

  public override void Save(SaveData dataToSave)
  {
    base.Save(dataToSave);
  }
  public override void Load(SaveData dataToLoad)
  {
    base.Load(dataToLoad);
  }

  public Dictionary<SharedItemData, int> CalculateTargetResourcesPerDay()
  {
    Dictionary<SharedItemData, int> targetResourcesPerDay = new Dictionary<SharedItemData, int>();
    foreach (SharedItemData item in targetResources)
    {
      // filter itemTargetsInRange based on item,
      // calculate distance of item from workplace center and 
      // compute formula based on distance, citizen carry capacity, item weight, number of citizens etc to determine itemsPerDay
      // add key value pair to dict
    }
    return targetResourcesPerDay;
  }

  // Ensure this is stripped at build time
  // public virtual void DrawWorkforce()
  // {
  //   Debug.Log("draw workforce");
  // }
}



// public class FoodPrepWorkforce : WorkforceData
// {

// }

// public class CraftWorkforceData : WorkforceData
// {
//   // Workbench (carpenter bench, cooking station)
//   // recipes
//   // fromInventories
//   // toInventories
//   public override void Save(SaveData dataToSave)
//   {
//     base.Save(dataToSave);
//   }
//   public override void Load(SaveData dataToLoad)
//   {
//     base.Load(dataToLoad);
//   }
// }
// public class TradeWorkforceData : WorkforceData
// {
//   // originSettlement = settlementId;
//   // destinationSettlement = settlementId;
//   // goodsToDeliver = ItemData goods to take from origin to destination
//   // goodsToReturn = ItemData  goods to take from destination to origin
//   // currentGoods current goods in the traders possesion
//   public override void Save(SaveData dataToSave)
//   {
//     base.Save(dataToSave);
//   }
//   public override void Load(SaveData dataToLoad)
//   {
//     base.Load(dataToLoad);
//   }
// }

// public class FarmWorkforceData : WorkforceData
// {

// }
