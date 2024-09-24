using System.Collections.Generic;
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemRecipe", menuName = "ScriptableObjects/ItemRecipe", order = 2)]
[Serializable]
public class ItemRecipe : Recipe
{
  public SharedItemData item;
}
