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

    [Header("Layers")] // Définie les layers avec lesquelles l'ennemy interagit
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private LayerMask playerLayerMask;

    [Header ("Patrol Settings")] // définie les paramètre de patrouille
    [SerializeField] private float patrolRadius = 10f;
    private Vector3 currentPatrolPoint;
    private bool hasPatrolpoint;

    [Header("Combat Settings")] // définie les paramètres de combat
    [SerializeField] private float attackCooldown = 2f;
    private bool isOnAttackCooldown;
    [SerializeField] private float forwardShotForce = 10f;
    [SerializeField] private float verticalShotForce = 5f;
    
    [Header("Detection Ranges")]  // Définie les champs de détection et d'attaque
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

    private void Update() // appelle les méthodes de détection et de l'état de comportement à chaque frame
    {
        DetectPlayer();
        UpdateBehaviourState();

      
    }

    private void OnDrawGizmosSelected() //Défini les champs de détection et de combat dans l'éditeur
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, engagementRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }

    private void DetectPlayer() // Défini le comportement de l'ennemy en fonction de la position du joueur
    {
        isPlayerVisible = Physics.CheckSphere(transform.position, visionRange, playerLayerMask); // Défini le champ de vision de l'ennemy
        isPlayerInRange = Physics.CheckSphere(transform.position, engagementRange, playerLayerMask); // Définie le champs d'attaque de l'ennemy
    }

    private void FireProjectile() // Permet de tirer un projectile sur le joueur
    {
        if (projectilePrefab == null || firePoint == null) return;

        Rigidbody projectileRb = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        projectileRb.AddForce(transform.forward * forwardShotForce, ForceMode.Impulse);

        Destroy(projectileRb.gameObject, 3f); // Détruit le projectile après 3 secondes
    }

    private void FindPatrolPoint() // Défini la zone de patrouille de l'ennemy
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

    private IEnumerator AttackCooldownRoutine() // Défini le cooldown entre les attaques 
    {
        isOnAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnAttackCooldown = false;
    }

    private void PerformPatrol() // Permet à l'ennemy de patrouiller dans une zone définie
    {
        if (!hasPatrolpoint)
            FindPatrolPoint();
         
        if (hasPatrolpoint)
           navAgent.SetDestination(currentPatrolPoint);

        if (Vector3.Distance(transform.position, currentPatrolPoint) < 1f)
            hasPatrolpoint = false;
    }

    private void PerformChase() // Permet à l'ennemy de pourisuivre le joueur lorsqu'il rentre dans son champs de vision 
    {
        if (playerTransform != null)
            navAgent.SetDestination(playerTransform.position);
    }

    private void PerformAttack() // Permet à l'ennemy d'attaquer le joueur lorsqu'il rentre dans son champs d'attaque
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

    private void UpdateBehaviourState() // Défini comment agis l'ennemy en fonction de la positon du joueur
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