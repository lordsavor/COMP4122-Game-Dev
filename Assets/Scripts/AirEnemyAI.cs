using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyAI : MonoBehaviour
{
    
    public Transform checkpoint;
    public float moveSpeed = 5f; // Speed when moving towards the checkpoint
    public float traceSpeed = 10f; // Speed when tracing the player
    public float acceleration = 5f; // Acceleration when transitioning to trace speed
    public float traceOffset = 10f; // Offset distance from the player

    private bool reachedCheckpoint = false;
    private bool isTracing = false;
    bool attackState = false;
    private Vector3 targetPosition;
    private float currentSpeed;
    private Rigidbody rb;

    public Transform player;
    public float sightRange=80, attackRange=50;
    public bool playerInSightRange, playerInAttackRange;
    [SerializeField] public LayerMask whatIsPlayer;

    private float timeBetweenShots;
    private bool canFire = true;
    [SerializeField] public Transform bulletSpawnPoint;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float bulletBaseSpeed = 200;

    void Start()
    {
        currentSpeed = moveSpeed;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange)
        {
            // Assign the detected player's transform to the player variable
            Collider[] players = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer);
            if (players.Length > 0)
            {
                player = players[0].transform;
            }
            StartTracing();
        }

        if (playerInAttackRange)
        {
            attackState = true;
            Attack();
        }
        else
        {
            attackState = false;
        }

        if (isTracing)
        {
            // Calculate the target position with an offset from the player's position
            Vector3 directionToPlayer = player.position - transform.position;
            Vector3 offsetDirection = Vector3.Cross(directionToPlayer.normalized, Vector3.up);
            targetPosition = player.position + (offsetDirection * traceOffset);

            // Smoothly increase the speed towards the trace speed
            currentSpeed = Mathf.Lerp(currentSpeed, traceSpeed, acceleration * Time.fixedDeltaTime);

            // Calculate the velocity towards the target position
            Vector3 desiredVelocity = (targetPosition - transform.position).normalized * currentSpeed;

            // Apply the desired velocity to the rigidbody
            rb.velocity = desiredVelocity;
            OnDrawGizmos();
        }
        else if (!reachedCheckpoint)
        {
            // Move towards the checkpoint with the move speed
            Vector3 desiredVelocity = (checkpoint.position - transform.position).normalized * moveSpeed;

            // Apply the desired velocity to the rigidbody
            rb.velocity = desiredVelocity;
        }
    }

    public void StartTracing()
    {
        isTracing = true;
    }
    public void StopTracing()
    {
        isTracing = false;
        currentSpeed = moveSpeed;
        rb.velocity = Vector3.zero;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,targetPosition);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == checkpoint)
        {
            reachedCheckpoint = true;
            rb.velocity = Vector3.zero;
        }
    }

    private void Attack()
    {
        if (canFire && attackState)
        {
            StartCoroutine(FireBullets());
        }
    }

    IEnumerator FireBullets()
    {
        canFire = false;

        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Get the direction to the target position
        Vector3 targetDirection = (player.transform.position - bulletSpawnPoint.position).normalized;

        bullet.GetComponent<Rigidbody>().velocity = targetDirection * bulletBaseSpeed;

        // Destroy the bullet after 5 seconds (adjust as needed)
        Destroy(bullet, 5f);

        yield return new WaitForSeconds(timeBetweenShots);

        canFire = true;
    }
}
