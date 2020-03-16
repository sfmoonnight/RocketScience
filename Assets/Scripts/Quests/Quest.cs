using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public List<QuestCollectible> collectibles;

    public bool keyQuest;

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

    public Quest(int keyQuestIdentity)
    {
        questIdentity = keyQuestIdentity;
        this.keyQuest = true;
        QuestCollectible kqc = new QuestCollectible(keyQuestIdentity, 0, 0, false);
        collectibles = new List<QuestCollectible>();
        this.collectibles.Add(kqc);
    }
}
