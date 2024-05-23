// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class IsEquipped : BTCitizenNode
// {
//   public override BTResult OnEvaluate()
//   {
//     bool hasEveryRequiredItem = true;
//     foreach (List<SharedItemData.ItemType> anyItems in context.citizen.GetPossibleItemsToEquip())
//     {
//       bool hasAnyRequiredItem = false;
//       foreach (SharedItemData.ItemType itemType in anyItems)
//       {
//         if (context.citizen.IsItemEquipped(itemType))
//         {
//           hasAnyRequiredItem = true;
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
//       return BTResult.SUCCESS;
//     }
//     return BTResult.FAILURE;
//   }
// }
