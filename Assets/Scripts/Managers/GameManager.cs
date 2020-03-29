using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class GameManager : MonoBehaviour
{
    public int answer;
    public Rocket rocket;
    //GameObject[] questions;
    public List<Collectable> collectibles; //all collectibles of the game
    public List<Collectable> keyCollectibles;//all key collectibles of the game
    public List<Question> planets;//---including all the planets and structure
    public List<int> constellationNotDiscovered;//constellations already discovered
    public List<int> constellationDiscovered;//constellations already discovered

    //---Prefabs from Resource folder
    public List<GameObject> collectiblePrefabs;
    public List<GameObject> planetPrefabs;
    public List<GameObject> constellationPrefabs;
    public bool inDungeon;

    public bool universeCreated;

    public int dungeonProgressTemp;

    private void Awake()
    {
        universeCreated = false;
        planets = new List<Question>();

        answer = Random.Range(-99, 100);
        rocket = GameObject.Find("Rocket").GetComponent<Rocket>();  
        
        LoadAllCollectibles();
        LoadAllPlanets();
        LoadAllConstellations();

        //GameObject.Find("CaptainLog").GetComponent<PseudoEvents>().CreatePseudoEvents();
        //UpdateQuestions();
    }
    // Start is called before the first frame update
    void Start()
    {
        

        dungeonProgressTemp = Toolbox.GetInstance().GetStatManager().gameState.keyDungeonProgress;
        //GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        //answer = gs.answer;
        if(Toolbox.GetInstance().GetStatManager().gameState.events.Count == 0)
        {
            //GameObject.Find("CaptainLog").GetComponent<PseudoEvents>().CreatePseudoEvents();
        }      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void loadCollectibles()
    //{
    //    collectibles = new List<GameObject>(Resources.LoadAll<GameObject>("Assets/Prefabs/Collectibles"));
    //    Debug.Assert(collectibles.Count > 0);
    //}
    public void ReloadMain()
    {
        //print("--------Reloading Main");
        //Toolbox.GetInstance().GetStatManager().LoadState();
        rocket = GameObject.Find("Rocket").GetComponent<Rocket>();
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        //print(gs.answer);
        //print(gs.playerPosition);
        
        answer = gs.answer;
        rocket.transform.position = gs.playerPosition;
        GameObject.Find("Money").GetComponent<Text>().text = gs.money.ToString();

        //print(gs.allPlanetData.Count);
        foreach (PlanetData pd in gs.allPlanetData)
        {
            //print("-------Recreating Planets");
            //print(pd.planetPrefabID);
            GameObject planet = Instantiate(planetPrefabs[pd.planetPrefabID]);
            Question q = planet.GetComponent<Question>();
            q.SetUpPlanet(pd);
        }

        //print("Reloading main");
        if (Toolbox.GetInstance().GetStatManager().gameState.dungeonEntered != 0)
        {
            //print("-------reload dungeon" + Toolbox.GetInstance().GetStatManager().gameState.dungeonEntered);
            foreach (Question q in planets)
            {
                if(q.planetID == Toolbox.GetInstance().GetStatManager().gameState.dungeonEntered)
                {
                    q.ActivateCollectables();
                }
            }
        }
    }

    public void LoadAllCollectibles()
    {
        Object[] availableCollectibles = Resources.LoadAll("Collectibles");
        collectibles = new List<Collectable>();
        foreach (Object o in availableCollectibles)
        {
            GameObject go = (GameObject)o;
            Collectable col = go.GetComponent<Collectable>();
            
            collectibles.Add(col);
            //collectiblePrefabs.Add(go);
        }
        //collectibles.Sort();

        Object[] availableKeyCollectibles = Resources.LoadAll("KeyCollectibles");
        keyCollectibles = new List<Collectable>();
        foreach (Object o in availableKeyCollectibles)
        {
            GameObject go = (GameObject)o;
            Collectable col = go.GetComponent<Collectable>();

            keyCollectibles.Add(col);
        }
    }

    public void LoadAllPlanets()
    {
        Object[] availablePlanets = Resources.LoadAll("Planets");
        planetPrefabs = new List<GameObject>();
        foreach (Object o in availablePlanets)
        {
            GameObject go = (GameObject)o;
            //Question q = go.GetComponent<Question>();

            planetPrefabs.Add(go);   
        }
    }

    public void LoadAllConstellations()
    {
        Object[] availableConstellations = Resources.LoadAll("Constellations");
        constellationPrefabs = new List<GameObject>();
        foreach (Object o in availableConstellations)
        {
            GameObject go = (GameObject)o;
            Constellation con = go.GetComponent<Constellation>();

            constellationPrefabs.Add(go);
            constellationNotDiscovered.Add(con.identity);
        }
    }

    public List<Collectable> GetAllCollectibles()
    {
        return collectibles;
    }

    public List<Collectable> GetAllKeyCollectibles()
    {
        return keyCollectibles;
    }

    public void UpdateQuestCollectible(int identity)
    {
        List<Quest> quests = Toolbox.GetInstance().GetStatManager().gameState.quests;
        int currIdx = Toolbox.GetInstance().GetStatManager().gameState.currQuestIndex;
        for (int i = currIdx; i < quests.Count; i++)
        {
            Quest q = quests[i];
            foreach (QuestCollectible qc in q.collectibles)
            {
                if (qc.identity == identity && !qc.collected)
                {
                    qc.collected = true;
                    GameObject.Find("QuestPanel").GetComponent<QuestPanelManager>().UpdateQuests();
                    return;
                }
            }
        }

        for (int i = 0; i < currIdx; i++)
        {
            Quest q = quests[i];
            foreach (QuestCollectible qc in q.collectibles)
            {
                if (qc.identity == identity)
                {
                    qc.collected = true;
                    GameObject.Find("QuestPanel").GetComponent<QuestPanelManager>().UpdateQuests();
                    return;
                }
            }
        }

    }

    /*public void UpdateQuestions()
    {
        questions = null;
        questions = GameObject.FindGameObjectsWithTag("question");
    }*/

    public void SetAnswer(int ans)
    {
        answer = ans;
        //print("Questions: " + questions);
        if (!inDungeon)
        {
            foreach (Question go in planets)
            {
                EquationManager em = go.eqTextMeshObj.GetComponent<EquationManager>();
                if (em.equation.answer == answer) // TODO: null reference exception here
                {
                    go.ActivateCollectables();
                }
                else
                {
                    go.DeactivateCollectables();
                }
            }
        }    
    }

    //---Add quest to quest panel
    public void PickUpQuest(NewQuest nq)
    {
        Quest q = new Quest();
        if (nq is FirstKeyQuest)
        {
            q.keyQuest = true;
        }
        q.collectibles = nq.collectibles;
        Toolbox.GetInstance().GetStatManager().gameState.quests.Add(q);
        GameObject.Find("QuestPanel").GetComponent<QuestPanelManager>().UpdateQuests();
    }

    public void PickUpQuest(Quest q)
    {
        Toolbox.GetInstance().GetStatManager().gameState.quests.Add(q);
        GameObject.Find("QuestPanel").GetComponent<QuestPanelManager>().UpdateQuests();
    }

    public Rocket GetRocket()
    {
        return rocket;
    }


    

}
