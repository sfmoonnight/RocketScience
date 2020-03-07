using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int answer;
    Rocket rocket;
    GameObject[] questions;
    public List<GameObject> collectibles; 
    public List<int> inventory;
    // Start is called before the first frame update
    private void Awake()
    {
        answer = Random.Range(-99, 100);
        rocket = GameObject.Find("Rocket").GetComponent<Rocket>();
        UpdateQuestions();

        //loadCollectibles();

    }
    void Start()
    {
        inventory = new List<int>();
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

    public Rocket GetRocket()
    {
        return rocket;
    }


}
