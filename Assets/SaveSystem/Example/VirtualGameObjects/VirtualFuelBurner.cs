using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualFuelBurner : VirtualBuildPiece
{
    // @TODO encapsulate the fuel data in FuelBurnerData class
    public float fuelAmount;
    public float fuelBurnRate;
    public float fuelBurnTime;
    public float fuelBurnTimeLeft;
    public bool isBurning;

    public VirtualFuelBurner(BuildPieceRecipe recipe, SharedDestructibleData destructibleData) : base(recipe, destructibleData)
    {

    }
}