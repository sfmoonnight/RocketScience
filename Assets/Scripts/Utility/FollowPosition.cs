using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    public Transform target;

    public bool useDamping;
    public float damping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!useDamping)
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
        }
        else
        {
            float x = Mathf.Lerp(transform.position.x, target.position.x, damping * Time.deltaTime);
            float y = Mathf.Lerp(transform.position.y, target.position.y, damping * Time.deltaTime);
            transform.position = new Vector3(x, y, -10);
        }

    }
}
