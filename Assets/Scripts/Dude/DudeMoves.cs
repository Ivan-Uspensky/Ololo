using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeMoves : MonoBehaviour {
    public float moveSpeed;
    public Camera watchCamera;
    Rigidbody myRBody;
    Vector3 moveInput;
    Vector3 moveVelocity;
    Vector3 pointToLook;
    
    void Start() {
      myRBody = GetComponent<Rigidbody>();
    }

    void Update() {
      moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
      moveVelocity = moveInput * moveSpeed;

      Ray cameraRay = watchCamera.ScreenPointToRay(Input.mousePosition);
      Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
      float rayLength;

      if (groundPlane.Raycast(cameraRay, out rayLength)) {
        pointToLook = cameraRay.GetPoint(rayLength);
        Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
        Vector3 fixedPtL = new Vector3(pointToLook.x, transform.position.y, pointToLook.z);
        transform.LookAt(fixedPtL);
      }


    }

    void FixedUpdate() {
      myRBody.velocity = moveVelocity;
    }
}
