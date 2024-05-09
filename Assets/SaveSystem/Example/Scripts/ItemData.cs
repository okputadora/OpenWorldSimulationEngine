[System.Serializable]
public class ItemData : ISaveableData
{
  public string itemName = "";
  public int itemHealth = 100;

  public void Load(SaveData dataToLoad)
  {
    itemName = dataToLoad.ReadString();
    itemHealth = dataToLoad.ReadInt();
  }

  public void Save(SaveData dataToSave)
  {
    dataToSave.Write(itemName);
    dataToSave.Write(itemHealth);
  }
}