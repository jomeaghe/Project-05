using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  // For NavMeshAgent, if you are using NavMesh

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 10f;  // Range at which the enemy can detect the player
    public float attackRange = 2f;      // Range at which the enemy can "hit" the player
    public float moveSpeed = 3f;        // Speed at which the enemy moves towards the player

    private Transform playerTransform;  // Reference to the player's transform
    private NavMeshAgent navAgent;      // Reference to the NavMeshAgent (optional)
    private bool isPlayerDetected = false;

    private void Start()
    {
        // Get the player's transform (find the player by tag)
        playerTransform = GameObject.FindWithTag("Player").transform;

        // Get the NavMeshAgent (only needed if you're using it for navigation)
        navAgent = GetComponent<NavMeshAgent>();
        if (navAgent != null)
        {
            navAgent.speed = moveSpeed;
        }
    }

    private void Update()
    {
        if (playerTransform == null) return;  // Safety check in case player is not found

        // Check if the player is within detection range
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRange)
        {
            isPlayerDetected = true;
        }
        else
        {
            isPlayerDetected = false;
        }

        // If the player is detected, move towards the player
        if (isPlayerDetected)
        {
            if (navAgent != null)
            {
                navAgent.SetDestination(playerTransform.position);  // Move the enemy toward the player
            }
            else
            {
                // Simple movement without NavMesh
                Vector3 direction = (playerTransform.position - transform.position).normalized;
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            }

            // If the enemy is within attack range, end the game (hit player)
            if (distanceToPlayer <= attackRange)
            {
                HitPlayer();
            }
        }
    }

    // This function is called when the enemy hits the player
    private void HitPlayer()
    {
        Debug.Log("Game Over! The enemy hit the player.");

        // Call a method to end the game, show the game over screen, etc.
        GameManager.instance.GameOver();
    }
}