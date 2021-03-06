﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Transform elevator;

    private float mouseXInput;
    private float mouseYInput;
    private bool lookingAround = false;

    void Start() {
        Cursor.visible = false;
    }

    private Vector3 velocityCameraFollow;
    public Vector3 cameraPosition = new Vector3(0, 0, -4);
    
    private void FixedUpdate() {
        if (elevator == null) {
            elevator = GameObject.FindGameObjectWithTag("Player").transform;
            return;
        }
        //transform.rotation = Quaternion.Euler(new Vector3(angle, ourDrone.GetComponent<DroneMovement>().currentYRotation, 0));
        lookingAround = GetInput();
        if (!lookingAround) {
            if (Time.time >= returnTime) {
                ReturnToPlayer();
            }
        }
        LookAround();
    }

    public float returnDelay = 1f;
    private float returnTime;
    bool GetInput() {
        mouseXInput = Input.GetAxis("Mouse X");
        mouseYInput = Input.GetAxis("Mouse Y");
        if (Input.GetAxis("Mouse ScrollWheel") != 0) {
            returnTime = 0;
            return false;
        }
        if ((mouseXInput != 0) || (mouseYInput != 0)) {
            returnTime = Time.time + returnDelay;
            return true;
        }
        return false;
    }

    public float ratio = 0.25f;
    void LookAround() {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y + (ratio * Input.GetAxis("Mouse Y")), cameraPosition.z);
    }

    public float smoothTime = 0.5f;
    void ReturnToPlayer() {
        //Debug.Log("Returning to Player");
        transform.position = Vector3.SmoothDamp(transform.position, elevator.transform.TransformPoint(cameraPosition), ref velocityCameraFollow, smoothTime);
    }
}
