// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CraftAction : BTCitizenNode
// {
//   private bool m_canCraft = true;
//   private Recipe m_recipe;
//   protected override void OnEnter()
//   {
//     m_recipe = context.citizen.GetCurrentRecipe();
//     m_canCraft = context.citizen.Craft(m_recipe);
//   }
//   public override BTResult OnEvaluate()
//   {
//     if (m_recipe == null && !m_canCraft)
//     {
//       return BTResult.FAILURE;
//     }
//     if (context.citizen.isCrafting)
//     {
//       return BTResult.RUNNING;
//     }
//     return BTResult.SUCCESS;
//   }
// }
