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

    float trackDistanceTotal = 10000f;

    internal enum ElementType { Track, WoodObs, Gap };
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
        AllElements.Add(ElementType.Track);
        AllElements.Add(ElementType.WoodObs);
        AllElements.Add(ElementType.Gap);

        AllElementsPrefabs = new Dictionary<ElementType, GameObject>();
        AllElementsPrefabs[ElementType.Track] = trackTile;
        AllElementsPrefabs[ElementType.WoodObs] = woodObsTile;
        AllElementsPrefabs[ElementType.Gap] = startGapTile;

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

    public void Generate()
    {
        topTrack = new List<Element>();
        midTrack = new List<Element>();
        botTrack = new List<Element>();

        float trackDistanceSoFar = 0f;

        float chanceAllLanesFree = 0.1f;
        float chanceTwoLanesFree = 0.6f;
        float chanceOneLaneFree = 0.3f;

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

                if (lane == 0)
                {
                    midTrack.Add(new Element(ElementType.Track, trackDistanceSoFar));
                    botTrack.Add(new Element(ElementType.Track, trackDistanceSoFar));

                    float prevTrack = trackDistanceSoFar + oneUnit;
                    trackDistanceSoFar = AddElementToTrack(trackDistanceSoFar, lane);
                    int len = (int)((trackDistanceSoFar - prevTrack) / oneUnit);
                    if (len > 0)
                    {
                        for (int j = 0; j < len; j++)
                        {
                            midTrack.Add(new Element(ElementType.Track, prevTrack + j * oneUnit));
                            botTrack.Add(new Element(ElementType.Track, prevTrack * j * oneUnit));
                        }
                    }

                }
                else if (lane == 1)
                {
                    topTrack.Add(new Element(ElementType.Track, trackDistanceSoFar));
                    botTrack.Add(new Element(ElementType.Track, trackDistanceSoFar));

                    float prevTrack = trackDistanceSoFar + oneUnit;
                    trackDistanceSoFar = AddElementToTrack(trackDistanceSoFar, lane);
                    int len = (int)((trackDistanceSoFar - prevTrack) / oneUnit);
                    if (len > 0)
                    {
                        for (int j = 0; j < len; j++)
                        {
                            topTrack.Add(new Element(ElementType.Track, prevTrack + j * oneUnit));
                            botTrack.Add(new Element(ElementType.Track, prevTrack * j * oneUnit));
                        }
                    }
                }
                else if (lane == 2)
                {
                    topTrack.Add(new Element(ElementType.Track, trackDistanceSoFar));
                    midTrack.Add(new Element(ElementType.Track, trackDistanceSoFar));

                    float prevTrack = trackDistanceSoFar + oneUnit;
                    trackDistanceSoFar = AddElementToTrack(trackDistanceSoFar, lane);
                    int len = (int)((trackDistanceSoFar - prevTrack) / oneUnit);
                    if (len > 0)
                    {
                        for (int j = 0; j < len; j++)
                        {
                            topTrack.Add(new Element(ElementType.Track, prevTrack + j * oneUnit));
                            midTrack.Add(new Element(ElementType.Track, prevTrack * j * oneUnit));
                        }
                    }
                }
            }
            // if we have one open lane and two obstacles
            else if (v <= chanceAllLanesFree + chanceTwoLanesFree + chanceOneLaneFree)
            {
                // choose the empty lane
                int lane = Random.Range(0, 2);

                float firstTrackDistance = 0f, secondTrackDistance = 0f;

                // top
                if (lane == 0)
                {
                    topTrack.Add(new Element(ElementType.Track, trackDistanceSoFar));

                    firstTrackDistance = AddElementToTrack(trackDistanceSoFar, 1);
                    secondTrackDistance = AddElementToTrack(trackDistanceSoFar, 2);

                    float prevTrack = trackDistanceSoFar + oneUnit;
                    int len = (int)((Mathf.Max(firstTrackDistance, secondTrackDistance) - prevTrack) / oneUnit);
                    if (len > 0)
                    {
                        for (int j = 0; j < len; j++)
                        {
                            topTrack.Add(new Element(ElementType.Track, prevTrack + j * oneUnit));
                        }
                    }
                }
                // mid
                else if (lane == 1)
                {
                    midTrack.Add(new Element(ElementType.Track, trackDistanceSoFar));

                    firstTrackDistance = AddElementToTrack(trackDistanceSoFar, 0);
                    secondTrackDistance = AddElementToTrack(trackDistanceSoFar, 2);

                    float prevTrack = trackDistanceSoFar + oneUnit;
                    int len = (int)((Mathf.Max(firstTrackDistance, secondTrackDistance) - prevTrack) / oneUnit);
                    if (len > 0)
                    {
                        for (int j = 0; j < len; j++)
                        {
                            midTrack.Add(new Element(ElementType.Track, prevTrack + j * oneUnit));
                        }
                    }
                }
                //bot
                else if (lane == 2)
                {
                    botTrack.Add(new Element(ElementType.Track, trackDistanceSoFar));

                    firstTrackDistance = AddElementToTrack(trackDistanceSoFar, 0);
                    secondTrackDistance = AddElementToTrack(trackDistanceSoFar, 1);

                    float prevTrack = trackDistanceSoFar + oneUnit;
                    int len = (int)((Mathf.Max(firstTrackDistance, secondTrackDistance) - prevTrack) / oneUnit);
                    if (len > 0)
                    {
                        for (int j = 0; j < len; j++)
                        {
                            botTrack.Add(new Element(ElementType.Track, prevTrack + j * oneUnit));
                        }
                    }
                }
                trackDistanceSoFar = Mathf.Max(firstTrackDistance, secondTrackDistance);
            }

            // train is 3 times longer than the obstacles, so add space
            trackDistanceSoFar = AddTracksToAllLanes(trackDistanceSoFar, 5);
        }
        Debug.Log("While ran " + safetyCounter + " times.");
    }

    float AddElementToTrack(float xPos, int l)
    {
        // get random element type
        int obsTypeInt = Random.Range(0, numOfElements);
        ElementType obsType = AllElements[obsTypeInt];
        
        if (obsType == ElementType.Gap)
        {
            Element e = new Element(obsType, xPos);
            int numOfGapUnits = Random.Range(1, 4);
            xPos += (2 + numOfGapUnits) * oneUnit;
            e.posEnd = xPos;

            if (l == 0) topTrack.Add(e);
            else if (l == 1) midTrack.Add(e);
            else if (l == 2) botTrack.Add(e);
            
            return xPos;
        }
        else
        {
            Element e = new Element(obsType, xPos);
            if (l == 0) topTrack.Add(e);
            else if (l == 1) midTrack.Add(e);
            else if (l == 2) botTrack.Add(e);

            xPos += oneUnit;
            return xPos;
        }
    }

    void CreateLevel()
    {
        /*
        int i = 0;
        int levelUnitNum = (int)(trackDistanceTotal / oneUnit);
        float trackPos = 0f;
        for (i = 0; i < levelUnitNum; i++)
        {
            GameObject newTile;
            newTile = Instantiate(trackTile, new Vector2(trackPos, topTrackYPos), Quaternion.identity) as GameObject;
            newTile = Instantiate(trackTile, new Vector2(trackPos, midTrackYPos), Quaternion.identity) as GameObject;
            newTile = Instantiate(trackTile, new Vector2(trackPos, botTrackYPos), Quaternion.identity) as GameObject;
            //newTile.transform.parent

            trackPos += oneUnit;
        }
        */

        foreach (Element e in topTrack)
        {
            if (e.type == ElementType.Gap)
            {
                GameObject newStartGapTile = Instantiate(startGapTile, new Vector2(e.pos, topTrackYPos), Quaternion.identity) as GameObject;
                //newTile.transform.parent
                int gaps = (int)((e.posEnd - e.pos) / oneUnit) - 2;
                for (int j=1; j <= gaps; j++)
                {
                    //GameObject newGapTile = Instantiate(woodObsTile, new Vector2(e.pos + j * oneUnit, topTrackYPos), Quaternion.identity) as GameObject;
                    //newTile.transform.parent
                }
                GameObject newEndGapTile = Instantiate(endGapTile, new Vector2(e.posEnd - oneUnit, topTrackYPos), Quaternion.identity) as GameObject;
                //newTile.transform.parent
            }
            else
            {
                GameObject newTile = Instantiate(AllElementsPrefabs[e.type], new Vector2(e.pos, topTrackYPos), Quaternion.identity) as GameObject;
                //newTile.transform.parent
            }
        }

        foreach (Element e in midTrack)
        {
            if (e.type == ElementType.Gap)
            {
                GameObject newStartGapTile = Instantiate(startGapTile, new Vector2(e.pos, midTrackYPos), Quaternion.identity) as GameObject;
                //newTile.transform.parent
                int gaps = (int)((e.posEnd - e.pos) / oneUnit) - 2;
                for (int j = 1; j <= gaps; j++)
                {
                    //GameObject newGapTile = Instantiate(woodObsTile, new Vector2(e.pos + j * oneUnit, midTrackYPos), Quaternion.identity) as GameObject;
                    //newTile.transform.parent
                }
                GameObject newEndGapTile = Instantiate(endGapTile, new Vector2(e.posEnd - oneUnit, midTrackYPos), Quaternion.identity) as GameObject;
                //newTile.transform.parent
            }
            else
            {
                GameObject newTile = Instantiate(AllElementsPrefabs[e.type], new Vector2(e.pos, midTrackYPos), Quaternion.identity) as GameObject;
                //newTile.transform.parent
            }
        }

        foreach (Element e in botTrack)
        {
            if (e.type == ElementType.Gap)
            {
                GameObject newStartGapTile = Instantiate(startGapTile, new Vector2(e.pos, botTrackYPos), Quaternion.identity) as GameObject;
                //newTile.transform.parent
                int gaps = (int)((e.posEnd - e.pos) / oneUnit) - 2;
                for (int j = 1; j <= gaps; j++)
                {
                    //GameObject newGapTile = Instantiate(woodObsTile, new Vector2(e.pos + j * oneUnit, botTrackYPos), Quaternion.identity) as GameObject;
                    //newTile.transform.parent
                }
                GameObject newEndGapTile = Instantiate(endGapTile, new Vector2(e.posEnd - oneUnit, botTrackYPos), Quaternion.identity) as GameObject;
                //newTile.transform.parent
            }
            else
            {
                GameObject newTile = Instantiate(AllElementsPrefabs[e.type], new Vector2(e.pos, botTrackYPos), Quaternion.identity) as GameObject;
                //newTile.transform.parent
            }
        }
    }

    void Update()
    {

    }
}
