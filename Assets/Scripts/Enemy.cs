using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed;
    private int waypoint = 0;

    private Rigidbody2D enemyRB;

    void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Pos: " + transform.position);
        Debug.Log("Waypoint: " + waypoints[waypoint].position);
        Debug.Log("Distance: " + Vector2.Distance(transform.position, waypoints[waypoint].position));

        // approximating because floating point math sucks
        if (Vector2.Distance(transform.position, waypoints[waypoint].position) > 0.1)
        {
            enemyRB.MovePosition(Vector2.MoveTowards(transform.position,
                                            waypoints[waypoint].position,
                                            speed));
        }

        else
        {
            waypoint = (waypoint + 1) % waypoints.Length;
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.CompareTag("Player"))
        {
            Destroy(coll.gameObject);
        }
    }
}
