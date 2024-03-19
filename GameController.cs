// namespace SpaceGame;
// using Players;

using System.Diagnostics;

class GameController
{
    public IBoard Board { get; set; }
    private Dictionary<IPlayer, PlayerChessData?> _players;
    public Queue<IPlayer> _playerTurn {get; private set;} = new();
    private Queue<IPlayer> _opponentTurn = new();

    public GameController(IPlayer p1, IPlayer p2, IBoard gameBoard)
    {
        Board = gameBoard;
        _players = new Dictionary<IPlayer, PlayerChessData?>();
        _players[p1] = new PlayerChessData();
        _players[p2] = new PlayerChessData();
    }
    public CheckMate _checkMate { get; set; }

    public ValidMove _validMove { get; set; }
    public GameStatus _gameStatus {get;set;} = GameStatus.INIT;
    public Func<Piece, Piece, bool>? PawnPromotionPlayer;
    public Func<Piece, Piece, bool, bool>? KingCastlingPlayer;
    // public Action<>? OnGameStatusUpdate
    // public Action<Piece, Location>? OnPieceMove
    public CheckMate CheckForCheckmate()
    {
        IEnumerable<Location> kingPlayer = Enumerable.Empty<Location>();
        List<Location> playerThreatKing = new();

        int countCheckMate = 0;
        int countProtectKing = 0;
        var opponentPieces = _players[_opponentTurn.Peek()].PlayerPieces;
        var currentPlayerPieces = _players[_playerTurn.Peek()].PlayerPieces;
        foreach (var item in currentPlayerPieces)
        {
            if (item.Key._pieceType == Type.KING)
            {
                kingPlayer = Board[item.Value.Row, item.Value.Column].SearchValidLocations(
                    new Location(item.Value.Row, item.Value.Column), 25, Board);
            }
        }
        if (_checkMate == CheckMate.CHECK)
        {
            foreach (var item in currentPlayerPieces)
            {
                if (item.Key._pieceType != Type.KING)
                {
                    Location locationKnight = new(item.Value.Row, item.Value.Column);
                    var playerPiecesMoves = item.Key.SearchValidLocations(locationKnight, 25, Board);
                    foreach (Location locationPlayer in playerPiecesMoves)
                    {
                        foreach (Location locationKingPlayer in kingPlayer)
                        {
                            if (locationPlayer.Column == locationKingPlayer.Column && locationPlayer.Row == locationKingPlayer.Row)
                            {
                                countProtectKing++;
                            }
                        }
                    }
                }
            }

            foreach (var item in opponentPieces)
            {
                var opponentPiecesMoves = item.Key.SearchValidLocations(new Location(item.Value.Row, item.Value.Column), 25, Board);
                foreach (var opponentValidMove in opponentPiecesMoves)
                {
                    foreach (var locationKingPlayer in kingPlayer)
                    {
                        if (opponentValidMove.Column == locationKingPlayer.Column && opponentValidMove.Row == locationKingPlayer.Row)
                        {
                            if (!playerThreatKing.Contains(opponentValidMove))
                            {
                                playerThreatKing.Add(new Location(opponentValidMove.Row, opponentValidMove.Column));
                            }
                            countCheckMate += 1;
                        }
                    }
                }
            }
            if (playerThreatKing.Count == kingPlayer.Count() && countProtectKing == 0)
            {
                _checkMate = CheckMate.CHECKMATE;
                _gameStatus = GameStatus.END;
            }
        }
        // Console.WriteLine("zzzz");
        // Console.WriteLine(playerThreatKing.Count());
        // Console.WriteLine(kingPlayer.Count());
        // Console.WriteLine(playerThreatKing.Count);
        // Console.WriteLine(countCheckMate);
        // Console.WriteLine();

        // Console.WriteLine("wkwkwk");
        // Console.WriteLine(_checkMate);
        return _checkMate;
    }


    public bool AssignPlayerColourSet(IPlayer player, Colour colour)
    {
        bool isDifferentColour = false;
        foreach (var item in _players)
        {
            if (item.Key == player)
            {
                if (colour != item.Value.PlayerColour)
                {
                    _players[player].PlayerColour = colour;
                    isDifferentColour = true;
                }

            }
        }
        return isDifferentColour;
    }
    public bool AssignPlayerColourSet(IPlayer p1, Colour c1, IPlayer p2, Colour c2)
    {
        // kaya javascript jadi assign key langsung assign colour
        bool isDifferentColour = false;
        if (c1 != c2)
        {
            _players[p1].PlayerColour = c1;
            _players[p2].PlayerColour = c2;
            isDifferentColour = true;
        }
        return isDifferentColour;
    }

