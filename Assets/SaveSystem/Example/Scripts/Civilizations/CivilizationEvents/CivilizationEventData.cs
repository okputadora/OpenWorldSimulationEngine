using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CivilizationEventData", menuName = "Civilizations/CivilizationEventData", order = 1)]
public class CivilizationEventData : Requirement
{
  public string eventName;
  public string id;

  public List<OneOfEachRequirement> oneOfEachRequirements = new List<OneOfEachRequirement>();
}