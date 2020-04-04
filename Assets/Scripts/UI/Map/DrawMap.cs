using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMap : MonoBehaviour
{
    GameManager gm;
    GameState gs;
    public GameObject canvasHolder;
    public GameObject mapBackground;
    public GameObject mapConsTemplate;

    List<MapConsTemplate> allConstellationOnMap;
    // Start is called before the first frame update
    void Start()
    {
        gm = Toolbox.GetInstance().GetGameManager();
        gs = Toolbox.GetInstance().GetStatManager().gameState;
        SetUpMap();
        DrawConstellation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUpMap()
    {
        float xyRatio = gm.universeSize.x / gm.universeSize.y;
        float y = canvasHolder.GetComponent<RectTransform>().rect.height;
        float x = xyRatio * y;
        print("height: " + y);
        print("map size: " + new Vector2(x, y));
        mapBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);
    }

    public void DrawConstellation()
    {
        allConstellationOnMap = new List<MapConsTemplate>();
        foreach(ConstellationData cd in gs.allConstellationData)
        {
            GameObject mapCons = Instantiate(mapConsTemplate, mapBackground.transform);
            ConstellationStructure cs = gm.constellationStructures[cd.constellationID];

            float xp = (cd.location.x / gm.universeSize.x) * mapBackground.GetComponent<RectTransform>().sizeDelta.x;
            float yp = (cd.location.y / gm.universeSize.y) * mapBackground.GetComponent<RectTransform>().sizeDelta.y;
            mapCons.GetComponent<RectTransform>().localPosition = new Vector2(xp, yp);

            MapConsTemplate mct = mapCons.GetComponent<MapConsTemplate>();

            allConstellationOnMap.Add(mct);
            mct.ConstructConstellation(cs, mapBackground);
            //Interpolate constellation data here
            print("ratio: " + cd.inUniverseRatio);
            float x = mapBackground.GetComponent<RectTransform>().rect.width * cd.inUniverseRatio.x;
            float y = mapBackground.GetComponent<RectTransform>().rect.height * cd.inUniverseRatio.y;
            float xr = x / mapConsTemplate.GetComponent<RectTransform>().rect.width;
            float yr = y / mapConsTemplate.GetComponent<RectTransform>().rect.height;
            mapCons.GetComponent<RectTransform>().localScale = new Vector2(xr, yr);
        } 
    }
}
