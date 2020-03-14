using System.Collections;
using System.Collections.Generic;
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
        scoreBorad.HideUI();
        startTime = Time.time;

    }

    public override void Update()
    {
        if(Time.time - startTime >= gameTime)
        {
            Finished();
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
        points.text = Toolbox.GetInstance().GetGameManager().answer.ToString();
        if(Toolbox.GetInstance().GetGameManager().answer >= (meterPoints * 0.65))
        {
            multi = 2;       
        }
        if (Toolbox.GetInstance().GetGameManager().answer >= meterPoints)
        {
            multi = 3;
        }
        bonus.text = multi.ToString();
        int num = Toolbox.GetInstance().GetGameManager().answer * multi;
        result.text = num.ToString();
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
