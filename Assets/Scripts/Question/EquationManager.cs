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
    public float relatedNumProx;
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

    List<Number> FilterNumbersByState(Number.State state)
    {
        List<Number> emptySlots = new List<Number>();
        foreach (Number n in numbers)
        {
            if (n.state == state)
            {
                emptySlots.Add(n);
            }
        }
        return emptySlots;
    }

    public void MakeRoom()
    {
        // Deactivate some active numbers
        List<Number> activeSlots = FilterNumbersByState(Number.State.active);
        //print("Found " + activeSlots.Count + " active slots.");
        for (int i = 0; i < 4; i++)
        {
            int r = Random.Range(0, activeSlots.Count);
            Number n = activeSlots[r];
            activeSlots.RemoveAt(r);
            //print("Deactivating* " + n.toString());
            n.Deactivate();
        }
    }

    public void OptimizeDifficulty(UpdateStrategy strat)
    {
        /*print("Working with " + numbers.Count + " numbers");
        foreach (Number n in numbers)
        {
            //print("Empty: " + n.IsEmpty());
            if (n.state == Number.State.active)
            {
                //print("Setting text");
                n.suffix = "^";
                n.SetNumberText();
            }
        }*/
        List<List<Number>> numPaths = GetNumberPaths();

        // Find empty slots
        List<Number> emptySlots = FilterNumbersByState(Number.State.inactive);
        

        foreach(List<Number> numPath in numPaths)
        {
            ActivateSlots(numPath, emptySlots);
        }
        /*foreach (Number n in necessaryNums)
        {
            n.SetSymbolString();
            print("Op: " + n.toString());
        }*/
        /*if (strat == UpdateStrategy.overwrite)
        {
            UpdateNumbers(necessaryNums);
        }
        else if (strat == UpdateStrategy.no_overwrite)
        {

        }*/
    }

    Number GetNearestSlot(List<Number> emptySlots, Number seed) {
        float minDist = float.PositiveInfinity;
        Number nearest = null;
        foreach (Number n in emptySlots)
        {
            float dist = (n.transform.position - seed.transform.position).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                nearest = n;
            }
        }
        return nearest;
    }

    void ActivateSlots(List<Number> numPath, List<Number> emptySlots)
    {
        if (numPath.Count > emptySlots.Count)
        {
            print("Warning: ran out of slots to assign path");
            return;
        }
        int r1 = Random.Range(0, emptySlots.Count);
        Number seedSlot = emptySlots[r1];
        emptySlots.RemoveAt(r1);
        seedSlot.Respawn(numPath[0].symbol, numPath[0].number);

        for (int i = 1; i < numPath.Count; i++)
        {
            Number slot = GetNearestSlot(emptySlots, seedSlot);
            emptySlots.Remove(slot);
            slot.Respawn(numPath[i].symbol, numPath[i].number);
        }
    }

    /*void UpdateNumbers(List<Number> nums)
    {
        int random1 = Random.Range(0, numbers.Count);
        Number seed = numbers[random1];
        currentSeed = seed;
        seed.SetSymbolString();
        print("Seed number is " + seed.toString());
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            seed.gameObject.transform.position, relatedNumProx);

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
    }*/

    List<List<Number>> GetNumberPaths()
    {
        List<List<Number>> nums = new List<List<Number>>();
        nums.Add(GenerateNumberSequence(2));
        nums.Add(GenerateNumberSequence(2));
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
