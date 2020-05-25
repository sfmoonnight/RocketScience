using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarOnMap : MonoBehaviour
{
    Animator anim;
    public Image image;
    public Vector2 universePosition;
    public string consName;
    public string name;
    public bool discovered;
    public bool activated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(discovered || activated)
        {
            //print("onmousedown");
            RefreshStarInfo();
            GameObject starinfo = GameObject.Find("StarInfo");
            starinfo.transform.localPosition = transform.localPosition + new Vector3(100, 0, 0);
            if (starinfo.transform.localPosition.x > 287)
            {
                starinfo.transform.localPosition = new Vector3(287, transform.localPosition.y + 70, 0);
                if(starinfo.transform.localPosition.y > 170)
                {
                    starinfo.transform.localPosition = new Vector3(287, transform.localPosition.y - 70, 0);
                }
            }
            starinfo.GetComponent<ToggleUI>().ShowUI();
            
        }   
    }

    public void Click()
    {
        if (discovered || activated)
        {
            //print("onmousedown");
            RefreshStarInfo();
            GameObject starinfo = GameObject.Find("StarInfo");
            starinfo.transform.localPosition = transform.localPosition + new Vector3(100, 0, 0);
            if (starinfo.transform.localPosition.x > 287)
            {
                starinfo.transform.localPosition = new Vector3(287, transform.localPosition.y + 70, 0);
                if (starinfo.transform.localPosition.y > 170)
                {
                    starinfo.transform.localPosition = new Vector3(287, transform.localPosition.y - 70, 0);
                }
            }
            starinfo.GetComponent<ToggleUI>().ShowUI();

        }
    }

    public void ShowStarOnMap()
    {
        image.enabled = true;
    }

    public void Discover()
    {
        discovered = true;
        image.enabled = true;
    }

    public void Activate()
    {
        print("Activate Map Star");
        //GetComponent<Image>().color = Color.red;
        activated = true;
        anim = GetComponent<Animator>();
        anim.SetBool("activated", true);
    }

    public void RefreshStarInfo()
    {
        GameManager gm = Toolbox.GetInstance().GetGameManager();
        string coordinates = "X: " + universePosition.x + " Y: " + universePosition.y;
        GameObject.Find("StarInfo").GetComponent<ToggleUI>().ChangeText2(coordinates);
        GameObject.Find("StarInfo").GetComponent<ToggleUI>().ShowObjects();
        if (discovered)
        {
            string info = consName + " " + name;
            GameObject.Find("StarInfo").GetComponent<ToggleUI>().ChangeText1(info);
            if (!activated)
            {
                GameObject.Find("StarInfo").GetComponent<ToggleUI>().HideObjects();
            }
        }
        else
        {
            if (activated)
            {
                GameObject.Find("StarInfo").GetComponent<ToggleUI>().ChangeText1("Unknow Star");
            }
        } 
        gm.warpToLocation = universePosition;
    }
}
