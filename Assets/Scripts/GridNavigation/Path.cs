using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// The Path.
public class Path {
	/// The nodes.
	protected List<Node> m_Nodes = new List<Node> ();
	/// The length of the path.
	protected float m_Length = 0f;
	/// Gets the nodes.
	public virtual List<Node> nodes {
		get {
			return m_Nodes;
		}
	}
	/// Gets the length of the path.
	public virtual float length {
		get {
			return m_Length;
		}
	}
	/// Bake the path.
	/// Making the path ready for usage, Such as caculating the length.
	public virtual void Bake() {
		List<Node> calculated = new List<Node>();
		m_Length = 0f;
		for (int i = 0; i < m_Nodes.Count; i++) {
			Node node = m_Nodes[i];
			for (int j = 0; j < node.connections.Count; j++) {
				Node connection = node.connections[j];
				// Don't calcualte calculated nodes
				if (m_Nodes.Contains(connection) && !calculated.Contains(connection)){
					// Calculating the distance between a node and connection when they are both available in path nodes list
					m_Length += Vector3.Distance(node.transform.position, connection.transform.position);
				}
			}
			calculated.Add(node);
		}
	}
	/// Returns a string that represents the current object.
	public override string ToString () {
		return string.Format (
			"Nodes: {0}\nLength: {1}",
			string.Join (
				", ",
				nodes.Select(node => node.name).ToArray()),
			length);
	}
}
