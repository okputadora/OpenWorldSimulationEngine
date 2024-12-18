using UnityEngine;
using System.Collections.Generic;
using System;
using System;

// public interface IVirtualObject
// {
//   public virtual void BaseInitizalize(bool isStatic, bool isDistant)
//   {

//   }
//   public virtual void Initialize(GameObject instance, Vector3 worldPosition, Vector2Int zoneID)
//   {

//   }
//   public virtual void Initialize(GameObject prefab, Vector3 worldPosition, Quaternion rotation, Vector3 scale, Vector2Int zoneID)
//   {

//   }

//   public virtual void Load(SaveData dataToLoad) { }

//   public virtual void Save(SaveData dataToSave) { }
// }
[System.Serializable]
public class VirtualGameObject
{
  public Vector3 worldPosition;
  public Vector3 scale;
  public Quaternion rotation;
  public Vector2Int zoneID;
  public int prefabID;
  public bool isStatic;
  public bool isDistant;

  private GameObject go;
  public Guid id;

  public void Initialize(int prefabID, bool isStatic, bool isDistant, Vector3 worldPosition, Quaternion rotation, Vector3 scale, Vector2Int zoneID)
  {
    this.prefabID = prefabID;
    this.isDistant = isDistant;
    this.isStatic = isStatic;
    this.worldPosition = worldPosition;
    this.rotation = rotation;
    this.scale = scale;
    this.zoneID = zoneID;
    this.id = Guid.NewGuid();
  }
  public virtual void Save(SaveData dataToSave)
  {
    if (go)
    {
      SyncDataWithGameObject(go);
    }
    dataToSave.Write(isDistant);
    dataToSave.Write(isStatic);
    dataToSave.Write(worldPosition);
    dataToSave.Write(rotation);
    dataToSave.Write(scale);
    dataToSave.Write(zoneID);
    dataToSave.Write(prefabID);
  }

  public virtual void Load(SaveData dataToLoad)
  {
    isDistant = dataToLoad.ReadBool();
    isStatic = dataToLoad.ReadBool();
    worldPosition = dataToLoad.ReadVector3();
    rotation = dataToLoad.ReadQuaternion();
    scale = dataToLoad.ReadVector3();
    zoneID = dataToLoad.ReadVector2Int();
    prefabID = dataToLoad.ReadInt();
  }

  /// <summary>
  /// Should be called when unloading a game object from scene or before saving
  /// </summary>
  public virtual void SyncDataWithGameObject(GameObject gameObject)
  {
    worldPosition = ZoneSystem.instance.GameToWorldPosition(gameObject.transform.position);
    rotation = gameObject.transform.rotation;
    scale = gameObject.transform.localScale;
  }

  /// <summary> Called when loading a game object from a virtual object</summary>
  public virtual void SyncGameObjectWithData(GameObject go)
  {
    // Debug.Log(typeof(this));
    go.GetComponent<DataSyncer>().objectData = this;
    go.transform.position = ZoneSystem.instance.WorldToGamePosition(worldPosition);
    go.transform.rotation = rotation;
    go.transform.localScale = scale;
  }

  public virtual InteractionResult Interact(Action<List<ItemData>> callback) {
    return InteractionResult.FAILURE;
  }
}