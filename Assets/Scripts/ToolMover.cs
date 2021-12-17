using System.Collections;
using UnityEngine;

public class ToolMover : MonoBehaviour
{
    [SerializeField] private float _speedRotate = 10.0f;
    private Vector3 _newPosition;
    private Vector3 _oldPosition;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        var dir = (_newPosition - _oldPosition).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(dir.x, dir.y, 0), Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _speedRotate);
    }

    public void Move(Vector3 newPosition)
    {
        newPosition.z = -0.05f;
        _newPosition = newPosition;
        _oldPosition = transform.position;
        transform.position = newPosition;
    }
}