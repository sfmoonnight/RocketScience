using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Constellation : MonoBehaviour
{
    public int identity;
    public List<Star> stars;
    public List<TextMeshPro> equitions;

    public Star firstStar;
    public Star lastStar;
    SpriteRenderer constellationSprite;
    List<Vector3> starsPosition;

    bool activated;
    bool showSprite;
    Color color;
    float counter = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        constellationSprite = GetComponent<SpriteRenderer>();
        Star[] st = GetComponentsInChildren<Star>();
        foreach(Star s in st)
        {
            stars.Add(s);
        }

        color = Color.white;
        color.a = 0;
        constellationSprite.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastStar.activated)
        {
            ShowConstellationSprite();
        }
    }

    public void drawConstellation()
    {
        if (!activated)
        {
            firstStar.ActivateSelf();
            activated = true;
        }
    }

    public void ShowConstellationSprite()
    {
        if (!showSprite)
        {
            counter *= 1.05f;
            color.a = Mathf.Lerp(0, 0.8f, counter);
            constellationSprite.color = color;
            if(color.a == 1)
            {
                showSprite = true;
            }
        }
    }
}
