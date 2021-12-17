using System.Collections.Generic;
using UnityEngine;

public class BrokenWall : MonoBehaviour
{
    [SerializeField] private List<PieceWall> _piecesWall;

    public List<PieceWall> GetAllPieces => _piecesWall;

    public void CheckBigPieces(List<PieceWall> neighbours)
    {
        foreach (var neighbor in neighbours)
        {
            var bigPiece = new List<PieceWall>();
            if (neighbor.CheckNeighbours(ref bigPiece))
            {
                foreach (var piece in bigPiece)
                    piece.BreakPeaceBigPeace();

                return;
            }
        }
    }
}
