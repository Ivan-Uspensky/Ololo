using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class TopDownCharacterMover : MonoBehaviour {
    private InputHandler moveInput;
    [SerializeField]
    private bool RotateTowardMouse;
    [SerializeField]
    private float MovementSpeed;
    [SerializeField]
    private float RotationSpeed;
    [SerializeField]
    private Camera Camera;

    private void Awake() {
        moveInput = GetComponent<InputHandler>();
    }

    void Update() {
        var targetVector = new Vector3(moveInput.InputVector.x, 0, moveInput.InputVector.y);
        var movementVector = MoveTowardTarget(targetVector);

        if (!RotateTowardMouse) {
            RotateTowardMovementVector(movementVector);
        }
        if (RotateTowardMouse) {
            RotateFromMouseVector();
        }
    }

    private void RotateFromMouseVector() {
        Ray ray = Camera.ScreenPointToRay(moveInput.MousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f)) {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector) {
        var speed = MovementSpeed * Time.deltaTime;
        targetVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void RotateTowardMovementVector(Vector3 movementDirection) {
        if(movementDirection.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed);
    }
}
