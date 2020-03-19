﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstKeyQuest : NewQuest
{
    public Quest firstKeyQuest;

    public override void Start()
    {
        //keyCollectible = new QuestCollectible(-1, 0, 0, false);
        //collectibles = new List<QuestCollectible>();
        //collectibles.Add(keyCollectible);

        firstKeyQuest = new Quest(-1);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HideNewQuest();
            GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
            gs.firstKeyQuestStatus = GameState.QuestStatus.Accepted;
            Toolbox.GetInstance().GetGameManager().PickUpQuest(firstKeyQuest);
        }
    }
}