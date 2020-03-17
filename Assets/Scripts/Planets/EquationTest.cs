using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationTest : MonoBehaviour
{
    public Question planet;
    public TextMesh eqTextMesh;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void testOpt()
    {
        Number seed = GameObject.Find("Equation").GetComponent<EquationManager>().currentSeed;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(seed.gameObject.transform.position, 20f);
        print("Collided * with " + colliders.Length);
    }

    public void test()
    {
        Addition1 eq = new Addition1();
        eqTextMesh.text = eq.toString();
        //planet.answer = eq.answer;
        print(eq.toString());
    }
}
