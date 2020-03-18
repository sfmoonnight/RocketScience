using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreatePseudoEvents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatePseudoEvents()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        gs.events.Clear();
        Event event1 = new Event(Event.EventType.NewCollectible, "Sunday", 1);    
        Event event2 = new Event(Event.EventType.NewCollectible, "Monday", 3);
        print(event1);
        print(event2);

        gs.events.Add(event1);
        gs.events.Add(event2);
        Toolbox.GetInstance().GetStatManager().SaveState();
        print(gs.events.Count);
    }
}
