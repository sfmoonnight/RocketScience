using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRecycler : MonoBehaviour
{
    //public MeteorGenerator mg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("number"))
        {
            Number num = collision.GetComponent<Number>();
            StartCoroutine(num.startingPoint.GetComponent<MeteorGenerator>().Restart(num));
            //num.startingPoint.GetComponent<MeteorGenerator>().Restart(num);
            //mg.Restart(num);
        }
    }
}
