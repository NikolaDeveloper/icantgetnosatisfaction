using UnityEngine;
using System.Collections;

public class SortDecoSprite : MonoBehaviour {
    
	void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y) * -1;
    }
}
