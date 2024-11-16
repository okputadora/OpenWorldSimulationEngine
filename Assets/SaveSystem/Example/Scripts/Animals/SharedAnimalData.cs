using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "ScriptableObjects/SharedAnimalData", order = 1)]
public class SharedAnimalData : SharedDestructibleData
{
  public GameObject prefab;
  public string animalName;
  public string id;

  public bool isHuntable;
  [Range(0, 100)]
  public int huntingDifficulty;
  public bool isTameable;
  public int minutesUntilTame = 30;
  public bool isMount;
  public enum AnimalType
  {
    Deer = 1,
    WildBoar = 20,
    Wolf = 30

  }
  public Sprite icon;
  public string description;

}