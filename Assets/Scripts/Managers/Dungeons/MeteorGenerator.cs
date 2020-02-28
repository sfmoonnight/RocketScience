using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorGenerator : MonoBehaviour
{
    public GameObject number;

    public List<GameObject> meteors = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GenerateMeteors(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateMeteors(int count)
    {   
        for(int i=0; i<count; i++)
        {
            float random = Random.Range(-15f, 15f);
            Vector3 offset = new Vector2(1, -1) * random;
            GameObject num = Instantiate(number, transform.position + offset, Quaternion.identity);
            num.GetComponent<Number>().GeneratePositiveValue();
            num.GetComponent<Number>().ShowNumber();
            float speed = Random.Range(3f, 9f);
            num.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, -1) * speed, ForceMode2D.Impulse);
            meteors.Add(num);
        }  
    }

    public void Restart(Number num)
    {
        float random = Random.Range(-15f, 15f);
        Vector3 offset = new Vector2(1, -1) * random;
        num.transform.position = transform.position + offset;
        num.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -1) * Random.Range(3f, 9f);
        num.GetComponent<Number>().GeneratePositiveValue();
        num.GetComponent<Number>().SetSymbolString();
        num.GetComponent<Number>().SetNumberText();
        num.GetComponent<Number>().ShowNumber();
    }
}
