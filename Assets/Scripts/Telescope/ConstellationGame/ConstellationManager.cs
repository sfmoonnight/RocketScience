using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConstellationManager : DungeonManager
{
    // Start is called before the first frame update
    public Camera camera;
    float counter = 0.05f;
    public Rocket rocket;
    public Constellation constellation;
    public SpriteRenderer darkBackground;

    Color color;
    bool numbersAttached = false;
    List<string> alphabet;
    public override void Start()
    {
        GetComponent<NumberGenerator>().GenerateRandomNumbers();
        color = Color.white;
        color.a = 0;
        darkBackground.color = color;

        GameObject con = Instantiate(Toolbox.GetInstance().GetGameManager().constellationPrefabs[1], new Vector3(0, 0, -2), Quaternion.identity);
        constellation = con.GetComponent<Constellation>();

        alphabet = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        GenerateStarValues();
        GenerateEquations();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (finished)
        {
            ZoomOut();
            if (camera.transform.position == new Vector3(0, 0, -10))
            {
                constellation.StartDrawing();
            }
        }
        //print("showsprite" + constellation.showSprite);
        foreach (Star s in constellation.stars)
        {
            if (!s.solved)
            {
                return;
            }
        }

        StartCoroutine(Finishing());
        
        if (constellation.showSprite)
        {
            print("showsprite" + constellation.showSprite);
            StartCoroutine(ShowNotification());
        }
    }

    private void LateUpdate()
    {
        if (!numbersAttached)
        {
            Number[] numbers = GameObject.FindObjectsOfType<Number>();
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i].AttachToNearestStar();
            }
            numbersAttached = true;
        }
    }

    void GenerateStarValues()
    {
        foreach(Star s in constellation.stars)
        {
            int value = Random.Range(1, 30);
            s.value = value;

            int index = Random.Range(0, alphabet.Count);
            s.SetText(alphabet[index]);
            alphabet.RemoveAt(index);
        }
    }

    void GenerateEquations()
    {
        int starCount = constellation.stars.Count;
        print("starcount " + starCount);
        int i = 0;
        foreach (TextMeshPro t in constellation.equitions)
        {

            Star s = constellation.stars[i];
            string symbol = s.variable;
            int val = s.value;

            float d1 = Random.Range(0f, 1f);
            List<Star> selected;
            if (d1 < 0.8)
            {
                // 2 var equation
                selected = RandomSelectStars(1, s);
            }
            else
            {
                // 3 var equation
                selected = RandomSelectStars(2, s);
            }
            selected.Add(s);

            BlankEquation eq = new BlankEquation();
            List<Element> els = new List<Element>();
            int literal = selected[0].value;
            for (int j = 0; j < selected.Count; j++)
            {
                if(j > 0)
                {
                    float d2 = Random.Range(0f, 1f);
                    if (d2 < 0.5)
                    {
                        els.Add(new Element(Element.ElementType.plus));
                        literal += selected[j].value;
                    }
                    else
                    {
                        els.Add(new Element(Element.ElementType.minus));
                        literal -= selected[j].value;
                    }
                }

                Element e = new Element(Element.ElementType.number, selected[j].value);
                e = e.asVar(selected[j].variable);
                els.Add(e);


            }
            els.Add(new Element(Element.ElementType.equals));
            els.Add(new Element(Element.ElementType.number, literal));

            eq.elements = els.ToArray();
            t.text = eq.toString();
            i += 1;
        }
    }

    private List<Star> RandomSelectStars(int v, Star ex)
    {
        List<Star> allstars = new List<Star>();
        List<Star> selected = new List<Star>();
        for (int i = 0; i < constellation.stars.Count; i++)
        {
            allstars.Add(constellation.stars[i]);
        }

        allstars.Remove(ex);

        for (int i = 0; i < v; i++)
        {
            int r = Random.Range(0, allstars.Count);
            Star sel = allstars[r];
            selected.Add(sel);
            allstars.Remove(sel);
        }
        return selected;
    }

    public void ZoomOut()
    {
        counter *= 1.05f;
        camera.GetComponent<FollowPosition>().enabled = false;
        camera.orthographicSize = Mathf.Lerp(15, 35, counter);
        camera.transform.position = Vector3.Lerp(new Vector3(rocket.transform.position.x, rocket.transform.position.y, -10), new Vector3(0,0,-10), counter);

        color.a = Mathf.Lerp(0, 1, counter);
        darkBackground.color = color;
    }

   

    IEnumerator Finishing()
    {
        if (!finished)
        {
            finished = true;
            yield return new WaitForSeconds(1.5f);

            Toolbox.GetInstance().GetGameManager().DiscoverConstellation(constellation.identity);
            //print("------When finish cons discovered: " + Toolbox.GetInstance().GetStatManager().gameState.constellationsDiscovered.Count);
        }  
    }

    IEnumerator ShowNotification()
    {
        yield return new WaitForSeconds(1.5f);
        scoreBorad.image.sprite = constellation.constellationSprite.sprite;
        scoreBorad.text.text = "You have discovered the " + constellation.name + " constellation!";
        scoreBorad.ShowUI();
    }
}
