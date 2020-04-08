using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOrbit : MonoBehaviour
{
    public GameObject center;
    public int segments;
    public float xradius;
    public float yradius;
    LineRenderer line;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = segments + 1;
        line.useWorldSpace = true;
        CreatePoints();
    }


    void CreatePoints()
    {
        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            Vector3 c = center.transform.position;
            line.SetPosition(i, c +  new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }
}
