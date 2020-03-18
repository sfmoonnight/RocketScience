using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class StatManager : MonoBehaviour
{
    public string savePath;
    public GameState gameState = new GameState();

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "rocketscience.save");
        LoadState();
    }

    // Start is called before the first frame update
    void Start()
    {
        //savePath = Application.persistentDataPath;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initializeGameState()
    {
        print("Initializing game state");
        gameState = new GameState();
        gameState.quests = new List<Quest>();
        gameState.currQuestIndex = 0;
        gameState.money = 0;
        gameState.keyDungeonProgress = 0;
        gameState.collected = new List<int>();
        gameState.allPlanetData = new List<PlanetData>();
        gameState.events = new List<Event>();
    }

    public void LoadState()
    {
        if (File.Exists(savePath))
        {
            /*BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(GetSavePath(), FileMode.Open);
            gameState = formatter.Deserialize(stream) as GameState;
            stream.Close();*/
            Debug.Log("Loading save file found in " + savePath + ".");
            Read();
        }
        else
        {
            Debug.Log("Save file not found in " + savePath + ". This must be a new game!");
            initializeGameState();
            SaveState();
        }
    }

    public void SaveState()
    {
        /*BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(GetSavePath(), FileMode.Create);
        formatter.Serialize(stream, gameState);
        stream.Close();*/
        string json = JsonUtility.ToJson(gameState);
        //print(json);
        Write(json);
    }

    public string GetSavePath()
    {
        return savePath;
    }

    void Write(string contents)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(savePath))
            {
                print("writing to file: \n" + contents);
                sw.Write(contents);
            }
        }
        catch (Exception e)
        {
            print(e);
        }
    }

    void Read()
    {
        string fileContents = "";
        try
        {
            using (StreamReader sr = new StreamReader(savePath))
            {
                fileContents = sr.ReadToEnd();
                gameState = JsonUtility.FromJson<GameState>(fileContents);
            }

            print("file contains: \n" + fileContents);
        }
        catch (Exception e)
        {
            print(e);
        }
    }


}
