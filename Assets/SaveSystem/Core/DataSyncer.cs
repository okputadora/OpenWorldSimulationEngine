using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
public class DataSyncer : MonoBehaviour
{
  public VirtualObjectType objectType;
  public bool shouldDestroyPermanently = false;
  public VirtualGameObject objectData;

  /** 
  */
  [Tooltip("Determines when to load and destroy this object based off of the ZoneSystem local and distant zones. When a local zone is converted to a distant zone (because the player moves away from this zone e.g.) all child objects where isDistance = false will be unloaded")]
  public bool isDistant;
  public bool isStatic;

  public void Update()
  {
    if (isStatic) return;
    CheckZone();
  }

  private void CheckZone()
  {
    ZoneSystem.instance.TryUpdateZone(gameObject, objectData);
    // ZoneSystem.instance.TryReparentZone();
  }

  // Called when creating a new object for the first time
  public VirtualGameObject CreateVirtualGameObject(GameObject instance, Vector3 worldPosition, Vector2Int zoneId)
  {
    VirtualGameObject vgo = VirtualObjectFactory.Create(objectType, gameObject);
    // combine now that we're calling these together
    vgo.BaseInitizalize(isStatic, isDistant);
    vgo.Initialize(instance, worldPosition, zoneId);
    if (objectType == VirtualObjectType.VirtualCraftingStation)
    {
      Debug.Log("Creating crafting station");
      Debug.Log(vgo);
      Debug.Log(vgo.GetType());
      Debug.Log(gameObject.GetComponent<CraftingStation>().virtualCraftingStation);
      Debug.Log(gameObject.GetComponent<BuildPiece>().virtualBuildPiece);
      Debug.Log(gameObject.GetComponent<CraftingStation>().virtualCraftingStation == gameObject.GetComponent<BuildPiece>().virtualBuildPiece);

    }
    // objectData = vgo;
    return vgo;
  }

  public VirtualGameObject CreateVirtualGameObject(GameObject prefab, Vector3 worldPosition, Quaternion rotation, Vector3 scale, Vector2Int zoneID)
  {
    VirtualGameObject vgo = VirtualObjectFactory.Create(objectType, prefab);
    // combine now that we're calling these together
    vgo.BaseInitizalize(isStatic, isDistant);
    vgo.Initialize(prefab, worldPosition, rotation, scale, zoneID);
    // objectData = vgo;

    return vgo;
  }



  // called when loading an object that already exists in memory
  public void AttachVirtualGameObject(VirtualGameObject vgo)
  {
    // vgo.SyncGameObjectWithData(gameObject);
    objectData = vgo;
  }

  private void OnDestroy()
  {
    if (!shouldDestroyPermanently)
    {
      objectData.SyncDataWithGameObject(gameObject);
    }
  }

  private void OnDrawGizmos()
  {
    DrawZoneParent();
  }

  private void DrawZoneParent()
  {
    GUIStyle style = new GUIStyle();
    style.normal.textColor = Color.blue;
    Handles.Label(transform.position, objectData.zoneID.ToString(), style);
  }
}