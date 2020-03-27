using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public LineRenderer line;
    public List<Star> nextStar;
    // Start is called before the first frame update
    void Start()
    {
        if(nextStar.Count > 0)
        {
            for (int i = 0; i < nextStar.Count; i++)
            {
                LineRenderer lr = Instantiate(line).GetComponent<LineRenderer>();
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, nextStar[i].transform.position);
            }
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateSelf()
    {

    }

    public void ActivateNextStar()
    {

    }
}
