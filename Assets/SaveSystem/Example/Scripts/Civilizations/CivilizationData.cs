using System.Collections.Generic;

public class CivilizationData : ISaveableData, ISimulatable
{
  private string civilizationName;
  private List<SettlementData> settlements = new List<SettlementData>();

  // political relationships (alliances, activeEnemies, ongoing wars)
  // policies (trade, domestic, etc)

  public void Load(SaveData dataToSave)
  {
    throw new System.NotImplementedException();
  }

  public void Save(SaveData dataToLoad)
  {
    throw new System.NotImplementedException();
  }

  public void Simulate(float deltaTime)
  {
    foreach (SettlementData settlement in settlements)
    {
      settlement.Simulate(deltaTime);
    }

  }
}

public class TradeData : ISaveableData
{
  public void Load(SaveData dataToSave)
  {
    throw new System.NotImplementedException();
  }

  public void Save(SaveData dataToLoad)
  {
    throw new System.NotImplementedException();
  }
}