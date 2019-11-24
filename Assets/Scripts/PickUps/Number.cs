using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    public enum Symbol {plus, minus, times, divide};
    public enum State {active, fading, inactive};

    [SerializeField] public Symbol symbol;
    TextMesh numberText;
    [SerializeField] public int number;
    public string symbolString;
    public string suffix = "";
    float cx, cy, perturb, prob;
    public State state = State.inactive;
    public Sprite circle;

    float creationTime;
    float lifespan;
    // Start is called before the first frame update

    public static Number fromInteger(int source)
    {
        Number n = new Number();
        n.SetNumber(Mathf.Abs(source));
        if (source < 0)
        {
            n.SetSymbol(Symbol.minus);
        }
        else
        {
            n.SetSymbol(Symbol.plus);
        }
        return n;
    }

    public bool IsEmpty()
    {
        return symbolString == null;
    }

    void Awake()
    {
        numberText = GetComponent<TextMesh>();
    }

    private void Start()
    {
        SetSymbolString();
        SetNumberText();
    }

    public void SetNumberText()
    {
        //print(numberText);
        numberText.text = toString();
    }

    public string toString()
    {
        string s = symbolString + number.ToString() + suffix;
        if (state == State.inactive)
        {
            s = "h " + s;
        }
        return s;
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
        state = State.inactive;
        SetNumberText();
        //GetComponent<MeshRenderer>().enabled = false;
        //GetComponent<Collider2D>().enabled = true;
    }

    public void ShowNumber()
    {
        state = State.active;
        //GetComponent<MeshRenderer>().enabled = true;
        //GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && state == State.active)
        {
            GameManager gm = Toolbox.GetInstance().GetGameManager();
            int currentAnswer = gm.answer;
            int newAnswer = applyOperation(currentAnswer);
            Toolbox.GetInstance().GetGameManager().SetAnswer(newAnswer);
            HideNumber();
        }
    }

    public int applyOperation(int input)
    {
        switch (symbol)
        {
            case Symbol.plus:
                input += number;
                break;
            case Symbol.minus:
                input -= number;
                break;
            case Symbol.times:
                input *= number;
                break;
            case Symbol.divide:
                input /= number;
                break;
        }
        return input;
    }

    public void GenerateRandom()
    {
        GenerateOperator();
        GenerateOperand();
        GeneratePosition();
        GenerateVisibility();
    }

    public void GenerateOperator()
    {
        int index = Random.Range(1, 5);
        switch (index)
        {
            case 1:
                SetSymbol(Symbol.plus);
                break;
            case 2:
                SetSymbol(Symbol.minus);
                break;
            case 3:
                SetSymbol(Symbol.times);
                break;
            default:
                SetSymbol(Symbol.divide);
                break;
        }
    }

    public void GenerateOperand()
    {
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
        
    }

    public void GenerateVisibility()
    {
        float p = Random.Range(0f, 1f);
        if (p > prob)
        {
            HideNumber();
        }
        else
        {
            ShowNumber();
        }
    }

    public void GeneratePosition()
    {
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

    public void AttachToNearest(float radius)
    {
        //https://forum.unity.com/threads/clean-est-way-to-find-nearest-object-of-many-c.44315/
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        
        Collider2D nearestCollider = null;
        float minSqrDistance = Mathf.Infinity;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "question")
            {
                float sqrDistanceToCenter = (transform.position - colliders[i].transform.position).sqrMagnitude;
                if (sqrDistanceToCenter < minSqrDistance)
                {
                    minSqrDistance = sqrDistanceToCenter;
                    nearestCollider = colliders[i];
                }
            }
        }
        if (nearestCollider != null)
        {
            EquationManager eqm = nearestCollider.gameObject.GetComponentInChildren<EquationManager>();
            eqm.numbers.Add(this);
            //print("In collider check: " + this.IsEmpty());
        }
    }
}
