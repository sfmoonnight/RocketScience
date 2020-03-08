using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public List<QuestCollectible> collectibles;

    public bool keyQuest;

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
        this.keyQuest = true;
        QuestCollectible kqc = new QuestCollectible(keyQuestIdentity, 0, 0, false);
        collectibles = new List<QuestCollectible>();
        this.collectibles.Add(kqc);
    }
}
