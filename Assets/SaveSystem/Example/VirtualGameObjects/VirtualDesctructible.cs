using UnityEngine;

public class VirtualDestructible : VirtualGameObject
{
  // health and damage data
  public DestructibleData destructibleData;

  public VirtualDestructible(SharedDestructibleData sharedDestructibleData) : base()
  {
    destructibleData = new DestructibleData(sharedDestructibleData);
  }
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