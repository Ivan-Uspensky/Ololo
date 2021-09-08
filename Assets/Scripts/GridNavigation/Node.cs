using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// The Node.
public class Node : MonoBehaviour {
	[SerializeField]
  protected List<Node> m_Connections = new List<Node>();
  [SerializeField]
  protected Transform currentShithead;
  public enum Nodetype { Connector, LowCover, HighCover };
  [SerializeField]
  public Nodetype type;
    
  List<Vector3> coverPoints;
  public virtual List<Node> connections {
		get {
			return m_Connections;
		}
	}
	public Node this[int index] {
		get {
			return m_Connections[index];
		}
	}
 
  void Start () {
		coverPoints = new List<Vector3>();
		CountChildren(transform);
	}

  void OnValidate () {
	  m_Connections = m_Connections.Distinct().ToList();
  }
	
  void CountChildren(Transform nodeParent) {
    foreach (Transform child in nodeParent) {
      coverPoints.Add(child.position);
    }
  }

  public List<Vector3> getCoverPoints() {
		return coverPoints;
	}

  public void SetActiveShithead(Transform shithead) {
    currentShithead = shithead;
  }
  void OnDrawGizmos() {
    Gizmos.color = Color.green;
    Gizmos.DrawSphere(transform.position, 0.5f);

    // for (int i = 0; i < m_Connections.Count; i++) {
		// 	Vector3 to = new Vector3(m_Connections[i].transform.position.x, m_Connections[i].transform.position.y, m_Connections[i].transform.position.z);
    //   Vector3 from = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
    //   Gizmos.color = Color.cyan;
    //   Gizmos.DrawLine(from, to);
		// }
  }

}
