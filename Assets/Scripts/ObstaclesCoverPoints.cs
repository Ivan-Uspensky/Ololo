using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesCoverPoints : MonoBehaviour {
  Renderer rnd = null;
  Vector3 newPoint1, newPoint2, newPoint3, newPoint4 = Vector3.zero;
  float newPointY = 0.5f;
  float newPointOffset = 0.5f;
  Vector3 newPointActiveSide;
  // int newPointPositionModifier = 1;
  
  void OnDrawGizmos() {
    Gizmos.color = Color.yellow;
    foreach (Transform child in transform) {
      if (child.gameObject.layer == 7) {
        if (child.gameObject.tag == "Cylinder") {
          // newPointPositionModifier = 1;
          // newPointActiveSide = rnd.bounds.center;
        }
        if (child.gameObject.tag == "Cube") {
          // newPointPositionModifier = 2;
        }
        rnd = child.GetComponent<Renderer>();
        if (rnd) {
          newPointActiveSide = rnd.bounds.size;
          
          newPoint1 = new Vector3(rnd.bounds.center.x + (newPointActiveSide.x / 2) + newPointOffset, newPointY, rnd.bounds.center.z);
          newPoint2 = new Vector3(rnd.bounds.center.x - (newPointActiveSide.x / 2) - newPointOffset, newPointY, rnd.bounds.center.z);
          newPoint3 = new Vector3(rnd.bounds.center.x, newPointY, rnd.bounds.center.z + (newPointActiveSide.z / 2) + newPointOffset);
          newPoint4 = new Vector3(rnd.bounds.center.x, newPointY, rnd.bounds.center.z - (newPointActiveSide.z / 2) - newPointOffset);
          
          // Gizmos.DrawSphere(newPoint1, 0.5f);
          // Gizmos.DrawSphere(newPoint2, 0.5f);
          // Gizmos.DrawSphere(newPoint3, 0.5f);
          // Gizmos.DrawSphere(newPoint4, 0.5f);
        }
      }
    }
  }
}
