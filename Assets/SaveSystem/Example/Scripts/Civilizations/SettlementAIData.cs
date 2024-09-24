using System;
using System.Collections.Generic;
using UnityEngine;

public class SettlementAIData : SettlementData
{
  // nearbyresources
  CivilizationAIData civilization;
  public int caloriesProducedPerDay = 0; //@TODO probably going to want to change this to kilocalories
  public int caloriesConsumedPerDay = 0;
  public int caloriesImportedPerDay = 0;
  public int caloriesExportedPerDay = 0;
  public int fuelProducedPerDay = 0;
  public int fuelConsumedPerDay = 0;
  public int bedCount;
  public List<TradeRouteData> tradeRoutes = new List<TradeRouteData>(); // might save under trader occupation data
  public List<CitizenData> idleCitizens = new List<CitizenData>();
  public List<CitizenData> employedCitizens = new List<CitizenData>();
  // public enum SettlementFocus (mining, logging, food production, general, etc)

  public SettlementAIData(Vector3 worldPosition, int initialCitizenCount)
  {
    this.worldPosition = worldPosition;
    Vector2Int zoneID = ZoneSystem.instance.GetZoneFromPosition(worldPosition);
    this.zoneID = zoneID;
    for (int x = -settlementRange; x <= settlementRange; x++)
    {
      for (int y = -settlementRange; y <= settlementRange; y++)
      {
        zones.Add(new Vector2Int(zoneID.x + x, zoneID.y + y));
      }
    }
    for (int i = 0; i < initialCitizenCount; i++)
    {
      CitizenData citizen = new CitizenData();
      idleCitizens.Add(citizen);
    }

  }

  public override void Simulate(float deltaTime)
  {
    base.Simulate(deltaTime); // simulates workers and citizens for AI and player settlements
    CheckNeeds();
    // MakeImprovements();
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
    int surplus = caloriesProducedPerDay + caloriesImportedPerDay + cusion - caloriesConsumedPerDay + caloriesExportedPerDay;
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
    }
    else if (surplus > cusion) // @TODO base -50 off of caution trait of the ruler / civilization
    {
      // either remove people from food production
      // or stop importing
    }
  }

  private void CheckHousingNeeds()
  {

  }

  private void CheckFuelNeeds()
  {

  }

  private void CheckMaterialNeeds()
  {

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
    if (civilization.strategy.trade > tradeThreshold) // @TODO add method to civilization to get trade favorability
    {
      if (tradeRoutes.Count > 0)
      {
        // do we have a trade route with food, can it be increased
        // do we have a trade route with a settlement that has surplus food
      }
      else
      {
        // establish a new trade route
      }
    }
    if (idleCitizens.Count > 0)
    {
      WorkforceData foodProductionWorkforce = GetFoodProductionWorkforce();
      if (foodProductionWorkforce != null && foodProductionWorkforce.TryAddWorker(idleCitizens[0]))
      {
        // add IdleCitizen to workforce
        idleCitizens.RemoveAt(0);
      }
      else
      {
        // create FoodProductionWorkforce
        // send int numberOfWorkers based off of calroieDeficit
        bool createdFoodWorkforce = CreateFoodGatherWorkforce();
        // newFoodProductionWorkforce.TryAddWorker(idleCitizens[0]);
        employedCitizens.Add(idleCitizens[0]);
        idleCitizens.RemoveAt(0);
      }
    }
    else
    {
      // pull citizens from other workforces to support food production
    }
    // determine path of least resistance --> using trade or acquiring our own food

    // do we have an established food workforce
    // do we have established trade routes
    // do we have workers doing non essential things
    // can we setup trade routes

  }

  private void ImproveFoodSupply()
  {

  }

  private WorkforceData GetFoodProductionWorkforce()
  {
    foreach (WorkforceData workforce in workforces)
    {
      if (workforce.GetType() == typeof(GatherWorkforceData))
      {
        foreach (SharedItemData itemData in ((GatherWorkforceData)workforce).targetResources)
        {
          if (itemData.itemType == SharedItemData.ItemType.Consumable)
          {
            return workforce;
          }
          // @TODO or if its cookable... then we might also need to account for having enough cooks
        }
      }
    }
    return null;
  }

  private bool CreateFoodGatherWorkforce()
  {
    // Determine available food resources
    List<VirtualAnimal> animalsInZones = new List<VirtualAnimal>();
    List<VirtualPickable> pickablesInZones = new List<VirtualPickable>();
    // consider abstracting into a method GetObjectsOfType 
    foreach (Vector2Int zoneID in zones)
    {
      List<VirtualGameObject> objects = ObjectSpawner.instance.GetObjectsInZone(zoneID);
      foreach (VirtualGameObject vgo in objects)
      {
        // check if vgo is of type item and item.itemType === food, VirtualAnimal && VirtualAnimal.isHuntable, VirtualPickable
        Type vgoType = vgo.GetType();
        if (vgoType == typeof(VirtualAnimal))
        {
          animalsInZones.Add(vgo as VirtualAnimal);
        }
        else if (vgoType == typeof(VirtualPickable))
        {
          pickablesInZones.Add(vgo as VirtualPickable);
        }
        else if (vgoType == typeof(VirtualItem))
        {

        }
      }
    }
    // maybe cet this to greater than a certain threshold, we dont want them hunting deer if theres just one
    if (pickablesInZones.Count == 0 && animalsInZones.Count == 0)
    {
      // what do we do if this is the case? prioritize expansion? trade?
      return false;
    }
    // determine number of citizens going to work on this
    // in the beginning of the game there may be available workers who we dont want to assign because we still need to fill
    // other occupations
    if (pickablesInZones.Count > animalsInZones.Count)
    {
      // create gatherer workforce
      // FoodGatherWorkforceData createdWorkforce = new FoodGatherWorkforceData(pickablesInZones, new List<CitizenData> { idleCitizens[0] });
      // workforces.Add(createdWorkforce);
    }
    else
    {
      // we neead weapons to do this
      // create hunter workforce
      // FoodGatherWorkforceData createdWorkforce = new FoodGatherWorkforceData(animalsInZones, new List<CitizenData> { idleCitizens[0] });
      // workforces.Add(createdWorkforce);
    }
    return true;
  }
}