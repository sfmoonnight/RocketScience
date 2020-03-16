using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonEntrence : MonoBehaviour
{
    //public string dungeonName;
    public int dungeonIndex;
    public bool keyDungeon; //TODO: Important to set true manually for key dungeons 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Toolbox.GetInstance().GetGameManager().inDungeon = true;
            GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
            //gs.dungeonPosition = transform.position;
            gs.playerPosition = collision.transform.position;
            //print("--------Player position" + gs.playerPosition);
            gs.answer = Toolbox.GetInstance().GetGameManager().answer;
            if (keyDungeon)
            {
                gs.dungeonEntered = 0;
            }    
            if (!keyDungeon)
            {
                gs.dungeonEntered = GetComponentInParent<Question>().planetID;
                print("--------dungeonEntering" + GetComponentInParent<Question>().planetID);
            }
            Toolbox.GetInstance().GetGameManager().planets.Clear();
            
            Toolbox.GetInstance().GetStatManager().SaveState();
            SceneManager.LoadScene(dungeonIndex);
        }
    }

    public void GenerateDungeon()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        dungeonIndex = Random.Range(1, gs.keyDungeonProgress + 1);
        print("-------dungeon index" + dungeonIndex);
        OpenEntrence();
    }

    public void OpenEntrence()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    public void CloseEntrence()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
