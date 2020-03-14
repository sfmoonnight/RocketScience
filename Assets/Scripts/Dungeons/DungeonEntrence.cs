using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonEntrence : MonoBehaviour
{
    public string dungeonName;

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
            GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
            gs.dungeonPosition = transform.position;
            gs.answer = Toolbox.GetInstance().GetGameManager().answer;
            SceneManager.LoadScene(dungeonName);
        }
    }
}
