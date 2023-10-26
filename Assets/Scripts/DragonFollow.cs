using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followSpeed = 3.0f; // Speed at which the dragon follows the player
    public float attackInterval = 10.0f; // Time interval between attacks
    public Animator dragonAnimator; // Reference to the dragon's animator

    private NavMeshAgent dragonAgent;
    private bool isFollowing = true;
    private float timeSinceLastAttack = 0.0f;

    void Start()
    {
        dragonAgent = GetComponent<NavMeshAgent>();
        timeSinceLastAttack = attackInterval; // Start with the dragon ready to attack
    }

    void Update()
    {
        if (isFollowing)
        {
            // Calculate the distance between the dragon and the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Move the dragon towards the player
            dragonAgent.SetDestination(player.position);
            dragonAgent.speed = followSpeed;

            // If the dragon gets close to the player, stop following and prepare to attack
            if (distanceToPlayer < 3.0f)
            {
                isFollowing = false;
                timeSinceLastAttack = 0.0f;
                dragonAgent.ResetPath(); // Stop moving
                dragonAnimator.SetTrigger("Attack"); // Trigger the attack animation
            }
        }
        else
        {
            // Dragon is not following, it's preparing to attack
            timeSinceLastAttack += Time.deltaTime;

            // If enough time has passed, resume following
            if (timeSinceLastAttack >= attackInterval)
            {
                isFollowing = true;
                dragonAnimator.SetTrigger("Walk"); // Trigger the walk animation
            }
        }
    }
}
