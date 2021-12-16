using UnityEngine;

public class PieceWall : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private bool _isBreak;

    private void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    public void Break()
    {
        if (_isBreak)
            return;

        _isBreak = true;

        var position = transform.localPosition;
        position.z += 0.5f;
        transform.localPosition = position;

        _rigidBody.isKinematic = false;
    }
}
