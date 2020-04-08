using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public GameObject center;
    public int segments;
    public float radius;
    public float speed;
    
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = center.transform.position + new Vector3(radius, 0, 0);
        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = segments + 1;
        line.useWorldSpace = true;
        CreatePoints();
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(center.transform.position, Vector3.forward, 10 * Time.deltaTime * speed);
    }

    void CreatePoints()
    {
        float xradius = radius;
        float yradius = radius;
        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            Vector3 c = center.transform.position;
            line.SetPosition(i, c + new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }
}
