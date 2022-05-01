using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewHitPoint : MonoBehaviour {
  public Transform redDot;
  
  Camera mainCamera;
  Ray ray;
  RaycastHit hitInfo;
  // float angle = 0f;
  // Vector3 axis = Vector3.zero;
  // int distanceToDot = 10;

  void Start() {
    mainCamera = Camera.main;
  }

  void Update() {
    ray.origin = mainCamera.transform.position;
    ray.direction = mainCamera.transform.forward;
    Physics.Raycast(ray, out hitInfo);
    transform.position = hitInfo.point;

    // if (hitInfo.distance <= distanceToDot) {
    //   redDot.position = hitInfo.point;
    // } else {
    //   mainCamera.transform.rotation.ToAngleAxis(out angle, out axis);
    //   redDot.position = redDot.position + Quaternion.AngleAxis(angle, axis) * Vector3.forward * distanceToDot;
    // }
  }
}
