using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(ShrimpAnimator))]
public class ShrimpMover : MonoBehaviour
{
    [Tooltip("Destination position to move to")]
    public Transform Destination;

    [Tooltip("Speed in units per second")]
    public float Speed;

    [Tooltip("Speed when powered up in units per second")]
    public float PoweredSpeed;

    private Rigidbody2D _rigidbody2D;

    private ShrimpAnimator _shrimpAnimator;

    private ShrimpHealth _shrimpHealth;

    /// <summary>
    /// Whether the shrimp has been powered up
    /// </summary>
    private bool _isPoweredUp = false;

    private void Start()
    {
        this._rigidbody2D = GetComponent<Rigidbody2D>();
        this._shrimpAnimator = GetComponent<ShrimpAnimator>();
        this._shrimpHealth = GetComponent<ShrimpHealth>();

        _shrimpHealth.PowerUp.AddListener(() =>
        {
            _isPoweredUp = true;
            KickInRandomDirection();
        });
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isPoweredUp)
            GoBounceAround();
        else
            GoToDestination();
    }

    private void GoBounceAround()
    {
        var currentViewport = Camera.main.WorldToViewportPoint(transform.position);

        // vertical walls of the screen
        if (currentViewport.x <= 0 || currentViewport.x >= 1)
        {
            _rigidbody2D.velocity *= new Vector2(-1, 1);
        }

        // horizontal walls
        if (currentViewport.y <= 0 || currentViewport.y >= 1)
        {
            _rigidbody2D.velocity *= new Vector2(1, -1);
        }
    }

    private void KickInRandomDirection()
    {
        // kick it in a random direction
        Random.InitState(System.DateTime.Now.Second);
        _rigidbody2D.velocity = Random.insideUnitCircle.normalized * PoweredSpeed;

    }

    private void GoToDestination()
    {
        float distanceLeft = Vector2.Distance(Destination.position, _rigidbody2D.position);

        // the shrimp arrived
        if (distanceLeft <= 0.05f) // proximity threshold, 0.05f makes the shrimp stop on the target
        {
            _rigidbody2D.velocity = Vector2.zero;
            return;
        }

        _rigidbody2D.velocity =
            ((Vector2)Destination.position - _rigidbody2D.position).normalized * Speed
        ;

        // animations
        if (_rigidbody2D.velocity.x < 0) this.transform.localScale = new Vector3(-1, 1, 1);
        else this.transform.localScale = Vector3.one;
    }
}
