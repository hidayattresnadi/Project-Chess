///<summary>
///Class for player to play the game by moving this piece at the board. The winner will be determined based on condition of this class
///Player will play the piece when the game starts
/// </summary>
public abstract class Piece
{
    public int Id {get;set;}
    protected static int _nextId = 1;
    public Colour PieceColour { get; set; }
    public Type PieceType { get; set; }
    public bool HasMoved { get; set; } = false;
    ///<summary>
    ///method with 2 parameter to generate valid moves of a piece.
    /// </summary>
    ///<param name="current">
    ///Location of the piece now
    /// </param>
    ///<param name="board">
    ///Board condition at game
    /// </param>
    /// <returns>
    /// List of Location valid moves from piece
    /// </returns>

    public abstract List<Location> SearchValidLocations(Location current, IBoard board);
     ///<summary> 
    ///method with 2 parameter to generate valid moves of piece to kill enemy king piece when the king enemy is checked.
    /// </summary>
    ///<param name="current">
    ///Location of the pawn piece now
    /// </param>
    ///<param name="board">
    ///Board condition at game
    /// </param>
    /// <returns>
    /// List of Location valid moves from piece to kill enemy king piece when the king enemy is checked
    /// </returns>
    public abstract List<Location> SearchValidLocationsCheck(Location current, IBoard board);
    
}

public class Pawn : Piece
{
    public Pawn(Colour colour, Type type)
    {
        this.PieceColour = colour;
        this.PieceType = type;
        this.Id=_nextId++;
    }
    ///<summary>
    ///method with 2 parameter to generate valid moves of pawn.
    /// </summary>
    ///<param name="current">
    ///Location of the pawn piece now
    /// </param>
    ///<param name="board">
    ///Board condition at game
    /// </param>
    /// <returns>
    /// List of Location valid moves from pawn piece
    /// </returns>
    public override List<Location> SearchValidLocations(Location current, IBoard board)
    {
        List<Location> validLocations = new();
        int direction = PieceColour == Colour.BLACK ? 1 : -1;
        int nextRank = HasMoved == true ?  (direction * 1) + current.Row : (direction * 2) + current.Row;
        if (nextRank >= 0 && nextRank < board.Columns)
        {
            if (HasMoved == false)
            {
                Location location2 = new(nextRank, current.Column);
                Piece pieceNotMoved = board[location2.Row, location2.Column];
                if (pieceNotMoved == null)
                {
                    validLocations.Add(location2);
                }
            }
            if (current.Column + 1 < board.Columns)
            {
                Piece pieceEaten = board[nextRank, current.Column + 1];
                if (pieceEaten != null && pieceEaten.PieceColour != PieceColour)
                {
                    Location location3 = new(nextRank, current.Column + 1);
                    validLocations.Add(location3);
                }
            }
            if (current.Column - 1 >= 0)
            {
                Piece pieceEaten2 = board[nextRank, current.Column - 1];
                if (pieceEaten2 != null && pieceEaten2.PieceColour != PieceColour)
                {
                    Location location4 = new(nextRank, current.Column - 1);
                    validLocations.Add(location4);
                }

            }

            Location location = new(current.Row + direction, current.Column);
            Piece pieceMoved = board[location.Row, location.Column];
            if (pieceMoved == null)
            {
                validLocations.Add(location);
            }
        }
        return validLocations;
    }
    ///<summary> 
    ///method with 2 parameter to generate valid moves of pawn to kill enemy king piece when the king enemy is checked.
    /// </summary>
    ///<param name="current">
    ///Location of the pawn piece now
    /// </param>
    ///<param name="board">
    ///Board condition at game
    /// </param>
    /// <returns>
    /// List of Location valid moves from pawn piece to kill enemy king piece when the king enemy is checked
    /// </returns>
    public override List<Location> SearchValidLocationsCheck(Location current, IBoard board)
    {
        List<Location> validLocations = new();
        int direction = PieceColour == Colour.BLACK ? 1 : -1;
        int nextRank = HasMoved == true ?  (direction * 1) + current.Row : (direction * 2) + current.Row;
        if (nextRank >= 0 && nextRank < board.Columns)
        {
            if (current.Column + 1 < board.Columns)
            {
                Piece pieceEaten = board[nextRank, current.Column + 1];
                if (pieceEaten != null && pieceEaten.PieceColour == PieceColour)
                {
                    Location location3 = new(nextRank, current.Column + 1);
                    validLocations.Add(location3);
                }
                if (pieceEaten == null)
                {
                    Location location3 = new(nextRank, current.Column + 1);
                    validLocations.Add(location3);
                }
            }
            if (current.Column - 1 >= 0)
            {
                Piece pieceEaten2 = board[nextRank, current.Column - 1];
                if (pieceEaten2 != null && pieceEaten2.PieceColour == PieceColour)
                {
                    Location location4 = new(nextRank, current.Column - 1);
                    validLocations.Add(location4);
                }
                if (pieceEaten2 == null)
                {
                    Location location3 = new(nextRank, current.Column - 1);
                    validLocations.Add(location3);
                }
            }
        }
        return validLocations;
    }
}

