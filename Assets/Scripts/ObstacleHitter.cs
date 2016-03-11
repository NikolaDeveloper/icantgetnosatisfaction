using UnityEngine;
using System.Collections;

public class ObstacleHitter : MonoBehaviour {

    TrainController tc;

    void Start()
    {
        tc = gameObject.GetComponent<TrainController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "block" || col.tag == "wood")
        {

            // slow down

            tc.throttleSpeed *= 0.5f;

            // find free lane

            bool topLaneIsFree = true;
            bool midLaneIsFree = true;
            bool botLaneIsFree = true;

            float trainBackEdge = gameObject.transform.position.x - 75f;
            float trainFrontEdge = gameObject.transform.position.x + 75f;

            foreach (ProcGen.Element e in ProcGen.Instance.topTrack)
            {
                // check if tile is within the trains positions
                if (e.pos + 25f >= trainBackEdge && e.pos - 25f <= trainFrontEdge)
                {
                    if (e.type != ProcGen.ElementType.Track)
                    {
                        topLaneIsFree = false;
                        break;
                    }
                }
            }

            foreach (ProcGen.Element e in ProcGen.Instance.midTrack)
            {
                // check if tile is within the trains positions
                if (e.pos + 25f >= trainBackEdge && e.pos - 25f <= trainFrontEdge)
                {
                    if (e.type != ProcGen.ElementType.Track)
                    {
                        midLaneIsFree = false;
                        break;
                    }
                }
            }

            foreach (ProcGen.Element e in ProcGen.Instance.botTrack)
            {
                // check if tile is within the trains positions
                if (e.pos + 25f >= trainBackEdge && e.pos - 25f <= trainFrontEdge)
                {
                    if (e.type != ProcGen.ElementType.Track)
                    {
                        botLaneIsFree = false;
                        break;
                    }
                }
            }

            if (topLaneIsFree)
            {
                tc.currentTrack = 0;
            }
            else if (midLaneIsFree)
            {
                tc.currentTrack = 1;
            }
            else if (botLaneIsFree)
            {
                tc.currentTrack = 2;
            }
            else
            {
                Debug.Log("ERRROR! No track is free for the train to move to.");
            }

        }
    }
    

	void Update()
    {
	
	}
}
