using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PieceWall : MonoBehaviour
{
    [SerializeField] private BrokenWall _brokenWall;
    [SerializeField] private Dust _dust;
    [SerializeField] private int _countEdge;
    [SerializeField] private List<PieceWall> _neighbours;
    [SerializeField] private bool _isEnd;

    private Rigidbody _rigidBody;
    private bool _isBreak;

    private void RemoveNeighbour(PieceWall neigbor) => _neighbours.Remove(neigbor);

    [SerializeField] private List<PieceWall> _NEWneighbours;

    [ContextMenu("ColorNeighbor")]
    private void ColorNeighbor()
    {
        foreach (var item in _NEWneighbours)
        {
            item.GetComponent<MeshRenderer>(). materials[1].color = Color.green;
        }
    }
    private void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();

        _NEWneighbours = _brokenWall.GetAllPieces.Where(p => p != this)
            .OrderBy(p => Vector3.Distance(transform.localPosition, p.transform.localPosition))
            .Take(_countEdge).ToList();
    }

    public void Break()
    {
        if (_isBreak)
            return;

        _isBreak = true;
        _rigidBody.isKinematic = false;
        var position = transform.localPosition;
        position.z += 0.4f;
        transform.localPosition = position;

        Instantiate(_dust, transform.position, Quaternion.identity);

        foreach (var neighbour in _neighbours)
            neighbour.RemoveNeighbour(this);

        _brokenWall.CheckBigPieces(_neighbours);
        _neighbours.Clear();
    }

    public void BreakPeaceBigPeace()
    {
        _isBreak = true;
        _rigidBody.isKinematic = false;
        var position = transform.localPosition;
        position.z += 0.4f;
        transform.localPosition = position;

        Instantiate(_dust, transform.position, Quaternion.identity);

        foreach (var neighbour in _neighbours)
            neighbour.RemoveNeighbour(this);

        _neighbours.Clear();
    }

    public bool CheckNeighbours(ref List<PieceWall> bigPiece)
    {
        if (_isEnd || _isBreak)
            return false;

        bigPiece.Add(this);
        foreach (var neighbor in _neighbours)
        {
            if (bigPiece.Any(p => p == neighbor))
                continue;
            if (!neighbor.CheckNeighbours(ref bigPiece))
                return false;
        }
        return true;
    }
}
