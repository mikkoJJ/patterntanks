using UnityEngine;
using System.Collections;

/// <summary>
/// Adds all children of this gameobject to the weapons list of the
/// given tank.
/// </summary>
public class AddChildrenAsTankWeapons : MonoBehaviour {

    public GameObject tank;
    
	void Start ()
    {
        if (tank == null) return;

        for (var i = 0; i < transform.childCount; i++) 
        {
            var go = transform.GetChild(i).gameObject;
            if (go.GetComponent<TankWeapon>() == null)
            {
                throw new System.ArgumentException("Invalid game object set as children to WEAPONS, no TankWeapon derived component found");
            }
            tank.GetComponentInChildren<TankGunControl>().weapons.Add(go);
        }
	}
}
