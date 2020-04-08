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
        
        SetUpMap();
        //DrawConstellation();
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
        print("Draw constellation on map");
        gs = Toolbox.GetInstance().GetStatManager().gameState;
        gm = Toolbox.GetInstance().GetGameManager();
        allConstellationOnMap = new List<MapConsTemplate>();
        print("cons discovered: " + gs.constellationsDiscovered.Count);

        foreach (ConstellationData cd in gs.allConstellationData)
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
        foreach (int i in gs.constellationsDiscovered)
        {
            allConstellationOnMap[i].ShowConstellationOnMap();
        }

        ActiveStars();
    }

    public void ActiveStars()
    {
        gs = Toolbox.GetInstance().GetStatManager().gameState;
        //print("drawMap activate stars");
        foreach (ConstellationData cd in gs.allConstellationData)
        {
            //print("drawMap activate stars count: " + cd.starsActivated.Count);
            if (cd.starsDiscovered.Count > 0)
            {
                //print("drawMap activate stars > 0");
                foreach (int i in cd.starsDiscovered)
                {
                    allConstellationOnMap[cd.constellationID].stars[i].GetComponent<StarOnMap>().ShowStarOnMap();
                }
            }

            if (cd.starsActivated.Count > 0)
            {
                //print("drawMap activate stars > 0");
                foreach (int i in cd.starsActivated)
                {
                    allConstellationOnMap[cd.constellationID].stars[i].GetComponent<StarOnMap>().Activate();
                }
            }
        }
    }
}
