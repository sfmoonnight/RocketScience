using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Star : MonoBehaviour
{
    public string variable;
    public int value;

    public ParticleSystem starParticle;
    public LineRenderer line;
    public List<Transform> nextStar;

    DrawLine drawLine;
    public bool activated;
    public bool solved;
    // Start is called before the first frame update
    void Start()
    {
        solved = true;
        drawLine = GetComponent<DrawLine>();
        drawLine.SetUpLine(transform, nextStar);
        starParticle.Stop();

        if (nextStar.Count > 0)
        {
            for (int i = 0; i < nextStar.Count; i++)
            {
                LineRenderer lr = Instantiate(line).GetComponent<LineRenderer>();
                lr.SetPosition(0, transform.position);
                if(nextStar.Count > 0)
                {
                    lr.SetPosition(1, nextStar[i].position);
                }
                else
                {
                    lr.SetPosition(1, transform.position);
                }
                
            }
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //print("Click on star");
        //print(value);
        if (Toolbox.GetInstance().GetGameManager().answer == value)
        {
            Rocket rocket = Toolbox.GetInstance().GetGameManager().GetRocket();
            rocket.MoveAndScoop(gameObject); //also play particle and set 'solved' to ture
        }
    }

    public void ActivateSelf()
    {
        if (!drawLine.enabled)
        {
            drawLine.enabled = true;
            activated = true;
        }  
    }

    public void ActivateNextStar()
    {
        //print("Activate next star");
        if(nextStar.Count > 0)
        {
            foreach (Transform s in nextStar)
            {
                s.GetComponent<Star>().ActivateSelf();
            }
        }   
    }

    public void SetText(string s)
    {
        variable = s;
        GetComponent<TextMeshPro>().text = s;
    }
}
