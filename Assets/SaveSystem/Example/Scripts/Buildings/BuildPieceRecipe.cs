using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildPieceRecipe", menuName = "Buildings/BuildPieceRecipe", order = 1)]
public class BuildPieceRecipe : Requirement
{
  public string id;
  // public BuildPieceCategory category;
  // public BuildPieceSubCategory subCategory;
  public string displayName;
  public bool isWalkable;

  public Sprite icon;
  // public Cost cost;
  public Ingredient[] ingredients;
  public OneOfEachItemRequirement oneOfEachItemRequirements = new OneOfEachItemRequirement();

}