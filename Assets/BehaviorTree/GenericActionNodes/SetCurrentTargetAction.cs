// using UnityEngine;
// public class SetCurrentTargetAction : BTCitizenNode
// {
//   public enum TargetType { PICKUP, DROPOFF, ATTACK, INTERACT }
//   [SerializeField] private TargetType m_targetType;
//   public override BTResult OnEvaluate()
//   {
//     bool didSet = false;
//     if (m_targetType == TargetType.DROPOFF)
//     {
//       didSet = context.citizen.SetCurrentInventoryDropoffTarget() != null;
//     }
//     else if (m_targetType == TargetType.PICKUP)
//     {
//       didSet = context.citizen.SetCurrentInventoryPickupTarget() != null;
//     }
//     else if (m_targetType == TargetType.INTERACT)
//     {
//       didSet = context.citizen.SetCurrentInteractTarget() != null;
//     }
//     else if (m_targetType == TargetType.ATTACK)
//     {
//       didSet = context.citizen.SetCurrentAttackTarget() != null;
//     }
//     // check if target is null, if so return FAILURE
//     return didSet ? BTResult.SUCCESS : BTResult.FAILURE;
//   }
// }