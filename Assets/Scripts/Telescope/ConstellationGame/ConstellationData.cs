using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConstellationData
{
    public int constellationID;
    //public string consName;
    public Vector3 location;
    public bool discovered;
    public List<Vector2> starsLocation;
    public List<string> starsName;
    public List<int> starsDiscovered;
    public List<int> starsActivated;
    public Vector2 inUniverseRatio;

    public ConstellationData(int id, Vector3 pos, bool discovered, List<Vector2> starsPos, List<string> starsName, List<int> discoveredStars, List<int> activeStars, Vector2 ratio)
    {
        this.constellationID = id;
        //this.consName = "Unknown";
        this.location = pos;
        this.discovered = discovered;
        this.starsLocation = starsPos;
        this.starsName = starsName;
        this.starsDiscovered = discoveredStars;
        this.starsActivated = activeStars;
        this.inUniverseRatio = ratio;
    }
}
