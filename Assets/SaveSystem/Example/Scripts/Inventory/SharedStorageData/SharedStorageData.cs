using UnityEngine;
[CreateAssetMenu(fileName = "SharedStorageData", menuName = "ScriptableObjects/SharedStorageData", order = 1)]
public class SharedStorageData : ScriptableObject
{
  public string id;
  public StorageType storageType;
  public string storageName;
  public GameObject prefab;

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