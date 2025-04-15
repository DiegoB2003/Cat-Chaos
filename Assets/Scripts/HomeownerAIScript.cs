using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class HomeownerAIScript : MonoBehaviour
{   
    private Animator anim;
    private bool isWaiting = false;
    private bool isChaseMusicPlaying = false;

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public AudioClip swoosh; 
    [SerializeField] private AudioSource audioSource;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private bool triggeredChase = false;
    private Coroutine chaseCoroutine;

    [SerializeField] private SceneAudioManager audioManager;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerCat").transform;
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        // Check for proximity
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInAttackRange && playerInSightRange)
        {
            // Stop persistent chase if we're ready to attack
            StopPersistentChase();
            AttackPlayer();
        }
        else if (triggeredChase)
        {
            ChasePlayer();
        }
        else if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        else if (!playerInSightRange && !playerInAttackRange)
        {
            Patrol();
            EndChaseMusic();
        }
    }

    private void Patrol()
    {   
        if (!walkPointSet && !isWaiting) SearchWalkPoint();

        if (walkPointSet && !isWaiting)
        {
            agent.SetDestination(walkPoint);
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", true);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (!isWaiting && walkPointSet && distanceToWalkPoint.magnitude < 1f)
        {
            StartCoroutine(IdleBeforeNextWalk());
        }
    }

    private IEnumerator IdleBeforeNextWalk()
    {
        isWaiting = true;
        walkPointSet = false;

        agent.SetDestination(transform.position); //Stop moving
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", false); //Play idle animation

        yield return new WaitForSeconds(5f); //Wait for 5 seconds

        isWaiting = false;
    }


    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            agent.SetDestination(walkPoint);
        }
    }

    private void ChasePlayer()
    {
        TriggerChaseMusic();
        agent.SetDestination(player.position);
        anim.SetBool("isRunning", true);
        anim.SetBool("isWalking", false);
    }

    private void AttackPlayer()
    {   
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", false);

        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Add attack code here depending on what we want later
            audioSource.Stop();
            audioSource.clip = swoosh;
            audioSource.loop = false;
            audioSource.Play();

            // anim.SetBool("isAttacking", true);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private IEnumerator StopChasingAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        triggeredChase = false;
    }

    public void TriggerPersistentChase(float duration)
    {
        triggeredChase = true;
        if (chaseCoroutine != null) StopCoroutine(chaseCoroutine);
        chaseCoroutine = StartCoroutine(StopChasingAfterTime(duration));
    }

    private void StopPersistentChase()
    {
        triggeredChase = false;
        if (chaseCoroutine != null) StopCoroutine(chaseCoroutine);
        chaseCoroutine = null;
    }

    public void TriggerChaseMusic()
    {
        if (audioManager != null && !isChaseMusicPlaying) {
            audioManager.PlayChaseMusic();
            isChaseMusicPlaying = true;
        }
    }

    public void EndChaseMusic()
    {
        if (audioManager != null && isChaseMusicPlaying) {
            audioManager.StopChaseMusicAndResumeBGM();
            isChaseMusicPlaying = false;
        }
    }
}
