using UnityEngine;
using System;
using System.Collections.Generic;
public class Inventory : ISaveableData
{
  public int totalCapacity;
  public float totalWeightCapacity;
  [SerializeField]
  public float currentWeight = 0;
  public int slotCount = 32;
  public bool isPlayerInventory;

  private Action<Inventory> onInventoryChange;
  public string displayName;
  public List<ItemData> items = new List<ItemData>();
  public Guid id { get; private set; }

  public Inventory(bool isPlayer)
  {
    this.isPlayerInventory = isPlayer;
    this.displayName = isPlayer ? "Player inventory" : "Citizen inventory";
    this.id = Guid.NewGuid();
    // load inventory from disc for player, derive it for citizens (...well we might want to save our own citizens data so trading is persistent)
    // if no data to load
    totalWeightCapacity = isPlayerInventory ? 3000 : 10;
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

  public void RegisterChangeListener(Action<Inventory> listener)
  {
    onInventoryChange += listener;
  }

  public void UnregisterChangeListener(Action<Inventory> listener)
  {
    onInventoryChange -= listener;
  }

}