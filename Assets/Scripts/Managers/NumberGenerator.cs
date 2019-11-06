using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGenerator : MonoBehaviour
{
    //public enum Symbol { plus, minus, times, division };

    public GameObject numberPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame 
    void Update()
    {
        // compute area that require numbers
        // 
    }

    public void GenerateRandomNumbers()
    {
        GenerateRandomNumbers(-5f, -5f, 5f, 5f, .8f);
        //GenerateRandomNumber(0f, 0f);
    }

    public void GenerateRandomNumbers(float left, float top, float right, float bottom, float spacing)
    {
        //float xlen = (right - left)/spacing;
        //float ylen = (bottom - top)/spacing;
        for (float i = left; i < right; i += spacing)
        {
            for (float j = top; j < bottom; j += spacing)
            {
                float cx = left * i + spacing / 2f;
                float cy = top * j + spacing / 2f;
                GenerateRandomNumber(cx, cy, spacing, .5f);

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
        //n.SetNumber(num);
        //n.SetSymbol(GenerateRandomSymbol());
        n.SetCenter(cx, cy);
        n.GenerateRandom();
        //number.transform.position = Vector2.zero;

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
