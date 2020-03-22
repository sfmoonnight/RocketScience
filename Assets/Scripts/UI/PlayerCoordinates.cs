using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCoordinates : MonoBehaviour
{
    public Transform target;

    public TextMeshPro xText;
    public TextMeshPro yText;

    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCoordinates();
    }

    public void UpdateCoordinates()
    {
        double x1 = Math.Round(target.transform.position.x, 2) + 150;
        double y1 = Math.Round(target.transform.position.y, 2) + 150;
        string x = "x: " + x1.ToString();
        string y = "y: " + y1.ToString();

        xText.text = x;
        yText.text = y;
    }

    public void ToggleCoordinates()
    {
        if (active)
        {
            HideCoordinates();
        }
        else
        {
            ShowCoordinates();
        }
    }

    public void HideCoordinates()
    {
        xText.GetComponent<MeshRenderer>().enabled = false;
        yText.GetComponent<MeshRenderer>().enabled = false;
        active = false;
    }

    public void ShowCoordinates()
    {
        xText.GetComponent<MeshRenderer>().enabled = true;
        yText.GetComponent<MeshRenderer>().enabled = true;
        active = true;
    }
}
