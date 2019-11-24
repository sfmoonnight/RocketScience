using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Number : MonoBehaviour
{
    public enum Symbol {plus, minus, times, divide};

    [SerializeField] Symbol symbol;
    TextMeshPro numberText;
    [SerializeField] int number;
    string symbolString;
    float cx, cy, perturb, prob;

    public Sprite circle;

    float creationTime;
    float lifespan;
    // Start is called before the first frame update
    void Start()
    {
        SetSymbolString();
        numberText = GetComponent<TextMeshPro>();
        numberText.text = symbolString + number.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSymbolString()
    {
        switch (symbol)
        {
            case Symbol.plus:
                symbolString = "+";
                break;
            case Symbol.minus:
                symbolString = "-";
                break;
            case Symbol.times:
                symbolString = "×";
                break;
            case Symbol.divide:
                symbolString = "÷";
                break;
        }
    }

    public void SetPerturb(float perturb)
    {
        this.perturb = perturb;
    }

    public void SetProb(float prob)
    {
        this.prob = prob;
    }

    public void SetCenter(float cx, float cy)
    {
        this.cx = cx;
        this.cy = cy;
    }

    public void SetNumber(int num)
    {
        number = num;
    }

    public void SetSymbol(Symbol s)
    {
        symbol = s;
    }

    public void HideNumber()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    public void ShowNumber()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int currentAnswer = Toolbox.GetInstance().GetGameManager().answer;
            switch (symbol)
            {
                case Symbol.plus:
                    currentAnswer += number;
                    break;
                case Symbol.minus:
                    currentAnswer -= number;
                    break;
                case Symbol.times:
                    currentAnswer *= number;
                    break;
                case Symbol.divide:
                    currentAnswer /= number;
                    break;
            }
            Toolbox.GetInstance().GetGameManager().SetAnswer(currentAnswer);
            HideNumber();
        }
    }

    public void GenerateRandom()
    {
        int index = Random.Range(1, 5);
        switch (index)
        {
            case 1:
                SetSymbol(Symbol.plus);
                Generate();
                break;
            case 2:
                SetSymbol(Symbol.minus);
                Generate();
                break;
            case 3:
                SetSymbol(Symbol.times);
                Generate();
                break;
            default:
                SetSymbol(Symbol.divide);
                Generate();
                break;
        }
    }

    public void Generate()
    {
        float p = Random.Range(0f, 1f);
        if (p > prob)
        {
            HideNumber();
        }
        int num;
        if(symbol == Symbol.plus || symbol == Symbol.minus)
        {
            num = Random.Range(1, 11);
        }else if (symbol == Symbol.times)
        {
            num = RandomIntExcept(-10, 10, new int[] {1});
        }
        else
        {
            num = RandomIntExcept(-10, 10, new int[] {0, 1});
        }
        
        SetNumber(num);
        float x = cx + Random.Range(-perturb, perturb);
        float y = cy + Random.Range(-perturb, perturb);
        transform.position = new Vector2(x, y);
    }

    public int RandomIntExcept(int min, int max, int[] except)
    {
        
        //int random = Random.Range(min, max + 1);
        //while (random in except) {
        //    random = Random.Range(min, max + 1);
        //}
        //return random;

        int random = Random.Range(min, max+1);
        foreach(int exc in except)
        {
            if (random == exc)
            {
                random = RandomIntExcept(min, max, except);
            }
        }     
        return random;
    }
}
