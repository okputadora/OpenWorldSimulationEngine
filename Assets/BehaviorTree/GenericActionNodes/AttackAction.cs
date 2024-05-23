// using UnityEngine;
// public class AttackAction : BTCitizenNode
// {
//   [SerializeField] private bool isAttacking = false;
//   private BTResult result;
//   private float cooldownStartTime = 0;
//   [SerializeField] private float cooldownDuration = 5;
//   [SerializeField] private bool isCooled = true;

//   protected override void OnEnter()
//   {
//     isAttacking = false;
//     isCooled = true;
//     result = BTResult.RUNNING;
//   }
//   public override BTResult OnEvaluate()
//   {
//     if (!isCooled && cooldownStartTime > 0 && cooldownStartTime + cooldownDuration <= Time.time)
//     {
//       isCooled = true;
//     }
//     if (result != BTResult.SUCCESS && !isAttacking && isCooled)
//     {
//       isAttacking = true;
//       GameObject currentTarget = context.citizen.GetCurrentTarget();
//       if (currentTarget == null)
//       {
//         return BTResult.SUCCESS;
//       }
//       context.citizen.Attack(true, OnAttackHit, null, 0, currentTarget);
//     }
//     if (result == BTResult.SUCCESS)
//     {
//       context.citizen.ClearCurrentTarget(); // could move to on exit
//     }

//     return result;
//   }


//   private void OnAttackHit(bool didHit, bool didDestroy, IDamageable damageable)
//   {
//     isAttacking = false;
//     if (didDestroy)
//     {
//       result = BTResult.SUCCESS;
//     }
//     else
//     {
//       isCooled = false;
//       cooldownStartTime = Time.time;
//     }
//     // if (!didDestroy)
//     // {
//     //   // reset attack target, it may have moved, we could add a isTargetStatic field to determine when we can just keep attacking
//     //   context.citizen.GoToCurrentAttackTarget();
//     //   isAttacking = false;
//     //   // controller.Attack(true, OnAttackHit, OnAttackComplete, cooldownTime, controller.citizenAI.currentTarget);
//     // }
//     // else
//     // {
//     //   // context.citizen.ClearCurrentTarget();
//     //   result = BTResult.SUCCESS;
//     //   isAttacking = false;
//     // }
//   }
// }