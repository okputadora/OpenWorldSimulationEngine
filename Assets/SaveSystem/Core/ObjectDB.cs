using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "ScriptableObjects/ObjectDB", fileName = "ObjectDB", order = 1)]
public class ObjectDB : ScriptableObject
{
  public List<GameObject> prefabs;

  public List<GameObject> GetAllPrefabs()
  {
    return prefabs;
  }

  public Dictionary<int, GameObject> GetPrefabDict()
  {
    Dictionary<int, GameObject> prefabsById = new Dictionary<int, GameObject>();
    foreach (GameObject prefab in prefabs)
    {
      // if (prefab.name.Contains("fallen_log"))
      // {
      //   print("adding fallen log with id: " + Utils.GetPrefabName(prefab).GetStableHashCode());
      // }
      prefabsById.Add(Utils.GetPrefabName(prefab).GetStableHashCode(), prefab);
    }
    return prefabsById;
  }
}