using UnityEngine;

public class PickableData : ISaveableData
{
  public SharedPickableData sharedPickableData;
  public string sharedPickableDataId;
  public int currentCount = 1;

  public PickableData(SharedPickableData sharedData)
  {
    sharedPickableData = sharedData;
    sharedPickableDataId = sharedData.id;
  }
  void ISaveableData.Load(SaveData dataToLoad)
  {
    throw new System.NotImplementedException();
  }

  void ISaveableData.Save(SaveData dataToSave)
  {
    throw new System.NotImplementedException();
  }
}