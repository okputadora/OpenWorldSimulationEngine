[System.Serializable]
public class AnimalData : IDestructible, ISaveableData
{
  public SharedAnimalData sharedAnimalData;
  public string sharedAnimalDataId;

  public AnimalData(SharedAnimalData sharedAnimalData)
  {
    this.sharedAnimalData = sharedAnimalData;
    sharedAnimalDataId = sharedAnimalData.id;
  }

  void IDestructible.Damage(HitData hitData)
  {
    throw new System.NotImplementedException();
  }

  DropTable IDestructible.GetDrop()
  {
    throw new System.NotImplementedException();
  }

  bool IDestructible.HasDamage()
  {
    throw new System.NotImplementedException();
  }

  void ISaveableData.Load(SaveData dataToLoad)
  {
    throw new System.NotImplementedException();
  }

  void ISaveableData.Save(SaveData dataToSave)
  {
    throw new System.NotImplementedException();
  }

  int IDestructible.GetHealth()
  {
    throw new System.NotImplementedException();
  }
}