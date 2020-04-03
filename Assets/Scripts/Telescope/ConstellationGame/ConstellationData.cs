using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConstellationData
{
    public int constellationID;
    public Vector3 location;
    public bool discovered;
    public List<int> starsActivated;

    public ConstellationData(int id, Vector3 pos, bool discovered, List<int> activeStars)
    {
        this.constellationID = id;
        this.location = pos;
        this.discovered = discovered;
        this.starsActivated = activeStars;
    }
}
