using System.Collections.Generic;
using UnityEngine;

public class WorkforceData : ISaveableData
{
  public string workforceName;
  public List<VirtualCitizen> citizens = new List<VirtualCitizen>();
  public HashSet<SharedItemData> itemTargets = new HashSet<SharedItemData>();
  public List<Vector2Int> zones;
  public int maxWorkers = 6;
  public bool isBlocked;
  public int priority = 10; // lower numbers = higher priority
  public SharedOccupationData sharedOccupationData;

  public WorkforceData(List<VirtualCitizen> citizens, string workforceName, List<Vector2Int> zones, HashSet<SharedItemData> targetResources = null)
  {
    this.workforceName = workforceName;
    this.citizens = citizens;
    this.itemTargets = targetResources;
    this.zones = zones;
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
  public virtual void Load(SaveData dataToSave)
  {
    throw new System.NotImplementedException();
  }

  public virtual void Save(SaveData dataToLoad)
  {
    throw new System.NotImplementedException();
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
