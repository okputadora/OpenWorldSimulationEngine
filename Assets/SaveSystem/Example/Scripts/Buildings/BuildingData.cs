using System.Collections.Generic;
public class BuildingData : ISaveableData
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

// scriptable object?
public class BuildingType
{
  public string name;
  public string id;
  public List<List<Requirement>> oneOfEachRequirements = new List<List<Requirement>>();
}
