using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class StatManager : MonoBehaviour
{
    public string savePath;
    public GameState gameState = new GameState();

    private void Awake()
    {
        // TODO: Remove these lines. They should be in LoadState
        print("Initializing game state");
        gameState.quests = new List<Quest>();
        gameState.currQuestIndex = 0;
        gameState.money = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        savePath = Application.persistentDataPath;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadState()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(GetSavePath(), FileMode.Open);
            gameState = formatter.Deserialize(stream) as GameState;
            stream.Close();
        }
        else
        {
            Debug.Log("Save file not found in " + savePath + ". This must be a new game!");
            gameState = new GameState();
            gameState.quests = new List<Quest>();
            gameState.currQuestIndex = 0;
            gameState.money = 0;

        }
    }

    public void SaveState()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(GetSavePath(), FileMode.Create);
        formatter.Serialize(stream, gameState);
        stream.Close();
    }

    public string GetSavePath()
    {
        return savePath;
    }


}
