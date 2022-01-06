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
        Player = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Player.transform.position;

        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); 
        Vector3 camAngle = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y * 3.0f, camAngle.y + mouseDelta.x * 3.0f, camAngle.z);

        Player.transform.rotation.SetEulerAngles(0.0f, transform.rotation.y, 0.0f);
    }
}
