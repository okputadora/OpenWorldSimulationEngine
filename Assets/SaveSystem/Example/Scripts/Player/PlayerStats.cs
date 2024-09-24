public class PlayerStats : ISaveableData
{
  public KnownObjectLists knownObjects;

  public void Load(SaveData dataToLoad)
  {
    knownObjects.Load(dataToLoad);
  }

  public void Save(SaveData dataToSave)
  {
    knownObjects.Save(dataToSave);
  }
}