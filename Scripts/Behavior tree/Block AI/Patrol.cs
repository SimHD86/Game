using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Enemy_damage
{
    public Transform[] waypoints;
    private int _currentwaypointIndex;
    [SerializeField] private float _speed;

    private void Update()
    {
            Transform wp = waypoints[_currentwaypointIndex];
            if (Vector3.Distance(transform.position, wp.position) < 0.01f)
            {
                _currentwaypointIndex = (_currentwaypointIndex + 1) % waypoints.Length;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, wp.position, _speed * Time.deltaTime);
            }
    }

}
