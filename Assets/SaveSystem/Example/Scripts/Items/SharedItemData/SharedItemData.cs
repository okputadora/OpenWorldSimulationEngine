using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "ScriptableObjects/SharedItemData", order = 1)]
public class SharedItemData : Requirement
{
  public string itemName;
  public string id;
  public enum SourceType
  {
    recipe = 0,
    gather = 1, // available on the ground
    gatherAndPickable = 2,
    pickable = 3,
    gatherAndAnimal = 4,
    animal = 5,
    gatherAndDestructible = 6,
    destructible = 7,
  }
  public ItemRecipe recipe;
  public SharedPickableData pickable;
  public SharedAnimalData animal;
  public SharedDestructibleData destructible;
  public SharedDestructibleData destructible2;

  // Food
  public int calories;
  // food effects
  public enum ItemType
  {
    Material = 1,
    CraftedMaterial = 2,
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
  public SourceType sourceType;

  public ItemData CreateItemDataInstance()
  {
    return new ItemData();
  }

}