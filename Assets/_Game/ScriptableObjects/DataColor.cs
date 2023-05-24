using System.Collections.Generic;
using System;
using UnityEngine;

public enum ColorType { None = 0, Blue = 1, Green = 2, Pink = 3, Red = 4, Gray = 5}

[CreateAssetMenu(menuName = "ScriptableObjects/DataColor", order = 1)]
public class DataColor : ScriptableObject
{
    [SerializeField] private List<Material> mats;

    public Material GetMat(ColorType color)
    {
        return mats[(int)color];
    }
}