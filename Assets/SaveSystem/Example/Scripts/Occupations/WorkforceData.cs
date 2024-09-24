using System.Collections.Generic;

public class WorkforceData : ISaveableData
{
  public List<CitizenData> citizens = new List<CitizenData>();
  public int maxWorkers;
  public bool isBlocked;

  public WorkforceData(List<CitizenData> citizens)
  {
    this.citizens = citizens;
  }
  // settlementId
  // civilizationId
  // maybe implement a schedule
  // requirements (tools, food, etc)

  public bool TryAddWorker(CitizenData citizen)
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
    List<CitizenData> citizens,
    List<SharedItemData> targetResources,
    bool needsToAttackToPickup,
    List<SharedDestructibleData> attackTargets
  ) : base(citizens)
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

public class FoodGatherWorkforceData : WorkforceData
{
  private bool isHuntingWorkforce;
  private List<SharedItemData> itemTargets;
  private List<SharedPickableData> pickableTargets;
  private List<SharedAnimalData> animalTargets;
  private List<VirtualPickable> pickables;
  private List<VirtualAnimal> animals;
  private List<VirtualItem> items;

  public FoodGatherWorkforceData(
    List<CitizenData> citizens,
    List<SharedItemData> itemtargets,
    List<SharedPickableData> pickableTargets
  ) : base(citizens)
  {
    this.pickableTargets = pickableTargets;
    // this.itemTargets = GetTargetsFromPickables
    isHuntingWorkforce = false;

  }

  public FoodGatherWorkforceData(List<CitizenData> citizens, List<VirtualAnimal> animals) : base(citizens)
  {
    this.animals = animals;
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
    int estimate = 0;
    // Dictionary<SharedItemData, int> targetResourcesPerDay = CalculateTargetResourcesPerDay();
    // foreach (KeyValuePair<SharedItemData, int> keyValue in targetResourcesPerDay)
    // {
    //   // estimate += keyValue.Key.calories * keyValue.Value;
    // }
    return estimate;
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
