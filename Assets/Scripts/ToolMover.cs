using UnityEngine;

public class ToolMover : MonoBehaviour
{

    public void Move(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}