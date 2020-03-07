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

    public Quest(bool keyQuest)
    {
        this.keyQuest = keyQuest;
    }
}
