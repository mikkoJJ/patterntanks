using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ExampleWeapon : TankWeapon {

    public float maximumShootForce = 30.0f;
    public float lifeTime = 1.0f;

    Rigidbody2D body;
    SpriteRenderer renderer;
    private float _life;
    
	void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();

        //disable the weapon until its fired:
        //body.isKinematic = true;
        renderer.enabled = false;
	}
	
	void Update ()
    {
        if (!renderer.enabled) return;

        //kill the speed if timer runs out because why not
        _life -= Time.deltaTime;
        if (_life <= 0)
        {
            body.velocity = Vector3.zero;
        }
	}


    // implement the fire method:
    public override void Fire(Vector3 from, float angle, float power)
    {
        Debug.Log(angle);
        //"spawn" the cannonball into the world
        //body.isKinematic = false;
        renderer.enabled = true;
        transform.position = from;
        body.velocity = new Vector3(0f, 0f, 0f);

        //convert angle to directional vector
        var rot = Quaternion.AngleAxis(angle, Vector3.forward);
        var dir = rot * Vector2.right;

        //scale the dir vector according to the power of the shot
        var force = new Vector2(dir.x, dir.y) * maximumShootForce * power;

        //fire away
        body.AddForce(force, ForceMode2D.Impulse);

        //start life countdown
        _life = lifeTime;
    }
}
