public class Board :IBoard
{
    // di board bakal ada 64 kotak yang akan diisi piece, termasuk null piece, di bawah merupakan inisialisasi
    private readonly Piece[,] _piecesOnBoard = new Piece[8, 8];
    public int Size { get; set; }
    public int Rows { get; set; }

    public int Columns { get; set; }

    public Board(int size, int rows, int columns)
    {
        this.Size = size;
        this.Rows = rows;
        this.Columns = columns;
    }

    // ini buat dapetin di board itu ada apa aja

    public Piece this[int Row, int col]
    {
        get { return _piecesOnBoard[Row, col]; }
        set { _piecesOnBoard[Row, col] = value; }
    }
    public Location Location {get;set;}
    // {
    //     public int Row { get; }
    //     public int Column { get; }

    //     public Location(int column, int row)
    //     {
    //         Column = column;
    //         Row = row;
    //     }
    // }
    // +Board(int size)
    // +Board(int rows, int columns)
    // +GetBoardInfo() : Piece[,]
    // +AssignPiecesToLocations(Piece[], Location[]) : Piece[,]
    public bool MovePieceToLocation(Piece newPiece, int col, int row, Location currentLocation)
    {
        bool isValidMove = false;
        if(newPiece!=null){
            var validMoves = newPiece.SearchValidLocations(currentLocation, 25, this);
        foreach (var item in validMoves)
        {
            if (validMoves.Count != 0)
            {
                if (item.Column == col && item.Row == row)
                {
                    isValidMove = true;
                    //   _piecesOnBoard[row, col] = newPiece;
                    newPiece._hasMoved = true;
                }
            }

        }
        }
        return isValidMove;
    }

    public bool MovePieceToLocation(Piece newPiece, Location newLocation, Location currentLocation)
    {
        bool isValidMove = false;
        var validMoves = newPiece.SearchValidLocations(currentLocation, 25, this);
        foreach (var item in validMoves)
        {
            if (item.Column == newLocation.Column && item.Row == newLocation.Row)
            {
                isValidMove = true;
                newPiece._hasMoved = true;
            }
        }
        return isValidMove;
    }
    public Piece[,] AssignPiecesToLocations(Piece piece, Location location)
    {
        _piecesOnBoard[location.Row, location.Column] = piece;
        Piece[,] boardInfo = new Piece[location.Row, location.Column];
        return boardInfo;
    }
    // +MovePieceToLocation(Piece newPiece, int row, int column) : bool
    public bool RemovePieceFromLocation(int row, int col)
    {
        _piecesOnBoard[row, col] = null;
        return true;
    }

    public bool RemovePieceFromLocation(Location location)
    {
        _piecesOnBoard[location.Row, location.Column] = null;
        return true;
    }
    // +RemovePieceFromLocation(int row, int column) : bool
}

public class Location
{
    public int Row { get; set; }
    public int Column { get; set; }

    public Location(int Row, int Column)
    {
        this.Row = Row;
        this.Column = Column;
    }
}