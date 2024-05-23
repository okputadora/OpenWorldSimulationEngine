// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EquipFromStorage : BTCitizenNode
// {
//   [SerializeField] private bool showErrorIconOnFail;
//   public override BTResult OnEvaluate()
//   {

//     // Can we move this whole thing into a method for the contextOwner?
//     Inventory inventory = context.citizen.GetNearestPickupInventory();
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
//       bool removedEveryItem = true;
//       foreach (SharedItemData.ItemType itemType in itemsToTransfer)
//       {
//         if (!context.citizen.RemoveItemFromInventoryStorage(itemType, 1))
//         {
//           removedEveryItem = false;
//           break;
//         }
//       }
//       if (removedEveryItem)
//       {
//         context.citizen.ClearCurrentTarget();
//         context.citizen.RemoveStatus(0);
//         foreach (SharedItemData.ItemType itemType in itemsToTransfer)
//         {
//           context.citizen.TryEquipItem(itemType);
//         }
//         return BTResult.SUCCESS;
//       }
//     }
//     if (showErrorIconOnFail)
//     {
//       context.citizen.AddStatus(0, $"Citizen is missing required items");
//     }
//     return BTResult.FAILURE;
//   }
// }
