using System.Collections.Generic;
using System;
using UnityEngine;

public class Recipe : ScriptableObject
{
    public string id;
    public int amount;
    public bool hasIngredients = true;
    public Ingredient[] ingredients;
    [Header("Requirements")]
    public int craftingLevel;
    public List<CraftingStation.CraftingType> requiredStations;
    public OneOfEachItemRequirement oneOfEachRequirements = new OneOfEachItemRequirement();
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

[Serializable]
public class OneOfEachRequirement
{
    public string name;
    public List<SharedItemData> requirements;
}


[Serializable]
public class OneOfEachItemRequirement
{
    public List<ItemRequirements> oneOfEach;
    public bool CheckItemRequirements(HashSet<string> itemIDs)
    {
        bool hasAllRequirements = true;
        foreach (ItemRequirements oneOfEachRequirement in oneOfEach)
        {
            bool hasAnyRequirement = false;
            foreach (SharedItemData item in oneOfEachRequirement.requirements)
            {
                if (itemIDs.Contains(item.id))
                {
                    hasAnyRequirement = true;
                    break;
                }
            }
            if (!hasAnyRequirement)
            {
                hasAllRequirements = false;
                break;
            }
        }
        return hasAllRequirements;
    }
}

[Serializable]
public class ItemRequirements
{
    public List<SharedItemData> requirements;

}