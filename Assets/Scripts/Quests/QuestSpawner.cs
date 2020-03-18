using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSpawner : MonoBehaviour
{
    public float interval;
    public GameObject newQuest;
    public GameObject firstKeyQuest;
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
        return true;
    }

    IEnumerator SpawnInterval()
    {
        while (true)
        {
            //print("spawning");
            if (SpawnCondition())
            {
                GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
                if (gs.firstKeyQuestStatus == GameState.QuestStatus.Enabled)
                {
                    SpawnQuest(firstKeyQuest);
                }
                else
                {
                    SpawnQuest(newQuest);
                }       
            }
            DestroyQuest();
            yield return new WaitForSeconds(interval);
        }

    }

    private void SpawnQuest(GameObject quest)
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
        GameObject nq = Instantiate(quest, position, Quaternion.identity);
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
