using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProcGen : MonoBehaviour {

    public static ProcGen Instance;

    // prefabs
    public GameObject trackTile, woodObsTile, startGapTile, endGapTile;
    public GameObject decoRock1, decoRock2, decoTree1, decoTree2, decoTree3;
    
    float topTrackYPos = 75f;
    float midTrackYPos = 0f;
    float botTrackYPos = -75f;

    float trackDistanceTotal = 6000f;

    internal enum ElementType { WoodObs, Gap, Track };
    int numOfElements = 3;

    float oneUnit = 50f;

    Dictionary<ElementType, GameObject> AllElementsPrefabs;

    List<ElementType> AllElements;

    internal class Element
    {
        internal ElementType type;
        internal float pos;
        internal float posEnd = -1f;

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
        AllElements = new List<ElementType>(numOfElements);
        AllElements.Add(ElementType.WoodObs);
        AllElements.Add(ElementType.Gap);
        AllElements.Add(ElementType.Track);

        AllElementsPrefabs = new Dictionary<ElementType, GameObject>();
        AllElementsPrefabs[ElementType.WoodObs] = woodObsTile;
        AllElementsPrefabs[ElementType.Gap] = startGapTile;
        AllElementsPrefabs[ElementType.Track] = trackTile;

        Generate();
        CreateLevel();
    }

    float AddTracksToAllLanes(float xPos, int n)
    {
        for (int i = 0; i < n; i++)
        {
            topTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
            midTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
            botTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
        }
        xPos += oneUnit * n;
        return xPos;
    }

    float AddOneObstacleTwoTracks(float xPos, int obsLane)
    {
        // get random element type
        int obsTypeInt = Random.Range(0, numOfElements - 1);
        ElementType obsType = AllElements[obsTypeInt];

        Element e = new Element(obsType, xPos);

        float newXPos = xPos;
        int numOfUnits = 0;

        if (obsType == ElementType.Gap)
        {
            numOfUnits = Random.Range(1, 7) + 2;
            newXPos += numOfUnits * oneUnit;
            e.posEnd = newXPos;
        }
        else
        {
            newXPos += oneUnit;
            numOfUnits = 1;
        }

        if (obsLane == 0)
        {
            topTrack.Add(e);
            for (int i = 0; i < numOfUnits; i++)
            {
                midTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
                botTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
            }
        }
        else if (obsLane == 1)
        {
            midTrack.Add(e);
            for (int i = 0; i < numOfUnits; i++)
            {
                topTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
                botTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
            }
        }
        else if (obsLane == 2)
        {
            botTrack.Add(e);
            for (int i = 0; i < numOfUnits; i++)
            {
                topTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
                midTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
            }
        }

        return newXPos;
    }

    float AddTwoObstaclesOneTrack(float xPos, int freeLane)
    {
        // get random element types
        int obsTypeInt1 = Random.Range(0, numOfElements - 1);
        ElementType obsType1 = AllElements[obsTypeInt1];

        int obsTypeInt2 = Random.Range(0, numOfElements - 1);
        ElementType obsType2 = AllElements[obsTypeInt2];

        Element e1 = new Element(obsType1, xPos);
        Element e2 = new Element(obsType2, xPos);

        float newXPos1 = xPos;
        float newXPos2 = xPos;
        int numOfUnits1 = 0;
        int numOfUnits2 = 0;

        if (obsType1 == ElementType.Gap)
        {
            numOfUnits1 = Random.Range(1, 4) + 2;
            newXPos1 += numOfUnits1 * oneUnit;
            e1.posEnd = newXPos1;
        }
        else
        {
            newXPos1 += oneUnit;
            numOfUnits1 = 1;
        }

        if (obsType2 == ElementType.Gap)
        {
            numOfUnits2 = Random.Range(1, 4) + 2;
            newXPos2 += numOfUnits2 * oneUnit;
            e2.posEnd = newXPos2;
        }
        else
        {
            newXPos2 += oneUnit;
            numOfUnits2 = 1;
        }
        
        if (freeLane == 0)
        {
            midTrack.Add(e1);
            botTrack.Add(e2);

            for (int i = 0; i < Mathf.Max(numOfUnits1, numOfUnits2); i++)
            {
                topTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
            }

            // add more to second obs lane if needed
            for (int i = numOfUnits2; i < numOfUnits1 - numOfUnits2 + 1; i++)
            {
                botTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
            }

            // add more to first obs lane if needed
            for (int i = numOfUnits1; i < numOfUnits2 - numOfUnits1 + 1; i++)
            {
                midTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
            }
            
        }
        else if (freeLane == 1)
        {
            topTrack.Add(e1);
            botTrack.Add(e2);

            for (int i = 0; i < Mathf.Max(numOfUnits1, numOfUnits2); i++)
            {
                midTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
            }

            // add more to second obs lane if needed
            for (int i = numOfUnits2; i < numOfUnits1 - numOfUnits2 + 1; i++)
            {
                botTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
            }

            // add more to first obs lane if needed
            for (int i = numOfUnits1; i < numOfUnits2 - numOfUnits1 + 1; i++)
            {
                topTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
            }
        }
        else if (freeLane == 2)
        {
            topTrack.Add(e1);
            midTrack.Add(e2);

            for (int i = 0; i < Mathf.Max(numOfUnits1, numOfUnits2); i++)
            {
                botTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
            }

            // add more to second obs lane if needed
            for (int i = numOfUnits2; i < numOfUnits1 - numOfUnits2 + 1; i++)
            {
                midTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
            }

            // add more to first obs lane if needed
            for (int i = numOfUnits1; i < numOfUnits2 - numOfUnits1 + 1; i++)
            {
                topTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
            }
        }
        
        return Mathf.Max(newXPos1, newXPos2);
    }

    public void Generate()
    {
        topTrack = new List<Element>();
        midTrack = new List<Element>();
        botTrack = new List<Element>();

        float trackDistanceSoFar = 0f;

        float chanceAllLanesFree = 0.1f;
        float chanceTwoLanesFree = 0.3f;
        float chanceOneLaneFree = 0.6f;

        // start making the level

        trackDistanceSoFar = AddTracksToAllLanes(trackDistanceSoFar, 5);

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
                trackDistanceSoFar = AddTracksToAllLanes(trackDistanceSoFar, 1);
            }
            // if we have two open lanes and one obstacle
            else if (v <= chanceAllLanesFree + chanceTwoLanesFree)
            {
                // choose the blocked lane
                int lane = Random.Range(0, 2);

                trackDistanceSoFar = AddOneObstacleTwoTracks(trackDistanceSoFar, lane);
            }
            // if we have one open lane and two obstacles
            else if (v <= chanceAllLanesFree + chanceTwoLanesFree + chanceOneLaneFree)
            {
                // choose the empty lane
                int lane = Random.Range(0, 2);

                trackDistanceSoFar = AddTwoObstaclesOneTrack(trackDistanceSoFar, lane);
            }

            // train is 3 times longer than the obstacles, so add space
            trackDistanceSoFar = AddTracksToAllLanes(trackDistanceSoFar, 5);
        }
        Debug.Log("While ran " + safetyCounter + " times.");
    }

    void CreateLevel()
    {
        foreach (Element e in topTrack)
        {
            if (e.type == ElementType.Gap)
            {
                GameObject newStartGapTile = Instantiate(startGapTile, new Vector2(e.pos, topTrackYPos), Quaternion.identity) as GameObject;
                int gaps = (int)((e.posEnd - e.pos) / oneUnit) - 2;
                for (int j=1; j <= gaps; j++)
                {
                    // add nothing
                    //GameObject newGapTile = Instantiate(woodObsTile, new Vector2(e.pos + j * oneUnit, topTrackYPos), Quaternion.identity) as GameObject;
                }
                GameObject newEndGapTile = Instantiate(endGapTile, new Vector2(e.posEnd - oneUnit, topTrackYPos), Quaternion.identity) as GameObject;
            }
            else if (e.type == ElementType.WoodObs)
            {
                GameObject newTile = Instantiate(AllElementsPrefabs[e.type], new Vector2(e.pos, topTrackYPos), Quaternion.identity) as GameObject;
                int flip = Random.Range(0, 2);
                if (flip == 1) newTile.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GameObject newTile = Instantiate(AllElementsPrefabs[e.type], new Vector2(e.pos, topTrackYPos), Quaternion.identity) as GameObject;
            }
        }

        foreach (Element e in midTrack)
        {
            if (e.type == ElementType.Gap)
            {
                GameObject newStartGapTile = Instantiate(startGapTile, new Vector2(e.pos, midTrackYPos), Quaternion.identity) as GameObject;
                int gaps = (int)((e.posEnd - e.pos) / oneUnit) - 2;
                for (int j = 1; j <= gaps; j++)
                {
                    // add nothing
                    //GameObject newGapTile = Instantiate(woodObsTile, new Vector2(e.pos + j * oneUnit, midTrackYPos), Quaternion.identity) as GameObject;
                }
                GameObject newEndGapTile = Instantiate(endGapTile, new Vector2(e.posEnd - oneUnit, midTrackYPos), Quaternion.identity) as GameObject;
            }
            else if (e.type == ElementType.WoodObs)
            {
                GameObject newTile = Instantiate(AllElementsPrefabs[e.type], new Vector2(e.pos, midTrackYPos), Quaternion.identity) as GameObject;
                int flip = Random.Range(0, 2);
                if (flip == 1) newTile.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GameObject newTile = Instantiate(AllElementsPrefabs[e.type], new Vector2(e.pos, midTrackYPos), Quaternion.identity) as GameObject;
            }
        }

        foreach (Element e in botTrack)
        {
            if (e.type == ElementType.Gap)
            {
                GameObject newStartGapTile = Instantiate(startGapTile, new Vector2(e.pos, botTrackYPos), Quaternion.identity) as GameObject;
                int gaps = (int)((e.posEnd - e.pos) / oneUnit) - 2;
                for (int j = 1; j <= gaps; j++)
                {
                    // add nothing
                    //GameObject newGapTile = Instantiate(woodObsTile, new Vector2(e.pos + j * oneUnit, botTrackYPos), Quaternion.identity) as GameObject;
                }
                GameObject newEndGapTile = Instantiate(endGapTile, new Vector2(e.posEnd - oneUnit, botTrackYPos), Quaternion.identity) as GameObject;
            }
            else if (e.type == ElementType.WoodObs)
            {
                GameObject newTile = Instantiate(AllElementsPrefabs[e.type], new Vector2(e.pos, botTrackYPos), Quaternion.identity) as GameObject;
                int flip = Random.Range(0, 2);
                if (flip == 1) newTile.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GameObject newTile = Instantiate(AllElementsPrefabs[e.type], new Vector2(e.pos, botTrackYPos), Quaternion.identity) as GameObject;
            }
        }
    }

    void Update()
    {

    }
}
