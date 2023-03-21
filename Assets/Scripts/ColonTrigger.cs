using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonTrigger : MonoBehaviour
{
    private bool isActive;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isActive = true;
        }
        SetActive();
    }

    private void SetActive()
    {
        if (isActive)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
