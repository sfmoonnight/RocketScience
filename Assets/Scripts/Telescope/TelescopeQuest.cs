using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelescopeQuest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
            GameManager gm = Toolbox.GetInstance().GetGameManager();
            if (gs.telescopeQuestStatus == GameState.QuestStatus.Accepted)
            {
                gs.telescopeQuestStatus = GameState.QuestStatus.Completed;
                Event newEvent = new Event(Event.EventType.KeyEvent, System.DateTime.Now.ToString(), 0);
                Toolbox.GetInstance().GetStatManager().gameState.events.Add(newEvent);
                GameObject.Find("QuestPanel").GetComponent<QuestPanelManager>().UpdateQuests();
                GameObject newItem = GameObject.Find("NotificationUI");
                newItem.GetComponent<NotificationQueue>().AddToQueue(gm.keySprites[0], "You have discovered 'The Great Telescope'!");
                newItem.GetComponent<NotificationQueue>().AddToQueue(gm.keySprites[1], "Welcome to the Great space telecope");
                newItem.GetComponent<NotificationQueue>().AddToQueue(gm.keySprites[3], "You are gifted two energy cards");
                //newItem.GetComponent<NotificationQueue>().AddToQueue(null, "Quest Completed!");
            }
        }
    }
}
