using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CraftingStation : BuildPiece, IInteractable
{
    public SharedCraftingStationData sharedCraftingStationData;
    [System.NonSerialized] public VirtualCraftingStation virtualCraftingStation;
    private bool isActive = false;
    private bool inUse;

    private void Awake()
    {
        // m_buildPiece = GetComponent<BuildPiece>();
    }

    public List<Recipe> GetAvailableRecipes()
    {
        return new List<Recipe>();
    }

    public string GetHoverText()
    {
        return "[E] to interact";
    }
    public GameObject GetHoverUI()
    {
        // if (m_buildPiece.m_data.m_isGhost) return m_buildPiece.GetHoverUI();
        return null;
    }

    public bool IsHoverable() => true;
    public void Initialize() { }

    public void Interact()
    {
        inUse = true;
    }

}

