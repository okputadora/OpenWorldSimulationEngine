using System;
using System.Collections.Generic;

[Serializable]
public class DropTable
{
  public List<DropData> dropData = new List<DropData>();

  public Dictionary<SharedItemData, int> CreateDrop()
  {
    Dictionary<SharedItemData, int> drop = new Dictionary<SharedItemData, int>();
    foreach (DropData data in dropData)
    {
      data.CreateDropData(out SharedItemData itemData, out int amount);
      drop.Add(itemData, amount);
    }
    return drop;
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