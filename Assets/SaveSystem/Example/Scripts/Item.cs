using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item : MonoBehaviour, ISaveableComponent<ItemData>, IInteractable
{
    public ItemData itemData;
    public SharedItemData sharedItemData;

    void Update()
    {

    }

    public void Save(SaveData dataToSave)
    {
        itemData.Save(dataToSave);
    }

    public void Load(SaveData dataToLoad) // dont think we need this
    {
        itemData.Load(dataToLoad);
        // read values off of itemData to update visuals
        if (itemData.itemHealth <= 0)
        {
            // show broken model
        }
    }

    public void HydrateData(ItemData data)
    {
        itemData = data;
    }

    public ItemData CreateNewData()
    {
        return new ItemData();
    }

    public void Interact()
    {

    }
    public bool IsHoverable()
    {
        return true;
    }
    public string GetHoverText()
    {
        return itemData.itemName;
    }
    public GameObject GetHoverUI()
    {
        return null;
    }
}
