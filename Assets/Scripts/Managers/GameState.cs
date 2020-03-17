using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    //---QuestStatus
    public enum QuestStatus { Disabled, Enabled, Accepted, Completed };

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

    //---Quests
    public int currQuestIndex; //current quest in quest panel

    public List<Quest> quests;

    //---Money
    public int money;

    //---QuestFinished
    public int questCount;

    //---First key quest status 
    public QuestStatus firstKeyQuestStatus = QuestStatus.Disabled;

    //---The dungeons that are unlocked
    public int keyDungeonProgress;
    
    //---All Planets in the Main
    public List<PlanetData> allPlanetData;
}
