using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoEvents : MonoBehaviour
{
    private void Awake()
    {
        CreatePseudoEvents();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatePseudoEvents()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        gs.events.Clear();
        Event event1 = new Event(Event.EventType.NewCollectible, "Write something Here", 1);    
        Event event2 = new Event(Event.EventType.NewCollectible, "Write something HERE!", 3);
        Event event3 = new Event(Event.EventType.NewCollectible, "Write something HERE!", 5);
        Event event4 = new Event(Event.EventType.KeyDungeon, "Something something", 1);
        Event event5 = new Event(Event.EventType.NewCollectible, "Write something HERE!", 4);
        Event event6 = new Event(Event.EventType.KeyDungeon, "Something something", 1);
        //print(event1);
        //print(event2);

        gs.events.Add(event1);
        gs.events.Add(event2);
        gs.events.Add(event3);
        gs.events.Add(event4);
        gs.events.Add(event5);
        gs.events.Add(event6);

        Toolbox.GetInstance().GetStatManager().SaveState();
        print(gs.events.Count);
    }
}
