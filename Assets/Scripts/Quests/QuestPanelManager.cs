using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanelManager : MonoBehaviour
{
    [Tooltip("This is the quest number to finish before triggering the first key quest")]
    public int questCount;
    public Text coordinates;
    public Sprite testImage;
    // Start is called before the first frame update
    void Start()
    {
        UpdateQuests();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateQuestsHelper()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        List<Quest> quests = gs.quests;

        if (quests.Count == 0)
        {
            Clear();
        }

        else
        {
            //print(gs.currQuestIndex);
            Quest currQuest = quests[gs.currQuestIndex];
            DrawQuest(currQuest);
        }
    }

    public void UpdateQuests()
    {
        UpdateQuestsHelper();

        StartCoroutine("CompleteQuests");

    }

    bool QuestCompleted(Quest q)
    {
        //print("Quest complete!");
        foreach (QuestCollectible qc in q.collectibles)
        {
            if (!qc.collected)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator CompleteQuests()
    {
        yield return new WaitForSeconds(1f);
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        List<Quest> quests = gs.quests;
        List<Quest> remaining = new List<Quest>();
        foreach (Quest q in quests)
        {
            
            if (QuestCompleted(q) && !q.keyQuest)
            {
                GameObject newItem = GameObject.Find("NotificationUI");
                newItem.GetComponent<NotificationQueue>().AddToQueue(testImage, "Quest Completed!");

                gs.money += 100;
                gs.questCount++;
                if (CheckQuestCount(questCount) && gs.telescopeQuestStatus == GameState.QuestStatus.Disabled)
                {
                    gs.telescopeQuestStatus = GameState.QuestStatus.Enabled;
                }
            }else if (QuestCompleted(q) && q.keyQuest)
            {
                
                GameObject newItem = GameObject.Find("NotificationUI");
                newItem.GetComponent<NotificationQueue>().AddToQueue(testImage, "New Game Type Unlocked!");

                //---When complete a key quest
                //gs.money += 500;
                //gs.questCount++;
                Event newEvent = new Event(Event.EventType.KeyDungeon, DateTime.Now.ToString(), gs.keyDungeonProgress);        

                Toolbox.GetInstance().GetStatManager().gameState.events.Add(newEvent);
            }
            else
            {
                remaining.Add(q);
            }            
        }
        //print("remaining" + remaining.Count);
        gs.quests = remaining;
        if (gs.currQuestIndex > remaining.Count - 1 && remaining.Count > 0)
        {
            gs.currQuestIndex = remaining.Count - 1;
        }

        UpdateQuestsHelper();
        GameObject.Find("Money").GetComponent<Text>().text = gs.money.ToString();

        Toolbox.GetInstance().GetStatManager().SaveState();
    }

    bool CheckQuestCount(int count)
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        if (gs.questCount >= count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Clear()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        coordinates.text = "";
    }

    public void DrawQuest(Quest q)
    {
        //List<Collectable> availableCollectibles = gameObject.GetComponent<DataContainer>().collectibles;
        Debug.Assert(Toolbox.GetInstance().GetGameManager().collectibles.Count > 0);
        Clear();
        foreach (QuestCollectible qc in q.collectibles) {
            Collectable col = getCollectibleByIdentity(qc.identity);
            // Instantiate new game object and set sprite to the sprite of col
            GameObject go = new GameObject();
            Image i = go.AddComponent<Image>();
            //print(i.sprite);
            //print(col);
            //i.sprite = col.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
            i.sprite = col.spriteRenderer.sprite;
            i.preserveAspect = true;
            if (!qc.collected)
            {
                i.color = Color.black;
            }
            //go.transform.SetParent(this.transform);
            go.GetComponent<RectTransform>().SetParent(this.transform);
            Vector2 v = new Vector2(qc.x, qc.y);
            go.GetComponent<RectTransform>().localPosition = v;
            go.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            //go.transform.position = v;
            //print("setting x and y " + v);
            go.SetActive(true);
        }
        if (q.coordinates != Vector2.zero)
        {
            coordinates.text = "X:" + q.coordinates.x.ToString() + "  Y:" + q.coordinates.y.ToString();
        }
    }

    private Collectable getCollectibleByIdentity(int identity)
    {
        List<Collectable> cols = Toolbox.GetInstance().GetGameManager().GetAllCollectibles();
        List<Collectable> kcols = Toolbox.GetInstance().GetGameManager().GetAllKeyCollectibles();
        //print("--------" + cols.Count);
        //print(identity);
        if(identity > 0)
        {
            foreach (Collectable col in cols)
            {
                if (col.identity == identity)
                {
                    return col;
                }
            }
        }

        if (identity < 0)
        {
            //print("--------" + kcols.Count);
            foreach (Collectable col in kcols)
            {
                if (col.identity == identity)
                {
                    
                    return col;
                }
            }
        }

        return null;
    }


    public void PreviousQuest()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        if (gs.quests.Count == 0)
        {
            gs.currQuestIndex = 0;

        }
        else if (gs.currQuestIndex == 0)
        {
            gs.currQuestIndex = gs.quests.Count - 1;

        }
        else
        {
            gs.currQuestIndex -= 1;
        }
        UpdateQuests();
    }

    public void NextQuest()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        if (gs.quests.Count == 0)
        {
            gs.currQuestIndex = 0;

        }
        else if (gs.currQuestIndex == gs.quests.Count - 1)
        {
            gs.currQuestIndex = 0;

        }
        else
        {
            gs.currQuestIndex += 1;
        }
        UpdateQuests();
    }
}
