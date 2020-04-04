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
        transform.position = position;
        GetComponent<SpriteRenderer>().sprite = gm.constellationPrefabs[constellationStructure.constellationID].GetComponent<SpriteRenderer>().sprite;
        stars = new List<StarInUniverse>();
        transform.localScale = constellationStructure.scale * Vector3.one;
        foreach (Vector2 p in constellationStructure.starsPosition)
        {
            GameObject star = Instantiate(starInUniverse, transform);
            StarInUniverse som = star.GetComponent<StarInUniverse>();
            stars.Add(som);
            som.starID = stars.IndexOf(som);
            som.constellationTemplate = this;
            star.transform.localPosition = p; 
            star.transform.localScale = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);

            GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
            gs.allConstellationData[constellationStructure.constellationID].starsLocation.Add(star.transform.position);
        }

        //print("constellation size: " + GetComponent<SpriteRenderer>().bounds.size.x);
    }
}
