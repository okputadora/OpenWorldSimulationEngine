using System.Collections.Generic;

public class CivilizationData : ISaveableData, ISimulatable
{
  protected string civilizationName;
  protected List<SettlementData> settlements = new List<SettlementData>();
  public KnownObjectLists knownObjects = new KnownObjectLists();

  // political relationships (alliances, activeEnemies, ongoing wars)
  // policies (trade, domestic, etc)

  public CivilizationData()
  {

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

