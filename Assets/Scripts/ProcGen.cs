using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProcGen : MonoBehaviour {

    public static ProcGen Instance;

    int indexTopTrack = 0;
    int indexMidTrack = 0;
    int indexBotTrack = 0;

    internal enum ElementType { GenericObstacleTile };

    int numOfElements = 1;

    float oneUnit = 50f;
    //internal float elementLength = 50f;

    List<ElementType> AllObstacles;
    
    internal class Element
    {
        internal ElementType type;
        internal float pos;

        internal Element(ElementType t, float p)
        {
            type = t;
            pos = p;
        }
    };

    internal List<Element> topTrack, midTrack, botTrack;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        AllObstacles = new List<ElementType>(numOfElements);
        AllObstacles.Add(ElementType.GenericObstacleTile);

        Generate();
        Debug.Log(topTrack.Count);
        Debug.Log(midTrack.Count);
        Debug.Log(botTrack.Count);
        foreach (Element e in topTrack)
        {
            Debug.Log("In TOP: " + e.type + " at X pos " + e.pos);
        }
    }

    public void Generate()
    {
        topTrack = new List<Element>();
        midTrack = new List<Element>();
        botTrack = new List<Element>();

        float trackDistanceSoFar = 0f;
        float trackDistanceTotal = 3000f;
        //float trackDistanceTotal = 5120f;

        // just for figuring stuff out
        float speed = 128f; // 128 pixels per second (go through a screen in 10 seconds)
        float time = 40f; // 40 seconds


        float chanceAllLanesFree = 0.1f;
        float chanceTwoLanesFree = 0.6f;
        float chanceOneLaneFree = 0.3f;

        // start making the level
        
        trackDistanceSoFar += oneUnit * 4f;

        // safety against bugs with endless loop
        int safetyCounter = 0;

        while (trackDistanceSoFar < trackDistanceTotal)
        {
            // safety against bugs with endless loop
            safetyCounter++;
            if (safetyCounter > 1000)
            {
                Debug.Log("WARNING! Endless Loop Safety Counter Ended!");
                break;
            }

            float v = Random.value;
            // if we have all lanes open
            if (v <= chanceAllLanesFree)
            {
                trackDistanceSoFar += oneUnit;
            }
            // if we have two open lanes and one obstacle
            else if (v <= chanceAllLanesFree + chanceTwoLanesFree)
            {
                // get random obstacle type
                int obsTypeInt = Random.Range(0, numOfElements);
                ElementType obsType = AllObstacles[obsTypeInt];

                // choose the blocked lane
                int lane = Random.Range(0, 2);
                // top
                if (lane == 0) topTrack.Add(new Element(obsType, trackDistanceSoFar));
                // mid
                else if (lane == 1) midTrack.Add(new Element(obsType, trackDistanceSoFar));
                // bot
                else if (lane == 2) botTrack.Add(new Element(obsType, trackDistanceSoFar));

                trackDistanceSoFar += oneUnit;
            }
            // if we have one open lane and two obstacles
            else if (v <= chanceAllLanesFree + chanceTwoLanesFree + chanceOneLaneFree)
            {
                // get random obstacle type
                int obsTypeInt1 = Random.Range(0, numOfElements);
                ElementType obsType1 = AllObstacles[obsTypeInt1];

                // get another random obstacle type
                int obsTypeInt2 = Random.Range(0, numOfElements);
                ElementType obsType2 = AllObstacles[obsTypeInt2];

                // choose the empty lane
                int lane = Random.Range(0, 2);
                // top
                if (lane == 0)
                {
                    midTrack.Add(new Element(obsType1, trackDistanceSoFar));
                    botTrack.Add(new Element(obsType2, trackDistanceSoFar));
                }
                // mid
                else if (lane == 1)
                {
                    topTrack.Add(new Element(obsType1, trackDistanceSoFar));
                    botTrack.Add(new Element(obsType2, trackDistanceSoFar));
                }
                //bot
                else if (lane == 2)
                {
                    topTrack.Add(new Element(obsType1, trackDistanceSoFar));
                    midTrack.Add(new Element(obsType1, trackDistanceSoFar));
                }
                trackDistanceSoFar += oneUnit;
            }

            // train is 3 times longer than the obstacles, so add space
            trackDistanceSoFar += oneUnit * 3f;
        }
        Debug.Log("While ran " + safetyCounter + " times.");
    }

    void CreateLevel()
    {
        foreach (Element e in topTrack)
        {

        }
    }

    void Update()
    {

    }
}
