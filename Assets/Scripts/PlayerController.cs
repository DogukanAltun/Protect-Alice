using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float Hiz = 10f;
    public bool playerHiding;
    private int obstacleDistance = 5;
    [SerializeField] private GameObject[] obstacles;
    private GameObject obstacle;
    private Animator anim;
    public Animator Anim { get { return anim; } set { anim = value; } }
    private bool Run = false;
    private Littlebrother brother;
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
        brother = GameObject.FindObjectOfType<Littlebrother>();
    }

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }
    void Update()
    {
         Mover();
    }

    private void Mover()
    {
        float Yatay = Input.GetAxisRaw("Horizontal") * Time.deltaTime * Hiz;
        float Dikey = Input.GetAxisRaw("Vertical") * Time.deltaTime * Hiz;
        Vector3 movementDirection = new Vector3(-Yatay, 0, -Dikey);
        transform.Translate(movementDirection);
        if(movementDirection != Vector3.zero)
        {
            transform.GetChild(0).transform.forward = movementDirection;
        }
        if(movementDirection == Vector3.zero && !Run)
        {
            anim.SetTrigger("Idle");
            Run = true;
        }
        else if (movementDirection != Vector3.zero && Run)
        {
            anim.SetTrigger("Run");
            Run = false;
        }
    }

    public void SpeedChanger(int Speed)
    {
        Hiz = Speed;
    }
    public void PlayerHide()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
          
            if (CountDistance(obstacles[i].transform, obstacleDistance))
            {
                obstacle = obstacles[i];
                if (obstacle != brother.Obstacle)
                {
                    //obstacle.HidinginsideofboxAnimation
                    playerHiding = true;
                    gameObject.SetActive(false);
                    return;
                }
                else
                {
                    Debug.Log("This obstacle already used");
                }
            }
        }
        Debug.Log("Not close enough to hiding");
    }

    public void Reveal()
    {
        //obstacle.RevealingAnimation
        playerHiding = false;
        gameObject.SetActive(true);
    }

    public bool CountDistance(Transform Object, int Value)
    {
        float distance = Vector3.Distance(transform.position, Object.position);
        if (distance < Value)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

   

}
