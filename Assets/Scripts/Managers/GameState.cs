using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    //---Answer
    public int answer;

    //---Player Position
    [SerializeField]
    public Vector3 playerPosition;

    //---dungeon just entered
    public int dungeonEntered;

    //---Location of dungeon just entered
    [SerializeField]
    public Vector3 dungeonPosition;

    //---Collectables
    public List<int> collected;
    public List<int> notCollected;
    
    //---Quests
    public int currQuestIndex; //current quest in quest panel

    public List<Quest> quests;

    //---Money
    public int money;

    //---QuestFinished
    public int questCount;

    //---QuestStatus
    public enum QuestStatus { Disabled, Enabled, Accepted, Completed };
    //---key quests status 
    public QuestStatus telescopeQuestStatus;

    //---The dungeons that are unlocked
    public int keyDungeonProgress;
    
    //---All Planets in the Main
    public List<PlanetData> allPlanetData;

    //---Events that happened
    public List<Event> events;
    public List<int> firstEventOnEachPage;

    //---Captain log page number for each section
    public int travelLogPageNumber;
    public int collectiblePageNumber; //Left page + right page is one page number
    public int keyDungeonPageNumber;

    //---Telescope and Constellations
    public int telescopeEnergyCard;
    public List<int> constellationsDiscovered;
    public List<int> constellationsNotDiscovered;
    public List<ConstellationData> allConstellationData;
}