    public Dictionary<IPlayer, Colour> AssignPlayerColourRandom(IPlayer p1, IPlayer p2)
    {
        Dictionary<IPlayer, Colour> dictIplayerColourRandom = new();
        var availableColors = Enum.GetValues(typeof(Colour)).Cast<Colour>().ToList();
        var rnd = new Random();
        availableColors = availableColors.OrderBy(c => rnd.Next()).ToList();
        dictIplayerColourRandom.Add(p1, availableColors[0]);
        _players[p1].PlayerColour = availableColors[0];
        dictIplayerColourRandom.Add(p2, availableColors[1]);
        _players[p2].PlayerColour = availableColors[1];
        return dictIplayerColourRandom;
    }

    public Dictionary<IPlayer, IDictionary<Piece, Location>> AssignPlayerPiece(IPlayer p1, IPlayer p2)
    {
        Dictionary<Piece, Location> dictP1 = new Dictionary<Piece, Location>();
        Dictionary<Piece, Location> dictP2 = new Dictionary<Piece, Location>();
        if (_players[p1].PlayerColour == Colour.BLACK)
        {
            dictP1.Add(new Rook(Colour.BLACK, Type.ROOK), new Location(0, 0));
            dictP1.Add(new Knight(Colour.BLACK, Type.KNIGHT), new Location(0, 1));
            dictP1.Add(new Bishop(Colour.BLACK, Type.BISHOP), new Location(0, 2));
            dictP1.Add(new Queen(Colour.BLACK, Type.QUEEN), new Location(0, 3));
            dictP1.Add(new King(Colour.BLACK, Type.KING), new Location(0, 4));
            dictP1.Add(new Bishop(Colour.BLACK, Type.BISHOP), new Location(0, 5));
            dictP1.Add(new Knight(Colour.BLACK, Type.KNIGHT), new Location(0, 6));
            dictP1.Add(new Rook(Colour.BLACK, Type.ROOK), new Location(0, 7));
        }
        else
        {
            dictP1.Add(new Rook(Colour.WHITE, Type.ROOK), new Location(7, 0));
            dictP1.Add(new Knight(Colour.WHITE, Type.KNIGHT), new Location(7, 1));
            dictP1.Add(new Bishop(Colour.WHITE, Type.BISHOP), new Location(7, 2));
            dictP1.Add(new Queen(Colour.WHITE, Type.QUEEN), new Location(7, 3));
            dictP1.Add(new King(Colour.WHITE, Type.KING), new Location(7, 4));
            dictP1.Add(new Bishop(Colour.WHITE, Type.BISHOP), new Location(7, 5));
            dictP1.Add(new Knight(Colour.WHITE, Type.KNIGHT), new Location(7, 6));
            dictP1.Add(new Rook(Colour.WHITE, Type.ROOK), new Location(7, 7));
        }


        if (_players[p2].PlayerColour == Colour.WHITE)
        {
            dictP2.Add(new Rook(Colour.WHITE, Type.ROOK), new Location(7, 0));
            dictP2.Add(new Knight(Colour.WHITE, Type.KNIGHT), new Location(7, 1));
            dictP2.Add(new Bishop(Colour.WHITE, Type.BISHOP), new Location(7, 2));
            dictP2.Add(new Queen(Colour.WHITE, Type.QUEEN), new Location(7, 3));
            dictP2.Add(new King(Colour.WHITE, Type.KING), new Location(7, 4));
            dictP2.Add(new Bishop(Colour.WHITE, Type.BISHOP), new Location(7, 5));
            dictP2.Add(new Knight(Colour.WHITE, Type.KNIGHT), new Location(7, 6));
            dictP2.Add(new Rook(Colour.WHITE, Type.ROOK), new Location(7, 7));
        }
        else
        {
            dictP1.Add(new Rook(Colour.BLACK, Type.ROOK), new Location(0, 0));
            dictP1.Add(new Knight(Colour.BLACK, Type.KNIGHT), new Location(0, 1));
            dictP1.Add(new Bishop(Colour.BLACK, Type.BISHOP), new Location(0, 2));
            dictP1.Add(new Queen(Colour.BLACK, Type.QUEEN), new Location(0, 3));
            dictP1.Add(new King(Colour.BLACK, Type.KING), new Location(0, 4));
            dictP1.Add(new Bishop(Colour.BLACK, Type.BISHOP), new Location(0, 5));
            dictP1.Add(new Knight(Colour.BLACK, Type.KNIGHT), new Location(0, 6));
            dictP1.Add(new Rook(Colour.BLACK, Type.ROOK), new Location(0, 7));
        }


        for (int i = 0; i < 8; i++)
        {
            if (_players[p1].PlayerColour == Colour.BLACK)
            {
                dictP1.Add(new Pawn(Colour.BLACK, Type.PAWN), new Location(1, i));
            }
            if (_players[p1].PlayerColour == Colour.WHITE)
            {
                dictP1.Add(new Pawn(Colour.WHITE, Type.PAWN), new Location(6, i));
            }
            if (_players[p2].PlayerColour == Colour.BLACK)
            {
                dictP2.Add(new Pawn(Colour.BLACK, Type.PAWN), new Location(1, i));
            }
            if (_players[p2].PlayerColour == Colour.WHITE)
            {
                dictP2.Add(new Pawn(Colour.WHITE, Type.PAWN), new Location(6, i));
            }
        }

        Dictionary<IPlayer, IDictionary<Piece, Location>> playerPieces = new Dictionary<IPlayer, IDictionary<Piece, Location>>();
        playerPieces.Add(p1, dictP1);
        foreach (var item in dictP1)
        {
            Board[item.Value.Row, item.Value.Column] = item.Key;
        }
        foreach (var item in dictP2)
        {
            Board[item.Value.Row, item.Value.Column] = item.Key;
        }
        _players[p1].PlayerPieces = dictP1;
        _players[p2].PlayerPieces = dictP2;
        playerPieces.Add(p2, dictP2);
        return playerPieces;
    }
    public int CalculatePlayerMaterial(IPlayer player)
    {
        int counter = 0;
        foreach (var item in _players[player].PlayerPieces)
        {
            Type type = item.Key._pieceType;
            int materialPoint = (int)type;
            counter += materialPoint;
        }
        _players[player].PlayerMaterial = counter;
        return counter;
    }
    public bool StartGame()
    {
        bool isStart = false;
        if (Board != null && _players.Count!=2)
        {
            isStart = true;
        }
        _checkMate = CheckMate.NONE;
        _gameStatus = GameStatus.START;
        _gameStatus = GameStatus.IN_PROGRESS;
        return isStart;
    }

