using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// This is the base component for all weapons. Other weapons need to be 
/// derived from this, as the game world uses this as an interface when interacting
/// with the weapon.
/// </summary>
public class TankWeapon : MonoBehaviour {

	/// <summary>
    /// Assign new name for your weapon!
    /// </summary>
    public string weaponName = "weapon name here";

    /// <summary>
    /// Called by the tank when it wants to fire this weapon. 
    /// 
    /// Implement this method in your weapon!
    /// </summary>
    /// <param name="from">the position of the tank, ie. where the shot is originating from</param>
    /// <param name="direction">the direction to shoot, ie. the current angle of the cannon in degrees</param>
    /// <param name="power">how much power has the tank given to the shot</param>
    public virtual void Fire(Vector3 from, float direction, float power)
    {
        throw new NotImplementedException("Tried to fire a weapon with no Fire() method implemented.");
    }

    /// <summary>
    /// The weapon should be instantiated by hand into the scene, so Start() is called normally
    /// when the scene loads. The weapon should probably hide itself, and only show on Fire().
    /// </summary>
    void Start() { }

    /// <summary>
    /// Update is called every frame as normal for Unity components.
    /// </summary>
    void Update() { }
}
