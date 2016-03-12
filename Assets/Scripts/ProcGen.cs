using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProcGen : MonoBehaviour {

    public static ProcGen Instance;

    // station prefabs
    public GameObject staionStripePrefab, stationStripeWithWindowPrefab, stationContainerPrefab;

    internal List<Station> AllStations;

    internal class Station
    {
        internal string name;
        internal int id;
        internal float arrivalTime;
        internal float distanceFromOrigin;

        internal Station(string n, int i, float t, float d)
        {
            name = n;
            id = i;
            arrivalTime = t;
            distanceFromOrigin = d;
        }
    };

    int closeStationUnits = 60;
    int farStationUnits = 120;
    int veryFarStationUnits = 240;

    // prefabs for track tiles
    public GameObject trackTile, woodObsTile, startGapTile, endGapTile/*, gapTile*/;
    // prefabs for decorations
    public GameObject decoRock1, decoRock2, decoTree1, decoTree2, decoTree3;
    
    float topTrackYPos = 75f;
    float midTrackYPos = 0f;
    float botTrackYPos = -75f;

    float stationYPos = 125f;

    float trackDistanceTotal = 6000f;

    internal enum ElementType { WoodObs, Gap, Track };
    int numOfElements = 3;

    internal float oneUnit = 50f;

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
        AllStations = new List<Station>(7);
        float nextStationPos = 0f;
        AllStations.Add(new Station("CHI", 1, 0f, nextStationPos));
        nextStationPos += veryFarStationUnits * oneUnit;
        AllStations.Add(new Station("DEN", 2, 90f, nextStationPos));
        nextStationPos += closeStationUnits * oneUnit;
        AllStations.Add(new Station("WIP", 3, 120f, nextStationPos));
        nextStationPos += farStationUnits * oneUnit;
        AllStations.Add(new Station("GSC", 4, 180f, nextStationPos));
        nextStationPos += veryFarStationUnits * oneUnit;
        AllStations.Add(new Station("SLC", 5, 270f, nextStationPos));
        nextStationPos += farStationUnits * oneUnit;
        AllStations.Add(new Station("SAC", 6, 330f, nextStationPos));
        nextStationPos += closeStationUnits * oneUnit;
        AllStations.Add(new Station("EMY", 7, 360f, nextStationPos));

        AllElements = new List<ElementType>(numOfElements);
        AllElements.Add(ElementType.WoodObs);
        AllElements.Add(ElementType.Gap);
        AllElements.Add(ElementType.Track);

        AllElementsPrefabs = new Dictionary<ElementType, GameObject>();
        AllElementsPrefabs[ElementType.WoodObs] = woodObsTile;
        AllElementsPrefabs[ElementType.Gap] = startGapTile;
        AllElementsPrefabs[ElementType.Track] = trackTile;

        CreateStations();
        Generate();
        CreateLevel();
    }

    // Stations

    void CreateStations()
    {
        foreach (Station s in AllStations)
        {
            GameObject station = Instantiate(stationContainerPrefab, new Vector2(s.distanceFromOrigin, stationYPos), Quaternion.identity) as GameObject;
            station.GetComponent<CreateMyStations>().setId(s.id);
        }
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
            for (int i = 1; i < numOfUnits; i++)
            {
                topTrack.Add(new Element(obsType, xPos + i * oneUnit));
            }

            for (int i = 0; i < numOfUnits; i++)
            {
                midTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
                botTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
            }
        }
        else if (obsLane == 1)
        {
            midTrack.Add(e);

            for (int i = 1; i < numOfUnits; i++)
            {
                midTrack.Add(new Element(obsType, xPos + i * oneUnit));
            }

            for (int i = 0; i < numOfUnits; i++)
            {
                topTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
                botTrack.Add(new Element(ElementType.Track, xPos + i * oneUnit));
            }
        }
        else if (obsLane == 2)
        {
            botTrack.Add(e);

            for (int i = 1; i < numOfUnits; i++)
            {
                botTrack.Add(new Element(obsType, xPos + i * oneUnit));
            }

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

            for (int i = 1; i < numOfUnits1; i++)
            {
                midTrack.Add(new Element(obsType1, xPos + i * oneUnit));
            }
            for (int i = 1; i < numOfUnits2; i++)
            {
                botTrack.Add(new Element(obsType2, xPos + i * oneUnit));
            }

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

            for (int i = 1; i < numOfUnits1; i++)
            {
                topTrack.Add(new Element(obsType1, xPos + i * oneUnit));
            }
            for (int i = 1; i < numOfUnits2; i++)
            {
                botTrack.Add(new Element(obsType2, xPos + i * oneUnit));
            }

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

            for (int i = 1; i < numOfUnits1; i++)
            {
                topTrack.Add(new Element(obsType1, xPos + i * oneUnit));
            }
            for (int i = 1; i < numOfUnits2; i++)
            {
                midTrack.Add(new Element(obsType2, xPos + i * oneUnit));
            }

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
        
        for (int stationNum = 1; stationNum < AllStations.Count; stationNum++)
        {
            float desiredTrackDistanceUntilNext = AllStations[stationNum].distanceFromOrigin - 13 * oneUnit;

            // create a station track bit
            trackDistanceSoFar = AddTracksToAllLanes(trackDistanceSoFar, 8);
            
            Debug.Log("Creating station " + AllStations[stationNum].name);
            Debug.Log("Station at " + AllStations[stationNum].distanceFromOrigin);

            // safety against bugs with endless loop
            int safetyCounter = 0;

            while (trackDistanceSoFar < desiredTrackDistanceUntilNext - 8 * oneUnit)
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

                if (desiredTrackDistanceUntilNext - trackDistanceSoFar >= oneUnit * 4)
                {
                    //trackDistanceSoFar = AddTracksToAllLanes(trackDistanceSoFar, 4);
                }
                // train is 3 times longer than the obstacles, so add space
                trackDistanceSoFar = AddTracksToAllLanes(trackDistanceSoFar, 4);
            }

            // add space before station
            if (AllStations[stationNum].distanceFromOrigin - trackDistanceSoFar <= 5 * oneUnit)
            {
                trackDistanceSoFar = AddTracksToAllLanes(trackDistanceSoFar, 5);
            }
            else
            {
                trackDistanceSoFar = AddTracksToAllLanes(trackDistanceSoFar, (int)((AllStations[stationNum].distanceFromOrigin - trackDistanceSoFar) / oneUnit));
            }
            Debug.Log("While ran " + safetyCounter + " times.");
            Debug.Log("trackDistanceSoFar = " + trackDistanceSoFar);
        }

        // add final strip of tracks at game end
        trackDistanceSoFar = AddTracksToAllLanes(trackDistanceSoFar, 10);

        Debug.Log("trackDistanceSoFar FINAL = " + trackDistanceSoFar);
    }

    void CreateLevel()
    {
        foreach (Element e in topTrack)
        {
            if (e.type == ElementType.Gap)
            {
                if (e.posEnd == -1f)
                {
                    // skip
                }
                else
                {
                    GameObject newStartGapTile = Instantiate(startGapTile, new Vector2(e.pos, topTrackYPos), Quaternion.identity) as GameObject;
                    int gaps = (int)((e.posEnd - e.pos) / oneUnit) - 2;
                    for (int j = 1; j <= gaps; j++)
                    {
                        //GameObject newGapTile = Instantiate(gapTile, new Vector2(e.pos + j * oneUnit, topTrackYPos), Quaternion.identity) as GameObject;
                    }
                    GameObject newEndGapTile = Instantiate(endGapTile, new Vector2(e.posEnd - oneUnit, topTrackYPos), Quaternion.identity) as GameObject;
                }
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
                if (e.posEnd == -1f)
                {
                    // skip
                }
                else
                {
                    GameObject newStartGapTile = Instantiate(startGapTile, new Vector2(e.pos, midTrackYPos), Quaternion.identity) as GameObject;
                    int gaps = (int)((e.posEnd - e.pos) / oneUnit) - 2;
                    for (int j = 1; j <= gaps; j++)
                    {
                        //GameObject newGapTile = Instantiate(gapTile, new Vector2(e.pos + j * oneUnit, midTrackYPos), Quaternion.identity) as GameObject;
                    }
                    GameObject newEndGapTile = Instantiate(endGapTile, new Vector2(e.posEnd - oneUnit, midTrackYPos), Quaternion.identity) as GameObject;
                }
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
                if (e.posEnd == -1f)
                {
                    // skip
                }
                else
                {
                    GameObject newStartGapTile = Instantiate(startGapTile, new Vector2(e.pos, botTrackYPos), Quaternion.identity) as GameObject;
                    int gaps = (int)((e.posEnd - e.pos) / oneUnit) - 2;
                    for (int j = 1; j <= gaps; j++)
                    {
                        //GameObject newGapTile = Instantiate(gapTile, new Vector2(e.pos + j * oneUnit, botTrackYPos), Quaternion.identity) as GameObject;
                    }
                    GameObject newEndGapTile = Instantiate(endGapTile, new Vector2(e.posEnd - oneUnit, botTrackYPos), Quaternion.identity) as GameObject;
                }
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
