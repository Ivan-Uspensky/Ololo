using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesScanner : MonoBehaviour {
  [Range(0, 360)]
  public float viewAngle;
  public float viewRadius;
  public LayerMask coverMask;

  public GameObject ShitheadsPack;
  HiveMind HiveMind;
  RaycastHit hit;
  List<GameObject> allCovers = new List<GameObject>();

  [HideInInspector]
  public List<GameObject> visibleCoversFront = new List<GameObject>();
  [HideInInspector]
  public List<GameObject> visibleCoversLeft = new List<GameObject>();
  [HideInInspector]
  public List<GameObject> visibleCoversRight = new List<GameObject>();

  void Start() {
    HiveMind = ShitheadsPack.GetComponent<HiveMind>();

    GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
    foreach(GameObject go in gos) {
      if (go.layer == 7) {
        allCovers.Add(go);
      }
    }

    StartCoroutine("FindClosestCoversWithDelay", 1);
  }

  IEnumerator FindClosestCoversWithDelay(float delay) {
    while (true) {
      yield return new WaitForSeconds(delay);
      FindClosestCovers();
    }
  }

  void FindClosestCovers() {

    visibleCoversFront.Clear();
    visibleCoversRight.Clear();
    visibleCoversLeft.Clear();

    for (int i = 0; i < allCovers.Count; i++) {
      GameObject target = allCovers[i];
      Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
      if (Physics.Raycast(transform.position, dirToTarget, out hit, viewRadius)) {
        if (hit.collider.gameObject.layer == 7 && hit.collider.gameObject.name == target.name) {
          float angle = Vector3.SignedAngle(transform.forward, dirToTarget, Vector3.up);
          if ((angle >= -viewAngle / 2) && (angle < viewAngle / 2)) {
              visibleCoversFront.Add(target);
          }
          if ((angle >= viewAngle / 2) && (angle < (viewAngle / 2 + 90) )) {
              visibleCoversRight.Add(target);
          }
          if ((angle >= (-viewAngle / 2 -90) ) && (angle <= -viewAngle / 2)) {
              visibleCoversLeft.Add(target);
          }
        }
      }
    }

    if (visibleCoversFront.Count > 3) visibleCoversFront = visibleCoversFront.GetRange(0, 3);
    if (visibleCoversRight.Count > 3) visibleCoversRight = visibleCoversRight.GetRange(0, 3);
    if (visibleCoversLeft.Count > 3) visibleCoversLeft = visibleCoversLeft.GetRange(0, 3);
  
    HiveMind.GeneratePaths(visibleCoversLeft, visibleCoversRight, visibleCoversFront);
  
  }
  
  // public List<Transform> GetCoversFront() {
  //   return visibleCoversFront;
  // }

  // public List<Transform> GetCoversLeft() {
  //   return visibleCoversLeft;
  // }

  // public List<Transform> GetCoversRight() {
  //   return visibleCoversRight;
  // }

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

  // void OnDrawGizmos() {
  //   Vector3 mod = Vector3.up * 2;
  //   Gizmos.color = Color.red;
  //   for (int i = 0; i < visibleCoversLeft.Count; i++) {
  //     Gizmos.DrawSphere(visibleCoversLeft[i].position + mod, 0.75f);
  //   }
  //   Gizmos.color = Color.green;
  //   for (int i = 0; i < visibleCoversRight.Count; i++) {
  //     Gizmos.DrawSphere(visibleCoversRight[i].position + mod, 0.75f);
  //   }
  //   Gizmos.color = Color.blue;
  //   for (int i = 0; i < visibleCoversFront.Count; i++) {
  //     Gizmos.DrawSphere(visibleCoversFront[i].position  + mod, 0.75f);
  //   }
  // }

}
