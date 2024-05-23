// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class HasIngredients : BTCitizenNode
// {
//   public override BTResult OnEvaluate()
//   {
//     List<Ingredient[]> recipes = context.citizen.GetRequiredRecipes();
//     if (recipes == null) return BTResult.FAILURE;
//     bool hasSomeCompleteRecipe = false;
//     foreach (Ingredient[] ingredients in recipes)
//     {
//       bool hasAllRequiredIngredients = true;
//       foreach (Ingredient ingredient in ingredients)
//       {
//         if (!context.citizen.humanoidData.inventory.HasItemOfAmount(ingredient.item.itemType, ingredient.amount))
//         {
//           hasAllRequiredIngredients = false;
//           break;
//         }
//       }
//       if (hasAllRequiredIngredients)
//       {
//         hasSomeCompleteRecipe = true;
//       }
//       // Debug.Log("inventory at capactiy");
//     }
//     if (hasSomeCompleteRecipe) return BTResult.SUCCESS;
//     return BTResult.FAILURE;
//     // Debug.Log("inventory not at capacity");
//   }
// }