public class Rook : Piece
{
    public Rook(Colour colour, Type type)
    {
        this.PieceColour = colour;
        this.PieceType = type;
        this.Id=_nextId++;
    }
    ///<summary>
    ///method with 2 parameter to generate valid moves of rook.
    /// </summary>
    ///<param name="current">
    ///Location of the rook piece now
    /// </param>
    ///<param name="board">
    ///Board condition at game
    /// </param>
    /// <returns>
    /// List of Location valid moves from rook piece
    /// </returns>
    public override List<Location> SearchValidLocations(Location current, IBoard board)
    {
        Colour opponentTurnColor = this.PieceColour==Colour.BLACK? Colour.WHITE : Colour.BLACK;
        List<Location> validLocations = new();
        for (int i = current.Row - 1; i >= 0; i--)
        {
            // gerakan ke atas
            if (board[i, current.Column] == null)
            {
                Location location = new(i, current.Column);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, current.Column].PieceColour != PieceColour)
                {
                    Location location2 = new(i, current.Column);
                    validLocations.Add(location2);
                }
                // if (board[i, current.Column].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(current.Row, i);
                //     validLocations.Add(location2);
                // }

                // if(board[i,current.Column].PieceType==Type.KING && checkmate==CheckMate.CHECK){
                //     continue;
                // }
                break;
            }
        }
        for (int i = current.Row + 1; i < board.Rows; i++)
        {
            // gerakan ke bawah
            if (board[i, current.Column] == null)
            {
                Location location = new(i, current.Column);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, current.Column].PieceColour != PieceColour)
                {
                    Location location2 = new(i, current.Column);
                    validLocations.Add(location2);
                }
                // if (board[i, current.Column].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(current.Row, i);
                //     validLocations.Add(location2);
                // }

                // if(board[i,current.Column].PieceType==Type.KING && checkmate==CheckMate.CHECK){
                //     continue;
                // }
                break;
            }
        }
        for (int i = current.Column + 1; i < board.Columns; i++)
        {
            // gerakan ke kanan
            if (board[current.Row, i] == null)
            {
                Location location = new(current.Row, i);
                validLocations.Add(location);
            }
            else
            {
                if (board[current.Row, i].PieceColour != PieceColour)
                {
                    Location location2 = new(current.Row, i);
                    validLocations.Add(location2);
                }
                // if (board[current.Row, i].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(current.Row, i);
                //     validLocations.Add(location2);
                // }

                // if(board[current.Row,i].PieceType==Type.KING && checkmate==CheckMate.CHECK){
                //     continue;
                // }
                break;
            }
        }
        for (int i = current.Column - 1; i >= 0; i--)
        {
            // gerakan ke kiri
            if (board[current.Row, i] == null)
            {
                Location location = new(current.Row, i);
                validLocations.Add(location);
            }
            else
            {
                if (board[current.Row, i].PieceColour != PieceColour)
                {
                    Location location2 = new(current.Row, i);
                    validLocations.Add(location2);
                }
                // if (board[current.Row, i].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(current.Row, i);
                //     validLocations.Add(location2);
                // }

                // if(board[current.Row,i].PieceType==Type.KING && checkmate==CheckMate.CHECK){
                //     continue;
                // }

                break;
            }
        }
        return validLocations;
    }
    ///<summary>
    ///method with 2 parameter to generate valid moves of rook to kill enemy king piece when the king enemy is checked.
    /// </summary>
    ///<param name="current">
    ///Location of the rook piece now
    /// </param>
    ///<param name="board">
    ///Board condition at game
    /// </param>
    /// <returns>
    /// List of Location valid moves from rook piece to kill enemy king piece when the king enemy is checked
    /// </returns>
    public override List<Location> SearchValidLocationsCheck(Location current, IBoard board)
    {
        Colour opponentTurnColor = this.PieceColour==Colour.BLACK? Colour.WHITE : Colour.BLACK;
        List<Location> validLocations = new();
        for (int i = current.Row - 1; i >= 0; i--)
        {
            // gerakan ke atas
            if (board[i, current.Column] == null)
            {
                Location location = new(i, current.Column);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, current.Column].PieceColour != PieceColour)
                {
                    Location location2 = new(i, current.Column);
                    validLocations.Add(location2);
                }
                if (board[i, current.Column].PieceColour == PieceColour)
                {
                    Location location2 = new(current.Row, i);
                    validLocations.Add(location2);
                }

                if(board[i,current.Column].PieceType==Type.KING){
                    continue;
                }
                break;
            }
        }
        for (int i = current.Row + 1; i < board.Rows; i++)
        {
            // gerakan ke bawah
            if (board[i, current.Column] == null)
            {
                Location location = new(i, current.Column);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, current.Column].PieceColour != PieceColour)
                {
                    Location location2 = new(i, current.Column);
                    validLocations.Add(location2);
                }
                if (board[i, current.Column].PieceColour == PieceColour)
                {
                    Location location2 = new(current.Row, i);
                    validLocations.Add(location2);
                }

                if(board[i,current.Column].PieceType==Type.KING){
                    continue;
                }
                break;
            }
        }
        for (int i = current.Column + 1; i < board.Columns; i++)
        {
            // gerakan ke kanan
            if (board[current.Row, i] == null)
            {
                Location location = new(current.Row, i);
                validLocations.Add(location);
            }
            else
            {
                if (board[current.Row, i].PieceColour != PieceColour)
                {
                    Location location2 = new(current.Row, i);
                    validLocations.Add(location2);
                }
                if (board[current.Row, i].PieceColour == PieceColour)
                {
                    Location location2 = new(current.Row, i);
                    validLocations.Add(location2);
                }

                if(board[current.Row,i].PieceType==Type.KING){
                    continue;
                }
                break;
            }
        }
        for (int i = current.Column - 1; i >= 0; i--)
        {
            // gerakan ke kiri
            if (board[current.Row, i] == null)
            {
                Location location = new(current.Row, i);
                validLocations.Add(location);
            }
            else
            {
                if (board[current.Row, i].PieceColour != PieceColour)
                {
                    Location location2 = new(current.Row, i);
                    validLocations.Add(location2);
                }
                if (board[current.Row, i].PieceColour == PieceColour)
                {
                    Location location2 = new(current.Row, i);
                    validLocations.Add(location2);
                }

                if(board[current.Row,i].PieceType==Type.KING){
                    continue;
                }

                break;
            }
        }
        return validLocations;
    }
}

