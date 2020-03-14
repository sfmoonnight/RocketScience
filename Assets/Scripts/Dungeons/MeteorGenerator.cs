using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorGenerator : MonoBehaviour
{
    public GameObject number;

    public int count;
    //[Tooltip("Frequency in seconds")]
    public int interval_min;
    public int interval_max;
    [Tooltip("Delay before generating at the beginning in seconds")]
    public int genDelay_min;
    public int genDelay_max;

    public Number.Symbol symbol;
    public int min;
    public int max;
    public Vector2 direction;

    public List<GameObject> meteors = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateMeteors(count));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GenerateMeteors(int count)
    {   
        for(int i=0; i<count; i++)
        {
            float ran = Random.Range(genDelay_min, genDelay_max);
            yield return new WaitForSeconds(ran);
            float random = Random.Range(-15f, 15f);
            Vector3 offset = new Vector2(-direction.x, direction.y) * random;
            GameObject num = Instantiate(number, transform.position + offset, Quaternion.identity);
            num.GetComponent<Number>().GenerateRandomValue(symbol, min, max);
            num.GetComponent<Number>().ShowNumber();
            float speed = Random.Range(3f, 9f);
            num.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
            meteors.Add(num);
            num.GetComponent<Number>().startingPoint = this.gameObject;
        }  
    }

    /*public void Restart(Number num)
    {
        StartCoroutine("RestartNumber");
    }*/

    public IEnumerator Restart(Number num)
    {
        //print("------called");
        int interval = Random.Range(interval_min, interval_max + 1);
        yield return new WaitForSeconds(interval);

        float random = Random.Range(-15f, 15f);
        Vector3 offset = new Vector2(-direction.x, direction.y) * random;
        num.transform.position = transform.position + offset;
        num.GetComponent<Rigidbody2D>().velocity = direction * Random.Range(3f, 9f);
        num.GetComponent<Number>().GenerateRandomValue(symbol, min, max);
        num.GetComponent<Number>().SetSymbolString();
        num.GetComponent<Number>().SetNumberText();
        num.GetComponent<Number>().ShowNumber();
    }
}
