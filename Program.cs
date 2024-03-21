using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
// using ChessGame;
// using Players;
class Program
{
    static void Main()
    {
        Console.WriteLine("input player 1 name:");
        string? name1 = Console.ReadLine();

        Console.WriteLine("input player 2 name:");
        string? name2 = Console.ReadLine();

        IPlayer p1 = new PlayerHuman(name1);
        IPlayer p2 = new PlayerHuman(name2);

        Console.WriteLine("please set board input, format is int size int rows int columns :");
        string? inputBoardSize = Console.ReadLine();
        string[] parts = inputBoardSize.Split(' ');
        int size = int.Parse(parts[0]);
        int rows = int.Parse(parts[1]);
        int columns = int.Parse(parts[2]);

        IBoard boardDefault = new Board(size, rows, columns);
        GameController gc = new(p1, p2, boardDefault);
        //  Player Select Colour to initiate game
        Console.WriteLine($"Do you want to select colour by yourself or random ? if you want random, type :RANDOM");
        string selectionColourMethod = Console.ReadLine();
        if (selectionColourMethod == "RANDOM")
        {
            gc.AssignPlayerColourRandom(p1, p2);
        }
        else
        {
            Console.WriteLine($"Please select colour {p1.Name} by input WHITE or BLACK");
            string inputColourP1 = Console.ReadLine();
            if (Enum.TryParse(inputColourP1, true, out Colour color))
            {
                gc.AssignPlayerColourSet(p1, color);
                Console.WriteLine($"Player color set to: {color}");
            }
            else
            {
                Console.WriteLine("Invalid color input. Please enter WHITE or BLACK.");
            }

            Console.WriteLine($"Please select colour {p2.Name} by input WHITE or BLACK");
            string inputColourP2 = Console.ReadLine();
            if (Enum.TryParse(inputColourP2, true, out Colour colorP2))
            {
                gc.AssignPlayerColourSet(p2, colorP2);
                Console.WriteLine($"Player color set to: {colorP2}");
            }
            else
            {
                Console.WriteLine("Invalid color input. Please enter WHITE or BLACK.");
            }
        }

        // set player turn first
        gc.setPlayerTurn();
        // set piece to player
        gc.AssignPlayerPiece(p1, p2);
        // start game 
        gc.StartGame();
        gc.GameStatusUpdate += gc.GameStatusUpdate1;
        gc.GameStatusUpdate.Invoke(gc.GameStatus);
        for (int row = 0; row < gc.Board.Rows; row++)
        {
            for (int col = 0; col < gc.Board.Columns; col++)
            {
                Console.Write("[" + gc.Board[row, col] + " " + "]");
            }
            Console.WriteLine();
        }
        gc.GameStatusUpdate.Invoke(gc.GameStatus);

        while (gc.GameStatus == GameStatus.IN_PROGRESS)
        {
            Console.WriteLine($"{gc.PlayerTurn.Peek().Name}, now is your turn");
            Console.WriteLine("Type the row and column to select the piece");
            string? inputBoardPiece = Console.ReadLine();
            string[] pieceBoard = inputBoardPiece.Split(' ');
            int rowPiece = int.Parse(pieceBoard[0]);
            int columnPiece = int.Parse(pieceBoard[1]);
            Console.WriteLine("Type the row and column to move the piece to selected location");
            string? inputMovePiece = Console.ReadLine();
            string[] pieceMove = inputMovePiece.Split(' ');
            int rowPieceMove = int.Parse(pieceMove[0]);
            int columnPieceMove = int.Parse(pieceMove[1]);
            gc.MovePiece(gc.PlayerTurn.Peek(), gc.Board[rowPiece, columnPiece], columnPieceMove, rowPieceMove);
            Console.Clear();
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Console.Write("[" + gc.Board[row, col] + " " + "]");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Movemet status : {gc.ValidMove}");
            Console.WriteLine($"Game status : {gc.GameStatus}");
            Console.WriteLine($"Checkmate status : {gc.CheckMate}");
            Console.WriteLine($"These are the points for player 1 : {gc.CalculatePlayerMaterial(p1)}");
            Console.WriteLine($"These are the points for player 2 : {gc.CalculatePlayerMaterial(p2)}");
        }
        Console.WriteLine($"The winner is {gc.PlayerTurn.Peek().Name}");
        Console.WriteLine($"These are the points for player 1 : {gc.CalculatePlayerMaterial(p1)}");
        Console.WriteLine($"These are the points for player 2 : {gc.CalculatePlayerMaterial(p2)}");
    }
}