public class PickableData : ISaveableData
{
  public SharedPickableData sharedPickableData;
  public string sharedPickableDataId;
  void ISaveableData.Load(SaveData dataToLoad)
  {
    throw new System.NotImplementedException();
  }

  void ISaveableData.Save(SaveData dataToSave)
  {
    throw new System.NotImplementedException();
  }
}