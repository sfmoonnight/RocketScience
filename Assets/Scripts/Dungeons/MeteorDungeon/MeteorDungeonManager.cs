using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MeteorDungeonManager : DungeonManager
{
    public Text points;
    public Text bonus;
    public Text result;
    public Text timer;

    public Image bonusMeter;
    public Image pin;

    [Tooltip("How long the game is in seconds")]
    public int gameTime;
    [Tooltip("How much points the full bonus meter represents")]
    public int meterPoints;

    int multi;
    float startTime;
    

    public override void Start()
    {
        Toolbox.GetInstance().GetGameManager().answer = 0;
        //Toolbox.GetInstance().GetGameManager().rocket.transform.position = new Vector3(0, 0, 0);
        //Toolbox.GetInstance().GetGameManager().rocket.StopMoving();
        scoreBorad.HideUI();
        startTime = Time.time;

    }

    public override void Update()
    {
        if(Time.time - startTime >= gameTime)
        {

            if (!finished)
            {
                Finished();
            }       
        }

        if((int)(gameTime - (Time.time - startTime)) >= 0)
        {
            timer.text = ((int)(gameTime - (Time.time - startTime))).ToString();
        }

        MovePin();
    }

    public override void Finished()
    {
        base.Finished();
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        points.text = Toolbox.GetInstance().GetGameManager().answer.ToString();
        if (Toolbox.GetInstance().GetGameManager().answer < (meterPoints * 0.65))
        {
            multi = 1;
        }
        if (Toolbox.GetInstance().GetGameManager().answer >= (meterPoints * 0.65))
        {
            multi = 2;       
        }
        if (Toolbox.GetInstance().GetGameManager().answer >= meterPoints)
        {
            multi = 3;
        }
        bonus.text = multi.ToString();
        int resultNum = Toolbox.GetInstance().GetGameManager().answer * multi;
        result.text = resultNum.ToString();

        gs.money += resultNum;
        //print(resultNum);
        //print("Money when finished: " + gs.money);
        if (gs.keyDungeonProgress < 1)
        {
            //gs.collected.Add(Toolbox.GetInstance().GetGameManager().keyCollectibles[0].identity);
            gs.collected.Add(-1);
            Event newEvent = new Event(Event.EventType.NewCollectible, DateTime.Now.ToString(), -1);
            Toolbox.GetInstance().GetStatManager().gameState.events.Add(newEvent);

            Toolbox.GetInstance().GetGameManager().dungeonProgressTemp += 1;
            print("collected" + Toolbox.GetInstance().GetGameManager().keyCollectibles[0].identity);
            print("key dungeon progress: " + gs.keyDungeonProgress);
        }
    }

    void MovePin()
    {
        float p = Mathf.Min((float)Toolbox.GetInstance().GetGameManager().answer / (float)meterPoints, 1f) * bonusMeter.GetComponent<RectTransform>().sizeDelta.x;
        //print(Toolbox.GetInstance().GetGameManager().answer);
        //print(meterPoints);
        //float p = 0;
        //print(bonusMeter.GetComponent<RectTransform>().sizeDelta.x);
        p = p - bonusMeter.GetComponent<RectTransform>().sizeDelta.x / 2f;
        pin.transform.localPosition = new Vector2(p, pin.transform.localPosition.y);

    }
}
