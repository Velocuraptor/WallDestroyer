using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private ToolMover _toolMover;

    private Vector3 _lastHitPosition;

    private void Start()
    {
        _lastHitPosition = _toolMover.transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            CreateRay();
        }
    }

    private void LateUpdate()
    {
        _toolMover.Move(_lastHitPosition);
    }

    private void CreateRay()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out var hit))
        {
            _lastHitPosition = hit.point;

            var pieceWall = hit.collider.gameObject.GetComponent<PieceWall>();
            if (pieceWall)
                pieceWall.Break();
        }
    }
}
