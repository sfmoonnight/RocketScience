using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public List<QuestCollectible> collectibles;

    public bool keyQuest;

    public Vector2 coordinates;

    //---Normal quests have identity 0, key quests have negative identity
    public int questIdentity;

    public Quest()
    {
        keyQuest = false;
    }

    public Quest(bool keyQuest, List<QuestCollectible> qcs)
    {
        this.keyQuest = keyQuest;
        this.collectibles = qcs;
    }

    public Quest(bool keyQuest)
    {
        this.keyQuest = keyQuest;
    }

    public Quest(int keyQuestIdentity, Vector2 coordinates)
    {
        questIdentity = keyQuestIdentity;
        this.keyQuest = true;
        QuestCollectible kqc = new QuestCollectible(keyQuestIdentity, 0, 0, false);
        collectibles = new List<QuestCollectible>();
        this.collectibles.Add(kqc);
        this.coordinates = coordinates;
}
}
