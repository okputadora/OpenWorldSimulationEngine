using System.Collections.Generic;

public class WorkforceData : ISaveableData
{
  public List<CitizenData> citizens = new List<CitizenData>();
  // maybe implement a schedule
  // requirements (tools, food, etc)
  public virtual void Load(SaveData dataToSave)
  {
    throw new System.NotImplementedException();
  }

  public virtual void Save(SaveData dataToLoad)
  {
    throw new System.NotImplementedException();
  }
}

public class GatherWorkforceData : WorkforceData
{
  // targetResources (wood, stone, ore, hides, meat, etc)
  // attackToGather = true/false
  // acctackTargets (trees, rocks, ore deposits, animals, etc)
  // settlementId
  // 

  public override void Save(SaveData dataToSave)
  {
    base.Save(dataToSave);
  }
  public override void Load(SaveData dataToLoad)
  {
    base.Load(dataToLoad);
  }
}

public class CraftWorkforceData : WorkforceData
{
  // Workbench (carpenter bench, cooking station)
  // recipes
  // fromInventories
  // toInventories
  public override void Save(SaveData dataToSave)
  {
    base.Save(dataToSave);
  }
  public override void Load(SaveData dataToLoad)
  {
    base.Load(dataToLoad);
  }
}
public class TradeWorkforceData : WorkforceData
{
  // originSettlement = settlementId;
  // destinationSettlement = settlementId;
  // goodsToDeliver = ItemData goods to take from origin to destination
  // goodsToReturn = ItemData  goods to take from destination to origin
  // currentGoods current goods in the traders possesion
  public override void Save(SaveData dataToSave)
  {
    base.Save(dataToSave);
  }
  public override void Load(SaveData dataToLoad)
  {
    base.Load(dataToLoad);
  }
}

