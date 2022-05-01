using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBoomstick : MonoBehaviour {
  public bool isFiring = false;
  public ParticleSystem[] muzzleFlash;
  public ParticleSystem hitEffect;
  public Transform raycastOrigin;
  public Transform raycastHit;
  public TrailRenderer tracer;

  Ray ray;
  RaycastHit hitInfo;

  public void StartFiring() {
    isFiring = true;
    foreach(var particle in muzzleFlash) {
      particle.Emit(1);
    }

    ray.origin = raycastOrigin.position;
    ray.direction = raycastHit.position - raycastOrigin.position;
    
    var tracerEffect = Instantiate(tracer, ray.origin, Quaternion.identity);
    tracer.AddPosition(ray.origin);

    if (Physics.Raycast(ray, out hitInfo)) {
      hitEffect.transform.position = hitInfo.point;
      hitEffect.transform.forward = hitInfo.normal;
      hitEffect.Emit(1);
      tracer.transform.position = hitInfo.point;
    }

  }

  public void StopFiring() {
    isFiring = false;
  }

}
