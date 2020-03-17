using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    bool initialized = false;

    float patchSize = 150f;
    float spacing = 50;

    int planetNumber = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        if(Toolbox.GetInstance().GetStatManager().gameState.allPlanetData.Count == 0)
        {
            GenerateRandomPlanets(-patchSize, -patchSize, patchSize, patchSize, spacing);
        }
        else
        {
            Toolbox.GetInstance().GetGameManager().ReloadMain();
        }
        
        /*if (Toolbox.GetInstance().GetGameManager().universeCreated)
        {
            print("------level manager start");
            Toolbox.GetInstance().GetStatManager().LoadState();
            print("--------dungeonEntered" + Toolbox.GetInstance().GetStatManager().gameState.dungeonEntered);
            Toolbox.GetInstance().GetGameManager().ReloadMain();
        }*/
        //Toolbox.GetInstance().GetGameManager().UpdateQuestions();   
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!initialized && Toolbox.GetInstance().GetGameManager().universeCreated)
        {
            print("------level manager start");
            Toolbox.GetInstance().GetStatManager().LoadState();
            print("--------dungeonEntered" + Toolbox.GetInstance().GetStatManager().gameState.dungeonEntered);
            Toolbox.GetInstance().GetGameManager().ReloadMain();
            initialized = true;
        }*/
    }

    public void GenerateRandomPlanets(float left, float top, float right, float bottom, float spacing)
    {
        float genprob = 0.2f;
        for (float i = left; i < right; i += spacing)
        {
            for (float j = top; j < bottom; j += spacing)
            {
                float cx = i + spacing / 2f;
                float cy = j + spacing / 2f;
                GenerateRandomPlanet(cx, cy, spacing / 4f, genprob);
                //print("Placing new at " + cx + " " + cy);
            }
        }
        print("------UniverseCreated");
        Toolbox.GetInstance().GetStatManager().SaveState();
    }

    public void GenerateRandomPlanet(float cx, float cy, float perturb, float showProb)
    {
        float p = Random.Range(0f, 1f);
        if (p < showProb)
        {
            int i = Random.Range(0, Toolbox.GetInstance().GetGameManager().planetPrefabs.Count);
            //print("-------" + planetPrefabs.Count);
            //print(i);
            GameObject planet = Instantiate(Toolbox.GetInstance().GetGameManager().planetPrefabs[i]);

            float x = cx + Random.Range(-perturb, perturb);
            float y = cy + Random.Range(-perturb, perturb);
            planet.transform.position = new Vector3(x, y, 0);

            //TODO: setup each planet - identity, environment, collectible pool etc.
            Question q = planet.GetComponent<Question>();
            q.planetID = planetNumber;
            planetNumber += 1;

            Toolbox.GetInstance().GetGameManager().planets.Add(planet.GetComponent<Question>());

            PlanetData pd = new PlanetData(i, q.planetID, planet.transform.position, q.openDungeon, q.options, q.collectables);
            print("Adding");
            Toolbox.GetInstance().GetStatManager().gameState.allPlanetData.Add(pd);
            print(Toolbox.GetInstance().GetStatManager().gameState.allPlanetData.Count);
        }
    }
}
