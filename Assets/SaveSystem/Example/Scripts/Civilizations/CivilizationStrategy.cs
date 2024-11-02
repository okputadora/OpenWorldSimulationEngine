using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CivilizationStrategy", menuName = "Civilizations/CivilizationStrategy", order = 1)]
public class CivilizationStrategy : ScriptableObject
{
  public string strategyName;
  [Range(0, 100)]
  public int citizenComfort;
  [Range(0, 100)]
  public int expansion;
  [Range(0, 100)]
  public int trade;
  [Range(0, 100)]
  public int food;
  [Range(0, 100)]
  public int foodVariety;
  [Range(0, 100)]
  public int shelter;
  [Range(0, 100)]
  public int clothing;
  [Range(0, 100)]
  // public int

  [HideInInspector] public int housing = 100;

  [Range(0, 100)]
  public int defense;
  [Range(0, 100)]
  public int education;
  [Range(0, 100)]
  public int religion;
  [Range(0, 100)]
  public int technology;


}