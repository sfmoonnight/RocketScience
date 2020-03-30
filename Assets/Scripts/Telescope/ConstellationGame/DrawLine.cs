using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    List<float> distances;
    List<ParticleSystem> particles;
    List<LineRenderer> lines;
    float starTime;
    float counter;

    Transform startPoint;
    List<Transform> endPoint;
    public float lineDrawSpeed;

    public ParticleSystem particleSystem;
    public LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        starTime = Time.time;
        particles = new List<ParticleSystem>();
        lines = new List<LineRenderer>();
        distances = new List<float>();
        counter = 0;

        for(int i = 0; i < endPoint.Count; i++)
        {
            LineRenderer lr = Instantiate(line).GetComponent<LineRenderer>();
            lr.SetPosition(0, startPoint.position);
            lr.SetPosition(1, startPoint.position);
            lines.Add(lr);

            float dis = Vector3.Distance(startPoint.position, endPoint[i].position);
            distances.Add(dis);

            ParticleSystem ps = Instantiate(particleSystem).GetComponent<ParticleSystem>();
            ps.transform.position = startPoint.position;
            var sh = ps.shape;
            sh.radius = 0;

            Vector3 delta = endPoint[i].position - startPoint.position;
            float angle = Mathf.Atan2(delta.y, delta.x);
            ps.transform.Rotate(Vector3.forward, Mathf.Rad2Deg * angle);

            particles.Add(ps);
        }
        //lr = GetComponent<LineRenderer>();     
    }

    public void SetUpLine(Transform starPoint, List<Transform> endPoint)
    {
        this.startPoint = starPoint;
        this.endPoint = endPoint; 
    }

    // Update is called once per frame
    void Update()
    {
        Draw();
    }

    void Draw()
    {
        for (int i = 0; i < endPoint.Count; i++)
        {
            if (counter < distances[i])
            {
                //counter += 0.1f / lineDrawSpeed;
                counter += 0.3f * lineDrawSpeed;

                float x = Mathf.Lerp(0, distances[i], counter);

                Vector3 pointA = startPoint.position;
                Vector3 pointB = endPoint[i].position;

                //Vector3 pointAlingLine = x * Vector3.Normalize(pointB - pointA) + pointA;
                Vector3 pointAlingLine = counter * Vector3.Normalize(pointB - pointA) + pointA;

                var sh = particles[i].shape;
                //particles[i].transform.position = 0.5f * x * Vector3.Normalize(pointAlingLine - pointA) + pointA;
                particles[i].transform.position = 0.5f * counter * Vector3.Normalize(pointAlingLine - pointA) + pointA;
                sh.radius = Vector3.Distance(pointA, pointAlingLine) / 2;

                lines[i].SetPosition(1, pointAlingLine);
                //print("not activating next star" + counter + "----" + distances[i]);

                if (Vector3.Distance(pointAlingLine, pointA) >= distances[i])
                {
                    //print("Activate next star");
                    GetComponent<Star>().ActivateNextStar();
                }
            }
        }    
    }
}
