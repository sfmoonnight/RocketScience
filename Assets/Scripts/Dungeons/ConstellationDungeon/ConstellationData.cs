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
}
