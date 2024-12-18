using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DropTable
{
  public List<DropData> dropData = new List<DropData>();

  public List<ItemData> CreateDrop()
  {
    List<ItemData> drop = new List<ItemData>();
    foreach (DropData data in dropData)
    {
      data.CreateDropData(out SharedItemData itemData, out int amount);
      for (int i = 0; i < amount; i++) {
        drop.Add(new ItemData(itemData, 1));
      }
    }
    return drop;
  }

  public HashSet<SharedItemData> PreviewDrop()
  {
    HashSet<SharedItemData> items = new HashSet<SharedItemData>();
    foreach (DropData dropData in dropData)
    {
      items.Add(dropData.itemData);
    }
    return items;
  }
}
[Serializable]
public class DropData
{
  public SharedItemData itemData;
  public float chanceOfDrop = 1;
  public int minDropCount = 0;
  public int maxDropCount = 1;

  public void CreateDropData(out SharedItemData iData, out int amount)
  {
    iData = itemData;
    amount = 0;
    if (UnityEngine.Random.value > chanceOfDrop) return;
    amount = UnityEngine.Random.Range(minDropCount, maxDropCount + 1);
  }
}