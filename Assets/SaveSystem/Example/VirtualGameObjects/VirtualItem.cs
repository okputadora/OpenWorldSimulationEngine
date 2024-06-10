using UnityEngine;

public class VirtualItem : VirtualGameObject
{
  public ItemData itemData;

  public override void Initialize(
    GameObject prefab,
    Vector3 position,
    Quaternion rotation,
    Vector3 scale,
    Vector2Int zoneID
  )
  {
    isStatic = true;
    base.Initialize(prefab, position, rotation, scale, zoneID);
    itemData = prefab.GetComponent<Item>().itemData; // this needs to be a copy of itemData, or does it? isnt it already unique to each instance of the GameObject created?
  }
  public override void Initialize(GameObject gameObject, Vector3 worldPosition, Vector2Int zoneID)
  {
    isStatic = true;
    base.Initialize(gameObject, worldPosition, zoneID);
    Debug.Log(gameObject);
    itemData = gameObject.GetComponent<Item>().itemData;
  }
  public override void Save(SaveData dataToSave)
  {
    base.Save(dataToSave);
    itemData.Save(dataToSave);
  }

  public override void Load(SaveData dataToLoad)
  {
    base.Load(dataToLoad);
    itemData = new ItemData();
    itemData.Load(dataToLoad);
  }

  public override void SyncDataWithGameObject(GameObject go)
  {
    base.SyncDataWithGameObject(go);
    go.GetComponent<Item>().HydrateData(itemData);
  }
}