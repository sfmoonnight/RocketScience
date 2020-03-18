using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewQuest : MonoBehaviour
{
    public List<QuestCollectible> collectibles;

    // Start is called before the first frame update
    public virtual void Start()
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
        int numCollectibles = Toolbox.GetInstance().GetGameManager().collectibles.Count;
        //print("--------" + numCollectibles);
        //int numCollectibles = GameObject.Find("QuestPanel").GetComponent<DataContainer>().collectibles.Count;
        Debug.Assert(numCollectibles > 0);
        for (int i = 0; i < count; i++)
        {
            int identity = Random.Range(1, numCollectibles + 1);
            float x = Random.Range(-50f, 50f);
            float y = Random.Range(-50f, 50f);
            bool collected = false;
            QuestCollectible qc = new QuestCollectible(identity, x, y, collected);
            
            collectibles.Add(qc);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //print("On trigger enter 1");
        if (collision.CompareTag("Player"))
        {
            //print("On trigger enter 2");
            HideNewQuest();

            Toolbox.GetInstance().GetGameManager().PickUpQuest(GetComponent<NewQuest>());
        }
    }

    public void HideNewQuest()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
