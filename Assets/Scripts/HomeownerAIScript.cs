using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class HomeownerAIScript : MonoBehaviour
{   
    private Animator anim;
    private bool isWaiting = false;
    private bool isChaseMusicPlaying = false;
    private bool noiseTriggered = false;
    private float noiseTriggerThreshold = 0.8f; // 80%
    private float stopChaseDistance = 3f; // ~10 feet


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

    void Update()
    {
        float currentNoise = NoiseManager.Instance.GetNoiseLevel();
        float maxNoise = NoiseManager.Instance.GetMaxNoise();
        float noisePercentage = currentNoise / maxNoise;

        // === Trigger behavior if noise is too high ===
        if (!noiseTriggered && noisePercentage >= noiseTriggerThreshold)
        {
            noiseTriggered = true;

            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (playerInAttackRange && playerInSightRange)
            {
                StopPersistentChase();
                AttackPlayer();
            }
            else if (distanceToPlayer <= stopChaseDistance)
            {
                ChasePlayer();
            }
            else
            {
                Patrol();
                EndChaseMusic();
            }
            // for later: Play alert animation or sound
            // anim.SetTrigger("Alerted");
            // audioSource.PlayOneShot(alertSound);
        }

        // === Reset trigger if noise drops below buffer threshold ===
        if (noiseTriggered && noisePercentage < noiseTriggerThreshold - 0.2f)
        {
            noiseTriggered = false;
        }

        // === If not triggered, stay idle ===
        if (!noiseTriggered)
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            return;
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