public class Bishop : Piece
{
    public Bishop(Colour colour, Type type)
    {
        this.PieceColour = colour;
        this.PieceType = type;
        this.Id=_nextId++;
    }
    ///<summary>
    ///method with 3 parameter to generate valid moves of bishop.
    /// </summary>
    ///<param name="current">
    ///Location of the bishop piece now
    /// </param>
    ///<param name="board">
    ///Board condition at game
    /// </param>
    /// <returns>
    /// List of Location valid moves from bishop piece
    /// </returns>
    public override List<Location> SearchValidLocations(Location current,  IBoard board)
    {
        Colour opponentTurnColor = this.PieceColour==Colour.BLACK? Colour.WHITE : Colour.BLACK;
        List<Location> validLocations = new();
        // gerakan ke atas kanan (NorthEast)
        int i = current.Row - 1;
        int j = current.Column + 1;
        while (i >= 0 && j < board.Columns)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, j].PieceColour != PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }
                // if (board[i, j].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(i, j);
                //     validLocations.Add(location2);
                // }

                // if(board[i,j].PieceType==Type.KING && checkmate==CheckMate.CHECK){
                //     continue;
                // }

                break;
            }
            i--;
            j++;
        }
        // gerakan ke atas kiri (NorthWest)
        i = current.Row - 1;
        j = current.Column - 1;
        while (i >= 0 && j >= 0)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, j].PieceColour != PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }
                // if (board[i, j].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(i,j);
                //     validLocations.Add(location2);
                // }

                // if(board[i, j].PieceType==Type.KING && checkmate==CheckMate.CHECK){
                //     continue;
                // }

                break;
            }
            i--;
            j--;
        }
        // gerakan ke bawah kanan south east
        i = current.Row + 1;
        j = current.Column + 1;
        while (i < board.Rows && j < board.Columns)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
           else
            {
                if (board[i, j].PieceColour != PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }
                // if (board[i, j].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(i, j);
                //     validLocations.Add(location2);
                // }

                // if(board[i, j].PieceType==Type.KING && checkmate==CheckMate.CHECK){
                //     continue;
                // }

                break;
            }
            i++;
            j++;
        }
        // gerakan ke bawah kiri south west
        i = current.Row + 1;
        j = current.Column - 1;
        while (i < board.Rows && j >= 0)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
           else
            {
                if (board[i, j].PieceColour != PieceColour)
                {
                    Location location2 = new(i,j);
                    validLocations.Add(location2);
                }
                // if (board[i, j].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(i,j);
                //     validLocations.Add(location2);
                // }

                // if(board[i, j].PieceType==Type.KING && checkmate==CheckMate.CHECK){
                //     continue;
                // }

                break;
            }
            i++;
            j--;
        }
        return validLocations;
    }
    ///<summary>
    ///method with 2 parameter to generate valid moves of bishop to kill enemy king piece when the king enemy is checked.
    /// </summary>
    ///<param name="current">
    ///Location of the rook piece now
    /// </param>
    ///<param name="board">
    ///Board condition at game
    /// </param>
    /// <returns>
    /// List of Location valid moves from bishop piece to kill enemy king piece when the king enemy is checked
    /// </returns>
    public override List<Location> SearchValidLocationsCheck(Location current,  IBoard board)
    {
        Colour opponentTurnColor = this.PieceColour==Colour.BLACK? Colour.WHITE : Colour.BLACK;
        List<Location> validLocations = new();
        // gerakan ke atas kanan (NorthEast)
        int i = current.Row - 1;
        int j = current.Column + 1;
        while (i >= 0 && j < board.Columns)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, j].PieceColour != PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }
                if (board[i, j].PieceColour == PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }

                if(board[i,j].PieceType==Type.KING){
                    continue;
                }

                break;
            }
            i--;
            j++;
        }
        // gerakan ke atas kiri (NorthWest)
        i = current.Row - 1;
        j = current.Column - 1;
        while (i >= 0 && j >= 0)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, j].PieceColour != PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }
                if (board[i, j].PieceColour == PieceColour)
                {
                    Location location2 = new(i,j);
                    validLocations.Add(location2);
                }

                if(board[i, j].PieceType==Type.KING){
                    continue;
                }

                break;
            }
            i--;
            j--;
        }
        // gerakan ke bawah kanan south east
        i = current.Row + 1;
        j = current.Column + 1;
        while (i < board.Rows && j < board.Columns)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
           else
            {
                if (board[i, j].PieceColour != PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }
                if (board[i, j].PieceColour == PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }

                if(board[i, j].PieceType==Type.KING){
                    continue;
                }

                break;
            }
            i++;
            j++;
        }
        // gerakan ke bawah kiri south west
        i = current.Row + 1;
        j = current.Column - 1;
        while (i < board.Rows && j >= 0)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
           else
            {
                if (board[i, j].PieceColour != PieceColour)
                {
                    Location location2 = new(i,j);
                    validLocations.Add(location2);
                }
                if (board[i, j].PieceColour == PieceColour)
                {
                    Location location2 = new(i,j);
                    validLocations.Add(location2);
                }

                if(board[i, j].PieceType==Type.KING){
                    continue;
                }

                break;
            }
            i++;
            j--;
        }
        return validLocations;
    }
}

