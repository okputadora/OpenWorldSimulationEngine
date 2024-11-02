
using UnityEngine;
using System.Collections.Generic;
// [CreateAssetMenu(fileName = "CivilizationEventData", menuName = "Civilizations/CivilizationEventData", order = 1)]
public class Requirement : ScriptableObject
{
  public virtual bool HasRequirement(int amount)
  {
    return true;
  }
}
// [System.Serializable]
// public class Requirement
// {

// } // maybe just make an itnerface if its going to be empty...or abstract

[System.Serializable]
public class RequirementAndAmount
{
  public Requirement requirement;
  public int amount;
}


[System.Serializable]
public class ItemRequirement
{
  public List<SharedItemData> items;
}

[System.Serializable]
public class BuildingRequirement : Requirement
{
  public string buildingType;
  public int amount;
}

[System.Serializable]
public class WorkforceRequirement : Requirement
{
  public SharedOccupationData occupation;
  public int amount;
}

[System.Serializable]
public class CivilizationEventRequirement : Requirement
{

}