    public bool EndGame()
    {
        bool isEndGame = false;
        // if checkmate == true
        if (_checkMate == CheckMate.CHECKMATE)
        {
            isEndGame = true;
        }
        return isEndGame;
    }

    public bool ResetBoard(Board newBoard)
    {
        bool isResetBoard = false;
        if (newBoard != null)
        {
            isResetBoard = true;
            Board = newBoard;
        }
        return isResetBoard;
    }

    public bool ResetPlayers()
    {
        _players = new Dictionary<IPlayer, PlayerChessData?>();
        return true;
    }
    public bool ChangePlayers(IPlayer oldPlayer, IPlayer newPlayer)
    {
        bool isChangePlayer = false;
        if (_players.ContainsKey(oldPlayer))
        {
            var value = _players[oldPlayer];
            _players[newPlayer] = value;
            _players.Remove(oldPlayer);
            isChangePlayer = true;
        }
        return isChangePlayer;
    }

    public IEnumerable<Location> GetPieceValidMoves(Piece piece)
    {
        IPlayer piecePlayer = new PlayerHuman ("dayat");
        foreach (var item in _players)
        {
            foreach (var playerPiece in item.Value.PlayerPieces)
            {
                if(piece==playerPiece.Key){
                    piecePlayer=item.Key;
                }
            }
        }
        Location currentLocation = GetLocationOfPiece(piece,piecePlayer);
        var validMoves = piece.SearchValidLocations(currentLocation, 25, Board);
        return validMoves;
    }

