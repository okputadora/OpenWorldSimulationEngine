using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// [RequireComponent(typeof(BuildPiece))]
public class CraftingStation : MonoBehaviour, IInteractable
{

    public Sprite stationIcon;
    public string description;
    private bool isActive = false;
    private BuildPiece m_buildPiece;
    private bool inUse;

    private void Awake()
    {
        m_buildPiece = GetComponent<BuildPiece>();
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

