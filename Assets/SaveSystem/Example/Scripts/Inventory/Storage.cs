using UnityEngine;

public class Storage : MonoBehaviour
{
  public InventoryData inventoryData;
  public SharedStorageData sharedStorageData;
  public VirtualStorage virtualStorage;

  public void HydrateData(SharedStorageData sharedStorageData)
  {
    this.sharedStorageData = sharedStorageData;
    inventoryData = new InventoryData(false, sharedStorageData.slotCount, sharedStorageData.maxWeight);
  }

  public void Save(SaveData dataToSave)
  {
    inventoryData.Save(dataToSave);
  }

  public void Load(SaveData dataToLoad)
  {
    inventoryData.Load(dataToLoad);
  }
}