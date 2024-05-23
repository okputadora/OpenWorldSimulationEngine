// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GoToSettlementHub : BTCitizenNode
// {
//   private bool m_isTargetSet = false;

//   protected override void OnEnter()
//   {
//     m_isTargetSet = context.citizen.GoToSettlement();
//   }

//   public override BTResult OnEvaluate()
//   {
//     if (!m_isTargetSet) return BTResult.FAILURE;
//     if (context.citizen.IsTargetReached())
//     {
//       return BTResult.SUCCESS;
//     }
//     return BTResult.RUNNING;
//   }
// }
