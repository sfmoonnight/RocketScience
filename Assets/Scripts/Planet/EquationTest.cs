using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationTest : MonoBehaviour
{
    public Planet planet;
    public TextMesh equation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void test()
    {
        Addition1 eq = new Addition1();
        equation.text = eq.toString();
        planet.answer = eq.answer;
        print(eq.toString());
    }
}
