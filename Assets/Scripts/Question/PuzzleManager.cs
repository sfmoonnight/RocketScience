using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : EquationManager
{
    public GameObject numberPrefab;
    public int startNumber;
    public int endNumber;
    public int length;
    public List<string> nums = new List<string>();

    List<NumberHelper> waypoints = new List<NumberHelper>();
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        startNumber = Toolbox.GetInstance().GetGameManager().answer;
        endNumber = equation.answer;
        waypoints = GenerateNumberSequence(length);

        foreach(NumberHelper nh in waypoints)
        {
            nums.Add(nh.toString());
            GenerateNumber(transform.position.x, transform.position.y, 5, 1, nh);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateNumber(float cx, float cy, float perturb, float showProb, NumberHelper nh)
    {
        GameObject number = Instantiate(numberPrefab);
        Number n = number.GetComponent<Number>();
        n.SetPerturb(perturb);
        n.SetProb(showProb);
        n.SetCenter(cx, cy);
        n.GenerateFromNumberHelper(nh);
    }
}
