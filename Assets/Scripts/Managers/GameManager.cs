using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int answer;
    // Start is called before the first frame update
    void Start()
    {
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
}
