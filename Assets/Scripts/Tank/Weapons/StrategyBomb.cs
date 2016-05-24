using UnityEngine;
using System.Collections;


/// <summary>
/// Interface for update strategies.
/// </summary>
interface IUpdater
{
    /// <summary>
    /// Updates game object.
    /// </summary>
    void Update(GameObject obj);
}


/// <summary>
/// Moves the game object constantly in a random direction.
/// </summary>
class RandomVelocity : IUpdater
{
    public void Update(GameObject obj)
    {
        var randomVelocity = Random.insideUnitCircle * 2f;
        obj.GetComponent<Rigidbody2D>().velocity += randomVelocity;
    }
}

/// <summary>
/// Moves the game object towards another game object.
/// </summary>
class Homing : IUpdater
{
    private GameObject target; // Follow target.
    private float speed = 10f; // Movement speed.

    public Homing(GameObject target)
    {
        this.target = target;
    }

    public void Update(GameObject obj)
    {
        if (target != null)
        {
            // Calculate direction vector to target.
            var direction = (target.transform.position - obj.transform.position).normalized;

            // Move towards target.
            obj.transform.position += direction * speed * Time.fixedDeltaTime;
        }
    }
}


/// <summary>
/// A tank weapon made using strategy pattern.
/// </summary>
public class StrategyBomb : TankWeapon
{
    [SerializeField]
    private float maximumShootForce = 30.0f;

    private IUpdater currentUpdater;
    private IUpdater[] updaters;
    private Rigidbody2D body;


    void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        // Find the other tank.
        var target = GameObject.Find("TargetTank");

        // Create an array of available strategies.
        updaters = new IUpdater[]
        {
            new RandomVelocity(),
            new Homing(target)
        };
    }
    
    // Fixed update is used because strategies can affect rigidbodies.
    void FixedUpdate()
    {
        if (currentUpdater != null)
        {
            // Update object based on current update strategy.
            currentUpdater.Update(gameObject);
        }
    }

    public override void Fire(Vector3 from, float angle, float power)
    {
        gameObject.SetActive(true);
        transform.position = from;
        body.velocity = new Vector3(0f, 0f, 0f);

        // Select random strategy.
        currentUpdater = updaters[Random.Range(0, updaters.Length)];

        // Convert angle to directional vector.
        var rot = Quaternion.AngleAxis(angle, Vector3.forward);
        var dir = rot * Vector2.right;

        // Scale the dir vector according to the power of the shot.
        var force = new Vector2(dir.x, dir.y) * maximumShootForce * power;

        // Fire away.
        body.AddForce(force, ForceMode2D.Impulse);
    }
}
