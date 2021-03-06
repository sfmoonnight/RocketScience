﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class ConstellationStructure :ScriptableObject
{
    public int constellationID;
    public string constellationName;
    public List<Vector2> starsPosition;
    public List<Vector2> connectionsEdgeList;
    
    public Dictionary<int, List<int>> connections;

    public float scale;//Used in generate cons in the universe
}
