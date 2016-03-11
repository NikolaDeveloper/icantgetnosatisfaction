using UnityEngine;
using System.Collections;

public class ObstacleHitter : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "block" || col.tag == "wood")
        {
            
            // find free lane

            foreach (ProcGen.Element e in ProcGen.Instance.topTrack)
            {
                float trainBackEdge = gameObject.transform.position.x - 75f;
                float trainFrontEdge = gameObject.transform.position.x + 75f;
                // check if tile is within the trains positions
                if (e.pos + 25f >= trainBackEdge || e.pos - 25f <= trainFrontEdge)
                {

                }
            }

        }
    }


    void Start()
    {
	
	}

	void Update()
    {
	
	}
}
