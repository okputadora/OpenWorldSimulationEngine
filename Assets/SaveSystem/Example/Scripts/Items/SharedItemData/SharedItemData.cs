using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "ScriptableObjects/SharedItemData", order = 1)]
public class SharedItemData : Requirement
{
  public string itemName;
  public string id;

  // Food
  public int calories;
  // food effects
  public enum ItemType
  {
    Material = 1,
    Consumable = 20,
    FoodIngredient = 21,
    OneHandedWeapon = 30,
    TwoHandedWeapon = 40,
    Tool = 50,
    Bow = 60,
    Shield = 70,
    Helmet = 80,
    Chest = 90,
    Legs = 100,
    // Cape 


  }
  public ItemType itemType;
  public Sprite icon;
  public string description;
  public float weight;
  public float durability = 100;
  public int maxStackSize;


  public ItemData CreateItemDataInstance()
  {
    return new ItemData();
  }

}