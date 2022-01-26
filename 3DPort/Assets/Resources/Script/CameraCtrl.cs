using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private GameObject Player;

    public bool FindTarget;

    RaycastHit hit;

    private void Awake()
    {
        MainCamera = transform.Find("Main Camera").GetComponent<Camera>();
        FindTarget = false;
    }

    void Start()
    {
        //CRay = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2));
    }

    private void FixedUpdate()
    {
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); 
        Vector3 camAngle = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y * 3.0f, camAngle.y + mouseDelta.x * 3.0f, camAngle.z);

        if(Input.anyKey)
            Player.transform.rotation = Quaternion.Euler(0.0f, camAngle.y + mouseDelta.x * 3.0f, 0.0f);

        
        if (Physics.Raycast(transform.Find("Main Camera").position, transform.Find("Main Camera").forward, out hit))
        {
            if (hit.transform != null)
                GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>().TargetPos = hit.point;
            else
                FindTarget = false;
        }
    }

    private void LateUpdate()
    {
        transform.position = Player.transform.position;
    }
}
