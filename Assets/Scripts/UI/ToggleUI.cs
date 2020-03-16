﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    public Image image;
    public bool active;
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
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        active = false;
    }

    public void ShowUI()
    {
        //print("show item UI");
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().interactable = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        active = true;
    }

    public void ChangeImage(Sprite s)
    {
        image.sprite = s;
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
