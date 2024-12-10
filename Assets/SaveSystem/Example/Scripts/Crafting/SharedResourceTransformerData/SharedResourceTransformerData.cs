using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Resource Transformer", menuName = "ScriptableObjects/SharedResourceTransformerData", order = 1)]
public class SharedResourceTransformerData : ScriptableObject
{
  [Header("Resource Transformer Data")]
  public int stationLevel;
  public string stationName;
  // public string description;
  public List<ItemRecipe> recipes;

  public TransformerType transformerType;
  public List<CraftingUpgrade> upgrades;

}

public enum TransformerType
{
  smelter,
  furnace,
}
