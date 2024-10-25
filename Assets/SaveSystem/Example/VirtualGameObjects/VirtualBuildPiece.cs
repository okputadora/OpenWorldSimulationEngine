using UnityEngine;

[System.Serializable]
public class VirtualBuildPiece : VirtualDestructible
{
  public BuildPieceData buildPieceData;
  public override void Save(SaveData dataToSave)
  {
    base.Save(dataToSave);
    buildPieceData.Save(dataToSave);

  }

  public override void Load(SaveData dataToLoad)
  {
    base.Load(dataToLoad);
    buildPieceData.Load(dataToLoad);
  }

  public override void SyncDataWithGameObject(GameObject go)
  {
    go.GetComponent<BuildPiece>().HydrateData(buildPieceData);
    base.SyncDataWithGameObject(go);
  }

}