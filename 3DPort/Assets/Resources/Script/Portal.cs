using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private bool PActive;

    private void Start()
    {
        PActive = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject.Find("TextBox").SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
                PActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("TextBox").SetActive(false);
        }
    }
}
