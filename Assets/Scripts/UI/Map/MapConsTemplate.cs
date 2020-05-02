using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapConsTemplate : MonoBehaviour
{
    public GameObject starOnMap;
    public LineRenderer mapLine;
    public Image image;

    GameManager gm;
    GameState gs;
    public List<GameObject> stars;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConstructConstellation(ConstellationStructure constellationStructure, GameObject mapBackground)
    {
        gm = Toolbox.GetInstance().GetGameManager();
        gs = Toolbox.GetInstance().GetStatManager().gameState;
        stars = new List<GameObject>();

        //print(gm);
        GetComponent<Image>().sprite = gm.constellationPrefabs[constellationStructure.constellationID].GetComponent<SpriteRenderer>().sprite;

        for (int i = 0; i< constellationStructure.starsPosition.Count; i++)
        {
            GameObject star = Instantiate(starOnMap, mapBackground.transform);
            //StarOnMap som = star.GetComponent<StarInUniverse>();
            stars.Add(star);
            //print("StarCount: " + gs.allConstellationData[constellationStructure.constellationID].starsLocation.Count);
            Vector2 universePos = gs.allConstellationData[constellationStructure.constellationID].starsLocation[i];
            float x = (universePos.x / gm.universeSize.x) * mapBackground.GetComponent<RectTransform>().sizeDelta.x;
            float y = (universePos.y / gm.universeSize.y) * mapBackground.GetComponent<RectTransform>().sizeDelta.y;

            star.GetComponent<StarOnMap>().universePosition = universePos;
            star.GetComponent<RectTransform>().localPosition = new Vector2(x, y);
            
            //som.starID = stars.IndexOf(som);
            //som.constellationTemplate = this;

            /*float xr = GetComponent<RectTransform>().sizeDelta.x / 10; //100 pixel per unity unit
            float yr = GetComponent<RectTransform>().sizeDelta.y / 10;
            print("Star ratio: " + xr);
            star.transform.localPosition = new Vector2(p.x * xr, p.y * yr);*/
            //star.transform.localPosition = p;
            //star.transform.localScale = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);
        }
    }

    public void ShowConstellationOnMap()
    {
        image.enabled = true;
    }
}
