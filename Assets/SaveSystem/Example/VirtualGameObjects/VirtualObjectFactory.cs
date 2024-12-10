using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class VirtualObjectFactory
{

    public static VirtualGameObject Create(VirtualObjectType objectType, GameObject go)
    {
        switch (objectType)
        {
            // probably need a different way to instantiate virtual citizens because we need to include their settlement and civilization date
            // or maybe we just add that to them in the Settlement class
            case VirtualObjectType.VirtualCitizen:
                Citizen citizen = go.GetComponent<Citizen>();
                // Debug.Log("new virtual citizen: " + Utils.GetPrefabName(go));
                VirtualCitizen vc = new VirtualCitizen();
                vc.InitializeBehaviorTree(go);
                citizen.virtualCitizen = vc;

                // citizen's inventory
                // citizenDestructibleData
                return vc;
            case VirtualObjectType.VirtualItem:
                {
                    Item item = go.GetComponent<Item>();
                    VirtualItem virtualItem = new VirtualItem(item.sharedItemData);
                    item.virtualItem = virtualItem;
                    return virtualItem;
                }
            case VirtualObjectType.VirtualDestructible:
                {
                    Destructible destructible = go.GetComponent<Destructible>();
                    VirtualDestructible vDestructible = new VirtualDestructible(destructible.sharedDestructibleData);
                    destructible.virtualDestructible = vDestructible;
                    return vDestructible;
                }
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
                {
                    Pickable pickable = go.GetComponent<Pickable>();
                    Destructible destructible = go.GetComponent<Destructible>();
                    VirtualPickable vPickable = new VirtualPickable(pickable.sharedPickableData);
                    pickable.virtualPickable = vPickable;
                    return vPickable;
                }
            case VirtualObjectType.VirtualCraftingStation:
                {
                    CraftingStation craftingStation = go.GetComponent<CraftingStation>();
                    BuildPiece buildPiece = go.GetComponent<BuildPiece>();
                    Destructible destructible = go.GetComponent<Destructible>();

                    VirtualCraftingStation vCraftingStation = new VirtualCraftingStation(craftingStation.sharedCraftingStationData, buildPiece.recipe, destructible.sharedDestructibleData);
                    craftingStation.virtualCraftingStation = vCraftingStation;
                    buildPiece.virtualBuildPiece = vCraftingStation;
                    destructible.virtualDestructible = vCraftingStation;
                    return vCraftingStation;
                }
            case VirtualObjectType.VirtualStorgage:
                {
                    Debug.Log("Creating virtual storage");
                    Storage storage = go.GetComponent<Storage>();
                    BuildPiece buildPiece = go.GetComponent<BuildPiece>();
                    Destructible destructible = go.GetComponent<Destructible>();

                    VirtualStorage vStorage = new VirtualStorage(storage.sharedStorageData, buildPiece.recipe, destructible.sharedDestructibleData);
                    storage.virtualStorage = vStorage;
                    buildPiece.virtualBuildPiece = vStorage;
                    // destructible.virtualDestructible = vStorage;
                    return vStorage;
                }
            case VirtualObjectType.VirtualAnimal:
                {
                    Animal animal = go.GetComponent<Animal>();
                    Destructible destructible = go.GetComponent<Destructible>();
                    VirtualAnimal vAnimal = new VirtualAnimal(animal.sharedAnimalData, destructible.sharedDestructibleData);
                    animal.virtualAnimal = vAnimal;
                    destructible.virtualDestructible = vAnimal;
                    return vAnimal;
                }
            // case VirtualObjectType.VirtualFuelBurner:
            //     {
            //         FuelBurner fuelBurner = go.GetComponent<FuelBurner>();
            //         Destructible destructible = go.GetComponent<Destructible>();
            //         VirtualFuelBurner vFuelBurner = new VirtualFuelBurner(fuelBurner.sharedFuelBurnerData, destructible.sharedDestructibleData);
            //         fuelBurner.virtualFuelBurner = vFuelBurner;
            //         destructible.virtualDestructible = vFuelBurner;
            //         return vFuelBurner;
            //     }
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
    VirtualStorgage,
}