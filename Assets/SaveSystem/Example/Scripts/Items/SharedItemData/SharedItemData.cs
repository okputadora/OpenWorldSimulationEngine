using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "ScriptableObjects/SharedItemData", order = 1)]
public class SharedItemData : SharedData
{
  public string itemName;
  public string itemId;
  public enum ItemType
  {
    Material = 1,
    Consumable = 2,
    OneHandedWeapon = 3,
    TwoHandedWeapon = 4,
    Tool = 5,
    Bow = 6,
    Shield = 7,
    Helmet = 8,
    Chest = 9,
    Legs = 10,
    // Cape 


  }
  public ItemType itemType;

  public ItemData CreateItemDataInstance()
  {
    return new ItemData();
  }
}