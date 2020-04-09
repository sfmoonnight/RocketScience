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
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        if (collision.CompareTag("Player"))
        {
            if(gs.telescopeQuestIndex == 0 && gs.telescopeActivated)
            {
                gs.telescopeQuestIndex++;

                Event newEvent = new Event(Event.EventType.KeyEvent, System.DateTime.Now.ToString(), 0);
                Toolbox.GetInstance().GetStatManager().gameState.events.Add(newEvent);

                GameObject newItem = GameObject.Find("NotificationUI");
                newItem.GetComponent<NotificationQueue>().AddToQueue(null, "Welcome to the Great space telecope");
                newItem.GetComponent<NotificationQueue>().AddToQueue(null, "You are gifted an energy card");
                gs.telescopeEnergyCard++;
                GameObject.Find("EnergyCard").GetComponent<Text>().text = gs.telescopeEnergyCard.ToString();
            }
        }
    }
}
