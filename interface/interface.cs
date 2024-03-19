public interface IPlayer
{
    public string Name { get; }
}

public interface IBoard
{
    public Piece this[int Row, int col]{get;set;}
    public bool MovePieceToLocation(Piece newPiece, int col, int row, Location currentLocation);
    public Piece[,] AssignPiecesToLocations(Piece piece, Location location);
    public bool RemovePieceFromLocation(int row, int col);
    public Location Location {get;set;}
    public int Size { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }
}