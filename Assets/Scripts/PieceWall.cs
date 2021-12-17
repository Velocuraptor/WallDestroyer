using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PieceWall : MonoBehaviour
{
    [SerializeField] private List<PieceWall> _neighbours;
    [SerializeField] private bool _isEnd;

    private Rigidbody _rigidBody;
    private bool _isBreak;

    public void RemoveNeighbour(PieceWall neigbor) => _neighbours.Remove(neigbor);

    private void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    public void Break()
    {
        if (_isBreak)
            return;

        _isBreak = true;

        var bigPiece = new List<PieceWall>();

        if (!IsBigPiece(ref bigPiece))
        {
            _rigidBody.isKinematic = false;
            DeleteNeighbours();
        }
        else
        {
            for (int i = 1; i < bigPiece.Count; i++)
            {
                bigPiece[i].gameObject.AddComponent<FixedJoint>().connectedBody = _rigidBody;
            }

            foreach (var piece in bigPiece)
                piece._rigidBody.isKinematic = false;

            foreach (var piece in bigPiece)
                piece.DeleteNeighbours();
        }

        var position = transform.localPosition;
        position.z += 0.4f;
        transform.localPosition = position;
    }

    private void DeleteNeighbours()
    {
        foreach (var neighbour in _neighbours)
            neighbour.RemoveNeighbour(this);
        _neighbours.Clear();
    }

    private bool IsBigPiece(ref List<PieceWall> bigPiece)
    {
        bigPiece.Add(this);

        if (_isEnd)
            return false;

        foreach (var neighbour in _neighbours)
        {
            if (bigPiece.Any(b => b == neighbour))
                continue;
            if (!neighbour.IsBigPiece(ref bigPiece))
                return false;
        }

        return true;
    }

}
