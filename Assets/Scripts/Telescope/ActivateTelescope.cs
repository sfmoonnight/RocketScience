using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTelescope : MonoBehaviour
{
    public GameObject jupiter;
    // Start is called before the first frame update
    void Start()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        if (gs.telescopeQuestStatus == GameState.QuestStatus.Accepted || gs.telescopeQuestStatus == GameState.QuestStatus.Completed)
        {
            ShowTelescope();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTelescope()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        jupiter.SetActive(true);
    }
}
