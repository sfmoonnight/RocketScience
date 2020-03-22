using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        Toolbox.GetInstance().GetStatManager().initializeGameState();
        Toolbox.GetInstance().GetStatManager().SaveState();
        Toolbox.GetInstance().GetStatManager().LoadState();
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        Toolbox.GetInstance().GetStatManager().SaveState();
        Application.Quit();
    }
}
