using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildPieceRecipe", menuName = "Buildings/BuildPieceRecipe", order = 1)]
public class BuildPieceRecipe : ScriptableObject
{
  [Header("Build Piece Data")]
  public string id; // maybe change this to enum. can;t decide
  public GameObject prefab; // this kind of breaks the pattern we established with items.
                            // With items We have ItemRecipe which references SharedItemData which has the prefab
                            // public BuildPieceCategory category;

  // public BuildPieceSubCategory subCategory;
  public string displayName;
  public bool isWalkable;

  public Sprite icon;
  // public Cost cost;
  public Ingredient[] ingredients;
  public OneOfEachItemRequirement oneOfEachItemRequirements = new OneOfEachItemRequirement();

  // public VirtualBuildPiece CreateVirtualBuildPieceInstance()
  // {
  //   return new VirtualBuildPiece(this);
  // }

}