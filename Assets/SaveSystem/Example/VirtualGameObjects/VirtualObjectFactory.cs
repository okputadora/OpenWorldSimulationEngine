using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualObjectFactory
{

    public static VirtualGameObject Create(VirtualObjectType objectType)
    {
        switch (objectType)
        {
            case VirtualObjectType.VirtualCitizen:
                return new VirtualCitizen();
            case VirtualObjectType.VirtualBuildPiece:
                return new VirtualBuildPiece();
            case VirtualObjectType.VirtualPickable:
                return new VirtualPickable();
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
    VirtualCampFire
}