using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CivilizationImproverment", menuName = "Civilizations/CivilizationImproverment", order = 1)]
public class CivilizationImproverment : ScriptableObject
{
  public CivilizationImprovermentType improvementType;
  public List<ItemRecipe> itemRecipes;
  // public List<SharedCraftingData> craftingStations;
  public List<BuildPieceRecipe> buildPieces;
  public List<SharedOccupationData> workforces;
}

public enum CivilizationImprovermentType
{
  Woodcutting,
  Mining,
  Crafting,
  Farming,
  Housing,
  FoodGather,
  FoodPrep,
  Trading,


}