public class Knight : Piece
{
    public Knight(Colour colour, Type type)
    {
        this.PieceColour = colour;
        this.PieceType = type;
        this.Id=_nextId++;
    }
    ///<summary>
    ///method with 3 parameter to generate valid moves of knight.
    /// </summary>
    ///<param name="current">
    ///Location of the pawn knight now
    /// </param>
    ///<param name="checkmate">
    ///Checkmate condition of checkmate status at the game
    /// </param>
    ///<param name="board">
    ///Board condition at game
    /// </param>
    /// <returns>
    /// List of Location valid moves from knight piece
    /// </returns>
    public override List<Location> SearchValidLocations(Location current,  IBoard board)
    {
        Colour opponentTurnColor = this.PieceColour==Colour.BLACK? Colour.WHITE : Colour.BLACK;
        List<Location> validLocations = new();

        int[,] knightMoves = {
            {2, 1}, {1, 2}, {-1, 2}, {-2, 1},
            {-2, -1}, {-1, -2}, {1, -2}, {2, -1}
        };

        for (int i = 0; i < knightMoves.GetLength(0); i++)
        {
            int newRow = current.Row + knightMoves[i, 0];
            int newCol = current.Column + knightMoves[i, 1];
            if (newRow >= 0 && newRow < board.Rows && newCol >= 0 && newCol < board.Columns)
            {
                if (board[newRow, newCol] == null)
                {
                    Location location = new(newRow, newCol);
                    validLocations.Add(location);
                }
                else if (board[newRow, newCol].PieceColour != PieceColour)
                {
                    Location location2 = new(newRow, newCol);
                    validLocations.Add(location2);
                }
                // else if (board[newRow, newCol].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(newRow, newCol);
                //     validLocations.Add(location2);
                // }
            }
        }
        return validLocations;
    }
    ///<summary>
    ///method with 2 parameter to generate valid moves of knight to kill enemy king piece when the king enemy is checked.
    /// </summary>
    ///<param name="current">
    ///Location of the rook piece now
    /// </param>
    ///<param name="board">
    ///Board condition at game
    /// </param>
    /// <returns>
    /// List of Location valid moves from knight piece to kill enemy king piece when the king enemy is checked
    /// </returns>
    public override List<Location> SearchValidLocationsCheck(Location current,  IBoard board)
    {
        Colour opponentTurnColor = this.PieceColour==Colour.BLACK? Colour.WHITE : Colour.BLACK;
        List<Location> validLocations = new();

        int[,] knightMoves = {
            {2, 1}, {1, 2}, {-1, 2}, {-2, 1},
            {-2, -1}, {-1, -2}, {1, -2}, {2, -1}
        };

        for (int i = 0; i < knightMoves.GetLength(0); i++)
        {
            int newRow = current.Row + knightMoves[i, 0];
            int newCol = current.Column + knightMoves[i, 1];
            if (newRow >= 0 && newRow < board.Rows && newCol >= 0 && newCol < board.Columns)
            {
                if (board[newRow, newCol] == null)
                {
                    Location location = new(newRow, newCol);
                    validLocations.Add(location);
                }
                else if (board[newRow, newCol].PieceColour != PieceColour)
                {
                    Location location2 = new(newRow, newCol);
                    validLocations.Add(location2);
                }
                else if (board[newRow, newCol].PieceColour == PieceColour)
                {
                    Location location2 = new(newRow, newCol);
                    validLocations.Add(location2);
                }
            }
        }
        return validLocations;
    }
}

