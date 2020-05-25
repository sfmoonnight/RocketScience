using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSpawner : MonoBehaviour
{
    public float interval;
    public GameObject newQuest;
    //public GameObject firstKeyQuest;
    List<GameObject> quests = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnInterval");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool SpawnCondition()
    {
        if (Toolbox.GetInstance().GetStatManager().gameState.collected.Count >= 3)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    IEnumerator SpawnInterval()
    {
        while (true)
        {
            //print("spawning");
            if (SpawnCondition())
            {
                GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
                if (gs.telescopeQuestStatus == GameState.QuestStatus.Enabled)
                {
                    Quest q = new Quest();
                    Reward r = new Reward(Reward.RewardType.EnergyCard, 2);
                    q.rewards.Add(r);
                    q.questIdentity = Quest.QuestIdentity.TelescopeActivation;
                    q.text = "Recieved a set of coordinates";
                    GameObject telescope = GameObject.Find("Telescope");
                    q.coordinates = (Vector2)telescope.transform.position + new Vector2(150, 150);
                    q.questImage = Toolbox.GetInstance().GetGameManager().keySprites[0];
                    SpawnQuest(q);
                }
                else
                {
                    int i = Random.Range(2, 4);
                    Quest q = GenerateCollectingQuest(i, false);
                    SpawnQuest(q);
                }       
            }
            DestroyQuest();
            yield return new WaitForSeconds(interval);
        }

    }

    /*public Quest GenerateLocationQuest(Vector2 coordinates)
    {

    }*/
    public Quest GenerateCollectingQuest(int count, bool includeUnknown)
    {
        List<QuestCollectible> collectibles = new List<QuestCollectible>();
        List<Reward> rewards = new List<Reward>();
        
        rewards.Add(Reward.currency100);

        int collected = Toolbox.GetInstance().GetStatManager().gameState.collected.Count;
        int notCollected = Toolbox.GetInstance().GetStatManager().gameState.notCollected.Count;
        //print("--------" + numCollectibles);
        //int numCollectibles = GameObject.Find("QuestPanel").GetComponent<DataContainer>().collectibles.Count;
        Debug.Assert(notCollected > 0);
        if (includeUnknown)
        {
            int index1 = Random.Range(0, notCollected);
            int id1 = Toolbox.GetInstance().GetStatManager().gameState.notCollected[index1];
            float x1 = Random.Range(-30f, 30f);
            float y1 = Random.Range(-25f, 37f);

            QuestCollectible qc1 = new QuestCollectible(id1, x1, y1, false);

            collectibles.Add(qc1);

            int num = count - 1;
            if(num > 0)
            {
                for (int i = 0; i < num; i++)
                {
                    int index = Random.Range(0, collected);
                    int id = Toolbox.GetInstance().GetStatManager().gameState.collected[index];
                    float x = Random.Range(-30f, 30f);
                    float y = Random.Range(-25f, 37f);

                    QuestCollectible qc = new QuestCollectible(id, x, y, false);

                    collectibles.Add(qc);
                }
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                int index = Random.Range(0, collected);
                int id = Toolbox.GetInstance().GetStatManager().gameState.collected[index];
                float x = Random.Range(-30f, 30f);
                float y = Random.Range(-25f, 37f);
                
                QuestCollectible qc = new QuestCollectible(id, x, y, false);

                collectibles.Add(qc);
            }
        }

        Quest quest = new Quest(Quest.QuestIdentity.Collecting, rewards, collectibles, "You have found a collecting request!", new Vector2(), null);
        return quest;
    }

    private void SpawnQuest(Quest quest)
    {
        float halfWidth = 50f;
        float halfHeight = 50f;
        float x, y;
        if (Random.Range(0f, 1f) > 0.5f)
        {
            x = Random.Range(-halfWidth, halfWidth);
            if (Random.Range(0f, 1f) > 0.5f)
            {
                y = -halfHeight;
            }
            else
            {
                y = halfHeight;
            }
        }
        else {
            y = Random.Range(-halfHeight, halfHeight);
            if (Random.Range(0f, 1f) > 0.5f)
            {
                x = -halfWidth;
            }
            else
            {
                x = halfWidth;
            }
        }
        Vector3 position = transform.position + new Vector3(x, y, 0);
        GameObject nq = Instantiate(newQuest, position, Quaternion.identity);
        nq.GetComponent<NewQuest>().quest = quest;
        quests.Add(nq);

        float speed = 4;
        float offsetX = Random.Range(-halfWidth, halfWidth)/2f;
        float offsetY = Random.Range(-halfHeight, halfHeight)/2f;
        Vector2 target = (new Vector2(offsetX, offsetY)) + (Vector2)transform.position;
        Vector2 direction = (target - (Vector2)nq.transform.position).normalized;
        nq.GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    void DestroyQuest()
    {
        List<GameObject> remaining = new List<GameObject>();
        foreach (GameObject q in quests)
        {
            if(Vector2.Distance(q.transform.position, transform.position) > 70)
            {
                Destroy(q);
            }
            else
            {
                remaining.Add(q);
            }
        }
        quests = remaining;
    }

}
