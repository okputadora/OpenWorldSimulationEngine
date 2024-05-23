using System.Collections.Generic;
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe", order = 2)]
[Serializable]
public class Recipe : ScriptableObject
{
    public SharedItemData item;
    [SerializeField]
    public int amount;
    public Ingredient[] ingredients;
    [Header("Requirements")]
    public int craftingLevel;
    public List<CraftingStation.CraftingType> requiredStations;
    [HideInInspector] public bool isAvailable;
    public float defaultCraftingTime = 5f;
    // technologies, crafting level etc

}
[Serializable]
public class Ingredient
{
    public SharedItemData item;
    // public SharedItemData.ItemType item; // can I just have a reference to the shared Item data here so we dont have to look it up in the item db every time? I think so
    public int amount;
    public Ingredient(SharedItemData i, int a)
    {
        item = i;
        amount = a;
    }
}