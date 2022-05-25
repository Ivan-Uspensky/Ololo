using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class DudeAims : MonoBehaviour {
  public float turnSpeed = 15;
  public float aimDuration = 0.3f;
  public Rig aimLayer;

  Camera mainCamera;

  void Start() {
    mainCamera = Camera.main;
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
  }

  void FixedUpdate() {
    float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
  }

  void LateUpdate() {
    if (aimLayer) {
    //   if (Input.GetButton("Fire2")) {
    //       aimLayer.weight += Time.deltaTime / aimDuration;
    //   } else {
    //       aimLayer.weight -= Time.deltaTime / aimDuration;
    //   }
      aimLayer.weight = 1;
    }
  }
}
