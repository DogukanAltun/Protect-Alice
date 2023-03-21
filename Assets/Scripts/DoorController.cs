using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorController : MonoBehaviour
{
    private PartedDoor[] Doors;
    private Enemy[] enemies;
    private GameObject player;
    public GameObject closestDoor;
    private void Start()
    {
        Doors = GameObject.FindObjectsOfType<PartedDoor>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindObjectsOfType<Enemy>();
    }

    private void Update()
    {
        CalculateClosestDoor();
    }

    public void BuildDoor()
    {
        PartedDoor partedDoor = closestDoor.GetComponent<PartedDoor>();
        partedDoor.anim.SetTrigger("Build");
        partedDoor.NavObstacle.enabled = true;
        partedDoor.DoorStatus.doorStatus = Enum.DoorStatus.Builted;
        StartCoroutine(buildLayer());
    }

    public IEnumerator buildLayer()
    {
        PartedDoor partedDoor = closestDoor.GetComponent<PartedDoor>();
        yield return new WaitForSeconds(1f);
        partedDoor.transform.parent.GetChild(1).transform.gameObject.SetActive(true);
    }

    public void DestroyDoor()
    {
        PartedDoor partedDoor = closestDoor.GetComponent<PartedDoor>();
        partedDoor.anim.SetTrigger("Destroy");
        partedDoor.NavObstacle.enabled = false;
        partedDoor.transform.parent.GetChild(1).transform.gameObject.SetActive(false);
        Transform col = partedDoor.transform.parent.transform.parent.transform;
        foreach (Enemy enemy in enemies)
        {
            if (enemy.GetComponent<PatrolView>().DoorOfViewCheck(col))
            {
                enemy.doorPatrol = true;
            }
        }
        partedDoor.DoorStatus.doorStatus = Enum.DoorStatus.Destroyed;
    }

    public void CalculateClosestDoor()
    {
        float closestdistance = 100;
        foreach (PartedDoor partedDoor in Doors)
        {
            float distance = Vector3.Distance(player.transform.position, partedDoor.transform.position);
            if (distance < closestdistance)
            {
                closestdistance = distance;
                closestDoor = partedDoor.gameObject;
                CanvasManager.instance.SetButton();
            }
        }
    }
}
