using UnityEngine;
using UnityEngine.AI;
using BNG;

public class TutorialAvatar : MonoBehaviour
{
    public Transform playerTransform;
    public float followDistance = 2.5f;
    public float rotationSpeed = 3.0f;

    private NavMeshAgent navAgent;
    private Animator animator;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navAgent.baseOffset = 0.8f;
        InitializePlayerTransform();
    }

    void Update()
    {
        // Recheck playerTransform if it becomes null (e.g., after scene reload)
        if (playerTransform == null || playerTransform.gameObject == null)
        {
            InitializePlayerTransform();
            if (playerTransform == null) return; // Exit if still not found
        }

        navAgent.destination = playerTransform.position;

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
    }

    void InitializePlayerTransform()
    {
        if (playerTransform == null)
        {
            BNGPlayerController player = FindFirstObjectByType<BNGPlayerController>();
            if (player != null)
            {
                playerTransform = player.transform;
            }
        }
    }
}
