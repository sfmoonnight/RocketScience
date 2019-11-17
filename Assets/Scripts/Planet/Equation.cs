using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Element
{
	public enum ElementType { plus, minus, times, divide, equals, sqrt, square, number};

	public ElementType type;
	public int data;
	public bool isVar;

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

    public string dataToString()
	{
		return isVar ? "X:"+data : ""+data;
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
    public int rndMin=-20, rndMax=21;

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

	public abstract void genElements();

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

public class Addition1: Equation
{
    // 1 + 1 = x
    public override void genElements()
	{
		answer = genNumber();
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

	}

}

public class SquareAddition1 : Equation
{
	// 1 + 1 = x^2
	public override void genElements()
	{

	}
}

