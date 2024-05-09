using UnityEngine;

public class VirtualCampFire : VirtualBuildPiece
{
  public CampFireData campFireData;
  public override void Save(SaveData dataToSave)
  {
    base.Save(dataToSave);
    campFireData.Save(dataToSave);

  }

  public override void Load(SaveData dataToLoad)
  {
    base.Load(dataToLoad);
    campFireData.Load(dataToLoad);
  }

  public override void SyncDataWithGameObject(GameObject go)
  {
    base.SyncDataWithGameObject(go);
    go.GetComponent<CampFire>().HydrateData(campFireData);
  }
}