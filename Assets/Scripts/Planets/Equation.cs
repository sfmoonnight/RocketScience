using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Element
{
	public enum ElementType { plus, minus, times, divide, equals, sqrt, square, number};

	public ElementType type;
	public int data;
	public bool isVar;
    public string varSymbol = "X";

    public Element(ElementType t)
	{
		type = t;
	}

	public Element(ElementType t, int d)
	{
		type = t;
		data = d;
	}

    public void setIsVar(bool v)
	{
		isVar = v;
	}

    public Element asVar()
	{
		setIsVar(true);
		return this;
	}

    public Element asVar(string s)
    {
        setIsVar(true);
        varSymbol = s;
        return this;
    }

    public string dataToString()
	{
		return isVar ? varSymbol : ""+data;
	}

    public string toString()
	{
        switch (type)
		{
			case ElementType.plus:
				return " + ";
			case ElementType.minus:
				return " - ";
			case ElementType.times:
				return " x ";
			case ElementType.divide:
				return " / ";
			case ElementType.equals:
				return " = ";
			case ElementType.number:
				return dataToString();
			case ElementType.sqrt:
				return "sqrt(" + dataToString() + ")";
			case ElementType.square:
				return dataToString() + "^2";
		}
		return "?";
	}
}

public abstract class Equation
{
    public int answer;
    public Element[] elements;
    public int rndMin=-30, rndMax=31;

    public Equation()
	{
        genElements();
	}
    public Equation(int rndMin, int rndMax)
	{
		this.rndMin = rndMin;
		this.rndMax = rndMax;
		genElements();
	}

    public int genNumber()
	{
        int num = Random.Range(rndMin, rndMax);
		return num;
	}

    public int genSingleNumber()
    {
        int num = Random.Range(-10, 11);
        return num;
    }

    public int GetFactor(int num)
    {
		List<int> factors = new List<int>();
		factors.Add(1);
        if(num != 0)
        {
			factors.Add(num);
		}
		
        for (int i = 2; i <= num/2; i++)
        {
            if(num % i == 0 && i !=0 )
            {
				factors.Add(i);
            }
        }
		int index = Random.Range(0, factors.Count);
		return factors[index];
    }

    public abstract void genElements();

	public abstract void genElements(int answer);

    public string toString()
	{
        string r = "";
        foreach (Element e in elements)
		{
			r += e.toString();
		}
		return r;
	}

}

public class BlankEquation : Equation
{
    // 
    public override void genElements()
    {
    }

    public override void genElements(int answer)
    {
    }
}

public class Addition1: Equation
{
    // 1 + 1 = x
    public override void genElements()
	{
		answer = genNumber();
		genElements(answer);
	}

	public override void genElements(int answer)
    {
		int op1 = genNumber();
		int op2 = answer - op1;
		elements = new Element[] {
			new Element(Element.ElementType.number, op2),
			new Element(Element.ElementType.plus),
			new Element(Element.ElementType.number, op1),
			new Element(Element.ElementType.equals),
			new Element(Element.ElementType.number, answer).asVar()
		};
	}
}

public class Addition2 : Equation
{
	// 1 + x = 5
	public override void genElements()
	{
        answer = genNumber();
		genElements(answer);
	}

	public override void genElements(int answer)
    {
		int op1 = genNumber();
		int op2 = answer + op1;
		elements = new Element[] {
			new Element(Element.ElementType.number, op1),
			new Element(Element.ElementType.plus),
			new Element(Element.ElementType.number, answer).asVar(),
			new Element(Element.ElementType.equals),
			new Element(Element.ElementType.number, op2)
		};
	}

}

public class SquareAddition1 : Equation
{
	// 1 + 1 = x^2
	public override void genElements()
	{

	}

	public override void genElements(int answer)
    {

    }
}

public class Multiplication1 : Equation
{
    // 1 x 1 = x
    public override void genElements()
    {
		answer = genNumber();
		genElements(answer);
	}

	public override void genElements(int answer)
    {
		int op1 = GetFactor(answer);
        // TODO: op1 could be 0
		int op2 = answer / op1;
		elements = new Element[] {
			new Element(Element.ElementType.number, op1),
			new Element(Element.ElementType.times),
			new Element(Element.ElementType.number, op2),
			new Element(Element.ElementType.equals),
			new Element(Element.ElementType.number, answer).asVar()
		};
	}
}
