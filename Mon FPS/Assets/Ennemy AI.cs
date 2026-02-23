using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class EnnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    [Header("Layers")]
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private LayerMask playerLayerMask;

    [Header ("Patrol Settings")] 
    [SerializeField] private float patrolRadius = 10f;
    private Vector3 currentPatrolPoint;
    private bool hasPatrolpoint;

    [Header("Combat Settings")]
    [SerializeField] private float attackCooldown = 2f;
    private bool isOnAttackCooldown;
    [SerializeField] private float forwardShotForce = 10f;
    [SerializeField] private float verticalShotForce = 5f;
    
    [Header("Detection Ranges")]  
    [SerializeField] private float visionRange = 15f;
    [SerializeField] private float engagementRange = 10f;

    private bool isPlayerVisible;
    private bool isPlayerInRange;
    private Animation animations;
    public int health = 100;

    public void DoDammage(int dammage)
    {
        health -= dammage;
        if ( health <= 0)
        {
          Destroy(gameObject);
        }
    }

    private void Awake()
    {
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.Find("Joueur");
            if (playerObj != null)
                playerTransform = playerObj.transform;

            if (navAgent == null)
                navAgent = GetComponent<NavMeshAgent>();
        }
    }

    private void Update()
    {
        DetectPlayer();
        UpdateBehaviourState();

      
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, engagementRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }

    private void DetectPlayer()
    {
        isPlayerVisible = Physics.CheckSphere(transform.position, visionRange, playerLayerMask);
        isPlayerInRange = Physics.CheckSphere(transform.position, engagementRange, playerLayerMask);
    }

    private void FireProjectile()
    {
        if (projectilePrefab == null || firePoint == null) return;

        Rigidbody projectileRb = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        projectileRb.AddForce(transform.forward * forwardShotForce, ForceMode.Impulse);

        Destroy(projectileRb.gameObject, 3f); // Détruit le projectile après 3 secondes
    }

    private void FindPatrolPoint()
    {
        float randomX = Random.Range(-patrolRadius, patrolRadius);
        float randomZ = Random.Range(-patrolRadius, patrolRadius);

        Vector3 potentialPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(potentialPoint, -playerTransform.up, 2f, terrainLayer))
        {
            currentPatrolPoint = potentialPoint;
            hasPatrolpoint = true;
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        isOnAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnAttackCooldown = false;
    }

    private void PerformPatrol()
    {
        if (!hasPatrolpoint)
            FindPatrolPoint();
         
        if (hasPatrolpoint)
           navAgent.SetDestination(currentPatrolPoint);

        if (Vector3.Distance(transform.position, currentPatrolPoint) < 1f)
            hasPatrolpoint = false;
    }

    private void PerformChase()
    {
        if (playerTransform != null)
            navAgent.SetDestination(playerTransform.position);
    }

    private void PerformAttack()
    {
        navAgent.SetDestination(transform.position); // s'arrête sur place pour attaquer

        if (playerTransform != null) 
        {
            transform.LookAt(playerTransform); // Regarde le joueur avant de tirer  
        }

        if (!isOnAttackCooldown)
        {
            FireProjectile();
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private void UpdateBehaviourState()
    {
        if (! isPlayerVisible && ! isPlayerInRange)
        {
            PerformPatrol();
        }
        else if (isPlayerVisible && !isPlayerInRange)
        {
            PerformChase();
        }
        else if (isPlayerInRange)
        {
            PerformAttack();
        }
    }











}