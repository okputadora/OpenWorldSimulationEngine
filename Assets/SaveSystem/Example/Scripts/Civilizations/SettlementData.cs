using System.Collections.Generic;
using UnityEngine;

public class SettlementData : ISaveableData, ISimulatable
{
  protected Vector3 worldPosition; // bounds?
  protected Vector2Int zoneID;
  protected List<Vector2Int> zones;
  protected static int settlementRange = 2;
  protected string settlementName;
  protected List<CitizenData> citizens;
  protected List<WorkforceData> workforces;
  protected List<BuildingData> buildings;
  protected bool isCapital;

  // buildings 
  // workbenches
  // campfires
  // 
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