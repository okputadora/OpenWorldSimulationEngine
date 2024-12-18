using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;
public class InventoryData : ISaveableData
{
  public int totalCapacity;
  public float maxWeight;
  [SerializeField]
  public float currentWeight = 0;
  public int slotCount = 32;
  public bool isPlayerInventoryData;

  public string displayName;
  public List<ItemData> items = new List<ItemData>();
  public Guid id { get; private set; }
    private Action<InventoryData> onInventoryChange;

  public InventoryData(bool isPlayer, int slotCount, int maxWeight)
  {
    isPlayerInventoryData = isPlayer;
    displayName = isPlayer ? "Player inventory" : "Citizen inventory";
    id = Guid.NewGuid();
    this.slotCount = slotCount;
    this.maxWeight = maxWeight;
    // load inventory from disc for player, derive it for citizens (...well we might want to save our own citizens data so trading is persistent)
    // if no data to load
    for (int i = 0; i < slotCount; i++)
    {
      items.Add(new ItemData());
    }
  }

  public bool AddItem(ItemData newItem)
  {
    bool wasAbleToAdd = false;
    if (AtCapacity())
    {
      return wasAbleToAdd;
    }
    if (HasItemWithRoom(newItem.sharedData.itemType, out ItemData existingItem))
    {
      if (existingItem.amount + newItem.amount > newItem.sharedData.maxStackSize)
      {
        // check if we will be able to add the remainded to an empty slot before adding the partial amount to the existing slot
        if (HasEmptySlot(out int index))
        {
          int partialAmount = newItem.sharedData.maxStackSize - existingItem.amount;
          existingItem.amount += partialAmount;
          newItem.amount -= partialAmount;
        }
        wasAbleToAdd = false;
      }
      else
      {
        existingItem.amount += newItem.amount; // need to overflow into other slot if too much and check if inventory is full
        wasAbleToAdd = true;
      }
    } 
    bool hasEmptySlot = HasEmptySlot(out int emptySlotIndex);
    if (!wasAbleToAdd && hasEmptySlot)
    {
      items[emptySlotIndex] = new ItemData(newItem.sharedData, newItem.amount); // can we just store the new item directly without making this new instance?
      wasAbleToAdd = true;
    }
    if (wasAbleToAdd)
    {
      if (onInventoryChange != null)
      {
        onInventoryChange(this);
      }
      currentWeight += newItem.sharedData.weight * newItem.amount;
    }
    return wasAbleToAdd;
  }
  private bool HasItemWithRoom(SharedItemData.ItemType itemType, out ItemData existingItem)
  {
    existingItem = null;
    foreach (ItemData item in items)
    {
      if (!item.isEmpty && item.sharedData.itemType == itemType && item.amount < item.sharedData.maxStackSize)
      {
        existingItem = item;
        return true;
      }
    }
    return false;
  }
  public bool AtCapacity()
  {
    if (currentWeight >= maxWeight)
    {
      // Debug.Log("INVENTORY FULL, should go drop off the wood we have");
    }
    return currentWeight >= maxWeight;
  }


  public bool HasEmptySlot(out int index)
  {
    for (int i = 0; i < slotCount; i++)
    {
      if (items[i].isEmpty)
      {
        index = i;
        return true;
      }

    }
    index = -1;
    return false;
  }

  public void RegisterChangeListener(Action<InventoryData> listener)
  {
    onInventoryChange += listener;
  }

  public void UnregisterChangeListener(Action<InventoryData> listener)
  {
    onInventoryChange -= listener;
  }

  public bool IsFull()
  {
    return currentWeight >= maxWeight;
  }

  public bool HasEmptySlot()
  {
    if (IsFull()) return false;
    return false;
  }

  public bool HasRoom(SharedItemData item)
  {
    if (IsFull()) return false;
    return false;
  }

  public bool HasRoom(string itemId)
  {
    if (IsFull()) return false;
    return false;
  }


  public void RemoveItem(ItemData item)
  {
    items.Remove(item);
    currentWeight -= item.sharedData.weight * item.amount;
    if (onInventoryChange != null)
    {
      onInventoryChange(this);
    }
  }

  public void RemoveItems(List<ItemData> itemsToRemove) {
      foreach(ItemData itemToRemove in itemsToRemove) {
        int index = items.IndexOf(itemToRemove);
        if (index == -1) continue;
        currentWeight -= itemToRemove.sharedData.weight * itemToRemove.amount;
        items[index] = new ItemData();
      }
    
    if (onInventoryChange != null)
    {
      onInventoryChange(this);
    }
  }
  public static void TransferAllItems(InventoryData from, InventoryData to)
  {
    List<ItemData> itemsToRemove = new List<ItemData>();
    foreach (ItemData item in from.items)
    {
      if (to.AddItem(item)) {
        itemsToRemove.Add(item);
      }
    }
    from.RemoveItems(itemsToRemove);
  }
  
  public static void TransferItemsOfType(InventoryData from, InventoryData to, HashSet<SharedItemData> sharedItems)
  {
   List<ItemData> itemsToRemove = new List<ItemData>();
    foreach (ItemData item in from.items)
    {
        // Debug.Log("checking item: " + item.sharedData.itemName);
        if (sharedItems.Contains(item.sharedData))
        {
            if (to.AddItem(item))
            {
              itemsToRemove.Add(item);
            }
        }
    }
    from.RemoveItems(itemsToRemove);
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
}