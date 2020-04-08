using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarInUniverse : MonoBehaviour
{
    public int starID;
    public ConstellationTemplate constellationTemplate;
    public Collider2D detection;
    public bool activated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!activated)
            {
                GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
                activated = true;
                foreach(ConstellationData cd in gs.allConstellationData)
                {
                    print("constellationID: " + constellationTemplate.constellationStructure.constellationID);
                    print("DataID: " + cd.constellationID);
                    if(constellationTemplate.constellationStructure.constellationID == cd.constellationID)
                    {
                        print("activated star ID: " + starID);
                        if (!cd.starsDiscovered.Contains(starID))
                        {
                            cd.starsDiscovered.Add(starID);
                        }
                        if (!cd.starsActivated.Contains(starID))
                        {
                            cd.starsActivated.Add(starID);
                        }     
                        GameObject.Find("MapCanvas").GetComponent<DrawMap>().ActiveStars();
                    }
                }
                Toolbox.GetInstance().GetStatManager().SaveState();
            }
        }
    }
}
