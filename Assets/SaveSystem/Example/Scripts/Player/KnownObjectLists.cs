using System.Collections.Generic;
using UnityEditor.Rendering;
public class KnownObjectLists : ISaveableData
{
  public HashSet<string> knownItems = new HashSet<string>();
  public HashSet<string> knownItemRecipes = new HashSet<string>();
  public HashSet<string> knownOccupations = new HashSet<string>();
  public HashSet<string> utilizedOccupations = new HashSet<string>();
  public HashSet<string> knownBuildPieces = new HashSet<string>();
  public HashSet<string> knownBuildPieceRecipes = new HashSet<string>();
  public HashSet<string> knownCraftingStations = new HashSet<string>();
  public HashSet<string> civilizationAchievements = new HashSet<string>();
  public HashSet<string> knownAnimals = new HashSet<string>();
  // potentially fetch this from the object DB
  public static List<SharedItemData> allItems = new List<SharedItemData>();
  public static List<SharedOccupationData> allOccupations = new List<SharedOccupationData>();
  public static List<ItemRecipe> allItemRecipes = new List<ItemRecipe>();
  public static List<BuildPieceRecipe> allBuildPieces = new List<BuildPieceRecipe>();
  public static List<CivilizationEventData> allCivilizationEvents = new List<CivilizationEventData>();
  public void Load(SaveData dataToSave)
  {
    throw new System.NotImplementedException();
  }

  public void Save(SaveData dataToLoad)
  {
    throw new System.NotImplementedException();
  }

  public void UpdateKnownItems(string itemID)
  {
    knownItems.Add(itemID);
    List<ItemRecipe> unlockedRecipes = TryUnlockItemRecipes();
    List<BuildPieceRecipe> unlockedBuildPieceRecipe = TryUnlockBuildPieceRecipes();
    // try unlock occupations
  }

  public void UpdateKnownBuildPieces(string buildPieceID)
  {
    knownBuildPieces.Add(buildPieceID);
    // try unlock occupations
  }

  public void UpdateKnownBuildPieceRecipes(string buildPieceID)
  {
    knownBuildPieceRecipes.Add(buildPieceID);
  }

  public void UpdateKnownOccupations(string occupationID)
  {
    if (!knownOccupations.Contains(occupationID))
    {
      knownOccupations.Add(occupationID);
    }
  }


  public void UpdateKnownItemRecipes(string recipeID)
  {
    if (!knownItemRecipes.Contains(recipeID))
    {
      knownItemRecipes.Add(recipeID);

    }
  }

  public void UpdateKnownCraftingStations(string stationID)
  {

  }

  public void UpdateKnownCivilizationEvents(string eventID)
  {
    if (!civilizationAchievements.Contains(eventID))
    {
      civilizationAchievements.Add(eventID);
    }
  }

  private List<ItemRecipe> TryUnlockItemRecipes()
  {
    List<ItemRecipe> unlockedRecipes = new List<ItemRecipe>();
    // @TODO account for requirements
    foreach (ItemRecipe recipe in allItemRecipes)
    {
      bool hasAllIngredients = true;
      foreach (Ingredient ingredient in recipe.ingredients)
      {
        if (!knownItems.Contains(ingredient.item.id))
        {
          hasAllIngredients = false;
          break;
        }
      }
      if (hasAllIngredients && !knownItemRecipes.Contains(recipe.id))
      {
        unlockedRecipes.Add(recipe);
      }
    }
    return unlockedRecipes;
  }

  private List<BuildPieceRecipe> TryUnlockBuildPieceRecipes()
  {
    List<BuildPieceRecipe> unlockedRecipes = new List<BuildPieceRecipe>();
    foreach (BuildPieceRecipe recipe in allBuildPieces)
    {
      if (!recipe.oneOfEachItemRequirements.CheckItemRequirements(knownItems)) break;
      bool hasAllIngredients = true;
      foreach (Ingredient ingredient in recipe.ingredients)
      {
        if (!knownItems.Contains(ingredient.item.id))
        {
          hasAllIngredients = false;
          break;
        }
      }
      if (hasAllIngredients && !knownBuildPieceRecipes.Contains(recipe.id))
      {
        unlockedRecipes.Add(recipe);
      }
    }
    return unlockedRecipes;
  }
}