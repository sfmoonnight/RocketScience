using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlanetData
{
    public int planetPrefabID;
    public int planetID;
    public Vector3 location;
    public bool ifDungeonOpened;
    public List<int> collectibleOptions;
    public List<int> collectiblesGenerated;
    public List<int> generationPoints;

    public PlanetData(int planetPrefabID, int planetID, Vector3 location, bool ifDungeonOpened, List<int> collectibleOptions, List<int> collectiblesGenerated, List<int> generationPoints)
    {
        this.planetPrefabID = planetPrefabID;
        this.planetID = planetID;
        this.location = location;
        this.ifDungeonOpened = ifDungeonOpened;
        this.collectibleOptions = collectibleOptions;
        this.collectiblesGenerated = collectiblesGenerated;
        this.generationPoints = generationPoints;
    }
}
