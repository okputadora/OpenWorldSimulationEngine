using System;
using System.Collections.Generic;
using UnityEngine;
public class CivilizationAIData : CivilizationData
{


  bool isPlayer = false;
  public CitizenData leader;
  public CivilizationStrategy strategy;
  // public Dictionary<CivilizationMilestone> unlockedMilestones;

  public CivilizationAIData(CivilizationStrategy strategy, Vector3 worldPosition, int initialCitizenCount, string civilizationName) : base(worldPosition, initialCitizenCount, civilizationName)
  {
    // Civilization strategy
    // create settlements
    // id = Guid.NewGuid();
    // SettlementAIData settlementData = new SettlementAIData(worldPosition, initialCitizenCount, this);
    // settlements.Add(settlementData);
  }
  public override void Simulate(float deltaTime)
  {
    // simulate top level strategies
    base.Simulate(deltaTime);
  }

  public void DetermineStartingStrategy(CivilizationStrategy strategy, StartingConditions startingConditions)
  {
    // starting biome
    int distanceToOcean;
    int distanceToRiver;
    int distnaceToLake;

  }

}

public class CivilizationAIStrategy
{
  public enum StrategyType
  {
    // Self sufficient, avoids conflict
    Isolationist,
    // Strong military, adverserial
    Expansionist,
    Balanced,
    // Strong economy and economic influence, 
    Globalist,
  }
}