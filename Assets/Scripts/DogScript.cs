using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogScript : MonoBehaviour
{
    public List<Transform> waypoints;
    public Transform player;
    public float triggerDistance = 8f;

    private NavMeshAgent agent;
    private Animator animator;
    private Transform currentWaypoint;
    private bool isWaiting = true;
    private bool isBarking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        //Pick random waypoint as the starting point
        PickNewWaypoint();
        transform.position = currentWaypoint.position;

        animator.SetBool("isWalking", false); //Start Dog as idle
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isWaiting && !isBarking && distanceToPlayer <= triggerDistance)
        {
            StartCoroutine(BarkAndMove());
        }

        if (!isWaiting && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetBool("isWalking", false); //Idle when at waypoint
            isWaiting = true;
        }
    }

    IEnumerator BarkAndMove()
    {
        // isBarking = true;
        // animator.SetTrigger("isBarking"); //Play bark animation
        yield return new WaitForSeconds(1f); //Bark duration

        //Pick new random waypoint
        PickNewWaypoint();
        agent.SetDestination(currentWaypoint.position);

        animator.SetBool("isWalking", true); //Play walking animation while moving

        isWaiting = false;
        isBarking = false;
    }

    void PickNewWaypoint()
    {
        if (waypoints.Count == 0) return;

        Transform newWaypoint;
        do
        {
            newWaypoint = waypoints[Random.Range(0, waypoints.Count)];
        }
        while (newWaypoint == currentWaypoint && waypoints.Count > 1); //Avoid picking the same waypoint

        currentWaypoint = newWaypoint;
    }
}
