using System.Collections.Generic;
using JetBrains.Annotations;

public class TradeRouteData : ISaveableData
{
  public List<TradeData> tradeStops;

  public void Load(SaveData dataToLoad)
  {
    throw new System.NotImplementedException();
  }

  public void Save(SaveData dataToSave)
  {
    throw new System.NotImplementedException();
  }
}


public class TradeData
{
  public SettlementData origin; // could just do id
  public SettlementData destination;
  public List<Dictionary<string, int>> itemsToPickUp = new List<Dictionary<string, int>>();
  public List<Dictionary<string, int>> itemsToDropOff = new List<Dictionary<string, int>>();


}