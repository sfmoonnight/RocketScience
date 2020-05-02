using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarOnMap : MonoBehaviour
{
    Animator anim;
    public Image image;
    public Vector2 universePosition;
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
            GameObject.Find("StarInfo").GetComponent<ToggleUI>().ShowUI();
        }   
    }

    public void ShowStarOnMap()
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
        //TODO: if only discovered, show star name, but hide warp botton
        //TODO: if only activated, show warp button, hide star name
        //TODO: if both, show everything
        gm.warpToLocation = universePosition;
    }
}
