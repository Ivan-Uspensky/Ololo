using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMind : MonoBehaviour {
  public Graph currentGraph;
  ShitHeadActs shitHeadActs;
  public List<Transform> shitHeads = new List<Transform>();
  List<List<GameObject>> allCovers = new List<List<GameObject>>();
  Node start; 
  Node end;
  int allCoversIndex;
  
  void Start() {
    allCoversIndex = 0;
  }

  public void GeneratePaths(List<GameObject> visibleCoversFront, List<GameObject> visibleCoversLeft, List<GameObject> visibleCoversRight) {
    allCovers.Clear();
    allCoversIndex = 0;
    allCovers.Add(visibleCoversLeft);
    allCovers.Add(visibleCoversRight);
    allCovers.Add(visibleCoversFront);
    for (int i = 0; i < shitHeads.Count; i++) {
      // exclude situation when bot has nowhere to go because current list of covers is empty       
      while(allCovers[allCoversIndex].Count == 0) {
        UpdateAllCoversIndex();    
      }
      //
      if (shitHeads[i].gameObject.activeSelf) {
        shitHeadActs = shitHeads[i].gameObject.GetComponent<ShitHeadActs>();
        // if (!shitHeadActs.HasPath) {
        shitHeadActs.SetCoveredPath(CoversGetPath(shitHeads[i].position, allCovers[allCoversIndex][0].transform.position).nodes);
        // }
      }
      UpdateAllCoversIndex();
    }
  }

  void UpdateAllCoversIndex() {
    allCoversIndex = allCoversIndex < 2 ? ++allCoversIndex : 0;
  }

	Path CoversGetPath(Vector3 from, Vector3 to) {
    Path currentPath = new Path();
    end = GetNearestCover(to);
    start = GetNearestCover(from);
		currentPath = currentGraph.GetShortestPath(start, end);
    return currentPath;
	}

  Node GetNearestCover(Vector3 shitHead) {
		float dist = 1000;
		float temp = 0;
		Node nearest = currentGraph.nodes[currentGraph.nodes.Count - 1];
		if (currentGraph.nodes.Count > 1 ) {
      for (int i = 0; i < currentGraph.nodes.Count - 1; i++) {
        // Debug.Log(shitHead + " - " + currentGraph.nodes[i] + " - " + i);
        temp = (shitHead - currentGraph.nodes[i].transform.position).sqrMagnitude;
        if (temp < dist) {
          dist = temp;
          nearest = currentGraph.nodes[i];
        }
      }
    }
		return nearest;
	}
}
