using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Constellation : MonoBehaviour
{
    public int identity;
    public string name;
    public List<Star> stars;
    public List<TextMeshPro> equitions;

    public Star firstStar;
    public Star lastStar;
    public SpriteRenderer constellationSprite;
    List<Vector3> starsPosition;

    public bool activated;
    public bool showSprite;
    Color color;
    float counter = 0.01f;
    // Start is called before the first frame update
    private void Awake()
    {
        Star[] st = GetComponentsInChildren<Star>();
        foreach (Star s in st)
        {
            stars.Add(s);
        }
    }
    void Start()
    {
        constellationSprite = GetComponent<SpriteRenderer>();
        

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
            GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
            firstStar.ActivateSelf();
            Event newEvent = new Event(Event.EventType.NewConstellation, System.DateTime.Now.ToString(), identity);
            gs.events.Add(newEvent);
            gs.constellationsDiscovered.Add(identity);
            gs.constellationsNotDiscovered.Remove(identity);
 
            if (!gs.telescopeActivated)
            {
                gs.collected.Add(-1);
                Event e = new Event(Event.EventType.NewCollectible, System.DateTime.Now.ToString(), -1);
                Toolbox.GetInstance().GetStatManager().gameState.events.Add(e);

                gs.telescopeActivated = true;
            }
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
            if(color.a == 0.8f)
            {
                showSprite = true;
            }
        }
    }
}
