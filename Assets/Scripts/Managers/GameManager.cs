using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int answer;
    Rocket rocket;
    GameObject[] questions;

    public List<int> inventory;
    // Start is called before the first frame update
    private void Awake()
    {
        answer = Random.Range(-99, 100);
        rocket = GameObject.Find("Rocket").GetComponent<Rocket>();
        questions = GameObject.FindGameObjectsWithTag("question");
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAnswer(int ans)
    {
        answer = ans;
        //print("Questions: " + questions);
        foreach (GameObject go in questions)
        {
            Question q = go.GetComponent<Question>();
            EquationManager em = q.eqTextMeshObj.GetComponent<EquationManager>();
            if (em.equation.answer == answer)
            {
                q.ActivateCollectables();
            }
            else
            {
                q.DeactivateCollectables();
            }
        }
    }

    public Rocket GetRocket()
    {
        return rocket;
    }


}
