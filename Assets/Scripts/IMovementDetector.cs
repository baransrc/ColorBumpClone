using UnityEngine;

public interface IMovementDetector
{
    Vector3 GetMovementDelta();
    bool IsTouchRegistered();
}