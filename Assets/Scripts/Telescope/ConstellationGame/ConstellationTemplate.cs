using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationTemplate : MonoBehaviour
{
    public ConstellationStructure constellationStructure;
    public GameObject starInUniverse;
    public bool firstTimeSetup; //Only used to set up a new in-universe constellation prefab

    //public int id;
    GameManager gm;
    public List<StarInUniverse> stars;
    // Start is called before the first frame update
    void Start()
    {
        if (firstTimeSetup)
        {
            SetUpConstellation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecordFeatures()
    {
        int id = constellationStructure.constellationID;
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        gm = Toolbox.GetInstance().GetGameManager();
        gs.allConstellationData[id].location = transform.position;

        float xSize = GetComponent<SpriteRenderer>().bounds.size.x;
        float ySize = GetComponent<SpriteRenderer>().bounds.size.y;
        float xRatio = xSize / gm.universeSize.x;
        float yRatio = ySize / gm.universeSize.y;
        Vector2 ratio = new Vector2(xRatio, yRatio);
        gs.allConstellationData[id].inUniverseRatio = ratio;
        print("id:" + id);
        print("dataID: " + gs.allConstellationData[id].constellationID);
        if (gs.allConstellationData[id].starsLocation.Count == 0)
        {
            foreach (StarInUniverse s in stars)
            {
                gs.allConstellationData[id].starsLocation.Add(s.transform.position);
            }
        }
        else
        {
            /*
            foreach (StarInUniverse s in stars)
            {
                gs.allConstellationData[id].starsLocation[s.starID] = s.transform.position;
            }*/
        } 
    }
    public void SetUpConstellation()
    {
        int id = constellationStructure.constellationID;
        gm = Toolbox.GetInstance().GetGameManager();
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        //print("constellation data count: " + gs.allConstellationData.Count);

        gs.allConstellationData[id].location = transform.position;
        //ConstellationData cd = new ConstellationData(constellationStructure.constellationID, transform.position, false, new List<Vector2>(), new List<int>(), new List<int>(), new Vector2());

        GetComponent<SpriteRenderer>().sprite = gm.constellationPrefabs[constellationStructure.constellationID].GetComponent<SpriteRenderer>().sprite;
        stars = new List<StarInUniverse>();
        transform.localScale = constellationStructure.scale * Vector3.one;

        float xSize = GetComponent<SpriteRenderer>().bounds.size.x;
        float ySize = GetComponent<SpriteRenderer>().bounds.size.y;
        float xRatio = xSize / gm.universeSize.x;
        float yRatio = ySize / gm.universeSize.y;
        Vector2 ratio = new Vector2(xRatio, yRatio);
        gs.allConstellationData[id].inUniverseRatio = ratio;

        gs.allConstellationData[id].starsLocation = new List<Vector2>();
        foreach (Vector2 p in constellationStructure.starsPosition)
        {
            GameObject star = Instantiate(starInUniverse, transform);
            StarInUniverse som = star.GetComponent<StarInUniverse>();
            stars.Add(som);
            som.starID = stars.IndexOf(som);
            som.constellationTemplate = this;
            star.transform.localPosition = p;
            star.transform.localScale = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);

            gs.allConstellationData[id].starsLocation.Add(star.transform.position);
        }

        //Toolbox.GetInstance().GetStatManager().gameState.allConstellationData.Add(cd);
        //Toolbox.GetInstance().GetStatManager().gameState.constellationsNotDiscovered.Add(constellationStructure.constellationID);
    }

    public void SetUpConstellation(Vector2 position, ConstellationStructure constellationStructure)
    {
        gm = Toolbox.GetInstance().GetGameManager();
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        ConstellationData cd = gs.allConstellationData[constellationStructure.constellationID];
        this.constellationStructure = constellationStructure;
        transform.position = position;

        GetComponent<SpriteRenderer>().sprite = gm.constellationPrefabs[constellationStructure.constellationID].GetComponent<SpriteRenderer>().sprite;
        stars = new List<StarInUniverse>();
        transform.localScale = constellationStructure.scale * Vector3.one;

        float xSize = GetComponent<SpriteRenderer>().bounds.size.x;
        float ySize = GetComponent<SpriteRenderer>().bounds.size.y;
        float xRatio = xSize / gm.universeSize.x;
        float yRatio = ySize / gm.universeSize.y;
        Vector2 ratio = new Vector2(xRatio, yRatio);
        cd.inUniverseRatio = ratio;
        
        cd.starsLocation = new List<Vector2>();
        foreach (Vector2 p in constellationStructure.starsPosition)
        {  
            GameObject star = Instantiate(starInUniverse, transform);
            StarInUniverse som = star.GetComponent<StarInUniverse>();
            stars.Add(som);
            som.starID = stars.IndexOf(som);
            som.constellationTemplate = this;
            star.transform.localPosition = p; 
            star.transform.localScale = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);
            
            cd.starsLocation.Add(star.transform.position);
        }

        //print("constellation size: " + GetComponent<SpriteRenderer>().bounds.size.x);
    }

  
}
