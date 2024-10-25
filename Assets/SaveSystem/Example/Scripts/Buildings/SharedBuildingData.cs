using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SharedBuildingData", menuName = "Buildings/SharedBuildingData", order = 1)]
public class SharedBuildingData : Requirement // maybe rename to BuildPieceRecipe
{
    public string id;
    // public BuildPieceCategory category;
    // public BuildPieceSubCategory subCategory;
    public string displayName;

    public Sprite icon;
    // public Cost cost;
    // HOW ARE WE GOING TO CHECK THESE DYNAMIC REQUIREMENTS????
    public List<OneOfEachRequirement> oneOfEachRequirements = new List<OneOfEachRequirement>();
    public enum BuildingType
    {
        House = 0,
        LumberYard = 1,
        SawPit = 2,
        CarpentryWorkshop = 3,
        Granary = 4,
        StoreHouse = 5,


    }
    public BuildingType buildingType;
    public Vector3 buildingOrigin;
    public List<VirtualBuildPiece> buildPieces = new List<VirtualBuildPiece>();

    public override bool HasRequirement(int amount)
    {
        return false;
    }

    public BuildingData CreateBuildingData(Vector3 origin)
    {
        return new BuildingData(this, origin);
    }
}


public class BuildCost
{
    public Dictionary<SharedItemData, int> cost = new Dictionary<SharedItemData, int>();
    public void AddItem(Ingredient ingredient)
    {
        if (cost.TryGetValue(ingredient.item, out int currentCost))
        {
            cost[ingredient.item] = currentCost + ingredient.amount;
            return;
        }
        cost.Add(ingredient.item, ingredient.amount);
    }
}
// scriptable object?
public class BuildingType
{
    public string name;
    public string id;
    public List<List<Requirement>> oneOfEachRequirements = new List<List<Requirement>>();
}
