using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomstickPickup : MonoBehaviour {
  public RaycastBoomstick weaponPrefab;  

  void OnTriggerEnter(Collider other) {
    ActiveBoomstick activeBoomstick = other.gameObject.GetComponent<ActiveBoomstick>();
    if (activeBoomstick) {
      RaycastBoomstick newWeapon = Instantiate(weaponPrefab);
      activeBoomstick.Equip(newWeapon);
    }
  }  

}
