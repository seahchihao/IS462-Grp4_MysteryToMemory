using UnityEngine;
using UnityEngine.AI;
using BNG; // VRIF namespace

public class TutorialAvatar : MonoBehaviour
{
    public Transform playerTransform;
    public float followDistance = 2.5f;
    public float rotationSpeed = 3.0f;
    
    private NavMeshAgent navAgent;
    private Animator animator;
    Vector3 dest;
    
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navAgent.baseOffset = 0.8f;
        
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
        dest = playerTransform.position;
        navAgent.destination = dest;

        if (playerTransform != null)
        {
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                animator.ResetTrigger("run");
                animator.SetTrigger("idle");
            }
            else
            {
                animator.ResetTrigger("idle");
                animator.SetTrigger("run");
            }
            navAgent.SetDestination(playerTransform.position);
        }

        if (playerTransform == null)
        {
            BNGPlayerController player = FindFirstObjectByType<BNGPlayerController>();
            if (player != null)
            {
                playerTransform = player.transform;
                Debug.Log("Reassigned player transform after scene load.");
            }
            return;
        }
    }
}