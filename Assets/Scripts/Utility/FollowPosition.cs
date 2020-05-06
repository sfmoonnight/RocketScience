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
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        //transform.position = new Vector3(gs.playerPosition.x, gs.playerPosition.y, -10);
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!useDamping)
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        }
        else
        {
            float x = Mathf.Lerp(transform.position.x, target.position.x, damping * Time.deltaTime);
            float y = Mathf.Lerp(transform.position.y, target.position.y, damping * Time.deltaTime);
            transform.position = new Vector3(x, y, transform.position.z);
        }

    }

    public void ChangeLocation()
    {
        float z = transform.position.z;
        float x = target.transform.position.x;
        float y = target.transform.position.y;
        transform.position = new Vector3(x, y, z);
    }
}
