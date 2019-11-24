using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationManager : MonoBehaviour
{
    public enum UpdateStrategy { overwrite, no_overwrite };

    public Number currentSeed;
    TextMesh eqTextMesh;
    public Equation equation;
    public List<Number> numbers;
    // Start is called before the first frame update

    private void Awake()
    {
        Addition1 eq = new Addition1();
        equation = eq;


        numbers = new List<Number>();
    }
    void Start()
    {

        eqTextMesh = gameObject.GetComponent<TextMesh>();
        eqTextMesh.text = equation.toString();
    }

    public void OptimizeDifficulty(UpdateStrategy strat)
    {
        print("Working with " + numbers.Count + " numbers");
        foreach (Number n in numbers)
        {
            //print("Empty: " + n.IsEmpty());
            if (n.state == Number.State.active)
            {
                //print("Setting text");
                n.suffix = "^";
                n.SetNumberText();
            }
        }
        List<Number> necessaryNums = GetNecessaryNumbers();

        foreach (Number n in necessaryNums)
        {
            n.SetSymbolString();
            print("Op: " + n.toString());
        }
        UpdateNumbers(necessaryNums, strat);
    }

    void UpdateNumbers(List<Number> nums, UpdateStrategy strat)
    {
        int random1 = Random.Range(0, numbers.Count);
        Number seed = numbers[random1];
        currentSeed = seed;
        seed.SetSymbolString();
        print("Seed number is " + seed.toString());
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(seed.gameObject.transform.position, 20f);
        print("Collided * with " + colliders.Length);

        foreach (Collider2D c in colliders)
        {
            Number near = c.gameObject.GetComponent<Number>();
            if (near != null)
            {
                near.suffix = "*";
                near.SetNumberText();
            }
        }
        seed.suffix = "**";
        seed.SetNumberText();

        List<GameObject> nearNums = new List<GameObject>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "number")
            {
                nearNums.Add(colliders[i].gameObject);
            }
        }



        if (nearNums.Count < nums.Count)
        {
            print("Error, not enough spaces to place numbers " + nearNums.Count + " " + nums.Count);
            return;
        }
        foreach (Number n in nums)
        {
            int random = Random.Range(0, nearNums.Count);
            Number updateNum = nearNums[random].GetComponent<Number>();
            nearNums.RemoveAt(random);

            updateNum.SetSymbol(n.symbol);
            updateNum.SetNumber(n.number);
            updateNum.SetSymbolString();
            updateNum.suffix = "~";
            updateNum.SetNumberText();
            updateNum.ShowNumber();
        }
        
    }

    List<Number> GetNecessaryNumbers()
    {
        List<Number> nums = GenerateNumberSequence(2);
        return nums;
    }

    List<Number> GenerateNumberSequence(int length)
    {
        int currAnswer = Toolbox.GetInstance().GetGameManager().answer;
        int corrAnswer = equation.answer;
        return GenerateNumberSequence(length, currAnswer, corrAnswer);
    }

    List<Number> GenerateNumberSequence(int length, int startNum, int endNum)
    {
        List<Number> nums = new List<Number>();
        int a = startNum;
        for (int i = 0; i < length; i++)
        {
            if (i == length - 1)
            {
                int delta = endNum - a;
                Number n = Number.fromInteger(delta);
                nums.Add(n);
            }
            else
            {
                Number n = new Number();
                n.GenerateOperator();
                n.GenerateOperand();
                nums.Add(n);
                a = n.applyOperation(a);
            }
        }
        return nums;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