    public CheckMate MovePiece(IPlayer player, Piece piece, int col, int row)
    {
        if (_checkMate == CheckMate.CHECK)
        {
            CheckForCheckmate();
            if (_checkMate == CheckMate.CHECKMATE)
            {
                EndGame();
            }
        }
        if (piece != null && _players[player].PlayerColour == piece._pieceColour)
        {
            Location currentLocation = GetLocationOfPiece(piece,player);
            bool canMove = Board.MovePieceToLocation(piece, col, row, currentLocation);

            if (canMove == true && player.Name == _playerTurn.Peek().Name)
            {
                // pindahin bidak
                // playeropposite kehilangan bidak
                foreach (var item in _players)
                {
                    if (player.Name != item.Key.Name && Board[row, col] != null)
                    {
                        _players[item.Key].PlayerPieces.Remove(Board[row, col]);
                    }
                }

                Board.AssignPiecesToLocations(piece, new Location(row, col));
                _players[player].PlayerPieces[piece].Column = col;
                _players[player].PlayerPieces[piece].Row = row;

                Board.RemovePieceFromLocation(currentLocation.Row, currentLocation.Column);
                foreach (var item in _players)
                {
                    CalculatePlayerMaterial(item.Key);
                    // SetPlayerInfoAll(item.Key);
                }

                //  Metode pengecekan skak
                Location setNewPieceLocation = new(row, col);
                var kingCheck = piece.SearchValidLocations(setNewPieceLocation, 25, Board);
                foreach (var validLocationPiece in kingCheck)
                {
                    var pieceAtLocation = Board[validLocationPiece.Row, validLocationPiece.Column];
                    if (pieceAtLocation != null && pieceAtLocation._pieceType == Type.KING)
                    {
                        _checkMate = CheckMate.CHECK;
                        break;
                    }
                    else
                    {
                        _checkMate = CheckMate.NONE;
                    }
                }
                foreach (var nextPlayer in _players)
                {
                    if (nextPlayer.Key != player)
                    {
                        NextTurn(nextPlayer.Key);
                    }
                    else
                    {
                        NextTurnOpponent(nextPlayer.Key);
                    }
                }
                _validMove=ValidMove.VALIDMOVE;
                // return _checkMate;
            }
            else{
                _validMove=ValidMove.INVALIDMOVE;
            }
        }
        else {
            _validMove=ValidMove.INVALIDMOVE;
        }
        
        return _checkMate;
    }
    public bool PawnPromotion(Piece pawnToPromote, Piece newPiece)
    {
        bool isPromotion = false;
        for (int i = 0; i < Board.Columns; i++)
        {
            if (Board[0, i]._pieceType == Type.PAWN || Board[Board.Rows - 1, i]._pieceType == Type.PAWN)
            {
                pawnToPromote._pieceType = newPiece._pieceType;
                isPromotion = true;
            }
        }
        return isPromotion;
    }

    public bool KingCastling(Piece pieceKing, Piece pieceRook, bool kingside)
    {
        bool isCastling = false;
        int kingRow = (_players[_playerTurn.Peek()]?.PlayerColour == Colour.WHITE) ? 7 : 0;

        int kingCol = 4;
        int rookCol = kingside ? 7 : 0;

        if (pieceKing._hasMoved == false && pieceRook._hasMoved == false)
        {
            bool piecesBetween = false;
            int startCol = kingside ? kingCol + 1 : rookCol + 1;
            int endCol = kingside ? rookCol - 1 : kingCol - 1;
            for (int col = startCol; col <= endCol; col++)
            {
                if (Board[kingRow, col] != null)
                {
                    piecesBetween = true;
                    break;
                }
            }

            if (!piecesBetween)
            {
                // gerak raja
                Board[kingRow, kingCol + (kingside ? 2 : -2)] = Board[kingRow, kingCol];
                Board[kingRow, kingCol] = null;

                // gerak benteng
                Board[kingRow, kingside ? kingCol + 1 : kingCol - 1] = Board[kingRow, rookCol];
                Board[kingRow, rookCol] = null;
                isCastling = true;
            }
        }
        return isCastling;
    }

    public PlayerChessData GetPlayerInfoAll(IPlayer player)
    {
        var infoPlayer = _players[player];
        return infoPlayer;
    }

