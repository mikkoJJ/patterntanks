using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Set the text on this UI text object to always be the name of the current
/// weapon on the given tank.
/// </summary>
public class GetWeaponNameToUI : MonoBehaviour {

    public GameObject tank;

	// Update is called once per frame
	void Update ()
    {
        GetComponent<Text>().text = tank.GetComponentInChildren<TankGunControl>().CurrentWeaponName;
	}
}
