using UnityEngine;
public class CampFire : MonoBehaviour, ISaveableComponent<CampFireData>
{
  public CampFireData campFireData;

  public void Start()
  {

  }

  public void Update()
  {
    if (campFireData.remainingFuel >= 0)
    {
      campFireData.Burn(Time.deltaTime);
    }
  }
  public void Save(SaveData dataToSave)
  {
    campFireData.Save(dataToSave);
  }

  public void Load(SaveData dataToLoad)
  {
    campFireData = new CampFireData();
    campFireData.Load(dataToLoad);
    if (campFireData.remainingFuel <= 0)
    {
      // vfx.turnOff()
    }
    else
    {
      // vfx.turnOn()
    }
  }

  public void HydrateData(CampFireData data)
  {
    campFireData = data;
  }

  public CampFireData CreateNewData()
  {
    return new CampFireData();
  }
}