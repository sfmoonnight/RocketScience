using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarOnMap : MonoBehaviour
{
    Animator anim;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowStarOnMap()
    {
        image.enabled = true;
    }

    public void Activate()
    {
        print("Activate Map Star");
        //GetComponent<Image>().color = Color.red;
        anim = GetComponent<Animator>();
        anim.SetBool("activated", true);
    }
}
