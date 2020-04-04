using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationTemplate : MonoBehaviour
{
    public ConstellationStructure constellationStructure;
    public GameObject starInUniverse;

    GameManager gm;
    List<StarInUniverse> stars;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
