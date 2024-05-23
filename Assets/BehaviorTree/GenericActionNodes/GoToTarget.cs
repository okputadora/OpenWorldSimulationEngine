// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GoToTarget : BTCitizenNode
// {
//   public enum TargetType { PICKUP, DROPOFF, ATTACK, INTERACT, CRAFT, BUILDING, BUILDPIECE }
//   [SerializeField] private TargetType m_targetType;
//   private object m_target; // SaveableGameObject or GameObject
//   private bool isTargetSet = false;
//   public override BTResult OnEvaluate()
//   {
//     if (m_target == null)
//     {
//       if (m_targetType == TargetType.DROPOFF)
//       {
//         // Debug.Log("trying to set dropoff");
//         m_target = context.citizen.SetCurrentInventoryDropoffTarget();
//       }
//       else if (m_targetType == TargetType.PICKUP)
//       {
//         m_target = context.citizen.SetCurrentInventoryPickupTarget();
//       }
//       else if (m_targetType == TargetType.INTERACT)
//       {
//         // Debug.Log("setting pickup target");
//         m_target = context.citizen.SetCurrentInteractTarget();
//       }
//       else if (m_targetType == TargetType.ATTACK)
//       {
//         // Debug.Log("setting attack target");
//         m_target = context.citizen.SetCurrentAttackTarget();
//       }
//       else if (m_targetType == TargetType.CRAFT)
//       {
//         m_target = context.citizen.SetCurrentCraftTarget();
//       }
//       else if (m_targetType == TargetType.BUILDING)
//       {
//         m_target = context.citizen.SetCurrentBuildingTarget();
//       }
//       else if (m_targetType == TargetType.BUILDPIECE)
//       {
//         Debug.Log("setting current build piece target");
//         m_target = context.citizen.SetCurrentBuildPieceTarget();
//         Debug.Log("since we just set target, the target should NOT be reached");
//         Debug.Log("is target reached: " + context.citizen.IsTargetReached());
//       }
//       // check if target is null, if so return FAILURE
//       return BTResult.RUNNING;
//     }
//     // if target is still null return failure
//     if (!context.citizen.IsTargetReached())
//     {
//       // Debug.Log(nodeDescription + " target is set, " + m_target + " checking if reached");
//       return BTResult.RUNNING;
//     }
//     // Debug.Log("BT: reached target!");
//     m_target = null;
//     // do we need to check if target was destroyed and return FAILED if so?
//     return BTResult.SUCCESS;
//   }
// }
