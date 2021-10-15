using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControl : MonoBehaviour
{
    public Camera GameCamera;
    public GameObject CameraMove;
    public float PanSpeed = 10.0f;
    private float offsetCameraX = 25;
    private Vector3 cameraDestinyPoint;  

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //GameCamera.transform.position = GameCamera.transform.position + new Vector3(move.y, 0, -move.x) * PanSpeed * Time.deltaTime;

        GameCamera.transform.position = new Vector3(CameraMove.transform.position.x - offsetCameraX, 15, CameraMove.transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }
    }

    public void HandleSelection()
    {
        var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
        Debug.Log(ray);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.point);
        }

        cameraDestinyPoint = new Vector3(hit.point.x, 15, hit.point.z);
        CameraMove.GetComponent<CameraMove>().GoTo(cameraDestinyPoint);
        //GameCamera.GetComponent<CameraMove>().GoTo(cameraDestinyPoint);
    }
    
}
