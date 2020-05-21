using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    public Image image;
    public Text text1;
    public Text text2;
    public List<GameObject> hideObjects;
    public bool active;
    public Action callback = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideUI()
    {
        //print("Inside HideUI");
        Time.timeScale = 1;
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        active = false;
        //print("Callback is " + callback);
        if (callback != null)
        {
            callback();
            //callback = null;
        }
    }

    public void ShowUI()
    {
        //print("show item UI");
        //Time.timeScale = 0;
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().interactable = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        active = true;
    }

    public void ChangeImage(Sprite s)
    {
        image.sprite = s;
    }

    public void ChangeText1(string s)
    {
        text1.text = s;
    }
    public void ChangeText2(string s)
    {
        text2.text = s;
    }

    public void HideObjects()
    {
        foreach (GameObject go in hideObjects)
        {
            go.SetActive(false);
        }
    }

    public void ShowObjects()
    {
        foreach (GameObject go in hideObjects)
        {
            go.SetActive(true);
        }
    }

    public void setCallback(Action a)
    {
        //print("Setting callback to " + a);
        callback = a;
    }

    public void Toggle()
    {
        if (active)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }
    }
}