    // public PlayerChessData SetPlayerInfoAll(IPlayer player)
    // {
    //     var infoPlayer = _players[player];
    //     Dictionary<Piece, Location> remainingPieces = new();
    //     for (int row = 0; row < Board.rows; row++)
    //     {
    //         for (int col = 0; col < Board.columns; col++)
    //         {
    //             if (Board[row, col] != null)
    //             {
    //                 if (Board[row, col]._pieceColour == _players[player].PlayerColour)
    //                 {
    //                     remainingPieces.Add(Board[row, col], new Location(row, col));
    //                 }
    //             }
    //         }
    //     }
    //     _players[player].PlayerPieces = remainingPieces;
    //     return infoPlayer;
    // }

    public Colour GetPlayerColour(IPlayer player)
    {
        var colourPlayer = _players[player].PlayerColour;
        return colourPlayer;
    }

    public Dictionary<Piece, Location> GetPlayerPieces(IPlayer player)
    {
        Dictionary<Piece, Location> infoPlayerPieces = new();
        _players[player].PlayerPieces=infoPlayerPieces;
        // for (int row = 0; row < 8; row++)
        // {
        //     for (int col = 0; col < 8; col++)
        //     {
        //         if (Board[row, col] != null)
        //         {
        //             IPlayer currentPlayer = _playerTurn.Peek();
        //             Colour currentPlayerColor = _players[currentPlayer].PlayerColour;
        //             if (currentPlayerColor == Board[row, col]._pieceColour)
        //             {
        //                 Location locationPiece = new(row, col);
        //                 infoPlayerPieces.Add(Board[row, col], locationPiece);
        //             }
        //         }
        //     }
        // }
        // _players[player].PlayerPieces = infoPlayerPieces;
        return infoPlayerPieces;
    }

    public int GetPlayerMaterial(IPlayer player)
    {
        int totalPlayerMaterial = CalculatePlayerMaterial(player);
        _players[player].PlayerMaterial = totalPlayerMaterial;
        return totalPlayerMaterial;
    }

    public Piece[,] GetBoardInfo()
    {
        Piece[,] boardInfo = new Piece[Board.Rows, Board.Columns];
        for (int row = 0; row < Board.Rows; row++)
        {
            for (int col = 0; col < Board.Columns; col++)
            {
                boardInfo[row, col] = Board[row, col];
            }
        }
        return boardInfo;
    }

    public Piece GetPieceOnLocation(Location location)
    {
        Piece getPiece = Board[location.Row, location.Column];
        return getPiece;
    }
    public Piece GetPieceOnLocation(int row, int col)
    {
        Piece getPiece = Board[row, col];
        return getPiece;
    }

    public Location GetLocationOfPiece(Piece pieceToFind, IPlayer player)
    {
        int rowFound = 0;
        int colFound = 0;
        foreach (var item in _players[player].PlayerPieces)
        {
            if (pieceToFind == item.Key)
            {
                rowFound = item.Value.Row;
                colFound = item.Value.Column;
            }
        }
        // for (int row = 0; row < Board.rows; row++)
        // {
        //     for (int col = 0; col < Board.columns; col++)
        //     {
        //         if (Board[row, col]._id == pieceToFind._id)
        //         {
        //             rowFound = row;
        //             colFound = col;
        //         }
        //     }
        // }
        // Location pieceLocation = new(pieceToFind.row,pieceToFind.col);
        Location pieceLocation = new(rowFound, colFound);
        return pieceLocation;
    }
    public CheckMate GetCheckMateStatus()
    {
        return _checkMate;
    }

    public GameStatus GetGameStatus()
    {
        return _gameStatus;
    }

    public void setPlayerTurn()
    {
        foreach (var item in _players)
        {
            if (item.Value.PlayerColour == Colour.WHITE)
            {
                NextTurn(item.Key);
            }
            else
            {
                NextTurnOpponent(item.Key);
            }
        }
    }
    public bool NextTurnOpponent(IPlayer player)
    {
        if (_playerTurn.Count == 0)
        {
            _opponentTurn.Enqueue(player);
        }
        else
        {
            _opponentTurn.Dequeue();
            _opponentTurn.Enqueue(player);
        }
        return true;
    }

    public bool NextTurn(IPlayer player)
    {
        if (_playerTurn.Count == 0)
        {
            _playerTurn.Enqueue(player);
        }
        else
        {
            _playerTurn.Dequeue();
            _playerTurn.Enqueue(player);
        }
        return true;
    }
}

public class PlayerChessData
{
    public Colour PlayerColour { get; set; }
    public int PlayerMaterial { get; set; }

    public Dictionary<Piece, Location> PlayerPieces { get; set; }
}