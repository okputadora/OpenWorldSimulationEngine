using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "ScriptableObjects/SharedItemData", order = 1)]
public class SharedItemData : SharedData
{
  public enum ItemType
  {
    Sword
  }
  public string itemName;
  public ItemType itemType;

  public ItemData CreateItemDataInstance()
  {
    return new ItemData();
  }
}