using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveKeyDungeon : MonoBehaviour
{
    public int dungeonID;
    public SpriteRenderer spriteRenderer;
    public GameObject entrance;
    // Start is called before the first frame update
    void Start()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        if(dungeonID <= gs.keyDungeonProgress)
        {
            ShowDungeon();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateDungeon()
    {
        spriteRenderer.enabled = true;
        entrance.SetActive(true);
    }

    public void ShowDungeon()
    {
        spriteRenderer.enabled = true;
        entrance.SetActive(false);
    }
}
