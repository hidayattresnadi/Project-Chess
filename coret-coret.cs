// // using Microsoft.Extensions.Logging;
// // using NLog.Extensions.Logging;
// // class Program
// // {
// //     static void Main()
// //     {
// //         Console.WriteLine("input player 1 name:");
// //         string? name1 = Console.ReadLine();

// //         Console.WriteLine("input player 2 name:");
// //         string? name2 = Console.ReadLine();

// //         IPlayer p1 = new PlayerHuman(name1);
// //         IPlayer p2 = new PlayerHuman(name2);

// //         Console.WriteLine("please set board input :");
// //         string? inputBoardSize = Console.ReadLine();
// //         string[] parts = inputBoardSize.Split(' ');
// //         int size = int.Parse(parts[0]);
// //         int rows = int.Parse(parts[1]);
// //         int columns = int.Parse(parts[2]);

// //         IBoard boardDefault = new Board(size, rows, columns);
// //         GameController gc = new(p1, p2, boardDefault);
// //         //  Player Select Colour to initiate game
// //         Console.WriteLine($"Do you want to select colour by yourself or random ? if you want random, type :RANDOM");
// //         string selectionColourMethod = Console.ReadLine();
// //         if (selectionColourMethod == "RANDOM")
// //         {
// //             gc.AssignPlayerColourRandom(p1, p2);
// //         }
// //         else
// //         {
// //             Console.WriteLine($"Please select colour {p1.Name} by input WHITE or BLACK");
// //             string inputColourP1 = Console.ReadLine();
// //             if (Enum.TryParse(inputColourP1, true, out Colour color))
// //             {
// //                 gc.AssignPlayerColourSet(p1, color);
// //                 Console.WriteLine($"Player color set to: {color}");
// //             }
// //             else
// //             {
// //                 Console.WriteLine("Invalid color input. Please enter WHITE or BLACK.");
// //             }

// //             Console.WriteLine($"Please select colour {p2.Name} by input WHITE or BLACK");
// //             string inputColourP2 = Console.ReadLine();
// //             if (Enum.TryParse(inputColourP2, true, out Colour colorP2))
// //             {
// //                 gc.AssignPlayerColourSet(p2, colorP2);
// //                 Console.WriteLine($"Player color set to: {colorP2}");
// //             }
// //             else
// //             {
// //                 Console.WriteLine("Invalid color input. Please enter WHITE or BLACK.");
// //             }
// //         }

// //         // set player turn first
// //         gc.setPlayerTurn();
// //         // set piece to player
// //         gc.AssignPlayerPiece(p1, p2);
// //         // start game 
// //         gc.StartGame();
// //         for (int row = 0; row < 8; row++)
// //         {
// //             for (int col = 0; col < 8; col++)
// //             {
// //                 Console.Write("[" + gc.Board[row, col] + " " + "]");
// //             }
// //             Console.WriteLine();
// //         }

// //         while (gc.GameStatus == GameStatus.IN_PROGRESS)
// //         {
// //             Console.WriteLine($"{gc.PlayerTurn.Peek().Name}, now is your turn");
// //             Console.WriteLine("Type the row and column to select the piece");
// //             string? inputBoardPiece = Console.ReadLine();
// //             string[] pieceBoard = inputBoardPiece.Split(' ');
// //             int rowPiece = int.Parse(pieceBoard[0]);
// //             int columnPiece = int.Parse(pieceBoard[1]);
// //             Console.WriteLine("Type the row and column to move the piece to selected location");
// //             string? inputMovePiece = Console.ReadLine();
// //             string[] pieceMove = inputMovePiece.Split(' ');
// //             int rowPieceMove = int.Parse(pieceMove[0]);
// //             int columnPieceMove = int.Parse(pieceMove[1]);
// //             gc.MovePiece(gc.PlayerTurn.Peek(), gc.Board[rowPiece, columnPiece], columnPieceMove, rowPieceMove);
// //             Console.Clear();
// //             for (int row = 0; row < 8; row++)
// //             {
// //                 for (int col = 0; col < 8; col++)
// //                 {
// //                     Console.Write("[" + gc.Board[row, col] + " " + "]");
// //                 }
// //                 Console.WriteLine();
// //             }
// //             Console.WriteLine($"Movemet status : {gc.ValidMove}");
// //         }
// //         Console.WriteLine($"The winner is dayat");
// //         Console.WriteLine("These are the points for each player : 25");
// //         gc.EndGame();
// //     }
// // }






