using System.Collections.Generic;

public class SettlementData : ISaveableData, ISimulatable
{
  private string settlementName;
  private List<CitizenData> citizens;
  private List<WorkforceData> occupations;
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


  public void Simulate(float deltaTime)
  {
    // simulate occupations
    // simulate "unemployed" citizens
  }
}