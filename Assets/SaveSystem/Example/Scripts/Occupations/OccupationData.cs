using System.Collections.Generic;

public class OccupationData : ISaveableData
{

  public SharedOccupationData sharedOccupationData;
  public virtual void Load(SaveData dataToSave)
  {
    throw new System.NotImplementedException();
  }

  public virtual void Save(SaveData dataToLoad)
  {
    throw new System.NotImplementedException();
  }
}

public class GatherOccupationData : OccupationData
{
  public List<SharedItemData> itemsToGather = new List<SharedItemData>();
  public override void Load(SaveData dataToLoad)
  {
    base.Load(dataToLoad);
  }

  public override void Save(SaveData dataToSave)
  {
    base.Save(dataToSave);
  }
}