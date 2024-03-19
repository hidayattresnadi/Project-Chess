// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main()
    {
        // Console.WriteLine("input player 1 name:");
        // string? name1 = Console.ReadLine();

        // Console.WriteLine("input player 2 name:");
        // string? name2 = Console.ReadLine();

        // IPlayer p1 = new PlayerHuman(name1);
        // IPlayer p2 = new PlayerHuman(name2);

        // Console.WriteLine("please set board input :");
        // string? inputBoardSize = Console.ReadLine();
        // string[] parts = inputBoardSize.Split(' ');
        // int size = int.Parse(parts[0]);
        // int rows = int.Parse(parts[1]);
        // int columns = int.Parse(parts[2]);
        
        // Board boardDefault = new(size,rows,columns);

        IPlayer p1 = new PlayerHuman("dayat");
        IPlayer p2 = new PlayerHuman("irham");
        
        IBoard boardDefault = new Board(64,8,8);


        
        // boardDefault[0, 0] = new Rook(Colour.BLACK, Type.ROOK);
        // boardDefault[0, 1] = new Knight(Colour.BLACK, Type.KNIGHT);
        // boardDefault[0, 2] = new Bishop(Colour.BLACK, Type.BISHOP);
        // boardDefault[0, 3] = new Queen(Colour.BLACK, Type.QUEEN);
        // boardDefault[0, 4] = new King(Colour.BLACK, Type.KING);
        // boardDefault[0, 5] = new Bishop(Colour.BLACK, Type.BISHOP);
        // boardDefault[0, 6] = new Knight(Colour.BLACK, Type.KNIGHT);
        // boardDefault[0, 7] = new Rook(Colour.BLACK, Type.ROOK);

        // boardDefault[7, 0] = new Rook(Colour.WHITE, Type.ROOK);
        // boardDefault[7, 1] = new Knight(Colour.WHITE, Type.KNIGHT);
        // boardDefault[7, 2] = new Bishop(Colour.WHITE, Type.BISHOP);
        // boardDefault[7, 3] = new Queen(Colour.WHITE, Type.QUEEN);
        // boardDefault[7, 4] = new King(Colour.WHITE, Type.KING);
        // boardDefault[7, 5] = new Bishop(Colour.WHITE, Type.BISHOP);
        // boardDefault[7, 6] = new Knight(Colour.WHITE, Type.KNIGHT);
        // boardDefault[7, 7] = new Rook(Colour.WHITE, Type.ROOK);

        // for (int i = 0; i < 8; i++)
        // {
        //     boardDefault[1, i] = new Pawn(Colour.BLACK, Type.PAWN);
        //     boardDefault[6, i] = new Pawn(Colour.WHITE, Type.PAWN);
        // }


        GameController gc = new(p1,p2,boardDefault);
        // Console.WriteLine($"Do you want to select colour by yourself or random ? if you want random, type :RANDOM");
        // string selectionColourMethod = Console.ReadLine();
        // if(selectionColourMethod == "RANDOM"){
        //     gc.AssignPlayerColourRandom(p1,p2);
        // }
        // else {
        //      Console.WriteLine($"Please select colour {p1.Name} by input WHITE or BLACK");
        // string inputColourP1 = Console.ReadLine();
        // if (Enum.TryParse(inputColourP1, true, out Colour color))
        // {
        //     // Assuming 'gc' is your GameController instance and 'p1' is your Player instance
        //     gc.AssignPlayerColourSet(p1, color);
        //     Console.WriteLine($"Player color set to: {color}");
        // }
        // else
        // {
        //     Console.WriteLine("Invalid color input. Please enter WHITE or BLACK.");
        // }

        // Console.WriteLine($"Please select colour {p2.Name} by input WHITE or BLACK");
        // string inputColourP2 = Console.ReadLine();
        // if (Enum.TryParse(inputColourP2, true, out Colour colorP2))
        // {
        //     // Assuming 'gc' is your GameController instance and 'p1' is your Player instance
        //     gc.AssignPlayerColourSet(p2, colorP2);
        //     Console.WriteLine($"Player color set to: {colorP2}");
        // }
        // else
        // {
        //     Console.WriteLine("Invalid color input. Please enter WHITE or BLACK.");
        // }
        // }
        
       

        gc.AssignPlayerColourSet(p1,Colour.BLACK);
        gc.AssignPlayerColourSet(p2,Colour.WHITE);
        gc.AssignPlayerPiece(p1,p2);
        gc.setPlayerTurn();
        gc.StartGame();
        // GameController gc = new(p1,p2,boardDefault);
        // gc.AssignPlayerPiece(p1,p2);
        // gc.NextTurn(p2);
        // gc.PawnPromotionPlayer+=gc.PawnPromotion;
        // gc.KingCastlingPlayer+=gc.KingCastlingPlayer;
        // Piece newPiece = new Queen(Colour.BLACK,Type.QUEEN);
        // gc.PawnPromotionPlayer.Invoke(boardDefault[0,7],newPiece);
        // gc.KingCastlingPlayer.Invoke(boardDefault[0,7],boardDefault[0,4],true);
        // for (int row = 0; row < 8; row++)
        // {
        //     for (int col = 0; col < 8; col++)
        //     {
        //         Console.Write("[" + boardDefault[row, col] + " " + "]");
        //     }
        //     Console.WriteLine();
        // }
        Location location = new(0,0);
        // gc.board.RemovePieceFromLocation(1,0);
        // gc.board.RemovePieceFromLocation(1,1);
        // // gc.board.RemovePieceFromLocation(1,7);
        // gc.board.RemovePieceFromLocation(1,2);
        // gc.board.RemovePieceFromLocation(1,3);
        // gc.board.RemovePieceFromLocation(1,4);
        // gc.board.RemovePieceFromLocation(1,5);
        // gc.board.RemovePieceFromLocation(1,6);
        // gc.board.RemovePieceFromLocation(0,7);
        // gc.board.RemovePieceFromLocation(0,6);
        // gc.board.RemovePieceFromLocation(0,5);
        // gc.board.RemovePieceFromLocation(0,3);
        // gc.board.RemovePieceFromLocation(0,2);
        // gc.board.RemovePieceFromLocation(0,1);
        // gc.board.RemovePieceFromLocation(0,0);


        // gc.board.RemovePieceFromLocation(6,1);
        // gc.board.RemovePieceFromLocation(6,2);
        // gc.board.RemovePieceFromLocation(6,3);
        // gc.board.RemovePieceFromLocation(6,4);
        // gc.board.RemovePieceFromLocation(6,5);
        // gc.board.RemovePieceFromLocation(6,0);
        // gc.board.RemovePieceFromLocation(6,6);
        // gc.board.RemovePieceFromLocation(6,7);

       
        // gc.board.RemovePieceFromLocation(7,1);
        // gc.board.RemovePieceFromLocation(7,2);
      
        // gc.board.RemovePieceFromLocation(7,4);
        // gc.board.RemovePieceFromLocation(7,5);
        // gc.board.RemovePieceFromLocation(7,6);
        
        // location.Column=2;
        // location.Row=1;
        // Location currentLocation = new(0,1);
        // Location current23 = new(3,3);
        
        // gc.MovePiece(p1,gc.board[1, 1], 1,2,currentLocation);
        gc.MovePiece(p2,gc.Board[6,3],3,4);
        gc.MovePiece(p1,gc.Board[1,2],2,3);
        gc.MovePiece(p2,gc.Board[4,3],2,3);
        gc.MovePiece(p1,gc.Board[1,3],3,2);
        gc.MovePiece(p2,gc.Board[3,2],3,2);
        gc.MovePiece(p1,gc.Board[1,0],0,2);
        gc.MovePiece(p2,gc.Board[2,3],3,1);
        Console.WriteLine(gc.MovePiece(p2,gc.Board[2,3],3,1));
        // gc.MovePiece(p1,gc.board[2,0],0,3,new Location(2,0));
        // Console.WriteLine(gc.CalculatePlayerMaterial(p1));
        // Console.WriteLine(gc.CalculatePlayerMaterial(p2));
        // var lala = gc.GetPieceValidMoves(gc.board[0,0]);
        // foreach (var item in lala)
        // {
        //     Console.WriteLine(item.Column);
        //     Console.WriteLine(item.Row);
        // }
        //  Console.WriteLine(gc.GetLocationOfPiece(gc.board[3,0],p1).Column);
        // Console.WriteLine("akwoakwoka xxxxxxx");
        // Console.WriteLine(gc.GetLocationOfPiece(gc.board[3,0],p1).Row);
        // gc.MovePiece(p2,gc.board[1,3],4,0,new Location(1,3));
        // Console.WriteLine(gc.board[1,3]._hasMoved);
        
        // gc.MovePiece(p1,gc.board[0,3],3,2,new Location(0,3));
        //  gc.MovePiece(p1,gc.board[3,2],3,2,new Location(3,2));
        // gc.MovePiece(p1,gc.board[7,3],4,6,current23);
        // // gc.NextTurn(p2);
        // gc.MovePiece(p2,gc.board[0,4],5,0,new Location(0,4));
        // // gc.NextTurn(p1);
        // gc.MovePiece(p1,gc.board[6,4],5,6,new Location(6,4));
        // // gc.NextTurn(p2);
        // gc.MovePiece(p2,gc.board[0,5],4,0,new Location(0,5));
        // gc.MovePiece(p1,gc.board[7,7],7,0,new Location(7,7));
        // gc.MovePiece(p2,gc.board[0,4],3,1,new Location(0,4));
        // gc.MovePiece(p1,gc.board[6,5],0,1,new Location(6,5));
        // gc.MovePiece(p2,gc.board[1,3],3,2,new Location(1,3));
        // gc.MovePiece(p1,gc.board[0,7],7,3,new Location(0,7));
        // gc.MovePiece(p2,gc.board[2,3],4,2,new Location(2,3));
        // gc.MovePiece(p1,gc.board[1,0],0,2,new Location(1,0));
        // gc.MovePiece(p2,gc.board[2,4],4,1,new Location(2,4));
        // gc.MovePiece(p1,gc.board[3,7],7,1,new Location(3,7));
        // gc.MovePiece(p2,gc.board[1,4],4,0,new Location(1,4));
        // // gc.KingCastling(gc.board[7,4],gc.board[7,0],false);
        // // // gc.NextTurn(p2);
        // gc.MovePiece(p1,gc.board[2,0],0,0,new Location(2,0));
        // gc.MovePiece(p2,gc.board[0,4],7,0,new Location(0,4));
        // gc.MovePiece(p2,gc.board[0,2],3,0,new Location(0,2));


        // Console.WriteLine(gc.CalculatePlayerMaterial(p2));
        // Console.WriteLine(gc.CalculatePlayerMaterial(p1));
        // Console.WriteLine(gc._checkMate);

        // gc.MovePiece(p2,boardDefault[0,4],5,0,new Location(0,4));
        // gc.MovePiece(p2,boardDefault[0,4],5,0,new Location(0,4));

        // gc.MovePiece(p1,boardDefault[7, 0], 0,0,new Location(7,0));
        // gc.MovePiece(p1,boardDefault[7, 7], 7,1,new Location(7,7));
        // boardDefault.RemovePieceFromLocation(7,7);
        // boardDefault.RemovePieceFromLocation(7,3);
        // boardDefault.RemovePieceFromLocation(7,0);
        // boardDefault.MovePieceToLocation(boardDefault[6, 0], 0,4);
        // boardDefault.MovePieceToLocation(boardDefault[1,2],1,3);
        // boardDefault.MovePieceToLocation(boardDefault[2, 1], 2,3);
        // boardDefault.RemovePieceFromLocation(1,1);
        // boardDefault.RemovePieceFromLocation(7,1);
        
        // boardDefault.RemovePieceFromLocation(1,2);
        // boardDefault.RemovePieceFromLocation(0,6);
        // boardDefault.MovePieceToLocation(boardDefault[6, 1], 1,4);
        // boardDefault.MovePieceToLocation(boardDefault[7, 0], 0,5);
        // boardDefault.MovePieceToLocation(boardDefault[5,0],4,5);
        // boardDefault.MovePieceToLocation(boardDefault[6,6],4,6);
        // boardDefault.MovePieceToLocation(boardDefault[6,4],4,4);
        // boardDefault.MovePieceToLocation(boardDefault[7,5],2,4);
        // boardDefault.MovePieceToLocation(boardDefault[6, 2], 2,4);
        // boardDefault.RemovePieceFromLocation(6,6);
        // boardDefault.RemovePieceFromLocation(5,7);
        // boardDefault.RemovePieceFromLocation(4,6);
        // boardDefault.RemovePieceFromLocation(0,5);
        // boardDefault.RemovePieceFromLocation(0,7);
        // boardDefault.RemovePieceFromLocation(5,7);
        // boardDefault.RemovePieceFromLocation(1,6);
        // boardDefault.RemovePieceFromLocation(2,6);
        // Console.WriteLine("aneh");
        // Console.WriteLine(boardDefault[3,1]);
        // Console.WriteLine(boardDefault[1,1]._hasMoved);
        // Console.WriteLine(boardDefault[1,1]+"wkwkwk");
        // Console.WriteLine("=======");
        // Console.WriteLine(boardDefault[7,5]._pieceColour+$"{boardDefault[7,2]._pieceType}");
        // sebelah kiri row, sebelah kanan column
        // var validMovesLists = boardDefault[3,2].SearchValidLocations(location,24,boardDefault);
        // var validMoveListsRook = boardDefault[1,7].SearchValidLocations(new Location(1,7),24,boardDefault);
        // var validMoveListsBishop = boardDefault[4,2].SearchValidLocations(location,24,boardDefault);
        // Console.WriteLine(boardDefault[7,1]);
        // var validMoveListsQueen = boardDefault[0,0].SearchValidLocations(new Location(0,0),24,boardDefault);
        // var validMoveListsKing = boardDefault[0,0].SearchValidLocations(location,24,boardDefault);
        // var validMoveListsKing4 = boardDefault[1,7].SearchValidLocations(new Location(1,7),24,boardDefault);
        //  var validMoveListsKing2 = boardDefault[0,4].SearchValidLocations(new Location(0,4),24,boardDefault);
        //  foreach (var item in dayat)
        //  {
        //     foreach(var zzz in item.Value){
                
            
        //     foreach(var item2 in validMoveListsKing2){
        //         if(zzz.Row==item2.Row && zzz.Column==item2.Column){
        //             // Console.WriteLine(zzz.Row);
        //             // Console.WriteLine(zzz.Column);
        //             // Console.WriteLine("lolol");
        //             count++;
        //         }
        //     }

            
        //  } 
        //  gc.CheckForCheckmate();
        // var validMoveListsKing3 = boardDefault[6,4].SearchValidLocations(new Location(6,4),24,boardDefault);
        // foreach (var item in validMoveListsRook)
        // {
        //     foreach(var item2 in validMoveListsKing2){
        //         if(item.Row==item2.Row && item.Column==item2.Column){
        //             Console.WriteLine(item.Row);
        //             Console.WriteLine(item.Column);
        //             Console.WriteLine("lolol");
        //             count++;
        //         }
        //     }
        // }
        // foreach (var item in validMoveListsQueen)
        // {
        //     foreach(var item2 in validMoveListsKing2){
        //         if(item.Row==item2.Row && item.Column==item2.Column){
        //             Console.WriteLine(item.Row);
        //             Console.WriteLine(item.Column);
        //             Console.WriteLine("lolol");
        //             count++;
        //         }
        //     }
        // }
        // Console.WriteLine("hitungan");
        // Console.WriteLine(count);
        // foreach (var item in validMoveListsKing2)
        // {
        //      Console.WriteLine($"[{item.Row},{item.Column}] kingsman");
        // }
        // foreach (var item in validMoveListsKing3)
        // {
        //      Console.WriteLine($"[{item.Row},{item.Column}] queen");
        // }

        // foreach (var item in validMoveListsKing4)
        // {
        //      Console.WriteLine($"[{item.Row},{item.Column}] ");
        // }
       
        for (int col = 0; col < 8; col++)
        {
            for (int row = 0; row < 8; row++)
            {
                Console.Write("[" + boardDefault[col, row] + " " + "]");
            }
            Console.WriteLine();
        }
        // var dayat = gc.GetPlayerInfoAll(p1).PlayerPieces;
        // foreach (var item in dayat)
        // {
        //     Console.WriteLine(item.Key);
        // }

        
    //   gc.CheckForCheckmate();
    //   Console.WriteLine(gc.GetCheckMateStatus());
    //   List<Location> kingPlayer = new();
    //    kingPlayer = boardDefault[0, 4].SearchValidLocations(new Location(0,4), 25, boardDefault);
    //    var opponentPiecesMoves = boardDefault[0, 0].SearchValidLocations(new Location(0,0), 25, boardDefault);
    //    var opponentPiecesMoves1 = boardDefault[1, 7].SearchValidLocations(new Location(1,7), 25, boardDefault);
    //     var opponentPiecesMoves2 = boardDefault[6, 4].SearchValidLocations(new Location(6,4), 25, boardDefault);
    //    foreach (var locationBlackKing in kingPlayer)
    //    {
    //     foreach (var locationWhite in opponentPiecesMoves)
    //     {
    //          if (locationWhite.Column == locationBlackKing.Column && locationWhite.Row == locationBlackKing.Row)
    //                                 {
    //                                     countCheckMate++;

    //                                 }
    //     }
    //    }
    //    foreach (var locationBlackKing in kingPlayer)
    //    {
    //     foreach (var locationWhite in opponentPiecesMoves1)
    //     {
    //          if (locationWhite.Column == locationBlackKing.Column && locationWhite.Row == locationBlackKing.Row)
    //                                 {
    //                                     countCheckMate++;
    //                                 }
    //     }
    //    }
    //    foreach (var locationBlackKing in kingPlayer)
    //    {
    //     foreach (var locationWhite in opponentPiecesMoves2)
    //     {
    //          if (locationWhite.Column == locationBlackKing.Column && locationWhite.Row == locationBlackKing.Row)
    //                                 {
    //                                     countCheckMate++;
    //                                 }
    //     }
    //    }
    //    Console.WriteLine(countCheckMate);
    //    Console.WriteLine("wkwkwk");
    //    Console.WriteLine(boardDefault[6,4]._id);
    //    Console.WriteLine(boardDefault[0,0]._id);
    //    Console.WriteLine(boardDefault[1,7]._id);
    //      Console.WriteLine(boardDefault[0,4]._id);
        // Console.WriteLine(gc._checkMate);
        // gc.AssignPlayerColourSet(p1,Colour.BLACK);
        // gc.AssignPlayerColourSet(p2,Colour.WHITE);
        // gc.AssignPlayerColourRandom(p1,p2);
        // Console.WriteLine(colourPlayerRandom);
    }
}
