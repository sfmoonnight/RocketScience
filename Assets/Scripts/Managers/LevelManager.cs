using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        print("------level manager start");
        Toolbox.GetInstance().GetStatManager().LoadState();
        print("--------dungeonEntered" + Toolbox.GetInstance().GetStatManager().gameState.dungeonEntered);
        Toolbox.GetInstance().GetGameManager().ReloadMain();




        Toolbox.GetInstance().GetGameManager().UpdateQuestions();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
