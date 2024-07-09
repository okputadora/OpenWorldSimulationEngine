// using System.Collections.Generic;
// using UnityEngine;
// using System;
// using XNode;

// [CreateAssetMenu(fileName = "SharedWorkforceData", menuName = "Occuppation/SharedWorkforceData", order = 1)]
// public class SharedWorkforceData : ScriptableObject
// {
//   public enum OccupationType
//   {
//     Unemployed = 0,
//     Hunter = 1,
//     Gatherer = 2,
//     Farmer = 3,
//     Builder = 4,
//     Soldier = 5,
//     Clergy = 6, // more specific?
//     LumberJack = 7,
//     Trader = 8,
//     Fisherman = 9,
//     Smith = 10,
//     ShipBuilder = 11,
//     Mason = 12,
//     Carpenter = 13,
//     Cook = 14,
//   }
//   public enum OccupationCategory
//   {
//     Gather,
//     Craft,
//     Builder,
//   }
//   [Header("Display")]
//   public OccupationType occupationType;
//   public OccupationCategory occupationCategory;
//   // public Occupation.Requirements requirements; // could be a dictionary?

//   public Sprite icon;
//   public string displayName;
//   [Header("Requirements")]
//   public List<Skill> requiredSkills;

//   [SerializeField] private List<OneOfRequiredItems> requiredItems;
//   [SerializeField] private BehaviorTreeGraph m_behaviorTree; // maybe make private and a method for retreiving it
//   public BehaviorTreeGraph beahviorTree { get { return m_behaviorTree; } private set { value = m_behaviorTree; } }
//   public SharedItemData.ItemType[] itemTargets;
//   public DamageableType[] attackTargets;

//   [Header("Stats")]
//   public int defaultCaloriesPerHour = 350;
//   public float maxTimePerDay = 3000;
//   public List<OccupationOutput> outputs;
//   public float minFatigue;
//   public float minRadius = 20;
//   public float maxRadius = 100;

//   public int GetCurrentCaloriesPerDay(Citizen citizen)
//   {
//     // formula for multiplying each citizen skill level in requiredSkills by defaultCalories per day
//     // more strength will get you higher output, but it will also burn more calories

//     // maybe move this to the workforce since ?
//     return defaultCaloriesPerHour * 24;
//   }

//   public List<List<SharedItemData.ItemType>> GetRequiredItems()
//   {
//     List<List<SharedItemData.ItemType>> _requiredItems = new List<List<SharedItemData.ItemType>>(); // @TODO Cache this
//     foreach (OneOfRequiredItems oneOfRequiredItems in requiredItems)
//     {
//       _requiredItems.Add(oneOfRequiredItems.items);
//     }
//     return _requiredItems;
//   }

//   public List<List<SharedItemData.ItemType>> GetRequiredEquipItems()
//   {
//     List<List<SharedItemData.ItemType>> _requiredItems = new List<List<SharedItemData.ItemType>>(); // @TODO Cache this
//     foreach (OneOfRequiredItems oneOfRequiredItems in requiredItems)
//     {
//       if (oneOfRequiredItems.m_shouldEquip)
//       {
//         _requiredItems.Add(oneOfRequiredItems.items);
//       }
//     }
//     return _requiredItems;
//   }

// }
