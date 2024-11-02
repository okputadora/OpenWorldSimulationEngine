[System.Serializable]
public class VirtualCraftingStation : VirtualBuildPiece
{
  // shared crafting station data?
  public SharedCraftingStationData sharedCraftingStationData;
  // this one's a little different, it doesnt require CraftingStationData, just shared crafting station data,
  // this could change if we add stuff like upgradeLevel, or other crafting station specific data
  public VirtualCraftingStation(SharedCraftingStationData sharedCraftingData, BuildPieceRecipe recipe, SharedDestructibleData sharedDestructibleData) : base(recipe, sharedDestructibleData)
  {
    this.sharedCraftingStationData = sharedCraftingData;
    // sharedCraftingStationData = new SharedCraftingStationData();
  }
}