using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBoomstick : MonoBehaviour {
  public Transform crossHairTarget;
  public UnityEngine.Animations.Rigging.Rig handIk;
  public Transform weaponParent;
  RaycastBoomstick weapon;
  
  void Start() {
    RaycastBoomstick existingWeapon = GetComponentInChildren<RaycastBoomstick>();
    if (existingWeapon) {
      Equip(existingWeapon);
    }
  }

  void Update() {
    if (weapon) {
      if (Input.GetButtonDown("Fire1")) {
        weapon.StartFiring();
      }
      if (weapon.isFiring) {
        weapon.UpdateFiring(Time.deltaTime);
      }
      weapon.UpdateBullets(Time.deltaTime);
      if (Input.GetButtonUp("Fire1")) {
        weapon.StopFiring();
      } 
    } else {
      handIk.weight = 0;
    }
  }

  public void Equip(RaycastBoomstick newWeapon) {
    if (weapon) {
      Destroy(weapon.gameObject);
    }
    weapon = newWeapon;
    weapon.raycastDestination = crossHairTarget;
    weapon.transform.parent = weaponParent;
    weapon.transform.localPosition = Vector3.zero;
    weapon.transform.localRotation = Quaternion.identity;
    handIk.weight = 1;
  }
}
