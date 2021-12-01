using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShitHeadActs : MonoBehaviour {
    public Transform dude;
    List<Node> myPath = new List<Node>();
    UnityEngine.AI.NavMeshAgent agent;
    Vector3 destinationTarget = Vector3.zero;
    void Start() {
        
    }

    void Update() {
        if (myPath.Count > 0) {
            destinationTarget = GetCoverPoint(myPath[myPath.Count - 1]);
            // agent.SetDestination(GetCoverPoint(myPath[myPath.Count - 1]));
        }    
    }
    
    public void SetCoveredPath(List<Node> path) {
        myPath = path;
    }
    
    Vector3 GetCoverPoint(Node node) {
        Vector3 farCP = Vector3.zero;
    
        if (node.type == Node.Nodetype.Connector) farCP = node.transform.position; 
        if (node.type == Node.Nodetype.LowCover) {
            float toDudeCurrent = 0;
            float toDudeMax = 0;
            foreach (Transform child in node.transform) {
                toDudeCurrent = (child.position - dude.position).sqrMagnitude;
                if (toDudeCurrent > toDudeMax) {
                    toDudeMax = toDudeCurrent;
                    farCP = child.position;
                }
            }
        }
        if (node.type == Node.Nodetype.HighCover) {
            // Debug.Log("need to think");
            farCP = node.transform.position;
        }
        return farCP;
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(destinationTarget + new Vector3(0,2,0), 0.75f);
        if (myPath.Count > 0) {
            Gizmos.color = Color.magenta;
            Vector3 currentPos = transform.position;
            for (int i = 0; i < myPath.Count; i++ ) {
                Handles.DrawLine(currentPos, myPath[i].transform.position);
                currentPos = myPath[i].transform.position;
            }
        } 
    }
}
