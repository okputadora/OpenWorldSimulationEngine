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
        LumberYard = 1,
        SawPit = 2,
        CarpentryWorkshop = 3,
        Granary = 4,
        StoreHouse = 5,


    }
    public BuildingType buildingType;

    public override bool HasRequirement(int amount)
    {
        return false;
    }
}