using UnityEngine;

public class VirtualCampFire : VirtualBuildPiece
{
  public CampFireData campFireData;
  // maybe replace this with SharedFuelBurnerData which will have the build piece recipe on it
  public VirtualCampFire(BuildPieceRecipe recipe, SharedDestructibleData sharedDestructibleData) : base(recipe, sharedDestructibleData)
  {
    campFireData = new CampFireData();

  }
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