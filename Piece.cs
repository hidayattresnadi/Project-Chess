public abstract class Piece
{
    public int _id {get;set;}
    protected static int _nextId = 1;
    public Colour _pieceColour { get; set; }
    public Type _pieceType { get; set; }
    public bool _hasMoved { get; set; } = false;

    // public int row {get;set;}

    // public int col {get;set;}

    public abstract List<Location> SearchValidLocations(Location current, int boardSize, IBoard board);
}

public class Pawn : Piece
{
    public Pawn(Colour colour, Type type)
    {
        this._pieceColour = colour;
        this._pieceType = type;
        this._id=_nextId++;
    }
    public override List<Location> SearchValidLocations(Location current, int boardSize, IBoard board)
    {
        List<Location> validLocations = new();
        int direction = _pieceColour == Colour.BLACK ? 1 : -1;
        int nextRank = _hasMoved == true ?  (direction * 1) + current.Row : (direction * 2) + current.Row;
        if (nextRank >= 0 && nextRank <= 8)
        {
            if (_hasMoved == false)
            {
                Location location2 = new(nextRank, current.Column);
                Piece pieceNotMoved = board[location2.Row, location2.Column];
                if (pieceNotMoved == null)
                {
                    validLocations.Add(location2);
                }
            }
            if (current.Column + 1 <= 7)
            {
                Piece pieceEaten = board[nextRank, current.Column + 1];
                if (pieceEaten != null && pieceEaten._pieceColour != _pieceColour)
                {
                    Location location3 = new(nextRank, current.Column + 1);
                    validLocations.Add(location3);
                }
            }
            if (current.Column - 1 >= 0)
            {
                Piece pieceEaten2 = board[nextRank, current.Column - 1];
                if (pieceEaten2 != null && pieceEaten2._pieceColour != _pieceColour)
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
}

public class Rook : Piece
{
    public Rook(Colour colour, Type type)
    {
        this._pieceColour = colour;
        this._pieceType = type;
         this._id=_nextId++;
    }
    public override List<Location> SearchValidLocations(Location current, int boardSize, IBoard board)
    {
        // Console.WriteLine("lplplp");
        // Console.WriteLine("benteng1");
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
                if (board[i, current.Column]._pieceColour != _pieceColour)
                {
                    Location location2 = new(i, current.Column);
                    validLocations.Add(location2);
                }
                break;
            }
        }
        for (int i = current.Row + 1; i < 8; i++)
        {
            // gerakan ke bawah
            if (board[i, current.Column] == null)
            {
                Location location = new(i, current.Column);
                validLocations.Add(location);
            }
            else
            {
                if (board[i, current.Column]._pieceColour != _pieceColour)
                {
                    Location location2 = new(i, current.Column);
                    validLocations.Add(location2);
                }

                break;
            }
        }
        for (int i = current.Column + 1; i < 8; i++)
        {
            // gerakan ke kanan
            if (board[current.Row, i] == null)
            {
                Location location = new(current.Row, i);
                validLocations.Add(location);
            }
            else
            {
                if (board[current.Row, i]._pieceColour != _pieceColour)
                {
                    Location location2 = new(current.Row, i);
                    validLocations.Add(location2);
                }

                // if(board[current.Row,i]._pieceType==Type.KING){
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
                if (board[current.Row, i]._pieceColour != _pieceColour)
                {
                    Location location2 = new(current.Row, i);
                    validLocations.Add(location2);
                }

                break;
            }
        }
        // Console.WriteLine("punya benteng");
        // Console.WriteLine("punya benteng");
        // Console.WriteLine(validLocations);
        return validLocations;
    }
}

public class Bishop : Piece
{
    public Bishop(Colour colour, Type type)
    {
        this._pieceColour = colour;
        this._pieceType = type;
        this._id=_nextId++;
    }
    public override List<Location> SearchValidLocations(Location current, int boardSize, IBoard board)
    {
        List<Location> validLocations = new();
        // gerakan ke atas kanan (NorthEast)
        int i = current.Row - 1;
        int j = current.Column + 1;
        while (i >= 0 && j < 8)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else if (board[i, j]._pieceColour != _pieceColour)
            {
                Location location2 = new(i, j);
                validLocations.Add(location2);
                break;
            }
            else
            {
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
            else if (board[i, j]._pieceColour != _pieceColour)
            {
                Location location2 = new(i, j);
                validLocations.Add(location2);
                break;
            }
            else
            {
                break;
            }
            i--;
            j--;
        }
        // gerakan ke bawah kanan south east
        i = current.Row + 1;
        j = current.Column + 1;
        while (i < 8 && j < 8)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else if (board[i, j]._pieceColour != _pieceColour)
            {
                Location location2 = new(i, j);
                validLocations.Add(location2);
                break;
            }
            else
            {
                break;
            }
            i++;
            j++;
        }
        // gerakan ke bawah kiri south west
        i = current.Row + 1;
        j = current.Column - 1;
        while (i < 8 && j >= 0)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else if (board[i, j]._pieceColour != _pieceColour)
            {
                Location location2 = new(i, j);
                validLocations.Add(location2);
                break;
            }
            else
            {
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
        this._pieceColour = colour;
        this._pieceType = type;
        this._id=_nextId++;
    }
    public override List<Location> SearchValidLocations(Location current, int boardSize, IBoard board)
    {
        List<Location> validLocations = new();

        int[,] knightMoves = {
            {2, 1}, {1, 2}, {-1, 2}, {-2, 1},
            {-2, -1}, {-1, -2}, {1, -2}, {2, -1}
        };

        for (int i = 0; i < knightMoves.GetLength(0); i++)
        {
            int newRow = current.Row + knightMoves[i, 0];
            int newCol = current.Column + knightMoves[i, 1];
            if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
            {
                if (board[newRow, newCol] == null)
                {
                    Location location = new(newRow, newCol);
                    validLocations.Add(location);
                }
                else if (board[newRow, newCol]._pieceColour != _pieceColour)
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
        this._pieceColour = colour;
        this._pieceType = type;
        this._id=_nextId++;
    }
    public override List<Location> SearchValidLocations(Location current, int boardSize, IBoard board)
    {
        // Console.WriteLine("wkkwkw");
        List<Location> validLocations = new();
        int[] rowMoves = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] colMoves = { -1, 0, 1, -1, 1, -1, 0, 1 };
        for (int i = 0; i < rowMoves.Length; i++)
        {
            int newRow = current.Row + rowMoves[i];
            int newCol = current.Column + colMoves[i];

            if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
            {
                if (board[newRow, newCol] == null)
                {
                    Location location = new(newRow, newCol);
                    validLocations.Add(location);
                }
                else if (board[newRow, newCol]._pieceColour != _pieceColour)
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
        this._pieceColour = colour;
        this._pieceType = type;
        this._id=_nextId++;
    }
    public override List<Location> SearchValidLocations(Location current, int boardSize, IBoard board)
    {
        // Console.WriteLine("masuk");
        List<Location> validLocations = new();
        // gerakan ke atas kanan (NorthEast)
        int i = current.Row - 1;
        int j = current.Column + 1;
        while (i >= 0 && j < 8)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else if (board[i, j]._pieceColour != _pieceColour)
            {
                Location location2 = new(i, j);
                validLocations.Add(location2);
                break;
            }
            else
            {
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
            else if (board[i, j]._pieceColour != _pieceColour)
            {
                Location location2 = new(i, j);
                validLocations.Add(location2);
                break;
            }
            else
            {
                break;
            }
            i--;
            j--;
        }
        // gerakan ke bawah kanan south east
        i = current.Row + 1;
        j = current.Column + 1;
        while (i < 8 && j < 8)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else if (board[i, j]._pieceColour != _pieceColour)
            {
                Location location2 = new(i, j);
                validLocations.Add(location2);
                break;
            }
            else
            {
                break;
            }
            i++;
            j++;
        }
        // gerakan ke bawah kiri south west
        i = current.Row + 1;
        j = current.Column - 1;
        while (i < 8 && j >= 0)
        {
            if (board[i, j] == null)
            {
                Location location = new(i, j);
                validLocations.Add(location);
            }
            else if (board[i, j]._pieceColour != _pieceColour)
            {
                Location location2 = new(i, j);
                validLocations.Add(location2);
                break;
            }
            else
            {
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
            else if (board[k, current.Column]._pieceColour != _pieceColour)
            {
                Location location2 = new(k, current.Column);
                validLocations.Add(location2);
                break;
            }
            else
            {
                break;
            }
        }
        for (int k = current.Row + 1; k < 8; k++)
        {
            // gerakan ke bawah
            if (board[k, current.Column] == null)
            {
                Location location = new(k, current.Column);
                validLocations.Add(location);
            }
            else if (board[k, current.Column]._pieceColour != _pieceColour)
            {
                Location location2 = new(k, current.Column);
                validLocations.Add(location2);
                break;
            }
            else
            {
                break;
            }
        }
        for (int k = current.Column + 1; k < 8; k++)
        {
            // gerakan ke kanan
            if (board[current.Row, k] == null)
            {
                Location location = new(current.Row, k);
                validLocations.Add(location);
            }
            else if (board[current.Row, k]._pieceColour != _pieceColour)
            {
                Location location2 = new(current.Row, k);
                validLocations.Add(location2);
                if(board[current.Row,k]._pieceType==Type.KING){
                    continue;
                }
                break;
            }
            else
            {
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
            else if (board[current.Row, k]._pieceColour != _pieceColour)
            {
                Location location2 = new(current.Row, k);
                validLocations.Add(location2);
                break;
            }
            else
            {
                break;
            }
        }
        return validLocations;
    }
}