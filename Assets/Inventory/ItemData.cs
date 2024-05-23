[System.Serializable]
public class ItemData : ISaveableData
{ // SharedData
  public int amount;
  public bool isEquipped;
  public bool isEmpty;
  public int itemHealth;

  public SharedItemData sharedData;
  public ItemData(SharedItemData sd, int amount)
  {
    this.sharedData = sd;
    this.amount = amount;
    this.isEmpty = false;
  }

  public ItemData()
  {
    this.sharedData = null;
    this.isEmpty = true;
  }

  public ItemData(SaveData data)
  {
    Load(data);
  }

  public void Save(SaveData data)
  {
    data.Write(amount);
    data.Write(isEquipped);
    data.Write(sharedData.itemName);
  }

  public void Load(SaveData data)
  {
    amount = data.ReadInt();
    isEquipped = data.ReadBool();
    string name = data.ReadString();
    sharedData = ObjectSpawner.instance.objectDB.GetSharedItemDataByName(name);
  }
}