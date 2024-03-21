public interface IBoard
{
    public Piece this[int Row, int col]{get;set;}
    ///<summary>
    ///method with 5 parameters and check if player's move is valid or not based on the position of the piece.
    /// </summary>
    ///<param name="newPiece">
    ///Piece which player wants to move 
    /// </param>
    ///<param name="col">
    ///int column which player wants the piece move to that column 
    /// </param>
    ///<param name="row">
    ///int row which player wants the piece move to that row 
    /// </param>
    ///<param name="currentLocation">
    ///Location of the piece at the board now 
    /// </param>
    /// <returns>
    /// bool by checking the newLocation from player input at parameter col and row
    /// </returns>
    public bool MovePieceToLocation(Piece newPiece, int col, int row, Location currentLocation);
    ///<summary>
    ///method with 2 parameters to set board coordinate with piece from the same board.
    /// </summary>
    ///<param name="piece">
    ///Piece which player wants to move 
    /// </param>
    ///<param name="location">
    ///Location which player wants the piece move to location 
    /// </param>
    /// <returns>
    /// Piece with new location at the board
    /// </returns>
    public Piece[,] AssignPiecesToLocations(Piece piece, Location location);
     ///<summary>
    ///method with 2 parameters to remove piece which has been set before.
    ///</summary>
    ///<param name="row">
    ///int row which before piece is put at 
    /// </param>
    ///<param name="column">
    ///int column which before piece is put at  
    /// </param>
    /// <returns>
    /// bool when the process success
    /// </returns>
    public bool RemovePieceFromLocation(int row, int col);
    public Location Location {get;set;}
    public int Size { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }
}