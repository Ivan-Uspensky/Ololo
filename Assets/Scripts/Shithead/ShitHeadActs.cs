using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEditor;

public class ShitHeadActs : MonoBehaviour {
    public Transform dude;
    List<Node> myPath = new List<Node>();
    UnityEngine.AI.NavMeshAgent agent;
    Vector3 destinationTarget;
    
    void Start() {
        destinationTarget = transform.position;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    
    void Update() {
        if (myPath.Count > 0) {
            // destinationTarget = GetCoverPoint(myPath[myPath.Count - 1]);
            destinationTarget = GetCoverPoint(myPath[0]);
            agent.SetDestination(destinationTarget);
        } else {
            destinationTarget = transform.position;
        }
        Debug.Log("myPath: " + myPath);
    }
    
    public void SetCoveredPath(List<Node> path) {
        myPath = path;
    }

    public bool HasPath() {
        if (myPath.Count > 0) {
            return true;
        }
        return false;
    }
    
    Vector3 GetCoverPoint(Node node) {
        Vector3 farCP = Vector3.zero;
        if (node.type == Node.Nodetype.Connector) farCP = node.transform.position; 
        if (node.type == Node.Nodetype.LowCover || node.type == Node.Nodetype.HighCover) {
            float toDudeMax = 0;
            foreach (Transform child in node.transform) {
                float toDudeCurrent = 0;
                toDudeCurrent = (child.position - dude.position).sqrMagnitude;
                if (toDudeCurrent > toDudeMax) {
                    toDudeMax = toDudeCurrent;
                    farCP = child.position;
                }
            }
        }
        // if (node.type == Node.Nodetype.HighCover) {
        //     // Debug.Log("need to think");
        //     farCP = node.transform.position;
        // }
        return farCP;
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(destinationTarget + new Vector3(0,2,0), 0.75f);
        if (myPath.Count > 0) {
            Gizmos.color = Color.magenta;
            Vector3 currentPos = transform.position;
            for (int i = 0; i < myPath.Count; i++ ) {
                Gizmos.DrawLine(currentPos, myPath[i].transform.position);
                currentPos = myPath[i].transform.position;
            }
        } 
    }
}
