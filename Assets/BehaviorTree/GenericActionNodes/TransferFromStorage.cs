// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TransferFromStorage : BTCitizenNode
// {
//   [SerializeField] private bool showErrorIconOnFail;
//   public override BTResult OnEvaluate()
//   {

//     // Can we move this whole thing into a method for the contextOwner?
//     Inventory inventory = context.citizen.GetNearestPickupInventory();
//     if (inventory == null)
//     {
//       Debug.Log("SOMETHING WENT WRONG INVENTORY IS NULL!");
//     }
//     bool hasEveryRequiredItem = true;
//     List<SharedItemData.ItemType> itemsToTransfer = new List<SharedItemData.ItemType>(); // @TODO cache this
//     foreach (List<SharedItemData.ItemType> someItems in context.citizen.GetPossibleItemsToEquip())
//     {
//       bool hasAnyRequiredItem = false;
//       foreach (SharedItemData.ItemType itemType in someItems)
//       {
//         if (inventory.HasItem(itemType))
//         {
//           hasAnyRequiredItem = true;
//           itemsToTransfer.Add(itemType);
//           break;
//         }
//       }
//       if (!hasAnyRequiredItem)
//       {
//         hasEveryRequiredItem = false;
//         break;
//       }
//     }
//     if (hasEveryRequiredItem)
//     {
//       // Debug.Log("has every required item");
//       bool removedEveryItem = true;
//       foreach (SharedItemData.ItemType itemType in itemsToTransfer)
//       {
//         // Debug.Log("trying to remove item: " + itemType);
//         if (!context.citizen.RemoveItemFromInventoryStorage(itemType, 1))
//         {
//           removedEveryItem = false;
//           break;
//         }
//       }
//       if (removedEveryItem)
//       {
//         // Debug.Log("removed every item");
//         context.citizen.ClearCurrentTarget();
//         context.citizen.RemoveStatus(0);
//         return BTResult.SUCCESS;
//       }
//     }
//     if (showErrorIconOnFail)
//     {
//       context.citizen.AddStatus(0, $"Citizen is missing required items");
//     }
//     return BTResult.FAILURE; // perhaps we should return running here? they shouldnt move on until this is compelted
//   }
// }
