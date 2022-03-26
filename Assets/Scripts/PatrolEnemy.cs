using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] Transform[] PatrolPoints;
    float speed = 8f;

    int waypointIndex;

    private float dist;

    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
        transform.LookAt(PatrolPoints[waypointIndex].position);
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position,PatrolPoints[waypointIndex].position);
        if(enemy.followPlayer == false)
        {
            if(dist > 5f)
            {
                ChangPatrolPoint();
            }
            Patrol();
        }
    }

    //for patrol 
    void Patrol() 
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void ChangPatrolPoint()
    {
        waypointIndex += 1;
        if(waypointIndex >= PatrolPoints.Length)
        {
            waypointIndex = 0;
        }
    //    transform.LookAt(PatrolPoints[waypointIndex].position);
    }

}
