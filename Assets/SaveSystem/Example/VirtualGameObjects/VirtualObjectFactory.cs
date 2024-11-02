using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualObjectFactory
{

    public static VirtualGameObject Create(VirtualObjectType objectType, GameObject go)
    {
        switch (objectType)
        {
            case VirtualObjectType.VirtualCitizen:
                Citizen citizen = go.GetComponent<Citizen>();
                // citizen's inventory
                // citizenDestructibleData

                return new VirtualCitizen();
            case VirtualObjectType.VirtualBuildPiece:
                {
                    BuildPiece buildPiece = go.GetComponent<BuildPiece>();
                    Destructible destructible = go.GetComponent<Destructible>();
                    VirtualBuildPiece vBuildPiece = new VirtualBuildPiece(buildPiece.recipe, destructible.sharedDestructibleData);
                    buildPiece.virtualBuildPiece = vBuildPiece;
                    destructible.virtualDestructible = vBuildPiece;
                    return vBuildPiece;
                }
            case VirtualObjectType.VirtualPickable:

                return new VirtualPickable();
            case VirtualObjectType.VirtualCraftingStation:
                {
                    CraftingStation craftingStation = go.GetComponent<CraftingStation>();
                    BuildPiece buildPiece = go.GetComponent<BuildPiece>();
                    Destructible destructible = go.GetComponent<Destructible>();

                    VirtualCraftingStation vCraftingStation = new VirtualCraftingStation(craftingStation.sharedCraftingStationData, buildPiece.recipe, destructible.sharedDestructibleData);
                    go.GetComponent<CraftingStation>().virtualCraftingStation = vCraftingStation;
                    go.GetComponent<BuildPiece>().virtualBuildPiece = vCraftingStation;
                    return vCraftingStation;
                }
            default:
                throw new NotSupportedException($"{objectType} not supported");
        }
    }
}

public enum VirtualObjectType
{
    VirtualCitizen,
    VirtualBuildPiece,
    VirtualDestructible,
    VirtualItem,
    VirtualPickable,
    VirtualAnimal,
    VirtualFuelBurner, // virtual fuel burner
    VirtualCraftingStation,
    VirtualResourceTransformer,
}