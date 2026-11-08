using UnityEngine;
using UnityEngine.AI;
using System.Collections; 

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer, whatIsWall;

    [SerializeField] public bool targetPlayer;
    [SerializeField] private HealthStamina healthStamina;

    // animations 
    private Animator animator;

    //patrolling
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    //attacking 
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    private int attackCount = 0;
    [SerializeField] private int maxAttack = 1; 

    //states 
    public float detectRange, attackRange;
    public bool playerInDetectRange, playerInAttackRange;

    private float nextAttackTime; 
    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        nextAttackTime = Time.time; 
        
    }



    private void Update()
    {
        //check for sight and attack range 
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); 
        playerInDetectRange = Physics.CheckSphere(transform.position, detectRange, whatIsPlayer);

        if (!(targetPlayer || playerInDetectRange) && !playerInAttackRange) Patroling();
        if ((targetPlayer || playerInDetectRange)&& !playerInAttackRange) ChasePlayer();
        if ((targetPlayer || playerInDetectRange) && playerInAttackRange) AttackPlayer();

    }
    private void Patroling()
    {
        animator.SetBool("IsRunning", false);
        agent.speed = 5f; 
        if (waypoints.Length == 0)
        {
            return;
        }
        Transform target = waypoints[currentWaypointIndex];
        agent.SetDestination(target.position);

        
        // only check horizontal distance, ignore Y
        Vector3 enemyFlat = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 waypointFlat = new Vector3(target.position.x, 0, target.position.z);

        if (Vector3.Distance(enemyFlat, waypointFlat) < 1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            //The % waypoints.Length wraps back to 0 after the last waypoint so it loops forever.
            
        }
    }

    
    private void ChasePlayer()
    {
        animator.SetBool("IsRunning", true);
        agent.speed = 17f; 
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        //enemy does not move while attacking 
        //agent.SetDestination(transform.position);
        //transform.LookAt(player); 

        if (Time.time >= nextAttackTime)
        {
            Debug.Log("attack >:( ");

            animator.SetTrigger("Attack"); 

           
            nextAttackTime = Time.time + timeBetweenAttacks;

            if(attackCount >= maxAttack)
            {
                attackCount = 0;
                SwitchToPatrol();
            }
           
            
        }
    }

    public void AnimDealDamage()
    {
        if (playerInAttackRange)
        {
            healthStamina.getdamaged(20);
            Debug.Log("Player Hit!");
        }
    }

    private void SwitchToPatrol()
    {
        Debug.Log("Swithcing to Patrol");
       
        playerInAttackRange = false;
        playerInDetectRange = false;
    }


   
}
