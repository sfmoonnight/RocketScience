using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UniverseManager : MonoBehaviour
{
    public GameObject constellationTemplet;
    float patchSize = 150f;
    float spacing = 50;

    int planetNumber = 1;
    // Start is called before the first frame update

    bool numbersAttached = false;
    private void Awake()
    {
        
    }
    void Start()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        GameObject.Find("TheUniverse").GetComponent<SpriteRenderer>().size = Toolbox.GetInstance().GetGameManager().universeSize;
        //print("Count2: " + Toolbox.GetInstance().GetStatManager().gameState.events.Count);
        //print("We out " + Toolbox.GetInstance().GetStatManager().gameState.eatshit);
        if (Toolbox.GetInstance().GetStatManager().gameState.allPlanetData.Count == 0)
        {
            GenerateRandomPlanets(-patchSize, -patchSize, patchSize, patchSize, spacing);
            GenerateConstellations();
        }
        else
        {
            ReloadMain();
        }
        GameObject.Find("MapCanvas").GetComponent<DrawMap>().DrawConstellation();

        if (Toolbox.GetInstance().GetGameManager().dungeonProgressTemp > gs.keyDungeonProgress)
        {
            gs.keyDungeonProgress += 1;
            Toolbox.GetInstance().GetGameManager().UpdateQuestCollectible(-gs.keyDungeonProgress);
            Toolbox.GetInstance().GetStatManager().SaveState();
        }
        if (gs.telescopeActivated)
        {
            Toolbox.GetInstance().GetGameManager().UpdateQuestCollectible(-1);
        }
        GameManager gm = Toolbox.GetInstance().GetGameManager();
        NumberGenerator ng = gm.rocket.GetComponent<NumberGenerator>();
        ng.GenerateRandomNumbers(-ng.patchSize, -ng.patchSize, ng.patchSize, ng.patchSize, ng.spacing);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
        if (!numbersAttached)
        {
            Number[] numbers = GameObject.FindObjectsOfType<Number>();
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i].AttachToNearestPlanet();
            }
            numbersAttached = true;
        }
    }

    public void ReloadMain()
    {
        //print("--------Reloading Main");
        GameManager gm = Toolbox.GetInstance().GetGameManager();
        gm.rocket = GameObject.Find("Rocket").GetComponent<Rocket>();
        Toolbox.GetInstance().GetStatManager().LoadState();
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        //print(gs.answer);
        //print(gs.playerPosition);

        gm.answer = gs.answer;
        gm.rocket.transform.position = gs.playerPosition;
        GameObject.Find("Money").GetComponent<Text>().text = gs.money.ToString();

        //print(gs.allPlanetData.Count);
        foreach (PlanetData pd in gs.allPlanetData)
        {
            gm.planets = new List<Question>();
            //print("-------Recreating Planets");
            //print(pd.planetPrefabID);
            GameObject planet = Instantiate(gm.planetPrefabs[pd.planetPrefabID]);
            Question q = planet.GetComponent<Question>();
            gm.planets.Add(q);
            q.SetUpPlanet(pd);
        }

        foreach (ConstellationData cd in gs.allConstellationData)
        {
            GameObject cons = Instantiate(constellationTemplet);
            print("constelation id: " + cd.constellationID);
            print("constellation sructure numbers: " + gm.constellationStructures.Count);
            cons.GetComponent<ConstellationTemplate>().SetUpConstellation(cd.location, gm.constellationStructures[cd.constellationID]);
        }

        //print("Reloading main");
        if (Toolbox.GetInstance().GetStatManager().gameState.dungeonEntered != 0)
        {
            //print("-------reload dungeon" + Toolbox.GetInstance().GetStatManager().gameState.dungeonEntered);
            foreach (Question q in gm.planets)
            {
                if (q.planetID == Toolbox.GetInstance().GetStatManager().gameState.dungeonEntered)
                {
                    q.ActivateCollectables();
                }
            }
        }
        
    }

    public void GenerateRandomPlanets(float left, float top, float right, float bottom, float spacing)
    {
        float genprob = 0.6f;
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
        print("------PlanetNumber: " + Toolbox.GetInstance().GetGameManager().planets.Count);
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

            //TODO: setup each planet - (identity), environment, (collectible pool) etc.
            Question q = planet.GetComponent<Question>();
            q.planetID = planetNumber;
            planetNumber += 1;
            q.GenerateCollectables();
            Toolbox.GetInstance().GetGameManager().planets.Add(planet.GetComponent<Question>());

            PlanetData pd = new PlanetData(i, q.planetID, planet.transform.position, q.openDungeon, q.options, q.collectiblesID, q.pointsWithCollectibles);
            //print("Adding");
            Toolbox.GetInstance().GetStatManager().gameState.allPlanetData.Add(pd);
            print(Toolbox.GetInstance().GetStatManager().gameState.allPlanetData.Count);
        }
    }

    public void GenerateConstellations()
    {
        GameManager gm = Toolbox.GetInstance().GetGameManager();
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        foreach (ConstellationStructure cs in gm.constellationStructures)
        {
            GameObject cons = Instantiate(constellationTemplet);
            //cons.GetComponent<ConstellationTemplate>().constellationStructure = cs;
            gs.constellationsNotDiscovered.Add(cs.constellationID);

            Vector2 position = new Vector2(50, 50);
            ConstellationData cd = new ConstellationData(cs.constellationID, position, false, new List<Vector2>(), new List<int>(), new List<int>(), new Vector2());
            gs.allConstellationData.Add(cd);

            cons.GetComponent<ConstellationTemplate>().SetUpConstellation(position, cs);
            //print("cons size: " + cons.GetComponent<SpriteRenderer>().bounds.size);
        }
    }
} 
