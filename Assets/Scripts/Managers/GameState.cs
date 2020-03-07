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

    //---Position
    public Vector3 position;

    //---Collectables
    public List<int> collectables;

    //---Quests
    public int currQuestIndex;
    public List<Quest> quests;

    //---Money
    public int money;

    //---QuestFinished
    public int questCount;

    //---First key quest status 
    public QuestStatus firstKeyQuestStatus = QuestStatus.Disabled;
    

}
