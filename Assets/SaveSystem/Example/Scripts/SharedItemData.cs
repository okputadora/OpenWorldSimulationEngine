using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "ScriptableObjects/SharedItemData", order = 1)]
public class SharedItemData : SharedData
{
  public string itemName;

  public ItemData CreateItemDataInstance()
  {
    return new ItemData();
  }
}