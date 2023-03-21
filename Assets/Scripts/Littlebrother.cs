using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Littlebrother : MonoBehaviour
{
   [SerializeField] private bool brotherHiding = false;
    public bool brotherhiding { get { return brotherHiding; } set { brotherHiding = value; } }
    [SerializeField] private bool followPlayer= true;
    private int fastMode = 15;
    private int slowMode = 10;
    private int playerDistance = 2;
    private int obstacleDistance = 5;
    private NavMeshAgent agent;
    private GameObject player;
    private Animator anim;
    private bool Run;
    [SerializeField] private GameObject[] obstacles;
    private GameObject obstacle;
    public GameObject Obstacle { get { return obstacle; } set { obstacle = value; } }


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
       if(followPlayer == true)
       {
            FollowPlayer();
       } 
    }
    public void Hide()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (PlayerController.instance.CountDistance(obstacles[i].transform, obstacleDistance) && brotherHiding == false)
            {
                PlayerController.instance.SpeedChanger(fastMode);
                obstacle = obstacles[i];
                //obstacle.HidinginsideofboxAnimation
                gameObject.SetActive(false);
                followPlayer = false;
                brotherHiding = true;
                return;
            }
        }
        Debug.Log("Not close enough to hiding");
        brotherHiding = false;
    }

    public void Collapse()
    {
      
            PlayerController.instance.SpeedChanger(slowMode);
            brotherHiding = false;
            //obstacle.RevealingAnimation
            gameObject.SetActive(true);
            followPlayer = true;
        
    }

    private void FollowPlayer()
    {
        if (PlayerController.instance.CountDistance(gameObject.transform , playerDistance))
        {
            anim.SetTrigger("Idle");
            agent.isStopped = true;
        }
        else
        {
            anim.SetTrigger("Run");
            agent.isStopped = false;
            agent.destination = player.transform.position;
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            Debug.Log("GameFinished");
            anim.SetTrigger("LevelUp");
            player.GetComponent<PlayerController>().Anim.SetTrigger("LevelUp");
            yield return new WaitForSeconds(3f);
            CanvasManager.instance.NextLevelCanvas();
        }
    }


}
