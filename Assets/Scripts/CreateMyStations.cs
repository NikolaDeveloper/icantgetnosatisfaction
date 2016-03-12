using UnityEngine;
using System.Collections;

public class CreateMyStations : MonoBehaviour {

    public int id;

    public void setId(int sid)
    {
        id = sid;
    }

    void Start()
    {
        

        int sLen = Random.Range(4, 8);
        for (int i = 0; i < sLen; i++)
        {
            GameObject stationStripe;
            int windows = Random.Range(0, 2);
            if (windows == 0) stationStripe = Instantiate(ProcGen.Instance.staionStripePrefab, new Vector2(gameObject.transform.position.x + i * ProcGen.Instance.oneUnit, gameObject.transform.position.y), Quaternion.identity) as GameObject;
            else stationStripe = Instantiate(ProcGen.Instance.stationStripeWithWindowPrefab, new Vector2(gameObject.transform.position.x + i * ProcGen.Instance.oneUnit, gameObject.transform.position.y), Quaternion.identity) as GameObject;
            stationStripe.transform.parent = gameObject.transform;
        }

        gameObject.GetComponent<BoxCollider2D>().size = new Vector2((sLen - 1) * ProcGen.Instance.oneUnit, 450f);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2((sLen - 1) * (ProcGen.Instance.oneUnit / 2f), -25f);
    }
}
