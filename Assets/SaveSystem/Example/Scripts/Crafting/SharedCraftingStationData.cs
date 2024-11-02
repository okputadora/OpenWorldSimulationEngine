using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Crafting Station", menuName = "ScriptableObjects/SharedCraftingStationData", order = 1)]
public class SharedCraftingStationData : ScriptableObject
{
  [Header("Crafting Station Data")]
  public int stationLevel;
  public string stationName;
  // public string description;
  public List<ItemRecipe> recipes;

  public CraftingType craftingType;
  public List<CraftingUpgrade> upgrades;

}

public enum CraftingType
{
  inventory, // no station required
  woodworking,
  cooking,
  metalworking,
  smelter,
  furnace,
}
