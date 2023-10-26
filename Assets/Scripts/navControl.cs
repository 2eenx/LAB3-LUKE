using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navControl : MonoBehaviour
{
    public GameObject Target;
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;

    bool isWalking = true;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isWalking)
        {
            agent.destination = Target.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Dragon")
        {
            isWalking = false;
            animator.SetTrigger("Attack");
            FaceTarget(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Dragon")
        {
            isWalking = true;
            animator.SetTrigger("Walk");
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (Target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
