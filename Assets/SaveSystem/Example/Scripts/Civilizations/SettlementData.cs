using System.Collections.Generic;
using UnityEngine;
using System;

public class SettlementData : ISaveableData, ISimulatable
{
  protected CivilizationAIData civilization;
  public Guid id;
  public Guid civilizationId;
  public Vector3 worldPosition { get; private set; } // bounds or radius?
  protected Vector2Int zoneID;
  protected List<Vector2Int> zones = new List<Vector2Int>();
  protected static int settlementRange = 2;
  protected string settlementName;
  public List<VirtualCitizen> citizens { get; private set; } = new List<VirtualCitizen>(); // actually need to store VirtualCitizen
  public List<VirtualCitizen> idleCitizens = new List<VirtualCitizen>();
  public List<VirtualCitizen> employedCitizens = new List<VirtualCitizen>();
  protected List<WorkforceData> workforces = new List<WorkforceData>();
  protected List<BuildingData> buildings = new List<BuildingData>();
  protected bool isCapital;
  protected bool isPlayerSettlement = true;
  // maybe put this in a struct or class of its own
  public int caloriesProducedPerDay = 0; //@TODO probably going to want to change this to kilocalories
  public int caloriesConsumedPerDay = 0;
  public int caloriesImportedPerDay = 0;
  public int caloriesExportedPerDay = 0;
  public int fuelProducedPerDay = 0;
  public int fuelConsumedPerDay = 0;
  public int bedCount;
  public List<TradeRouteData> tradeRoutes = new List<TradeRouteData>(); // might save under trader occupation data
  // probably move these down to SettlementData

  // buildings 
  // workbenches
  // campfires
  // 
  public SettlementData()
  {

  }

  public SettlementData(Vector3 worldPosition, int initialCitizenCount, CivilizationData civilization)
  {
    id = Guid.NewGuid();
    this.worldPosition = worldPosition;
    civilizationId = civilization.id;
    zoneID = ZoneSystem.instance.GetZoneFromPosition(worldPosition);

    for (int x = -settlementRange; x <= settlementRange; x++)
    {
      for (int y = -settlementRange; y <= settlementRange; y++)
      {
        zones.Add(new Vector2Int(zoneID.x + x, zoneID.y + y));
      }
    }
    for (int i = 0; i < initialCitizenCount; i++)
    {
      // need object spawner to add VirtualCitizen
      // add slight offsets for world position
      Vector3 citizenPosition = worldPosition + new Vector3(i, worldPosition.y, i);
      VirtualCitizen citizen = ObjectSpawner.instance.CreateVirtualCitizen(citizenPosition, this.zoneID, this, civilization);
      // CitizenData citizen = new CitizenData();
      idleCitizens.Add(citizen);
      citizens.Add(citizen);
    }
  }
  public void Load(SaveData dataToSave)
  {
    throw new System.NotImplementedException();
  }

  public void Save(SaveData dataToLoad)
  {
    throw new System.NotImplementedException();
  }


  public virtual void Simulate(float deltaTime)
  {
    // simulate occupations
    foreach (WorkforceData workforce in workforces)
    {
      workforce.Simulate(deltaTime);
    }
    // simulate "unemployed" citizens
  }
}