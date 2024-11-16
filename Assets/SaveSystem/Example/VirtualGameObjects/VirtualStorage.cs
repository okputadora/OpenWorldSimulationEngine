using UnityEngine;

public class VirtualStorage : VirtualBuildPiece
{
  public InventoryData inventoryData;

  public VirtualStorage(SharedStorageData storgageData, BuildPieceRecipe recipe, SharedDestructibleData sharedDestructibelData) : base(recipe, sharedDestructibelData)
  {
    // need either SharedInventoryData (which may be overkill) or just the slot count and weight limit
    inventoryData = new InventoryData(false, storgageData.slotCount, storgageData.maxWeight);
  }
  public override void Save(SaveData dataToSave)
  {
    base.Save(dataToSave);
    inventoryData.Save(dataToSave);

  }

  public override void Load(SaveData dataToLoad)
  {
    base.Load(dataToLoad);
    inventoryData.Load(dataToLoad);
  }

  public override void SyncDataWithGameObject(GameObject go)
  {
    // go.GetComponent<Storage>().HydrateData(buildPieceData);
    base.SyncDataWithGameObject(go);
  }

}