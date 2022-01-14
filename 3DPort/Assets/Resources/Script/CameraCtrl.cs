using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private GameObject Player;

    private void Awake()
    {
        MainCamera = transform.Find("Main Camera").GetComponent<Camera>();

    }

    void Start()
    {
    }

    private void FixedUpdate()
    {
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        transform.position = Player.transform.position;

        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); 
        Vector3 camAngle = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y * 3.0f, camAngle.y + mouseDelta.x * 3.0f, camAngle.z);

        if(Input.anyKey)
            Player.transform.rotation = Quaternion.Euler(0.0f, camAngle.y + mouseDelta.x * 3.0f, 0.0f);

        //if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.R))
    }
}
