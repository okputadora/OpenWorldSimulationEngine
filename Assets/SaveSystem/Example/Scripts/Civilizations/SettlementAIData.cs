using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SettlementAIData : SettlementData
{

  public HashSet<EntityAndAmount<SharedItemData>> materialNeeds = new HashSet<EntityAndAmount<SharedItemData>>();
  public List<EntityAndAmount<BuildPieceRecipe>> buildPieceNeeds;
  public List<EntityAndAmount<SharedBuildingData>> buildingNeeds;
  public List<BuildingData> buildings;

  public SettlementAIData(Vector3 worldPosition, int initialCitizenCount, CivilizationData civilization, string settlementName) : base(worldPosition, initialCitizenCount, civilization, settlementName)
  {
    isPlayerSettlement = false;
    // create default build pieces
    // 1 camp fire
    // Could create a method in the object spawner for creating the initial starting build pieces for the settlements
    SharedCraftingStationData woodworkingBench = ObjectSpawner.instance.objectDB.GetCraftingStationByID("woodworkingBench");
    SharedStorageData woodChest = ObjectSpawner.instance.objectDB.GetStorageContainerByID("woodChest");
    VirtualCraftingStation vcs = ObjectSpawner.instance.CreateNew(woodworkingBench.prefab, worldPosition, Quaternion.identity, Vector3.one) as VirtualCraftingStation;
    VirtualStorage vs1 = ObjectSpawner.instance.CreateNew(woodChest.prefab, worldPosition + Vector3.one * 2, Quaternion.identity, Vector3.one) as VirtualStorage;
    VirtualStorage vs2 = ObjectSpawner.instance.CreateNew(woodChest.prefab, worldPosition + Vector3.one, Quaternion.identity, Vector3.one) as VirtualStorage;
    inactiveCraftingStations.Add(vcs);
    inactiveStorage.Add(vs1);
    inactiveStorage.Add(vs2);
    // ObjectSpawner.instance.CreateBuildPiece("campfire", worldPosition);
    // BuildPieceRecipe woodChestRecipe = ObjectSpawner.instance.objectDB.GetBuildPieceRecipeByID("woodChest");
    // VirtualBuildPiece virtualWoodChest = woodChestRecipe.CreateVirtualBuildPieceInstance();
    // 4 wooden chests
    // 1 workbench


  }

  public override void Simulate(float deltaTime)
  {
    base.Simulate(deltaTime); // simulates workers and citizens for AI and player settlements
    CheckNeeds();
    // MakeImprovements();
    // Simulat occupations? or will TheSimulation do that
    // Make AI settlement decisions
  }
  private void CheckNeeds()
  {
    // need a way to alter the order of calling these functions,
    // the order will depend on the priorites of the civilization. 
    // all will have food, housing, and fuel first and then the other will be civilization specific
    CheckFoodNeeds();
    CheckHousingNeeds();
    CheckFuelNeeds();
    CheckMaterialNeeds();
    // CheckIndustryNeeds();
    // CheckTradeNeeds();
    // CheckDefensiveNeeds();
    // CheckExpansionNeeds();
    // CheckReligiousNeeds();
    // Check

  }

  private void CheckFoodNeeds()
  {
    int cusion = 0; // base this off of caution and any other traits

    int surplus = caloriesProducedPerDay + caloriesImportedPerDay + cusion - (caloriesConsumedPerDay + caloriesExportedPerDay);
    if (surplus < 0)
    {
      int deficit = Mathf.Abs(surplus);
      if (deficit <= caloriesExportedPerDay)
      {
        UpdateConsumableExports(caloriesExportedPerDay - deficit);
      }
      else
      {
        UpdateConsumableExports(0);
        // increase food production or trade
        IncreaseFoodSupply(deficit);
      }
      return;
    }
    if (surplus > cusion) // @TODO base -50 off of caution trait of the ruler / civilization
    {
      // either remove people from food production
      // or stop importing
    }
  }

  private void CheckHousingNeeds()
  {
    if (bedCount < citizens.Count)
    {
      IncreaseHousingSupply(citizens.Count - bedCount);
    }

  }

  private void CheckFuelNeeds()
  {

  }

  private void CheckMaterialNeeds()
  {
    HashSet<SharedItemData> uniqueItemTargets = new HashSet<SharedItemData>();

    foreach (WorkforceData workforce in workforces)
    {
      if (workforce.GetType() == typeof(GatherWorkforceData)) // exclude food gather workforces
      {
        foreach (SharedItemData item in workforce.itemTargets)
        {
          uniqueItemTargets.Add(item);
        }
      }
      // else if (workforce.GetType() == typeof(CraftingWorkforceData))
      // {
      //   foreach (SharedItemData item in workforce.itemTargets)
      //   {
      //     uniqueItemTargets.Add(item);
      //   }
      // }
      else if (workforce.GetType() == typeof(ConstructionWorkforceData))
      {
        foreach (SharedItemData item in workforce.itemTargets)
        {
          uniqueItemTargets.Add(item);
        }
      }
      foreach (SharedItemData item in workforce.itemTargets)
      {
        uniqueItemTargets.Add(item);
      }
    }
    HashSet<EntityAndAmount<SharedItemData>> newMaterialNeeds = new HashSet<EntityAndAmount<SharedItemData>>();
    foreach (var material in materialNeeds.ToList())
    {
      if (uniqueItemTargets.Contains(material.item))
      {
        newMaterialNeeds.Remove(material);
      }
    }
    materialNeeds = newMaterialNeeds;
    foreach (EntityAndAmount<SharedItemData> material in materialNeeds)
    {
      // create a workforce to get this material
      CreateMaterialWorkforce(material);
    }

  }

  private void CreateMaterialWorkforce(EntityAndAmount<SharedItemData> material)
  {
    // could make a dictionary of items to workforces with the key as item.id
    // or we could be more descriptive with the item types 
    // gatherableMaterial, craftableMaterial, etc.
    if (material.item.itemType == SharedItemData.ItemType.Material)
    {
      // create a workforce to get this material
      // determine number of citizens from material.amount?
      CreateGatherWorkforce(material.amount, new List<SharedItemData> { material.item });
    }
    else if (material.item.itemType == SharedItemData.ItemType.CraftedMaterial)
    {
      // create a workforce to craft this material
    }
    // determine number
    // create a workforce to build this building
    // determine number
  }

  private void UpdateConsumableExports(int caloriesPerDay)
  {
    //calculate caloriesPerDay per item of food being exported
  }

  private void IncreaseFoodSupply(int calorieDeficitPerDay)
  {
    // Determine how we will increase supply, trade or in-house
    // do we have available workers 
    int tradeThreshold = 0;
    // if (civilization.strategy.trade > tradeThreshold) // @TODO add method to civilization to get trade favorability
    // {
    //   if (tradeRoutes.Count > 0)
    //   {
    //     // do we have a trade route with food, can it be increased
    //     // do we have a trade route with a settlement that has surplus food
    //   }
    //   else
    //   {
    //     // establish a new trade route
    //   }
    // }
    if (idleCitizens.Count > 0)
    {
      FoodGatherWorkforceData foodProductionWorkforce = GetFoodProductionWorkforce();

      if (foodProductionWorkforce != null)
      {
        // add IdleCitizen to workforce
        bool didAddWorker = foodProductionWorkforce.TryAddWorker(idleCitizens[0]);
        if (didAddWorker)
        {
          employedCitizens.Add(idleCitizens[0]);
          idleCitizens.RemoveAt(0);
          RecalculateFoodProduction();
        }
      }
      else
      {
        // create FoodProductionWorkforce
        // send int numberOfWorkers based off of calroieDeficit
        int workerCount = calorieDeficitPerDay / caloriesCreatedPerPersonPerDay;
        FoodGatherWorkforceData createdFoodWorkforce = CreateFoodGatherWorkforce(workerCount);
        if (createdFoodWorkforce == null)
        {
          // try other methods of acquiring food
          // register no food state of emergency
          return;
        }

      }
    }
    else
    {
      // pull citizens from other workforces to support food production
      // do we have established trade routes
      // do we have workers doing non essential things
      // can we setup trade routes
    }
    // Debug.Log($"Idle citizens remaning: {idleCitizens.Count}");
    // Debug.Log($"Employed citizens: {employedCitizens.Count}");
    // Debug.Log($"workforce count: " + workforces.Count);

  }

  private void ImproveFoodSupply()
  {

  }

  private FoodGatherWorkforceData GetFoodProductionWorkforce()
  {
    foreach (WorkforceData workforce in workforces)
    {
      if (workforce.GetType() == typeof(FoodGatherWorkforceData))
      {
        return workforce as FoodGatherWorkforceData;
      }
    }
    return null;
  }

  private FoodGatherWorkforceData CreateFoodGatherWorkforce(int workerCount, bool preferHunting = false)
  {
    // maybe move all this to FoodGatherWorkforce
    // Determine if we have the storage (minimum 2 wood chests)
    List<VirtualStorage> availableStorage = GetAvailableStorage(2);
    if (availableStorage == null)
    {
      // add storage to build queue
      // buil
      return null;
    }
    // Determine available food resources
    FoodGatherWorkforceData createdWorkforce = null;
    List<VirtualPickable> pickablesInZones = new List<VirtualPickable>();
    List<VirtualAnimal> animalsInZones = new List<VirtualAnimal>();
    foreach (Vector2Int zoneID in zones) // we might want to limit them to zones 
    {
      List<VirtualPickable> pickables = ObjectSpawner.instance.GetObjectsInZone<VirtualPickable>(zoneID);
      if (pickables != null)
      {
        pickablesInZones.AddRange(pickables);
      }
      List<VirtualAnimal> animals = ObjectSpawner.instance.GetObjectsInZone<VirtualAnimal>(zoneID);
      if (animals != null)
      {
        animalsInZones.AddRange(animals);

      }
    }
    // maybe set this to greater than a certain threshold, we dont want them hunting deer if theres just one
    if (pickablesInZones.Count == 0 && animalsInZones.Count == 0)
    {
      // what do we do if this is the case? prioritize expansion? trade? trigger state of emergency?
      return null;
    }
    // determine number of citizens going to work on this
    // in the beginning of the game there may be available workers who we dont want to assign because we still need to fill
    // other occupations
    if (pickablesInZones.Count > animalsInZones.Count)
    {
      // create gatherer workforce
      HashSet<SharedPickableData> pickableTypes = new HashSet<SharedPickableData>();
      HashSet<SharedItemData> itemTypes = new HashSet<SharedItemData>();
      foreach (VirtualPickable pickable in pickablesInZones)
      {
        pickableTypes.Add(pickable.pickableData.sharedPickableData);
        itemTypes.AddRange(pickable.pickableData.sharedPickableData.GetItemsFromDrop());
      }
      int maxWorkerCount = Mathf.Min(workerCount, idleCitizens.Count);
      List<VirtualCitizen> workers = idleCitizens.GetRange(0, maxWorkerCount);
      // Debug.Log("item targets: " + itemTypes);
      // need to check if we have the free storage and if not add them to the build queue
      // get SharedWorkforceData from ObjectDB of type FoodGatherWorkforce
      SharedFoodGatherOccupationData workforceData = ObjectSpawner.instance.objectDB.GetFoodGatherOccupationData();
      createdWorkforce = workforceData.CreateFoodGatherWorkforceData(
        "Food gather workforce",
        workers,
        zones,
        new List<VirtualStorage> { availableStorage[0] },
        new List<VirtualStorage> { availableStorage[1] }
      );
      employedCitizens.Add(idleCitizens[0]);
      idleCitizens.RemoveRange(0, maxWorkerCount);
      workforces.Add(createdWorkforce);
      RecalculateFoodProduction();
    }
    else
    {
      // we neead weapons to do this
      // create hunter workforce
      // FoodGatherWorkforceData createdWorkforce = new FoodGatherWorkforceData(animalsInZones, new List<CitizenData> { idleCitizens[0] });
      // workforces.Add(createdWorkforce);
    }
    return createdWorkforce;
  }

  private void RecalculateFoodProduction()
  {
    caloriesProducedPerDay = 0;
    foreach (WorkforceData workforce in workforces)
    {
      // Debug.Log("workforce: " + workforce);
      if (workforce.GetType() == typeof(FoodGatherWorkforceData))
      {
        caloriesProducedPerDay += ((FoodGatherWorkforceData)workforce).GetEstimatedCaloriesPerDay();
      }
    }
    // Debug.Log("updating calories produced per day: " + caloriesProducedPerDay);
  }

  private void CreateCraftingWorkforce(int workerCount, List<SharedItemData> itemsToCraft)
  {

  }

  private void CreateGatherWorkforce(int workerCount, List<SharedItemData> itemsToGather)
  {
    List<VirtualCitizen> citizens = idleCitizens.GetRange(0, workerCount);
    GatherWorkforceData gatherWorkforce = new GatherWorkforceData($"{itemsToGather[0].itemName} Gatherers", zones, citizens, itemsToGather, false, null);
    workforces.Add(gatherWorkforce);
  }
  private void IncreaseHousingSupply(int numberOfBeds)
  {
    // Eventually we will need the civilization manager to determine the best house this civilization is able to build
    // by checking their known/acquired items;

    SharedBuildingData house = CivilziationManager.instance.GetHouse();
    // need to know how many beds this house has
    // could save that info on SharedBuildingData or just go through the build pieces and count the beds

    ConstructionWorkforceData constructionWorkforce = GetConstructionWorkforce();
    if (GetConstructionWorkforce() == null)
    {
      // create construction workforce
      // add citizens to workforce
      // need to add inventory to the construction workforce
      // add workforce to workforces
    }


    // need to make sure the houses are by a fire if they dont include one in their buildPieces
    /// maybe we just have it include in the build pieces
    /// 
    QueueBuildingConstruction(house);

  }

  private ConstructionWorkforceData GetConstructionWorkforce()
  {
    foreach (WorkforceData workforce in workforces)
    {
      if (workforce.GetType() == typeof(ConstructionWorkforceData))
      {
        return workforce as ConstructionWorkforceData;
      }
    }
    return null;
  }

  private void QueueBuildingConstruction(SharedBuildingData building)
  {
    HashSet<EntityAndAmount<SharedItemData>> requiredMaterials = new HashSet<EntityAndAmount<SharedItemData>>();

    // add building to list of building needs
    foreach (VirtualBuildPiece buildPiece in building.buildPieces)
    {
      foreach (Ingredient ingredient in buildPiece.buildPieceData.recipe.ingredients)
      {
        EntityAndAmount<SharedItemData> existingMaterial = requiredMaterials.FirstOrDefault(m => m.item.Equals(ingredient.item));
        if (existingMaterial != null)
        {
          existingMaterial.amount += ingredient.amount;
        }
        else
        {
          requiredMaterials.Add(new EntityAndAmount<SharedItemData>(ingredient.item, ingredient.amount));
        }
      }
    }
    // buildings.Add(building);
  }

  private void ImproveHousing()
  {
    // increase comfort, 
    // furniture etc
  }
}