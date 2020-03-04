using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewQuest : MonoBehaviour
{
    public List<QuestCollectible> collectibles;

    // Start is called before the first frame update
    void Start()
    {
        GenerateCollectibles(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateCollectibles(int count)
    {
        collectibles = new List<QuestCollectible>();
        //int numCollectibles = Toolbox.GetInstance().GetGameManager().collectibles.Count;
        int numCollectibles = GameObject.Find("QuestPanel").GetComponent<DataContainer>().collectibles.Count;
        Debug.Assert(numCollectibles > 0);
        for (int i = 0; i < count; i++)
        {
            QuestCollectible qc = new QuestCollectible();
            qc.identity = Random.Range(1, numCollectibles+1);
            qc.x = Random.Range(-50f, 50f);
            qc.y = Random.Range(-50f, 50f);
            qc.collected = false;
            collectibles.Add(qc);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("On trigger enter 1");
        if (collision.CompareTag("Player"))
        {
            print("On trigger enter 2");
            HideNewQuest();

            Toolbox.GetInstance().GetGameManager().PickUpQuest(GetComponent<NewQuest>());
        }
    }

    void HideNewQuest()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
