using System.Collections.Generic;
using System;
using UnityEngine;
public class CivilizationData : ISaveableData, ISimulatable
{
  public Guid id;
  protected string civilizationName;
  public List<SettlementData> settlements = new List<SettlementData>(); // private set
  public KnownObjectLists knownObjects = new KnownObjectLists();

  // political relationships (alliances, activeEnemies, ongoing wars)
  // policies (trade, domestic, etc)
  public CivilizationData()
  {

  }

  public CivilizationData(Vector3 worldPosition, int initialCitizenCount)
  {
    id = Guid.NewGuid();
    SettlementAIData settlementData = new SettlementAIData(worldPosition, initialCitizenCount, this);
    settlements.Add(settlementData);
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
    // simulate civilization level things
    // then simulate all settlements
    foreach (SettlementData settlement in settlements)
    {
      settlement.Simulate(deltaTime);
    }

  }

  // call when adding a new citizen to the civilization
  private void CheckPopulationEvents()
  {
    // CivilziationManager.instance.PopulationEvents;
    // 
  }
}