public class King : Piece
{
    public King(Colour colour, Type type)
    {
        this.PieceColour = colour;
        this.PieceType = type;
        this.Id=_nextId++;
    }
    ///<summary>
    ///method with 3 parameter to generate valid moves of king.
    /// </summary>
    ///<param name="current">
    ///Location of the king piece now
    /// </param>
    ///<param name="board">
    ///Board condition at game
    /// </param>
    /// <returns>
    /// List of Location valid moves from king piece
    /// </returns>
    public override List<Location> SearchValidLocations(Location current,  IBoard board)
    {
        Colour opponentTurnColor = this.PieceColour==Colour.BLACK? Colour.WHITE : Colour.BLACK;
        List<Location> validLocations = new();
        int[] rowMoves = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] colMoves = { -1, 0, 1, -1, 1, -1, 0, 1 };
        for (int i = 0; i < rowMoves.Length; i++)
        {
            int newRow = current.Row + rowMoves[i];
            int newCol = current.Column + colMoves[i];

            if (newRow >= 0 && newRow < board.Rows && newCol >= 0 && newCol < board.Columns)
            {
                if (board[newRow, newCol] == null)
                {
                    Location location = new(newRow, newCol);
                    validLocations.Add(location);
                }
                else if (board[newRow, newCol].PieceColour != PieceColour)
                {
                    Location location2 = new(newRow, newCol);
                    validLocations.Add(location2);
                }
                // else if (board[newRow, newCol].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(newRow, newCol);
                //     validLocations.Add(location2);
                // }
            }
        }
        return validLocations;
    }
    ///<summary>
    ///method with 2 parameter to generate valid moves of king to kill enemy king piece when the king enemy is checked.
    /// </summary>
    ///<param name="current">
    ///Location of the rook piece now
    /// </param>
    ///<param name="board">
    ///Board condition at game
    /// </param>
    /// <returns>
    /// List of Location valid moves from king piece to kill enemy king piece when the king enemy is checked
    /// </returns>
    public override List<Location> SearchValidLocationsCheck(Location current,  IBoard board)
    {
        Colour opponentTurnColor = this.PieceColour==Colour.BLACK? Colour.WHITE : Colour.BLACK;
        List<Location> validLocations = new();
        int[] rowMoves = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] colMoves = { -1, 0, 1, -1, 1, -1, 0, 1 };
        for (int i = 0; i < rowMoves.Length; i++)
        {
            int newRow = current.Row + rowMoves[i];
            int newCol = current.Column + colMoves[i];

            if (newRow >= 0 && newRow < board.Rows && newCol >= 0 && newCol < board.Columns)
            {
                if (board[newRow, newCol] == null)
                {
                    Location location = new(newRow, newCol);
                    validLocations.Add(location);
                }
                else if (board[newRow, newCol].PieceColour != PieceColour)
                {
                    Location location2 = new(newRow, newCol);
                    validLocations.Add(location2);
                }
                else if (board[newRow, newCol].PieceColour == PieceColour)
                {
                    Location location2 = new(newRow, newCol);
                    validLocations.Add(location2);
                }
            }
        }
        return validLocations;
    }
}

