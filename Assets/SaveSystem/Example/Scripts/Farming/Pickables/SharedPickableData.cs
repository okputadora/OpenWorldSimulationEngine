using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SharedPickableData", order = 1)]
public class SharedPickableData : ScriptableObject
{
    public string pickableName;
    public string id;
    [SerializeField]
    private DropTable itemDrops;
    [SerializeField]private int minDropCount = 1;
    [SerializeField]private int maxDropCount = 1;

    bool canHandPick = true;
    bool canMachinePick = false;
    public Sprite icon;
    public string description;
    public bool isRenawable;
    public int timeToRenew;
    public int aiTimeToPick;

    public List<SharedItemData> GetItemsFromDrop()
    {
        return itemDrops.dropData.Select(dropData => dropData.itemData).ToList();
    }

    public List<ItemData> CreateDrop()
    {
        return itemDrops.CreateDrop();
    }

    public HashSet<SharedItemData> PreviewDrop()
    {
        return itemDrops.PreviewDrop();
    }

    public int GetDropCount()
    {
        return Random.Range(minDropCount, maxDropCount);
    }
}