// // using Microsoft.Extensions.Logging;
// // using NLog.Extensions.Logging;
// // class Program
// // {
// //     static void Main()
// //     {
// //         IPlayer p1 = new PlayerHuman("dayat");
// //         IPlayer p2 = new PlayerHuman("Irham");

// //         IBoard boardChess = new Board(64, 8, 8);

// //         GameController gc = new GameController(p1, p2, boardChess);

// //         gc.AssignPlayerColourSet(p1, Colour.BLACK);
// //         gc.AssignPlayerColourSet(p2, Colour.WHITE);
// //         gc.AssignPlayerPiece(p1, p2);
// //         gc.setPlayerTurn();

// //         // skakmat semisal benteng kolom 7 baris 0 sama kolom 0 baris 1
// //         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[7,0],0,0);
// //         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,4],4,1);
// //         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,0],0,1);
// //         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[1,4],4,0);
// //         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[7,7],7,0);

// //         // menggunakan ratu biar skak tapi deket raja
// //         gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[7,3],3,2);
// //         gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,4],5,0);
// //         gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[2,3],3,1);
// //         gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,5],6,0);
// //         gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[1,3],2,1);
// //         gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,6],5,0);
// //         gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[7,0],0,1);
// //         gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,5],6,0);
// //         gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[1,2],6,1);
// //          gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[7,7],7,1);
// //            gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,5],4,0);

// //         // gc.GetPieceValidMoves(gc.Board[7]);
// //         // gc.CheckForCheckmate();
// //         Console.WriteLine(gc.CheckMate);


// //         for (int row = 0; row < 8; row++)
// //         {
// //             for (int col = 0; col < 8; col++)
// //             {
// //                 Console.Write("[" + gc.Board[row, col] + " " + "]");
// //             }
// //             Console.WriteLine();
// //         }
// //     }

// // }





// // using Microsoft.Extensions.Logging;
// // using NLog.Extensions.Logging;
// // using Players;
// // using ChessGame;
// class Program
// {
//     static void Main()
//     {
//         IPlayer p1 = new PlayerHuman("dayat");
//         IPlayer p2 = new PlayerHuman("Irham");

//         IBoard boardChess = new Board(64, 8, 8);

//         GameController gc = new GameController(p1, p2, boardChess);

//         gc.AssignPlayerColourSet(p1, Colour.BLACK);
//         gc.AssignPlayerColourSet(p2, Colour.WHITE);
//         gc.AssignPlayerPiece(p1, p2);
//         gc.setPlayerTurn();

//         // skakmat semisal benteng kolom 7 baris 0 sama kolom 0 baris 1
//         gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[7,0],0,0);
//         gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,4],4,1);
//         gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,0],0,1);
//         gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[1,4],4,0);
//         gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[7,7],7,0);

//         // menggunakan ratu biar skak tapi deket raja
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[7,3],3,2);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,4],5,0);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[2,3],3,1);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,5],6,0);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[1,3],2,1);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,6],5,0);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[7,0],0,1);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,5],6,0);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[1,2],6,1);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[7,7],7,1);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,5],4,0);
//         // Console.WriteLine(gc.CalculatePlayerMaterial(p1));
//         //  Console.WriteLine(gc.CalculatePlayerMaterial(p2));
//         //  var dayat = gc.GetPieceValidMoves(gc.Board[7,4]);
//         //  foreach (var item in dayat)
//         //  {
//         //      Console.WriteLine(item.Column);
//         //      Console.WriteLine(item.Row);
//         //      Console.WriteLine("wkwkw");
//         //  }
//         //  Console.WriteLine(gc.GetPieceValidMoves(gc.Board[7,4]));

//         // semisal raja berada di kanan dan diskakmat dari benteng sebelah kiri
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[7,3],3,2);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,4],5,0);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[2,3],3,1);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,5],6,0);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[1,3],2,1);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,6],5,0);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[7,0],0,1);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[0,5],6,0);
//         // gc.MovePiece(gc.PlayerTurn.Peek(),gc.Board[1,0],0,0);

//         // gc.GetPieceValidMoves(gc.Board[7]);
//         // gc.CheckForCheckmate();
//         // Console.WriteLine(gc.CheckMate);


//         for (int row = 0; row < 8; row++)
//         {
//             for (int col = 0; col < 8; col++)
//             {
//                 Console.Write("[" + gc.Board[row, col] + " " + "]");
//             }
//             Console.WriteLine();
//         }
//     }

// }