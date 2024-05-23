// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TransferIngredientsFromStorage : BTCitizenNode
// {
//   [SerializeField] private bool showErrorIconOnFail;
//   // private Recipe m_recipe;
//   private List<Ingredient[]> m_ingredients;
//   protected override void OnEnter()
//   {
//     // m_recipe = context.citizen.GetCurrentRecipe();
//     m_ingredients = context.citizen.GetRequiredRecipes();
//     // Debug.Log("transferingredients from storage: " + m_ingredients.Count);
//   }
//   public override BTResult OnEvaluate()
//   {

//     // Can we move this whole thing into a method for the contextOwner?
//     Inventory inventory = context.citizen.GetNearestPickupInventory(); // @TODO need a way to cycle through all inventories
//     List<SharedItemData.ItemType> itemsToTransfer = new List<SharedItemData.ItemType>(); // @TODO Cache this
//     bool hasSomeCompleteRecipe = false;
//     // @TODO WE MIGHT NOT NEED TO DO THIS CHECK, REMOVE ITEM FROM INVENTORY STORAGE MIGHT DO THAT FOR US
//     // in fact we might want them to pickup partial amounts for this because they might be distributed amongst various inventories
//     // we should allpow them to pickup partial recipes and then we need to add a BTCitizenNode to check if all workforce inventories have been searched,
//     // if they have and we still dont have a single required recipe, then we show the error icon

//     // IF on the other hand, the citizen has limited space in their inventory, then we do want to prioritize picking the ingredients to complete a recipe
//     // instead of just grabbing what we have
//     foreach (Ingredient[] ingredientList in m_ingredients)
//     {
//       bool removedAllRequiredIngredients = true;
//       foreach (Ingredient ingredient in ingredientList)
//       {
//         if (!context.citizen.RemoveItemFromInventoryStorage(ingredient.item.itemType, ingredient.amount))
//         {
//           removedAllRequiredIngredients = false;
//         }
//       }
//       if (removedAllRequiredIngredients)
//       {
//         hasSomeCompleteRecipe = true;
//       }
//     }
//     if (hasSomeCompleteRecipe) return BTResult.SUCCESS;
//     return BTResult.FAILURE;
//   }




//   // foreach (Ingredient ingredient in m_recipe.ingredients)
//   // {
//   //   if (!inventory.HasItemOfAmount(ingredient.item.itemType, ingredient.amount))
//   //   {
//   //     hasEveryIngredient = false;
//   //     break;
//   //   }
//   // }
//   // // there could be available ingredients in other containers, how will we search those?
//   // if (hasEveryIngredient)
//   // {
//   //   bool removedEveryIngredient = true;
//   //   foreach (Ingredient ingredient in m_recipe.ingredients)
//   //   {
//   //     if (!context.citizen.RemoveItemFromInventoryStorage(ingredient.item.itemType, ingredient.amount))
//   //     {
//   //       Debug.Log("failed to remove all of the ingredients");
//   //       removedEveryIngredient = false;
//   //       break;
//   //     }
//   //   }
//   //   if (removedEveryIngredient)
//   //   {
//   //     Debug.Log("transfered all the ingredients from a storage container");
//   //     context.citizen.RemoveStatus(0);
//   //     return BTResult.SUCCESS;
//   //   }
//   // }

//   // if (showErrorIconOnFail)
//   // {
//   //   context.citizen.AddStatus(0, $"Citizen is missing required items");
//   // }
//   // return BTResult.FAILURE;

// }
