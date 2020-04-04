﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConstellationData
{
    public int constellationID;
    public Vector3 location;
    public bool discovered;
    public List<Vector2> starsLocation;
    public List<int> starsActivated;
    public Vector2 inUniverseRatio;

    public ConstellationData(int id, Vector3 pos, bool discovered, List<Vector2> starsPos, List<int> activeStars, Vector2 ratio)
    {
        this.constellationID = id;
        this.location = pos;
        this.discovered = discovered;
        this.starsLocation = starsPos;
        this.starsActivated = activeStars;
        this.inUniverseRatio = ratio;
    }
}
