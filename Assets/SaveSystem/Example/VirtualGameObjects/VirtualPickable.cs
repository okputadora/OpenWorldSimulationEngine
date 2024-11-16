using UnityEngine;

public class VirtualPickable : VirtualGameObject // Berry Bushes and Crops, etc..
{
  public PickableData pickableData;

  public VirtualPickable(SharedPickableData sharedPickableData) : base()
  {
    pickableData = new PickableData(sharedPickableData);
  }

  // this GameObject isnt always the instance...someitmes it is the prefab
  // public override void Initialize(GameObject instance, Vector3 worldPosition, Vector2Int zoneID)
  // {
  //   base.Initialize(instance, worldPosition, zoneID);
  //   SharedPickableData sharedPickableData = instance.GetComponent<Pickable>().sharedPickableData;
  //   pickableData = new PickableData(sharedPickableData);
  // }

  // public override void Initialize(GameObject prefab, Vector3 worldPosition, Quaternion rotation, Vector3 scale, Vector2Int zoneID)
  // {
  //   base.Initialize(prefab, worldPosition, rotation, scale, zoneID);
  //   SharedPickableData sharedPickableData = prefab.GetComponent<Pickable>().sharedPickableData;
  //   pickableData = new PickableData(sharedPickableData);
  // }
}