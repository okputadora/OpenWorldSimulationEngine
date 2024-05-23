// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FollowPlayer : BTCitizenNode
// {
//   private object m_target; // SaveableGameObject or GameObject
//   private bool m_isTargetSet = false;

//   protected override void OnEnter()
//   {
//     m_isTargetSet = context.citizen.FollowPlayer();
//   }

//   public override BTResult OnEvaluate()
//   {
//     if (!m_isTargetSet)
//     {
//       // if retry
//       m_isTargetSet = context.citizen.FollowPlayer();
//       return BTResult.FAILURE;
//     }
//     return BTResult.RUNNING;
//   }
// }
