using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 500f;
    public float nextWaypointDistance = 3.0f;
    public float stalkingDistance = 5.0f;
    public Transform graphics;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedPathEnd = false;
    public float pathUpdateTime = 0.1f;

    private Seeker seeker;
    private Rigidbody2D rb;

    public enum Behavior
    {
        Stalking,
        Chasing
    };
    private Behavior currentBehavior = Behavior.Stalking;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateTime);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            switch (currentBehavior)
            {
                case Behavior.Chasing:
                    seeker.StartPath(rb.position, target.position, OnPathComplete);
                    break;

                case Behavior.Stalking:
                    float dist = Vector2.Distance(rb.position, target.position);
                    Vector2 newTarget = rb.position + (((Vector2)target.position - rb.position).normalized * (dist - stalkingDistance));
                    seeker.StartPath(rb.position, newTarget, OnPathComplete);
                    break;

                default: break;
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedPathEnd = true;
            return;
        } else
        {
            reachedPathEnd = false;
        }

        // Move the enemy in the required direction
        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = dir * speed * Time.deltaTime;
        rb.AddForce(force);

        // Update its status along the path
        float dist = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWaypointDistance)
            currentWaypoint++;

        // Flip the enemy as necessary
        if (force.x >= 0.01f)
        {
            graphics.localScale = new Vector3(-3f, 7f, 1f);
        } else if (force.x <= -0.01f)
        {
            graphics.localScale = new Vector3(3f, 7f, 1f);
        }
    }

    public void SetBehavior(Behavior desiredBehavior)
    {
        currentBehavior = desiredBehavior;
    }
}
