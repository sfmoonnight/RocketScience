using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event
{
    public enum EventType { NewCollectible, NewConstellation, Quest, KeyEvent, KeyDungeon, NewSkin };

    public string time;    
    public EventType eventType;
    public int collectibleIdentity = 0;
    public int constellationIdentity = 0;
    public int QuestIdentity = 0;
    public int keyEventIdentity = 0;
    public int keyDungeonIdentity = 0;
    public int skinIdentity = 0;

    public Event(EventType eventType, string time, int identity)
    {
        this.eventType = eventType;
        this.time = time;
        if(eventType == EventType.NewCollectible)
        {
            this.collectibleIdentity = identity;
        }
        if (eventType == EventType.NewConstellation)
        {
            this.constellationIdentity = identity;
        }
        if (eventType == EventType.Quest)
        {
            this.QuestIdentity = identity;
        }
        if (eventType == EventType.KeyEvent)
        {
            this.keyEventIdentity = identity;
        }
        if (eventType == EventType.KeyDungeon)
        {
            this.keyDungeonIdentity = identity;
        }
        if (eventType == EventType.NewSkin)
        {
            this.skinIdentity = identity;
        }   
    }
}
