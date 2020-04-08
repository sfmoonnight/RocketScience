using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstKeyQuest : NewQuest
{
    public Quest firstKeyQuest;

    GameObject telescope;

    public override void Start()
    {
        telescope = GameObject.Find("Telescope");
        //keyCollectible = new QuestCollectible(-1, 0, 0, false);
        //collectibles = new List<QuestCollectible>();
        //collectibles.Add(keyCollectible);
        Vector2 coor = (Vector2) telescope.transform.position + new Vector2(150, 150);
        firstKeyQuest = new Quest(-1, coor);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HideNewQuest();
            GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
            gs.telescopeQuestStatus = GameState.QuestStatus.Accepted;
            //GameObject.Find("MeteorDungeon").GetComponent<ActiveKeyDungeon>().ActivateDungeon();
            telescope.GetComponent<ActivateTelescope>().ShowTelescope();
            Toolbox.GetInstance().GetGameManager().PickUpQuest(firstKeyQuest);
        }
    }
}
