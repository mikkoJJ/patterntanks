using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The component that handles player control of the tank gun.
/// </summary>
public class TankGunControl : MonoBehaviour {
    /// <summary>
    /// A list of all the weapons this gun has access to. Each weapon needs to have a component derived
    /// from the TankWeapon class.
    /// </summary>
    public List<GameObject> weapons;

    private int _currentWeapon = 0;

	void Start ()
    {
        //check weapon classes
        foreach(var weapon in weapons)
        {
            if (weapon.GetComponent<TankWeapon>() == null)
            {
                throw new System.ArgumentException("Invalid weapon assigned to tank: no TankWeapon-derived component found!");
            }
        }
	}
	
	void Update ()
    {
        //set cannon facing:
        var mousePos = Input.mousePosition;
        var dir = mousePos - Camera.main.WorldToScreenPoint(transform.position);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);

        //change weapon on input:
        if (Input.GetMouseButtonDown(1))
        {
            if (++_currentWeapon >= weapons.Count)
            {
                _currentWeapon = 0;
            }
        }
    }

    /// <summary>
    /// The name of the currently selected weapon
    /// </summary>
    public string CurrentWeaponName
    {
        get
        {
            if (weapons.Count <= 0)
            {
                return "<NO WEAPONS>";
            }
            return weapons[_currentWeapon].GetComponent<TankWeapon>().weaponName;
        }
    }

    /// <summary>
    /// Called by powerbar once charging is complete.
    /// </summary>
    /// <param name="power"></param>
    public void Shoot(float power)
    {
        weapons[_currentWeapon].GetComponent<TankWeapon>().Fire(transform.position, transform.eulerAngles.z, power);
    }
}
