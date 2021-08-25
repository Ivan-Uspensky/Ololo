using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (ObstaclesScanner))]
public class ObstaclesScannerData : Editor  {
	void OnSceneGUI() {
		ObstaclesScanner fow = (ObstaclesScanner)target;
		Handles.color = Color.white;
		Handles.DrawWireArc (fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
		Vector3 viewAngleA = fow.DirFromAngle (-fow.viewAngle / 2, false);
		Vector3 viewAngleB = fow.DirFromAngle (fow.viewAngle / 2, false);
    Vector3 viewAngleС = fow.DirFromAngle (fow.viewAngle / 2 + 90, false);
    Vector3 viewAngleD = fow.DirFromAngle (-fow.viewAngle / 2 - 90, false);

		Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
		Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);
    Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleС * fow.viewRadius);
    Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleD * fow.viewRadius);

		Handles.color = Color.red;
		foreach (GameObject cover in fow.visibleCoversLeft) {
			Handles.DrawLine (fow.transform.position, cover.transform.position);
		}
    Handles.color = Color.green;
		foreach (GameObject cover in fow.visibleCoversRight) {
			Handles.DrawLine (fow.transform.position, cover.transform.position);
		}
    Handles.color = Color.blue;
		foreach (GameObject cover in fow.visibleCoversFront) {
			Handles.DrawLine (fow.transform.position, cover.transform.position);
		}
	}
}
