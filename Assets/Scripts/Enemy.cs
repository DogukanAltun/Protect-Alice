using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float Range;
    private PlayerView PlayerVision;
    private PatrolView PatrolVision;
    private NavMeshAgent navMesh;
    private GameObject player;
    private LevelDesign levelDesigner;
    private CanvasManager canvasManager;
    private Animator anim;
    public int enemyGetFaster;
    public int enemySpeed;
    private float WaitTime;
    private float DoorTime;
    private float FollowTime = 5f;
    private float suspiciousTime = 2f;
    private bool DoorPatrol;
    private bool waiting;
    private bool Run;
    private int RoomNumber;
    public bool doorPatrol { get { return DoorPatrol; } set { DoorPatrol = value; } }
    public int roomNumber { get { return RoomNumber; } set { RoomNumber = value; } }


    private void Start()
    {
        anim = transform.GetChild(1).GetComponent<Animator>();
        canvasManager = FindObjectOfType<CanvasManager>();
        navMesh = GetComponent<NavMeshAgent>();
        PlayerVision = gameObject.GetComponent<PlayerView>();
        PatrolVision = gameObject.GetComponent<PatrolView>();
        levelDesigner = FindObjectOfType<LevelDesign>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyGetFaster = levelDesigner.enemyGetFaster;
        enemySpeed = levelDesigner.enemySpeed;
    }

    private void Update()
    {
        if (PlayerVision.canseePlayer == true)
        {
            CatchHim();
        }
        else if (DoorPatrol == true)
        {
            DoorTime += Time.deltaTime;
            GoToPlayer();
            if(DoorTime > FollowTime)
            {
                Run = false;
                WaitTime = 0;
                waiting = true;
                DoorPatrol = false;
            }
        }
        else if (WaitTime < suspiciousTime && waiting)
        {
            anim.SetTrigger("Idle");
            PatrolVision.ChangePatrol();
            navMesh.isStopped = true;
            waiting = false;
        }
        else if (!waiting)
        {
            DoorTime = 0;
            navMesh.speed = enemySpeed;
            PatrolVision.PatrolTime();
        }
        WaitTime += Time.deltaTime;
    }

    private void GoToPlayer()
    {
        Debug.Log(PlayerVision.canseePlayer);
        navMesh.isStopped = false;
        navMesh.speed = enemyGetFaster;
        navMesh.destination = player.transform.position;
        if (!Run)
        {
            anim.SetTrigger("Run");
            Run = true;
        }
    }

    private void CatchHim()
    {
        if (canvasManager.Stopped == false)
        {
            navMesh.isStopped = true;
            CanvasManager.instance.GameOverCanvas();
            canvasManager.Stopped = true;
        }
    }
   
}
