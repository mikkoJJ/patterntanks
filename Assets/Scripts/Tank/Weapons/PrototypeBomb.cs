using UnityEngine;
using System.Collections;


/// <summary>
/// A tank weapon made using prototype pattern.
/// </summary>
public class PrototypeBomb : TankWeapon
{
    [SerializeField]
    private float maximumShootForce = 30.0f;

    [SerializeField]
    private float maxLife = 0.3f;

    [SerializeField]
    private int numberOfSplits = 7;

    private Rigidbody2D body;
    private float life;


    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        life = maxLife;
    }

    void Update()
    {
        life -= Time.deltaTime;

        // Split into smaller parts when dying.
        if (life <= 0f)
        {
            if (numberOfSplits > 0)
            {
                numberOfSplits--;

                // Split into two parts.
                var clone1 = Clone();
                var clone2 = Clone();

                // Make the parts a bit smaller and add random velocity.
                clone1.transform.localScale = transform.localScale * 0.7f;
                clone2.transform.localScale = transform.localScale * 0.7f;
                clone1.body.velocity = body.velocity + Random.insideUnitCircle * 2f;
                clone2.body.velocity = body.velocity + Random.insideUnitCircle * 2f;
            }

            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Clones self and returns the PrototypeBomb component.
    /// </summary>
    private PrototypeBomb Clone()
    {
        var clone = Instantiate(gameObject, transform.position, Quaternion.identity) as GameObject;
        clone.SetActive(true);
        return clone.GetComponent<PrototypeBomb>();
    }

    public override void Fire(Vector3 from, float angle, float power)
    {
        // Create clone of the original object and set velocity and position.
        var clone = Clone();
        clone.transform.position = from;
        clone.body.velocity = new Vector3(0f, 0f, 0f);

        var rot = Quaternion.AngleAxis(angle, Vector3.forward);
        var dir = rot * Vector2.right;

        // Scale the dir vector according to the power of the shot.
        var force = new Vector2(dir.x, dir.y) * maximumShootForce * power;

        // Fire away.
        clone.body.AddForce(force, ForceMode2D.Impulse);
    }
}
