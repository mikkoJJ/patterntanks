using UnityEngine;
using System.Collections;

/// <summary>
/// Handles charging the power when mouse is pressed, and 
/// passes power information to the tank when depressed.
/// </summary>
public class PowerBar : MonoBehaviour {

    public float chargeSpeed = 0.01f;

    private TankGunControl gun;
    private bool _charging = false;
    private float _current = 0;

	// Use this for initialization
	void Start ()
    {
        gun = transform.parent.GetComponentInChildren<TankGunControl>();

        if (gun == null)
        {
            Debug.LogError("Powerbar does not seem to be the child of a proper tank, no TankGunControl component found.");
        }
	}
	
	
	void Update ()
    { 
        if ( Input.GetMouseButton(0) )
        {
            _charging = true;
            _current = Mathf.Lerp(_current, 1, chargeSpeed); //charge using linear interpolation (fast to get near 100%, very slow to get all the way)
        }
        else
        {
            if (_charging)
            {
                Shoot(); //mouse was just released, so shoot.
            }
            _charging = false;
            _current = 0;
        }

        transform.localScale = new Vector3(1, _current, 0);
    }

    /// <summary>
    /// We have charged, so instruct the tank to shoot.
    /// </summary>
    void Shoot()
    {
        gun.Shoot(_current);
    }
}
