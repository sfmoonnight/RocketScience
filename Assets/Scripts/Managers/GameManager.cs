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
    public Vector2 universeSize;

    public List<Collectable> collectibles; //all collectibles of the game
    public List<Collectable> keyCollectibles;//all key collectibles of the game
    public List<Question> planets;//---including all the planets and structure
    //public List<int> constellationNotDiscovered;//constellations not discovered
    //public List<int> constellationDiscovered;//constellations already discovered

    //---Prefabs from Resource folder
    public List<GameObject> collectiblePrefabs;
    public List<GameObject> planetPrefabs;
    public List<GameObject> constellationPrefabs;
    public List<Sprite> keySprites;
    public List<ConstellationStructure> constellationStructures;
    public bool inDungeon;

    public bool universeCreated;

    public int dungeonProgressTemp;

    private void Awake()
    {
        universeCreated = false;
        planets = new List<Question>();
        //constellationNotDiscovered = new List<int>();
        //constellationDiscovered = new List<int>();
        universeSize = new Vector2(1400, 850);
        //answer = Random.Range(-99, 100);
        rocket = GameObject.Find("Rocket").GetComponent<Rocket>();

        LoadAllCollectibles();
        LoadAllPlanets();
        LoadAllConstellations();
        LoadAllKeySprites();

        //GameObject.Find("CaptainLog").GetComponent<PseudoEvents>().CreatePseudoEvents();
        //UpdateQuestions();
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(Collectable col in collectibles)
        {
            Toolbox.GetInstance().GetStatManager().gameState.notCollected.Add(col.identity);
        }

        dungeonProgressTemp = Toolbox.GetInstance().GetStatManager().gameState.keyDungeonProgress;
        //GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        //answer = gs.answer;
        //CreateConstellationData();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        constellationPrefabs = new List<GameObject>();
        constellationStructures = new List<ConstellationStructure>();

        Object[] availableConstellations = Resources.LoadAll("Constellations");
        Object[] Structures = Resources.LoadAll("ConstellationStructure");

        foreach (Object o in availableConstellations)
        {
            GameObject go = (GameObject)o;
            Constellation con = go.GetComponent<Constellation>();

            constellationPrefabs.Add(go);
            //constellationNotDiscovered.Add(con.identity); 
        }

        foreach (Object o in Structures)
        {
            ConstellationStructure cs = (ConstellationStructure)o;
            constellationStructures.Add(cs);
        }
    }

    

    public void LoadAllKeySprites()
    {
        keySprites = new List<Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("KeySprites");

        foreach(Sprite s in sprites)
        {
            keySprites.Add(s);
        }
    }

    public void DiscoverConstellation(int id)
    {
        print("Discover Constellation " + id);
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        gs.constellationsNotDiscovered.Remove(id);
        gs.constellationsDiscovered.Add(id);
        gs.allConstellationData[id].discovered = true;
        for(int i = 0; i < gs.allConstellationData[id].starsLocation.Count; i++)
        {
            if (!gs.allConstellationData[id].starsDiscovered.Contains(i))
            {
                gs.allConstellationData[id].starsDiscovered.Add(i);
            }
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
            if(q.questIdentity == Quest.QuestIdentity.Collecting)
            {
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
        }

        for (int i = 0; i < currIdx; i++)
        {
            Quest q = quests[i];
            if(q.questIdentity == Quest.QuestIdentity.Collecting)
            {
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
    }

    public void QuickSave()
    {
        Toolbox.GetInstance().GetStatManager().gameState.answer = answer;
        Toolbox.GetInstance().GetStatManager().gameState.playerPosition = rocket.transform.position;
        Toolbox.GetInstance().GetStatManager().SaveState();
    }

    public void PausePlayer()
    {
        //TODO:
    }

    public void ResumePlayer()
    {
        //TODO:
    }

    public void PickUpNumber(int ans)
    {
        answer = ans;
        //print("Questions: " + questions);
        if (!inDungeon)
        {
            foreach (Question go in planets)
            {
                EquationManager em = go.eqTextMeshObj.GetComponent<EquationManager>();
                try
                {
                    if (em.equation.answer == answer) // TODO: null reference exception here
                    {
                        //go.ActivateCollectables();
                        go.StartActivatePlanet();
                    }
                    else
                    {
                        //go.DeactivateCollectables();
                    }
                }
                catch (System.NullReferenceException e)
                {
                    print("FAILED");
                    print("em: " + em);
                    if (em != null)
                    {
                        print("em equation" + em.equation);
                        if (em.equation != null)
                        {
                            print("em equation answer" + em.equation.answer);
                        }
                    }
                }
            }
        }
        QuickSave();
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
