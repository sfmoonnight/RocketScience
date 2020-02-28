using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberHelper
{
    public Number.Symbol symbol;
    public int number;

    public NumberHelper()
    {
        GenerateOperator();
        GenerateOperand();
    }

    public NumberHelper(Number.Symbol symbol, int number)
    {
        this.symbol = symbol;
        this.number = number;
    }
    public NumberHelper(Number.Symbol symbol)
    {
        this.symbol = symbol;
        GenerateOperand();
    }
    public void SetNumber(int num)
    {
        number = num;
    }

    public void SetSymbol(Number.Symbol s)
    {
        symbol = s;
    }

    public int applyOperation(int input)
    {
        switch (symbol)
        {
            case Number.Symbol.plus:
                input += number;
                break;
            case Number.Symbol.minus:
                input -= number;
                break;
            case Number.Symbol.times:
                input *= number;
                break;
            case Number.Symbol.divide:
                input /= number;
                break;
        }
        return input;
    }

    public string toString()
    {
        return GetSymbolString() + number.ToString();
    }

    public string GetSymbolString()
    {
        string symbolString = "";
        switch (symbol)
        {
            case Number.Symbol.plus:
                symbolString = "+";
                break;
            case Number.Symbol.minus:
                symbolString = "-";
                break;
            case Number.Symbol.times:
                symbolString = "×";
                break;
            case Number.Symbol.divide:
                symbolString = "÷";
                break;
        }
        return symbolString;
    }

    public void GenerateOperator()
    {
        int index = Random.Range(1, 5);
        switch (index)
        {
            case 1:
                SetSymbol(Number.Symbol.plus);
                break;
            case 2:
                SetSymbol(Number.Symbol.minus);
                break;
            case 3:
                SetSymbol(Number.Symbol.times);
                break;
            default:
                SetSymbol(Number.Symbol.divide);
                break;
        }
    }

    public void GenerateOperand()
    {
        int num;
        if (symbol == Number.Symbol.plus || symbol == Number.Symbol.minus)
        {
            num = Random.Range(1, 11);
        }
        else if (symbol == Number.Symbol.times)
        {
            num = RandomIntExcept(-10, 10, new int[] { 1 });
        }
        else
        {
            num = RandomIntExcept(-10, 10, new int[] { 0, 1 });
        }

        SetNumber(num);

    }

    public int RandomIntExcept(int min, int max, int[] except)
    {

        //int random = Random.Range(min, max + 1);
        //while (random in except) {
        //    random = Random.Range(min, max + 1);
        //}
        //return random;

        int random = Random.Range(min, max + 1);
        foreach (int exc in except)
        {
            if (random == exc)
            {
                random = RandomIntExcept(min, max, except);
            }
        }
        return random;
    }
}
