using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationManager : DungeonManager
{
    // Start is called before the first frame update
    public Camera camera;
    float counter = 0.05f;
    public Rocket rocket;
    public Constellation constellation;
    public SpriteRenderer darkBackground;

    Color color;
    public override void Start()
    {
        GetComponent<NumberGenerator>().GenerateRandomNumbers();
        color = Color.white;
        color.a = 0;
        darkBackground.color = color;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (finished)
        {
            ZoomOut();
            if (camera.transform.position == new Vector3(0, 0, -10))
            {
                constellation.drawConstellation();
            }
        }

    
        foreach(Star s in constellation.stars)
        {
            if (!s.solved)
            {
                return;
            }
        }

        StartCoroutine(Finishing());
    }

    public void ZoomOut()
    {
        counter *= 1.05f;
        camera.GetComponent<FollowPosition>().enabled = false;
        camera.orthographicSize = Mathf.Lerp(15, 35, counter);
        camera.transform.position = Vector3.Lerp(new Vector3(rocket.transform.position.x, rocket.transform.position.y, -10), new Vector3(0,0,-10), counter);

        color.a = Mathf.Lerp(0, 1, counter);
        darkBackground.color = color;
    }

    IEnumerator Finishing()
    {
        yield return new WaitForSeconds(1.5f);
        finished = true;
    }
}
