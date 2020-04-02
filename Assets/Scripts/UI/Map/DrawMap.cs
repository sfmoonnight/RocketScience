using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMap : MonoBehaviour
{
    GameManager gm;
    public GameObject mapBackground;
    // Start is called before the first frame update
    void Start()
    {
        gm = Toolbox.GetInstance().GetGameManager();
        SetUpMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUpMap()
    {
        float xyRatio = gm.universeSize.x / gm.universeSize.y;
        float y = mapBackground.GetComponent<RectTransform>().rect.height;
        float x = xyRatio * y;
        print("height: " + y);
        print("map size: " + new Vector2(x, y));
        GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);
    }

    void ExtractConstellation()
    {
        //GameObject.
    }
}
