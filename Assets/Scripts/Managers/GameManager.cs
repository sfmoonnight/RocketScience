using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int answer;
    Rocket rocket;
    // Start is called before the first frame update
    void Start()
    {
        rocket = GameObject.Find("Rocket").GetComponent<Rocket>();
        answer = Random.Range(-99, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAnswer(int ans)
    {
        answer = ans;
    }

    public Rocket GetRocket()
    {
        return rocket;
    }
}