public class Queen : Piece
{
    public Queen(Colour colour, Type type)
    {
        this.PieceColour = colour;
        this.PieceType = type;
        this.Id=_nextId++;
    }
    ///<summary>
    ///method with 3 parameter to generate valid moves of queen.
    /// </summary>
    ///<param name="current">
    ///Location of the queen piece now
    /// </param>
    ///<param name="board">
    ///Board condition at game
    /// </param>
    /// <returns>
    /// List of Location valid moves from queen piece
    /// </returns>
    public override List<Location> SearchValidLocations(Location current,  IBoard board)
    {
        Colour opponentTurnColor = this.PieceColour==Colour.BLACK? Colour.WHITE : Colour.BLACK;
        // Console.WriteLine("masuk");
        List<Location> validLocations = new();
        // gerakan ke atas kanan (NorthEast)
        int i = current.Row - 1;
        int j = current.Column + 1;
        while (i >= 0 && j < board.Columns)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, j].PieceColour != PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }
                // if (board[i, j].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(i, j);
                //     validLocations.Add(location2);
                // }

                // if(board[i, j].PieceType==Type.KING && checkmate==CheckMate.CHECK){
                //     continue;
                // }

                break;
            }
            i--;
            j++;
        }
        // gerakan ke atas kiri (NorthWest)
        i = current.Row - 1;
        j = current.Column - 1;
        while (i >= 0 && j >= 0)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, j].PieceColour != PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }
                // if (board[i, j].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(i, j);
                //     validLocations.Add(location2);
                // }

                // if(board[i, j].PieceType==Type.KING && checkmate==CheckMate.CHECK){
                //     continue;
                // }

                break;
            }
            i--;
            j--;
        }
        // gerakan ke bawah kanan south east
        i = current.Row + 1;
        j = current.Column + 1;
        while (i < board.Rows && j < board.Columns)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, j].PieceColour != PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }
                // if (board[i, j].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(i, j);
                //     validLocations.Add(location2);
                // }

                // if(board[i, j].PieceType==Type.KING && checkmate==CheckMate.CHECK){
                //     continue;
                // }

                break;
            }
            i++;
            j++;
        }
        // gerakan ke bawah kiri south west
        i = current.Row + 1;
        j = current.Column - 1;
        while (i < board.Rows && j >= 0)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, j].PieceColour != PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }
                // if (board[i, j].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(i, j);
                //     validLocations.Add(location2);
                // }

                // if(board[i, j].PieceType==Type.KING && checkmate==CheckMate.CHECK){
                //     continue;
                // }

                break;
            }
            i++;
            j--;
        }
        for (int k = current.Row - 1; k >= 0; k--)
        {
            // gerakan ke atas
            if (board[k, current.Column] == null)
            {
                Location location = new(k, current.Column);
                validLocations.Add(location);
            }
            else
            {
                if (board[k, current.Column].PieceColour != PieceColour)
                {
                    Location location2 = new(k, current.Column);
                    validLocations.Add(location2);
                }
                // if (board[k, current.Column].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(k, current.Column);
                //     validLocations.Add(location2);
                // }

                // if(board[k, current.Column].PieceType==Type.KING && checkmate==CheckMate.CHECK){
                //     continue;
                // }

                break;
            }
        }
        for (int k = current.Row + 1; k < board.Rows; k++)
        {
            // gerakan ke bawah
            if (board[k, current.Column] == null)
            {
                Location location = new(k, current.Column);
                validLocations.Add(location);
            }
            else
            {
                if (board[k, current.Column].PieceColour != PieceColour)
                {
                    Location location2 = new(k, current.Column);
                    validLocations.Add(location2);
                }
                // if (board[k, current.Column].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(k, current.Column);
                //     validLocations.Add(location2);
                // }

                // if(board[k, current.Column].PieceType==Type.KING && checkmate==CheckMate.CHECK){
                //     continue;
                // }

                break;
            }
        }
        for (int k = current.Column + 1; k < board.Columns; k++)
        {
            // gerakan ke kanan
            if (board[current.Row, k] == null)
            {
                Location location = new(current.Row, k);
                validLocations.Add(location);
            }
            else
            {
                if (board[current.Row, k].PieceColour != PieceColour)
                {
                    Location location2 = new(current.Row, k);
                    validLocations.Add(location2);
                }
                // if (board[current.Row, k].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(current.Row, k);
                //     validLocations.Add(location2);
                // }

                // if(board[current.Row,k].PieceType==Type.KING && checkmate==CheckMate.CHECK){
                //     continue;
                // }

                break;
            }
        }
        for (int k = current.Column - 1; k >= 0; k--)
        {
            // gerakan ke kiri
            if (board[current.Row, k] == null)
            {
                Location location = new(current.Row, k);
                validLocations.Add(location);
            }
            else
            {
                if (board[current.Row, k].PieceColour != PieceColour)
                {
                    Location location2 = new(current.Row, k);
                    validLocations.Add(location2);
                }
                // if (board[current.Row, k].PieceColour == PieceColour && checkmate==CheckMate.CHECK)
                // {
                //     Location location2 = new(current.Row, k);
                //     validLocations.Add(location2);
                // }

                // if(board[current.Row,k].PieceType==Type.KING && checkmate==CheckMate.CHECK){
                //     continue;
                // }

                break;
            }
        }
        return validLocations;
    }
    ///<summary>
    ///method with 2 parameter to generate valid moves of queen to kill enemy king piece when the king enemy is checked.
    /// </summary>
    ///<param name="current">
    ///Location of the rook piece now
    /// </param>
    ///<param name="board">
    ///Board condition at game
    /// </param>
    /// <returns>
    /// List of Location valid moves from queen piece to kill enemy king piece when the king enemy is checked
    /// </returns>
    public override List<Location> SearchValidLocationsCheck(Location current,  IBoard board)
    {
        Colour opponentTurnColor = this.PieceColour==Colour.BLACK? Colour.WHITE : Colour.BLACK;
        // Console.WriteLine("masuk");
        List<Location> validLocations = new();
        // gerakan ke atas kanan (NorthEast)
        int i = current.Row - 1;
        int j = current.Column + 1;
        while (i >= 0 && j < board.Columns)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, j].PieceColour != PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }
                if (board[i, j].PieceColour == PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }

                if(board[i, j].PieceType==Type.KING){
                    continue;
                }

                break;
            }
            i--;
            j++;
        }
        // gerakan ke atas kiri (NorthWest)
        i = current.Row - 1;
        j = current.Column - 1;
        while (i >= 0 && j >= 0)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, j].PieceColour != PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }
                if (board[i, j].PieceColour == PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }

                if(board[i, j].PieceType==Type.KING){
                    continue;
                }

                break;
            }
            i--;
            j--;
        }
        // gerakan ke bawah kanan south east
        i = current.Row + 1;
        j = current.Column + 1;
        while (i < board.Rows && j < board.Columns)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, j].PieceColour != PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }
                if (board[i, j].PieceColour == PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }

                if(board[i, j].PieceType==Type.KING){
                    continue;
                }

                break;
            }
            i++;
            j++;
        }
        // gerakan ke bawah kiri south west
        i = current.Row + 1;
        j = current.Column - 1;
        while (i < board.Rows && j >= 0)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, j].PieceColour != PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }
                if (board[i, j].PieceColour == PieceColour)
                {
                    Location location2 = new(i, j);
                    validLocations.Add(location2);
                }

                if(board[i, j].PieceType==Type.KING){
                    continue;
                }

                break;
            }
            i++;
            j--;
        }
        for (int k = current.Row - 1; k >= 0; k--)
        {
            // gerakan ke atas
            if (board[k, current.Column] == null)
            {
                Location location = new(k, current.Column);
                validLocations.Add(location);
            }
            else
            {
                if (board[k, current.Column].PieceColour != PieceColour)
                {
                    Location location2 = new(k, current.Column);
                    validLocations.Add(location2);
                }
                if (board[k, current.Column].PieceColour == PieceColour)
                {
                    Location location2 = new(k, current.Column);
                    validLocations.Add(location2);
                }

                if(board[k, current.Column].PieceType==Type.KING){
                    continue;
                }

                break;
            }
        }
        for (int k = current.Row + 1; k < board.Rows; k++)
        {
            // gerakan ke bawah
            if (board[k, current.Column] == null)
            {
                Location location = new(k, current.Column);
                validLocations.Add(location);
            }
            else
            {
                if (board[k, current.Column].PieceColour != PieceColour)
                {
                    Location location2 = new(k, current.Column);
                    validLocations.Add(location2);
                }
                if (board[k, current.Column].PieceColour == PieceColour)
                {
                    Location location2 = new(k, current.Column);
                    validLocations.Add(location2);
                }

                if(board[k, current.Column].PieceType==Type.KING){
                    continue;
                }

                break;
            }
        }
        for (int k = current.Column + 1; k < board.Columns; k++)
        {
            // gerakan ke kanan
            if (board[current.Row, k] == null)
            {
                Location location = new(current.Row, k);
                validLocations.Add(location);
            }
            else
            {
                if (board[current.Row, k].PieceColour != PieceColour)
                {
                    Location location2 = new(current.Row, k);
                    validLocations.Add(location2);
                }
                if (board[current.Row, k].PieceColour == PieceColour)
                {
                    Location location2 = new(current.Row, k);
                    validLocations.Add(location2);
                }

                if(board[current.Row,k].PieceType==Type.KING){
                    continue;
                }

                break;
            }
        }
        for (int k = current.Column - 1; k >= 0; k--)
        {
            // gerakan ke kiri
            if (board[current.Row, k] == null)
            {
                Location location = new(current.Row, k);
                validLocations.Add(location);
            }
            else
            {
                if (board[current.Row, k].PieceColour != PieceColour)
                {
                    Location location2 = new(current.Row, k);
                    validLocations.Add(location2);
                }
                if (board[current.Row, k].PieceColour == PieceColour)
                {
                    Location location2 = new(current.Row, k);
                    validLocations.Add(location2);
                }

                if(board[current.Row,k].PieceType==Type.KING){
                    continue;
                }

                break;
            }
        }
        return validLocations;
    }
}