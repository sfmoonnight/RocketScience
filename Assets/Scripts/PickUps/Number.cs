using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Number : MonoBehaviour
{
    public enum Symbol {plus, minus, times, divide};
    public enum State {active, fadein, fadeout, inactive};

    [SerializeField] public Symbol symbol;
    TextMeshPro numberText;
    public int number;
    public string symbolString;
    public string suffix;

    float cx, cy, perturb, prob;
    public State state;
    public Sprite circle;

    public float fadeinTime;
    public float fadeoutTime;

    Animator anim;
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
        numberText = GetComponent<TextMeshPro>();
        anim = GetComponent<Animator>();
        state = State.inactive;
        suffix = "";
    }

    private void Start()
    {
        SetSymbolString();
        SetNumberText();
    }

    public void SetNumberText()
    {
        //print(numberText);
        string rawText = toString();
        for (int i = 0; i <= 9; i++) {
            rawText = rawText.Replace(i.ToString(), "<sprite="+i+">");
        }
        rawText = rawText.Replace("+", "<sprite=" + 10 + ">");
        rawText = rawText.Replace("-", "<sprite=" + 11 + ">");
        rawText = rawText.Replace("×", "<sprite=" + 12 + ">");
        rawText = rawText.Replace("÷", "<sprite=" + 13 + ">");

        numberText.text = rawText;
        //numberText.text = toString();
    }

    public string toString()
    {
        string s = symbolString + number.ToString() + suffix;
        //if (state == State.inactive)
        //{
        //    s = "h " + s;
        //}
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

    void HideNumber()
    {
        state = State.inactive;
        SetNumberText();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<TextMeshPro>().enabled = false;
        //GetComponent<Collider2D>().enabled = true;
    }

    void ShowNumber()
    {
        state = State.active;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<TextMeshPro>().enabled = true;
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
        NumberHelper nh = new NumberHelper(symbol, number);
        return nh.applyOperation(input);
    }

    public void GenerateRandom()
    {
        NumberHelper nh = new NumberHelper();
        SetSymbol(nh.symbol);
        SetNumber(nh.number);
        GeneratePosition();
        GenerateVisibility();
    }

    public void GenerateFromNumberHelper(NumberHelper nh)
    {
        SetSymbol(nh.symbol);
        SetNumber(nh.number);
        GeneratePosition();
        GenerateVisibility();
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

    public void Respawn(Symbol newSym, int newNum)
    {
        SetSymbol(newSym);
        SetNumber(newNum);
        SetSymbolString();
        SetNumberText();
        StartCoroutine("Fadein");
    }

    public void Deactivate()
    {
        //print("Deactivate called for " + toString());
        StartCoroutine("Fadeout");
    }

    IEnumerator Fadein()
    {
        //print("Fading in " + toString());
        // TODO: Run fadein animation
        anim.SetTrigger("fadein");
        ShowNumber();      
        yield return new WaitForSeconds(fadeinTime);
    }


    IEnumerator Fadeout()
    {
        //print("Fading out " + toString());
        // TODO: Run fadeout animation
        anim.SetTrigger("fadeout");
        yield return new WaitForSeconds(fadeoutTime);
        anim.SetTrigger("idle");
        HideNumber();
    }

}
