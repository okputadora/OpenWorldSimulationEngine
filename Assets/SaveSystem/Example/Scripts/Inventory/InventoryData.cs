using UnityEngine;
using System;
using System.Collections.Generic;
public class InventoryData : ISaveableData
{
  public int totalCapacity;
  public float maxWeight;
  [SerializeField]
  public float currentWeight = 0;
  public int slotCount = 32;
  public bool isPlayerInventoryData;

  private Action<InventoryData> onInventoryDataChange;
  public string displayName;
  public List<ItemData> items = new List<ItemData>();
  public Guid id { get; private set; }

  public InventoryData(bool isPlayer, int slotCount, int maxWeight)
  {
    this.isPlayerInventoryData = isPlayer;
    this.displayName = isPlayer ? "Player inventory" : "Citizen inventory";
    this.id = Guid.NewGuid();
    this.slotCount = slotCount;
    this.maxWeight = maxWeight;
    // load inventory from disc for player, derive it for citizens (...well we might want to save our own citizens data so trading is persistent)
    // if no data to load
    for (int i = 0; i < slotCount; i++)
    {
      this.items.Add(new ItemData());
    }
  }

  public void Save(SaveData dataToSave)
  {
    dataToSave.Write(id);
    dataToSave.Write(items.Count);
    foreach (ItemData item in items)
    {
      item.Save(dataToSave);
    }
  }

  public void Load(SaveData dataToLoad)
  {
    id = dataToLoad.ReadId();
    int count = dataToLoad.ReadInt();
    for (int i = 0; i < count; i++)
    {
      ItemData item = new ItemData();
      item.Load(dataToLoad);
    }
  }

  public void RegisterChangeListener(Action<InventoryData> listener)
  {
    onInventoryDataChange += listener;
  }

  public void UnregisterChangeListener(Action<InventoryData> listener)
  {
    onInventoryDataChange -= listener;
  }

}