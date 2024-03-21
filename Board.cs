///<summary>
///Class to save board condition while the game is playing. It shows the list of pieces which are at board
/// </summary>
public class Board : IBoard
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
    public Location Location { get; set; }
    public bool MovePieceToLocation(Piece newPiece, int col, int row, Location currentLocation)
    {
        bool isValidMove = false;
        if (newPiece != null)
        {
            var validMoves = newPiece.SearchValidLocations(currentLocation, this);
            foreach (var item in validMoves)
            {
                if (validMoves.Count != 0)
                {
                    if (item.Column == col && item.Row == row)
                    {
                        isValidMove = true;
                        //   _piecesOnBoard[row, col] = newPiece;
                        newPiece.HasMoved = true;
                    }
                }
            }
        }
        return isValidMove;
    }
    ///<summary>
    ///method with 5 parameters and check if player's move is valid or not based on the position of the piece.
    /// </summary>
    ///<param name="newPiece">
    ///Piece which player wants to move 
    /// </param>
    ///<param name="newLocation">
    ///Location which player wants the piece move to location 
    /// </param>
    ///<param name="currentLocation">
    ///Location of the piece at the board now 
    /// </param>
    /// <returns>
    /// bool by checking the newLocation from player input at parameter col and row
    /// </returns>

    public bool MovePieceToLocation(Piece newPiece, Location newLocation, Location currentLocation)
    {
        bool isValidMove = false;
        if (newPiece != null)
        {
            var validMoves = newPiece.SearchValidLocations(currentLocation, this);
            foreach (var item in validMoves)
            {
                if (item.Column == newLocation.Column && item.Row == newLocation.Row)
                {
                    isValidMove = true;
                    newPiece.HasMoved = true;
                }
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
    public bool RemovePieceFromLocation(int row, int col)
    {
        _piecesOnBoard[row, col] = null;
        return true;
    }
    ///<summary>
    ///method with 2 parameters to remove piece which has been set before.
    ///</summary>
    ///<param name="location">
    ///Location which before piece is put at 
    /// </param>
    /// <returns>
    /// bool if the process success
    /// </returns>

    public bool RemovePieceFromLocation(Location location)
    {
        _piecesOnBoard[location.Row, location.Column] = null;
        return true;
    }
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
    public Location()
    {
        
    }
}