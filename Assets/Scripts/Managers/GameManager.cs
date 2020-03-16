using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public int answer;
    public Rocket rocket;
    GameObject[] questions;
    public List<Collectable> collectibles;
    public List<Collectable> keyCollectibles;
    public List<Question> planets;//---including all the planets and structures
    public List<int> inventory;

    public bool inDungeon;
    // Start is called before the first frame update
    private void Awake()
    {
        answer = Random.Range(-99, 100);
        rocket = GameObject.Find("Rocket").GetComponent<Rocket>();
        planets = new List<Question>();
        UpdateQuestions();
        LoadAllCollectibles();
        //loadCollectibles();

    }
    void Start()
    {
        inventory = new List<int>();
        //GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        //answer = gs.answer;
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
        
        rocket = GameObject.Find("Rocket").GetComponent<Rocket>();
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        print(gs.answer);
        print(gs.playerPosition);
        
        answer = gs.answer;
        rocket.transform.position = gs.playerPosition;

        print("Reloading main");
        if (Toolbox.GetInstance().GetStatManager().gameState.dungeonEntered != 0)
        {
            print("-------reload dungeon" + Toolbox.GetInstance().GetStatManager().gameState.dungeonEntered);
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
        }

        Object[] availableKeyCollectibles = Resources.LoadAll("KeyCollectibles");
        keyCollectibles = new List<Collectable>();
        foreach (Object o in availableKeyCollectibles)
        {
            GameObject go = (GameObject)o;
            Collectable col = go.GetComponent<Collectable>();

            keyCollectibles.Add(col);
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

    public void UpdateQuestions()
    {
        questions = null;
        questions = GameObject.FindGameObjectsWithTag("question");
    }

    public void SetAnswer(int ans)
    {
        answer = ans;
        //print("Questions: " + questions);
        if (!inDungeon)
        {
            foreach (GameObject go in questions)
            {
                Question q = go.GetComponent<Question>();
                EquationManager em = q.eqTextMeshObj.GetComponent<EquationManager>();
                if (em.equation.answer == answer)
                {
                    q.ActivateCollectables();
                }
                else
                {
                    q.DeactivateCollectables();
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
