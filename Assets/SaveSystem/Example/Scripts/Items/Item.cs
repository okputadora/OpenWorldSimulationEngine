using UnityEngine;

public class Item : MonoBehaviour, ISaveableComponent<VirtualItem>, IInteractable
{
    public SharedItemData sharedItemData;
    public VirtualItem virtualItem;

    void Update()
    {

    }

    public void Save(SaveData dataToSave)
    {
        virtualItem.itemData.Save(dataToSave);
    }

    public void Load(SaveData dataToLoad) // dont think we need this
    {
        virtualItem.itemData.Load(dataToLoad);
        // read values off of itemData to update visuals
        if (virtualItem.itemData.itemHealth <= 0)
        {
            // show broken model
        }
    }

    public void HydrateData(VirtualItem virtualItem)
    {
        this.virtualItem = virtualItem;
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
        return virtualItem.itemData.sharedData.name;
    }
    public GameObject GetHoverUI()
    {
        return null;
    }
}
