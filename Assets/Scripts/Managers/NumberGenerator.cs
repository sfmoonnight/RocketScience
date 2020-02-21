using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGenerator : MonoBehaviour
{
    //public enum Symbol { plus, minus, times, division };

    public GameObject numberPrefab;
    public GameObject rocket;
    public float patchSize;
    public float spacing;
    public float genprob;
    float refreshRate;
    public float fadeInTime;
    public float fadeOutTime;
    public float pauseTime;
    float lastRefresh;

    public bool initialized = false;

    // The radius around each number to look for question (i.e. planet)
    // to attach to
    public float attachRadius;

    float minx, miny, maxx, maxy;
    // Start is called before the first frame update

    private void Awake()
    {
        rocket = GameObject.Find("Rocket");
        refreshRate = fadeInTime + fadeOutTime + pauseTime + 2;

        //initBoundaries();
    }
    public virtual void Start()
    {
        GenerateRandomNumbers(-patchSize, -patchSize, patchSize, patchSize, spacing);
    }

    public virtual void LateUpdate()
    {
        if (!initialized)
        {
            OptimizeDifficulty(EquationManager.UpdateStrategy.overwrite);
            initialized = true;
            lastRefresh = Time.time;
        }
        else
        {
            if (Time.time - lastRefresh > refreshRate)
            {
                // Refresh the numbers
                //print("Time to refresh the universe");
                StartCoroutine("Refresh");
                lastRefresh = Time.time;
            }
        }
    }

    IEnumerator Refresh()
    {
        //print("Fading out numbers...");
        // Fade out some numbers
        MakeRoomForNewNumbers();

        yield return new WaitForSeconds(fadeOutTime + 1f);

        //print("Fading in numbers...");
        // Fade in new numbers
        OptimizeDifficulty(EquationManager.UpdateStrategy.no_overwrite);

    }

    void MakeRoomForNewNumbers()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("question");
        //print("Found " + gos.Length + "questions");
        foreach (GameObject g in gos)
        {
            EquationManager em = g.GetComponentInChildren<EquationManager>();
            em.MakeRoom();
        }
    }

    public void OptimizeDifficulty(EquationManager.UpdateStrategy strat)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("question");

        foreach (GameObject g in gos)
        {
            EquationManager em = g.GetComponentInChildren<EquationManager>();
            //print("Going to optimize");
            em.OptimizeDifficulty(strat);
        }
    }

    // Update is called once per frame 
    void Update()
    {
        // compute area that require numbers
        // 
    }
    private void FixedUpdate()
    {
        //GenerateRandomNumbers();
    }

    void initBoundaries()
    {
        Vector2 pos = rocket.transform.position;
        minx = pos.x - patchSize;
        miny = pos.y - patchSize;
        maxx = pos.x + patchSize;
        maxy = pos.y + patchSize;
        //print("minx " + minx + " maxx " + maxx);
        GenerateRandomNumbers(minx, miny, maxx, maxy, spacing);
    }

    public void GenerateRandomNumbers()
    {
        //GenerateRandomNumbers(-5f, -5f, 5f, 5f, .8f);
        //GenerateRandomNumber(0f, 0f);
        Vector2 pos = rocket.transform.position;
        if (pos.x < minx + patchSize)
        {
            GenerateRandomNumbers(minx - patchSize, miny, minx, maxy, spacing);
            minx -= patchSize;
        }
        if (pos.x > maxx - patchSize)
        {
            GenerateRandomNumbers(maxx, miny, maxx + patchSize, maxy, spacing);
            maxx += patchSize;
        }
        if (pos.y < miny + patchSize)
        {
            GenerateRandomNumbers(minx, miny - patchSize, maxx, miny, spacing);
            miny -= patchSize;
        }
        if (pos.y > maxy - patchSize)
        {
            GenerateRandomNumbers(minx, maxy, maxx, maxy + patchSize, spacing);
            maxy += patchSize;
        }



    }

    public void GenerateRandomNumbers(float left, float top, float right, float bottom, float spacing)
    {
        //print("Generating for " + left + " " + top + " " + right + " " + bottom);
        //float xlen = (right - left)/spacing;
        //float ylen = (bottom - top)/spacing;
        for (float i = left; i < right; i += spacing)
        {
            for (float j = top; j < bottom; j += spacing)
            {
                //float cx = left * i + spacing / 2f;
                //float cy = top * j + spacing / 2f;
                float cx = i + spacing/2f;
                float cy = j + spacing/2f;
                GenerateRandomNumber(cx, cy, spacing/4f, genprob);
                //print("Placing new at " + cx + " " + cy);
            }
        }
    }

    public void GenerateRandomNumber(float cx, float cy, float perturb, float showProb)
    {
        //int num = Random.Range(0, 11);
        //Vector3 pos = new Vector3(x, y, 0f);
        GameObject number = Instantiate(numberPrefab);
        Number n = number.GetComponent<Number>();
        n.SetPerturb(perturb);
        n.SetProb(showProb);
        n.fadeinTime = fadeInTime;
        n.fadeoutTime = fadeOutTime;
        //n.SetNumber(num);
        //n.SetSymbol(GenerateRandomSymbol());
        n.SetCenter(cx, cy);
        n.GenerateRandom();
        //number.transform.position = Vector2.zero;
        //n.SetSymbolString();
        n.AttachToNearest(attachRadius);
    }

    /*public Number.Symbol GenerateRandomSymbol()
    {
        int index = Random.Range(1, 5);
        switch (index)
        {
            case 1:
                return Number.Symbol.plus;
            case 2:
                return Number.Symbol.minus;
            case 3:
                return Number.Symbol.times;
            default:
                return Number.Symbol.divide;
        }
    }*/
}
