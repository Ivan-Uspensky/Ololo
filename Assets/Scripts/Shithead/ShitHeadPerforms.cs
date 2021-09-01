using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShitHeadPerforms : MonoBehaviour {
  [HideInInspector]
  List<Node> prevPath = new List<Node>();
  List<Node> myPath = new List<Node>();
  List<Node> tempPath = new List<Node>();
  public float distanceToStop;
  float sqrDistanceToStop;
  UnityEngine.AI.NavMeshAgent agent;
  Node prevLastNode;
  Node currentLastNode;
  int nodeIndex;
  public int ID;

  void Start() {
    nodeIndex = 0;
    sqrDistanceToStop = distanceToStop * distanceToStop;
    agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
  }

  void Update() {
    if (myPath.Count > 0) {
      if ((new Vector3(transform.position.x, 0, transform.position.z) - myPath[nodeIndex].transform.position).sqrMagnitude > sqrDistanceToStop) {
        if (nodeIndex < myPath.Count) {
          agent.SetDestination(myPath[nodeIndex].transform.position);
          // Debug.Log("current LastNode: " + myPath[nodeIndex]);
        }
      } else {
        if (nodeIndex < myPath.Count - 1) {
          // currentLastNode = myPath[nodeIndex];
          nodeIndex++;
          Debug.Log("new LastNode: " + myPath[nodeIndex]);
        }
      }
    }
  }

  public void SetCoveredPath(List<Node> path) {
    // 
    string nodes = "1: ";
    for (int i = 0; i < path.Count; i++) {
      nodes += path[i] + " - ";
    }
    nodes += "!";
    Debug.Log(nodes);
    nodes = "2: ";
    for (int i = 0; i < myPath.Count; i++) {
      nodes += myPath[i] + " - ";
    }
    nodes += "!";
    Debug.Log(nodes);
    // 
    if (!CompareLists(path, myPath)) {
      if (myPath.Count == 0) {
        myPath = path;
      }
      Debug.Log("nodeIndex: " + nodeIndex);
      Debug.Log(myPath[nodeIndex] + " - " + path[0]);
      
      Node lastNode = myPath[nodeIndex];
      int lastNodePosition = path.IndexOf(lastNode);
      if (lastNodePosition >= 0) {
        tempPath.Clear();
        for (int i = lastNodePosition; i < path.Count; i++) {
          tempPath.Add(path[i]);
        }
      } else {
        tempPath.Clear();
        tempPath.Insert(0, lastNode);
        for (int i = 0; i < path.Count; i++) {
          // GetCoverPoint
          tempPath.Add(path[i]);
        }
      }
      myPath = tempPath;
      nodeIndex = 0;
    } else {
      Debug.Log(" path remains the same ");
    }
  }

  public bool HasPath() {
    return myPath.Count > 0;
  }

  bool CompareLists<T>(List<T> aListA, List<T> aListB) {
    if (aListA == null || aListB == null || aListA.Count != aListB.Count)
      return false;
    if (aListA.Count == 0)
      return true;
    Dictionary<T, int> lookUp = new Dictionary<T, int>();
    for(int i = 0; i < aListA.Count; i++) {
      int count = 0;
      if (!lookUp.TryGetValue(aListA[i], out count)) {
        lookUp.Add(aListA[i], 1);
        continue;
      }
      lookUp[aListA[i]] = count + 1;
    }
    for (int i = 0; i < aListB.Count; i++) {
      int count = 0;
      if (!lookUp.TryGetValue(aListB[i], out count)) {
        return false;
      }
      count--;
      if (count <= 0) {
        lookUp.Remove(aListB[i]);
      } else {
        lookUp[aListB[i]] = count;
      }
    }
    return lookUp.Count == 0;
  }

  void OnDrawGizmos() {
    // Debug.Log("ID: " + ID + ", nodes: " + myPath.Count);
    if (myPath.Count > 0) {
      Gizmos.color = Color.red;
      Gizmos.DrawSphere(myPath[nodeIndex].transform.position + new Vector3(0,2,0), 0.75f);
      Gizmos.color = Color.magenta;
      Handles.color = Color.magenta;
      Gizmos.DrawSphere(myPath[myPath.Count - 1].transform.position + new Vector3(0,2,0), 0.75f);
      Vector3 currentPos = transform.position;
      for (int i = nodeIndex; i < myPath.Count; i++ ) {
			  Handles.DrawLine(currentPos, myPath[i].transform.position);
			  currentPos = myPath[i].transform.position;
		  }
    }
    
  }
}
