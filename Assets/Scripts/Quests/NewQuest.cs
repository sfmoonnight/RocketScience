using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewQuest : MonoBehaviour
{
    public Quest quest;
    //public List<QuestCollectible> collectibles;

    // Start is called before the first frame update
    public virtual void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //print("On trigger enter 1");
        if (collision.CompareTag("Player"))
        {
            //print("On trigger enter 2");
            //HideNewQuest();
            GameObject.Find("QuestMenu").GetComponent<QuestMenu>().newQuest = this;
            GameObject.Find("QuestMenu").GetComponent<ToggleUI>().ChangeText(quest.text);
            GameObject.Find("QuestMenu").GetComponent<ToggleUI>().ShowUI();
            //TODO:Pause game except UI
        }
    }

    public void HideNewQuest()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
