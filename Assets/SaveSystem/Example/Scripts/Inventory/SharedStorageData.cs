using UnityEngine;
public class SharedStorageData : ScriptableObject
{
  public string id;
  public StorageType storageType;

  public int slotCount;
  public int maxWeight;
  public enum StorageType
  {
    Chest,
    Cart,
    Boat,
    Other
  }
}