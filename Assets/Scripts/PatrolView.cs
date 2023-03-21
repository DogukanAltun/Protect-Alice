using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PatrolView : MonoBehaviour
{
    public LayerMask colonMask;
    public LayerMask obstructionMask;
    public LayerMask doorMask;
    private NavMeshAgent navMesh;
    private Transform targetObject;
    private bool canSeePatrol;
    [SerializeField] private int j = 0;
    [SerializeField] private int add = 1;
    [SerializeField] private int colonLength;
    [SerializeField] private Transform target;
    [SerializeField] private float Range;
    [SerializeField] private float viewRadius = 70;
    public Collider[] rangeChecks;


    public static PatrolView instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        navMesh = gameObject.GetComponent<NavMeshAgent>();
        rangeChecks = Physics.OverlapSphere(transform.position, viewRadius, colonMask);
        CheckVisibility();
    }
    
    public bool CountDistance(Transform closestPatrol)
    {
        float distance = Vector3.Distance(gameObject.transform.position, closestPatrol.position);
    
        if (distance < Range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangePatrol()
    {
        navMesh.isStopped = true;
        if (j < colonLength && j >= 0)
        {
            if (j + add == colonLength)
            {
                j = colonLength - 2;
                add = -1;
            }
            else if (j + add == -1)
            {
                add = 1;
                j = 1;
            }
            else
            {
                j += add;
            }
        }
        CheckVisibility();
    }
    public void PatrolTime()
    {
        if (canSeePatrol)
        {
            if (!CountDistance(targetObject) )
            {
                navMesh.isStopped = false;
                navMesh.destination = targetObject.position;
            }
            else
            {
                ChangePatrol();
            }
        }
        else
        {
            ChangePatrol();
        }
    }

    private void CheckVisibility()
    {
        colonLength = rangeChecks.Length;
        if (colonLength != 0)
        {
            target = rangeChecks[j].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < 360 / 2)
            {
                 float distanceToTarget = Vector3.Distance(transform.position, target.position);
                 if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask) && !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, doorMask))
                 { 
                        targetObject = rangeChecks[j].transform;
                        canSeePatrol = true;
                 }
                 else
                 {
                     canSeePatrol = false;
                 }
            }
            else
            {
                canSeePatrol = false;
            }
        }
    }

    public bool DoorOfViewCheck(Transform door)
    {
            Vector3 directionToTarget = (door.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < 358 / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, door.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
    }
}
