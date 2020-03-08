using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollectible
{
    public int identity;
    //---Position in quest panel
    public float x;
    public float y;

    public bool collected;

    public QuestCollectible(int identity, float x, float y, bool collected)
    {
        this.identity = identity;
        this.x = x;
        this.y = y;
        this.collected = collected;
    }
}
