using System.Collections;
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
        Vector2 coor = new Vector2(90, 100);
        firstKeyQuest = new Quest(-1, coor);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HideNewQuest();
            GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
            gs.firstKeyQuestStatus = GameState.QuestStatus.Accepted;
            GameObject.Find("MeteorDungeon").GetComponent<ActiveKeyDungeon>().ActivateDungeon();
            Toolbox.GetInstance().GetGameManager().PickUpQuest(firstKeyQuest);
        }
    }
}
