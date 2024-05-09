using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
public class DataSyncer : MonoBehaviour
{
  [ReadOnly] public string typeName;
  public bool shouldDestroyPermanently = false;
  public VirtualGameObject objectData;

  /** 
  */
  [Tooltip("Determines when to load and destroy this object based off of the ZoneSystem local and distant zones. When a local zone is converted to a distance zone (because the player moves away from this zone e.g.) all child objects where isDistance = false will be unloaded")]
  public bool isDistant;
  public bool isStatic;

  // Called when creating a new object for the first time
  public VirtualGameObject CreateVirtualGameObject()
  {
    VirtualGameObject vgo = null;
    try
    {
      vgo = Activator.CreateInstance(Type.GetType(typeName)) as VirtualGameObject;
      vgo.BaseInitizalize(isDistant, isStatic);
      objectData = vgo;
    }
    catch (Exception e)
    {
      Debug.Log("error spawning: " + typeName);
      Debug.LogError(e);
      // we need to throw an error, the game should not run if this fails
    }
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