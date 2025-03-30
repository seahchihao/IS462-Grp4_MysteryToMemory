using UnityEngine;
using UnityEngine.AI;
using BNG; // VRIF namespace

public class NPCFollowVRPlayer : MonoBehaviour
{
    public Transform playerTransform; // Reference to your XR Rig
    public float followDistance = 2.5f;
    public float rotationSpeed = 3.0f;
    
    private NavMeshAgent navAgent;
    private Animator animator;
    
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        // If player not assigned, find the PlayerController from VRIF
        if (playerTransform == null)
        {
            BNGPlayerController player = FindFirstObjectByType<BNGPlayerController>();
            if (player != null)
            {
                playerTransform = player.transform;
            }
        }
    }
    
    void Update()
    {
        if (playerTransform != null)
        {
            // Set the NPC's destination to the player's position
            navAgent.SetDestination(playerTransform.position);
            
            // If using an animator, control animations based on movement
            // if (animator != null)
            // {
            //     // Set animation parameter based on whether the NPC is moving
            //     animator.SetBool("IsWalking", navAgent.velocity.magnitude > 0.1f);
            // }
            
            // Make the NPC look at the player's position (at same height)
            // Vector3 lookPosition = new Vector3(playerTransform.position.x, 
            //                                   transform.position.y, 
            //                                   playerTransform.position.z);
            // transform.LookAt(lookPosition);

            // Vector3 playerDirection = playerTransform.position - transform.position;
            // playerDirection.y = 0;
            // Quaternion targetRotation = Quaternion.LookRotation(playerDirection);

            // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
