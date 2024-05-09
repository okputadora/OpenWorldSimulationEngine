using UnityEngine;
using System.Collections.Generic;
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

  // public VirtualGameObject(bool isDistant, bool isStatic)
  // {
  //   this.isDistant = isDistant;
  //   this.isStatic = isStatic;
  // }

  public void BaseInitizalize(bool isStatic, bool isDistant)
  {
    this.isDistant = isDistant;
    this.isStatic = isStatic;
  }
  public virtual void Initialize(GameObject prefab, Vector3 worldPosition, Quaternion rotation, Vector3 scale, Vector2Int zoneID)
  {
    this.worldPosition = worldPosition;
    this.rotation = rotation;
    this.scale = scale;
    this.zoneID = zoneID;
    prefabID = Utils.GetPrefabName(prefab).GetStableHashCode();
  }

  // Creating new objects in ObjectSpawner
  public virtual void Initialize(GameObject instance, Vector3 worldPosition, Vector2Int zoneID)
  {
    this.worldPosition = worldPosition;
    rotation = instance.transform.rotation;
    scale = instance.transform.localScale;
    this.zoneID = zoneID;
    prefabID = Utils.GetPrefabName(instance).GetStableHashCode();
    go = instance;
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

  // Should be called when unloading a game object from scene or before saving
  public virtual void SyncDataWithGameObject(GameObject gameObject)
  {
    worldPosition = ZoneSystem.instance.GetWorldPositionFromGamePosition(gameObject.transform.position);
    rotation = gameObject.transform.rotation;
    scale = gameObject.transform.localScale;
  }

  // Called when loading a game object from a virtual object
  public virtual void SyncGameObjectWithData(GameObject go)
  {
    go.transform.position = worldPosition;
    go.transform.rotation = rotation;
    go.transform.localScale = scale;
  }

}