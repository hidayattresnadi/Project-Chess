// Console.WriteLine("input player 1 name:");
//         string? name1 = Console.ReadLine();

//         Console.WriteLine("input player 2 name:");
//         string? name2 = Console.ReadLine();

//         IPlayer p1 = new PlayerHuman(name1);
//         IPlayer p2 = new PlayerHuman(name2);

//         Console.WriteLine("please set board input :");
//         string? inputBoardSize = Console.ReadLine();
//         string[] parts = inputBoardSize.Split(' ');
//         int size = int.Parse(parts[0]);
//         int rows = int.Parse(parts[1]);
//         int columns = int.Parse(parts[2]);

//          IBoard boardDefault = new Board(size,rows,columns);
//          GameController gc = new(p1,p2,boardDefault);
//         //  Player Select Colour to initiate game
//           Console.WriteLine($"Do you want to select colour by yourself or random ? if you want random, type :RANDOM");
//         string selectionColourMethod = Console.ReadLine();
//         if(selectionColourMethod == "RANDOM"){
//             gc.AssignPlayerColourRandom(p1,p2);
//         }
//         else {
//              Console.WriteLine($"Please select colour {p1.Name} by input WHITE or BLACK");
//         string inputColourP1 = Console.ReadLine();
//         if (Enum.TryParse(inputColourP1, true, out Colour color))
//         {
//             // Assuming 'gc' is your GameController instance and 'p1' is your Player instance
//             gc.AssignPlayerColourSet(p1, color);
//             Console.WriteLine($"Player color set to: {color}");
//         }
//         else
//         {
//             Console.WriteLine("Invalid color input. Please enter WHITE or BLACK.");
//         }

//         Console.WriteLine($"Please select colour {p2.Name} by input WHITE or BLACK");
//         string inputColourP2 = Console.ReadLine();
//         if (Enum.TryParse(inputColourP2, true, out Colour colorP2))
//         {
//             // Assuming 'gc' is your GameController instance and 'p1' is your Player instance
//             gc.AssignPlayerColourSet(p2, colorP2);
//             Console.WriteLine($"Player color set to: {colorP2}");
//         }
//         else
//         {
//             Console.WriteLine("Invalid color input. Please enter WHITE or BLACK.");
//         }
//         }
//         // set player turn first
//         gc.setPlayerTurn();
//         // set piece to player
//         gc.AssignPlayerPiece(p1,p2);
//         // start game 
//         gc.StartGame();

//         while (gc._gameStatus==GameStatus.IN_PROGRESS)
//         {
//         string? inputBoardPiece = Console.ReadLine();
//         string[] parts = inputBoardSize.Split(' ');
//         int rowPiece = int.Parse(parts[0]);
//         int columnPiece = int.Parse(parts[1]);
//         string? inputMovePiece = Console.ReadLine();
//         string[] parts = inputMovePiece.Split(' ');
//         int rowPieceMove = int.Parse(parts[0]);
//         int columnPieceMove = int.Parse(parts[1]);
//             gc.MovePiece( gc._playerTurn.Peek(), gc.Board[rowPiece, columnPiece], columnPieceMove, rowPieceMove);
//             Console.Clear();
//             for (int col = 0; col < 8; col++)
//             {
//                 for (int row = 0; row < 8; row++)
//                 {
//                     Console.Write("[" + boardDefault[col, row] + " " + "]");
//                 }
//                 Console.WriteLine();
//         }
//         }