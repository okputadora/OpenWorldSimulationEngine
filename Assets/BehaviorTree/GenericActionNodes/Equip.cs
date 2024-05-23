// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Equip : BTCitizenNode
// {
//   [SerializeField] private bool showErrorIconOnFail;
//   public override BTResult OnEvaluate()
//   {
//     // Debug.Log("Equip Action");
//     if (context.citizen.isSitting)
//     {
//       context.citizen.Stand();
//       return BTResult.RUNNING;
//     }
//     bool hasEveryRequiredItem = true;
//     List<SharedItemData.ItemType> itemsToEquip = new List<SharedItemData.ItemType>(); // @TODO Cache this
//     foreach (List<SharedItemData.ItemType> someItems in context.citizen.GetPossibleItemsToEquip())
//     {
//       bool hasAnyRequiredItem = false;
//       foreach (SharedItemData.ItemType itemType in someItems)
//       {
//         if (context.citizen.humanoidData.inventory.HasItem(itemType))
//         {
//           hasAnyRequiredItem = true;
//           itemsToEquip.Add(itemType);
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
//       bool equippedEveryItem = true;
//       foreach (SharedItemData.ItemType itemType in itemsToEquip)
//       {
//         if (!context.citizen.TryEquipItem(itemType))
//         {
//           equippedEveryItem = false;
//         }
//       }
//       if (equippedEveryItem)
//       {
//         context.citizen.RemoveStatus(0);
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
