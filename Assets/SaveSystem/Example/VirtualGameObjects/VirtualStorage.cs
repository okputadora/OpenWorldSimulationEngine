using UnityEngine;

public class VirtualStorage : VirtualBuildPiece
{
  public InventoryData inventoryData;
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