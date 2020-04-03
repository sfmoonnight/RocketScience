using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationTemplate : MonoBehaviour
{
    public ConstellationStructure constellationStructure;
    public GameObject starOnMap;

    List<StarOnMap> stars;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpConstellation()
    {
        stars = new List<StarOnMap>();
        transform.localScale = constellationStructure.scale * Vector3.one;
        foreach (Vector2 p in constellationStructure.starsPosition)
        {
            GameObject star = Instantiate(starOnMap, transform);
            StarOnMap som = star.GetComponent<StarOnMap>();
            stars.Add(som);
            som.starID = stars.IndexOf(som);
            som.constellationTemplate = this;
            star.transform.localPosition = p; 
            star.transform.localScale = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);
        } 
    }
}
