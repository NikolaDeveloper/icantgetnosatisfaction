using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObstacleHitter : MonoBehaviour {

    TrainController tc;

    SpriteRenderer main, stripe, windows;

    Color savedColor, flashColor, savedColorStripe, flashColorStripe, savedColorWindows, flashColorWindows;

    void Start()
    {
        tc = gameObject.GetComponent<TrainController>();

        main = gameObject.GetComponent<SpriteRenderer>();
        stripe = gameObject.GetComponentsInChildren<SpriteRenderer>()[0];
        windows = gameObject.GetComponentsInChildren<SpriteRenderer>()[1];

        savedColor = main.color;
        flashColor = new Color(1 - savedColor.r, 1 - savedColor.g, 1 - savedColor.b, 0.7f);
        savedColorStripe = stripe.color;
        flashColorStripe = new Color(1 - savedColorStripe.r, 1 - savedColorStripe.g, 1 - savedColorStripe.b, 0.7f);
        savedColorWindows = windows.color;
        flashColorWindows = new Color(1 - savedColorWindows.r, 1 - savedColorWindows.g, 1 - savedColorWindows.b, 0.7f);
    }

    IEnumerator ColorFlash()
    {
        for (int i = 0; i < 3; i++)
        {
            main.color = savedColor;
            stripe.color = savedColorStripe;
            windows.color = savedColorWindows;
            yield return new WaitForSeconds(0.1f);
            main.color = flashColor;
            stripe.color = flashColorStripe;
            windows.color = flashColorWindows;
            yield return new WaitForSeconds(0.1f);
        }

        // restore original color
        main.color = savedColor;
        stripe.color = savedColorStripe;
        windows.color = savedColorWindows;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "block" || col.tag == "wood")
        {
            // play sound
            SoundController.Instance.PlayObstacleHitSound();

            // satisfaction hit
            PlayerStats.GetInstance ().satisfaction -= 5;

            // flash
            StartCoroutine(ColorFlash());

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

            if (tc.currentTrack == 0)
            {
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
            else if (tc.currentTrack == 1)
            {
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
            else if (tc.currentTrack == 2)
            {
                if (midLaneIsFree)
                {
                    tc.currentTrack = 1;
                }
                else if (botLaneIsFree)
                {
                    tc.currentTrack = 2;
                }
                else if (topLaneIsFree)
                {
                    tc.currentTrack = 0;
                }
                else
                {
                    Debug.Log("ERRROR! No track is free for the train to move to.");
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
