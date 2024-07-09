using UnityEngine;

public class VirtualDestructible : VirtualGameObject
{
  // health and damage data
  public override void Save(SaveData dataToSave)
  {
    base.Save(dataToSave);

  }

  public override void Load(SaveData dataToLoad)
  {
    base.Load(dataToLoad);
  }

  public override void SyncDataWithGameObject(GameObject go)
  {
    base.SyncDataWithGameObject(go);
  }
}