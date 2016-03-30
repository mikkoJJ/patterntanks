using UnityEngine;
using System.Collections;

/// <summary>
/// Make all the child sprites of the tank use the same sprite color to
/// allow for color adjusting in a single place.
/// </summary>
public class InheritSpriteColorFromParent : MonoBehaviour {

	
	void Start ()
    {
        GetComponent<SpriteRenderer>().color = transform.parent.GetComponent<SpriteRenderer>().color;
	}
	
}
