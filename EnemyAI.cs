using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;


    //patrolling

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    public GameObject enemySwordHitbox;
    public AudioClip swingNoise;
    private AudioSource audioSource;
    //states

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player Controller").transform;
        agent = GetComponent<NavMeshAgent>();
    }




    private void FixedUpdate()
    {

        //Check for sight and Attack range

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // sets AI state based off of if the player is inrange to be seen / attacked
        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        if (playerInAttackRange && playerInSightRange)
            AttackPlayer();

    }



    //function to handle AI wandering on the navmesh. 
    private void Patroling()
    {
        PlayTargetAnimation("walk", false, true);
        if (!walkPointSet)
            SearchWalkPoint();
            

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached

        if (distanceToWalkPoint.magnitude < 2f)
        {
            walkPointSet = false;
        }


    }

    // function that create walk points for the nav mesh agent to move too randomly, on the nav mesh. 
    private void SearchWalkPoint()
    {
        // calculate a random walk point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet= true;
        }
    }

    // if triggered, walkpoint = player location 
    private void ChasePlayer()
    {

        print("Chasing player");
        
        agent.SetDestination(player.position);
    }

    //runs attack anim
    private void AttackPlayer()
    {
        //stop movement
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //attack code

            PlayTargetAnimation("Sword Slash", true, false);
            Invoke("showHitbox", 0.5f);
            audioSource.PlayOneShot(swingNoise);
            print("attacked player");
            Invoke("hideHitbox", 0.6f);


            //

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        PlayTargetAnimation("walk", false, true);
    }

    private void showHitbox()
    {
        enemySwordHitbox.SetActive(true);
    }
    private void hideHitbox()
    {
        enemySwordHitbox.SetActive(false);
    }

    //special duplicate of animation manager function designed for Enemy AIs since they using the player function would affect player actions.
    public void PlayTargetAnimation(string targetAnimation, bool isAttacking, bool isMoving)
    {
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isMoving", isMoving);
        animator.CrossFade(targetAnimation, 0.2f);
    }

}
