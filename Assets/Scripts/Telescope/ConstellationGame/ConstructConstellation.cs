using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructConstellation : MonoBehaviour
{
    public ConstellationStructure constellationStructure;
    public List<GameObject> stars;
    // Start is called before the first frame update
    void Start()
    {
        constellationStructure.starsPosition = new List<Vector2>();
        constellationStructure.connectionsEdgeList = new List<Vector2>();
        constellationStructure.connections = new Dictionary<int, List<int>>();
        /*constellationStructure.starsPosition.Clear();
        constellationStructure.connectionsEdgeList.Clear();
        constellationStructure.connections.Clear();*/
        /*foreach (GameObject go in stars)
        {
            constellationStructure.starsPosition.Add(go.transform.localPosition);

            go.GetComponent<>
        }*/

        Constellation cons = GetComponent<Constellation>();
        constellationStructure.constellationName = cons.name;
        List<Star> trackIndex = new List<Star>();
        int count = 0;
        print("star count here " + cons.stars.Count);
        foreach (Star s in cons.stars) {
            constellationStructure.starsPosition.Add(s.transform.localPosition);
            trackIndex.Add(s);
            count++;
            print("On star " + count);
        }

        foreach (Star s in trackIndex)
        {
            int indexS = trackIndex.IndexOf(s);
            constellationStructure.connections[indexS] = new List<int>();
            foreach (Transform t in s.nextStar)
            {
                Star o = t.gameObject.GetComponent<Star>();
                int indexO = trackIndex.IndexOf(o);
                constellationStructure.connections[indexS].Add(indexO);
                constellationStructure.connectionsEdgeList.Add(new Vector2(indexS, indexO));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnValidate()
    {
        constellationStructure.starsPosition = new List<Vector2>();
        foreach (GameObject go in stars)
        {
            constellationStructure.starsPosition.Add(go.transform.localPosition);
        }
    }*/
}
