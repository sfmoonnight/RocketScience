using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    //---Answer
    public int answer;

    //---Position
    public Vector3 position;

    //---Collectables
    public List<int> collectables;

    //---Quests
    public int currQuestIndex;
    public List<Quest> quests;

}
