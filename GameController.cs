// namespace ChessGame;
// using Players;

using System.Diagnostics;
///<summary>
///Class to control the process of chess game, from initiate until the game is finished
///This class arrange to addPlayer,addBoard,assignColourPiece,assignPieces,start the game, move piece,checking game status 
///until the game ends with one of the player get checkmate
/// </summary>

class GameController
{
    public IBoard Board { get; set; }
    private Dictionary<IPlayer, PlayerChessData?> _players;
    public Queue<IPlayer> PlayerTurn { get; private set; } = new();
    private Queue<IPlayer> OpponentTurn = new();

    public GameController(IPlayer p1, IPlayer p2, IBoard gameBoard)
    {
        Board = gameBoard;
        _players = new Dictionary<IPlayer, PlayerChessData?>();
        _players[p1] = new PlayerChessData();
        _players[p2] = new PlayerChessData();
    }
    public CheckMate CheckMate { get; set; } = CheckMate.NONE;

    public ValidMove ValidMove { get; set; }
    public GameStatus GameStatus { get; set; } = GameStatus.INIT;
    public Func<Piece, Piece, bool>? PawnPromotionPlayer;
    public Func<Piece, Piece, bool, bool>? KingCastlingPlayer;

    public Action<GameStatus> GameStatusUpdate;

    public void GameStatusUpdate1(GameStatus gameStatus)
    {
        if (gameStatus == GameStatus.START)
        {
            GameStatus = GameStatus.IN_PROGRESS;
        }
        else if (CheckMate == CheckMate.CHECKMATE)
        {
            GameStatus = GameStatus.END;
        }
        else if (gameStatus == GameStatus.INIT)
        {
            GameStatus = GameStatus.START;
        }
    }

    ///<summary>
    ///method without parameter to check game condition if checkmate happens or not.
    /// </summary>
    /// <returns>
    /// status check at chess game
    /// </returns>
    public CheckMate CheckForCheckmate()
    {
        List<Location> kingPlayer = new();
        List<Location> playerThreatKing = new();

        int countProtectKing = 0;
        var opponentPieces = _players[OpponentTurn.Peek()].PlayerPieces;
        var currentPlayerPieces = _players[PlayerTurn.Peek()].PlayerPieces;
        int rowKingOpponent = 0;
        int columnKingOpponent = 0;
        foreach (var item in opponentPieces)
        {
            if (item.Key.PieceType == Type.KING)
            {
                rowKingOpponent = item.Value.Row;
                columnKingOpponent = item.Value.Column;
                kingPlayer = Board[item.Value.Row, item.Value.Column].SearchValidLocations(new Location(item.Value.Row, item.Value.Column), CheckMate, Board);
                break;
            }
        }
        // buat ngilangin valid move proyeksi ketika giliran player yang diskak
        // for (int i = kingPlayer.Count - 1; i >= 0; i--)
        // {
        //     if(Board[kingPlayer[i].Row,kingPlayer[i].Column].PieceColour==_players[PlayerTurn.Peek()].PlayerColour){
        //         kingPlayer.Remove(kingPlayer[i]);
        //     }
        // }
        if (CheckMate == CheckMate.CHECK)
        {
            foreach (var item in opponentPieces)
            {
                if (item.Key.PieceType != Type.KING)
                {
                    Location locationKnight = new(item.Value.Row, item.Value.Column);
                    var playerPiecesMoves = item.Key.SearchValidLocations(locationKnight, CheckMate, Board);
                    foreach (Location locationPlayer in playerPiecesMoves)
                    {
                        // if(Board[locationPlayer.Row,locationPlayer.Column].PieceColour==_players[PlayerTurn.Peek()].PlayerColour){
                        //     continue;
                        // }
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

            foreach (var item in currentPlayerPieces)
            {
                var currentPlayerPiecesMoves = item.Key.SearchValidLocations(new Location(item.Value.Row, item.Value.Column), CheckMate, Board);
                foreach (var currentPlayerValidMove in currentPlayerPiecesMoves)
                {
                    foreach (var locationKingPlayer in kingPlayer)
                    {
                        if (currentPlayerValidMove.Column == locationKingPlayer.Column && currentPlayerValidMove.Row == locationKingPlayer.Row)
                        {
                            if (!playerThreatKing.Any(loc => loc.Row == currentPlayerValidMove.Row && loc.Column == currentPlayerValidMove.Column))
                            {
                                playerThreatKing.Add(new Location(currentPlayerValidMove.Row, currentPlayerValidMove.Column));
                            }
                        }
                    }
                }
            }
            if (playerThreatKing.Count == kingPlayer.Count && countProtectKing == 0)
            {
                CheckMate = CheckMate.CHECKMATE;
                GameStatus = GameStatus.END;
            }
        }
        return CheckMate;
    }

    ///<summary>
    ///method with 2 parameter to assign colour to player
    /// </summary>
    ///<param name="player">
    ///IPlayer who choose the colour
    /// </param>
    ///<param name="colour">
    ///enum Colour which player chose
    /// </param>
    /// <returns>
    /// boolean if assign player colour process is success
    /// </returns>

    public bool AssignPlayerColourSet(IPlayer player, Colour colour)
    {
        bool isDifferentColour = false;
        Colour colourP1 = Colour.WHITE;
        if (colour == Colour.WHITE || colour == Colour.BLACK)
        {
            foreach (var item in _players)
            {
                if (item.Key == player && colourP1!=colour)
                {
                      _players[player].PlayerColour = colour;
                      isDifferentColour = true;
                      colourP1=colour;
                }
                colourP1=item.Value.PlayerColour;
            }
        }
        return isDifferentColour;
    }
    ///<summary>
    ///method with 2 parameter to assign colour to each player randomly
    /// </summary>
    ///<param name="p1">
    ///IPlayer one who participate at the game
    /// </param>
    ///<param name="p2">
    ///IPlayer two who participate at the game
    /// </param>
    /// <returns>
    /// result of assigning colour random to two players and shows the colour too
    /// </returns>

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
    ///<summary>
    ///method with 2 parameter to assign pieces to each player based on their colour
    /// </summary>
    ///<param name="p1">
    ///IPlayer one who participate at the game
    /// </param>
    ///<param name="p2">
    ///IPlayer two who participate at the game
    /// </param>
    /// <returns>
    /// result of assigning pieces to two players and shows the location of the pieces too
    /// </returns>

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
            dictP2.Add(new Rook(Colour.BLACK, Type.ROOK), new Location(0, 0));
            dictP2.Add(new Knight(Colour.BLACK, Type.KNIGHT), new Location(0, 1));
            dictP2.Add(new Bishop(Colour.BLACK, Type.BISHOP), new Location(0, 2));
            dictP2.Add(new Queen(Colour.BLACK, Type.QUEEN), new Location(0, 3));
            dictP2.Add(new King(Colour.BLACK, Type.KING), new Location(0, 4));
            dictP2.Add(new Bishop(Colour.BLACK, Type.BISHOP), new Location(0, 5));
            dictP2.Add(new Knight(Colour.BLACK, Type.KNIGHT), new Location(0, 6));
            dictP2.Add(new Rook(Colour.BLACK, Type.ROOK), new Location(0, 7));
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
    ///<summary>
    ///method with 1 parameter to calculate player point by calculating the remaining material that player has
    /// </summary>
    ///<param name="player">
    ///IPlayer to calculate his point 
    /// </param>
    /// <returns>
    /// result of point player material
    /// </returns>
    public int CalculatePlayerMaterial(IPlayer player)
    {
        int counter = 0;
        foreach (var item in _players[player].PlayerPieces)
        {
            Type type = item.Key.PieceType;
            int materialPoint = (int)type;
            counter += materialPoint;
        }
        _players[player].PlayerMaterial = counter;
        return counter;
    }
    ///<summary>
    ///method without parameter to start the game
    ///</summary>
    /// <returns>
    /// boolean by validating the 1 board and 2 players have existed at data
    /// </returns>
    public bool StartGame()
    {
        bool isStart = false;
        if (Board != null && _players.Count != 2)
        {
            isStart = true;
        }
        return isStart;
    }
    ///<summary>
    ///method without parameter to end the game
    ///</summary>
    /// <returns>
    /// boolean by validating if opponent is at checkmate. If yes, end the game
    /// </returns>

    public bool EndGame()
    {
        bool isEndGame = false;
        // if checkmate == true
        if (CheckMate == CheckMate.CHECKMATE)
        {
            isEndGame = true;
            GameStatus = GameStatus.END;
        }
        return isEndGame;
    }
    ///<summary>
    ///method with 1 parameter to reset board
    ///</summary>
    /// <param name="newBoard">
    /// IBoard from user
    /// </param>
    /// <returns>
    /// boolean by validating if new board is exist or not
    /// </returns>

    public bool ResetBoard(IBoard newBoard)
    {
        bool isResetBoard = false;
        if (newBoard != null)
        {
            isResetBoard = true;
            Board = newBoard;
        }
        return isResetBoard;
    }
    ///<summary>
    ///method without parameter to reset players
    ///</summary>
    /// <returns>
    /// boolean if the player is reset then reset the game data and player
    /// </returns>

    public bool ResetPlayers()
    {
        _players = new Dictionary<IPlayer, PlayerChessData?>();
        return true;
    }
    ///<summary>
    ///method with two parameters to substitute player
    ///</summary>
    ///<param name="oldPlayer">
    ///player who is at game
    ///</param>
    ///<param name="newPlayer">
    ///player who wants to substitute the current player
    ///</param>
    /// <returns>
    /// boolean if the player is the old player exist and substitute with the new one
    /// </returns>
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
    ///<summary>
    ///method with one parameters to get how many of valid moves from a piece
    ///</summary>
    ///<param name="piece">
    ///object piece
    ///</param>
    /// <returns>
    /// Ienumerable of location object of a piece moves from chosen piece
    /// </returns>

    public IEnumerable<Location> GetPieceValidMoves(Piece piece)
    {
        IPlayer piecePlayer = new PlayerHuman("dayat");
        foreach (var item in _players)
        {
            foreach (var playerPiece in item.Value.PlayerPieces)
            {
                if (piece == playerPiece.Key)
                {
                    piecePlayer = item.Key;
                }
            }
        }
        Location currentLocation = GetLocationOfPiece(piece, piecePlayer);
        var validMoves = piece.SearchValidLocations(currentLocation, CheckMate, Board);
        return validMoves;
    }
    ///<summary>
    ///method with four parameters to move piece at the board if the move is valid
    ///</summary>
    ///<param name="player">
    ///object Iplayer who wants to move a piece and his turn
    ///</param>
    ///<param name="piece">
    ///object Piece which has been chosen by player
    ///</param>
    ///<param name="col">
    ///integer column which peace want to be set at
    ///</param>
    ///<param name="row">
    ///integer row which peace want to be set at
    ///</param>
    /// <returns>
    /// checkmate condition because of the piece move 
    /// </returns>

    public CheckMate MovePiece(IPlayer player, Piece piece, int col, int row)
    {
        if (piece != null && _players[player].PlayerColour == piece.PieceColour)
        {
            Location currentLocation = GetLocationOfPiece(piece, player);
            bool canMove = Board.MovePieceToLocation(piece, col, row, currentLocation, CheckMate);

            if (canMove == true && player.Name == PlayerTurn.Peek().Name)
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
                _players[player]!.PlayerPieces[piece] = new Location(row, col);

                Board.RemovePieceFromLocation(currentLocation.Row, currentLocation.Column);
                foreach (var item in _players)
                {
                    CalculatePlayerMaterial(item.Key);
                    // SetPlayerInfoAll(item.Key);
                }

                //  Metode pengecekan skak
                Location setNewPieceLocation = new(row, col);
                var kingCheck = piece.SearchValidLocations(setNewPieceLocation, CheckMate, Board);
                foreach (var validLocationPiece in kingCheck)
                {
                    var pieceAtLocation = Board[validLocationPiece.Row, validLocationPiece.Column];
                    if (pieceAtLocation != null && pieceAtLocation.PieceType == Type.KING)
                    {
                        CheckMate = CheckMate.CHECK;
                        break;
                    }
                    else
                    {
                        CheckMate = CheckMate.NONE;
                    }
                }
                ValidMove = ValidMove.VALIDMOVE;
                // return CheckMate;
            }
            else
            {
                ValidMove = ValidMove.INVALIDMOVE;
            }
        }
        else
        {
            ValidMove = ValidMove.INVALIDMOVE;
        }
        if (CheckMate == CheckMate.CHECK)
        {
            CheckForCheckmate();
            if (CheckMate == CheckMate.CHECKMATE)
            {
                EndGame();
            }
        }
        if (ValidMove == ValidMove.VALIDMOVE)
        {
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
        }

        return CheckMate;
    }
    ///<summary>
    ///method with two parameters to promote a pawn and change type piece of pawn if it reaches the last square of their movement
    ///</summary>
    ///<param name="pawnToPromote">
    ///object piece pawn who wants to be promoted
    ///</param>
    ///<param name="newPiece">
    ///object piece which type was chosen by player to change the pawn type of his piece
    ///</param>
    /// <returns>
    /// bool if condition of pawnPromotion is fulfilled 
    /// </returns>

    public bool PawnPromotion(Piece pawnToPromote, Piece newPiece)
    {
        bool isPromotion = false;
        for (int i = 0; i < Board.Columns; i++)
        {
            if (Board[0, i].PieceType == Type.PAWN || Board[Board.Rows - 1, i].PieceType == Type.PAWN)
            {
                pawnToPromote.PieceType = newPiece.PieceType;
                isPromotion = true;
            }
        }
        return isPromotion;
    }
    ///<summary>
    ///method with three parameters to do king castle between rook and king
    ///</summary>
    ///<param name="pieceKing">
    ///object piece with type king 
    ///</param>
    ///<param name="pieceRook">
    ///object piece with type rook 
    ///</param>
    ///<param name="kingside">
    ///bool which one do the kingcastle kind : queenside or kingside 
    ///</param>
    /// <returns>
    /// bool if condition of KingCastling is fulfilled and can do the king castling 
    /// </returns>

    public bool KingCastling(Piece pieceKing, Piece pieceRook, bool kingside)
    {
        bool isCastling = false;
        int kingRow = (_players[PlayerTurn.Peek()]?.PlayerColour == Colour.WHITE) ? 7 : 0;

        int kingCol = 4;
        int rookCol = kingside ? 7 : 0;

        if (pieceKing.HasMoved == false && pieceRook.HasMoved == false)
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
                Board[kingRow, kingCol + (kingside ? 2 : -2)] = Board[kingRow, kingCol];
                Board[kingRow, kingCol] = null;

                Board[kingRow, kingside ? kingCol + 1 : kingCol - 1] = Board[kingRow, rookCol];
                Board[kingRow, rookCol] = null;
                isCastling = true;
            }
        }
        return isCastling;
    }
    ///<summary>
    ///method to get info of a player. It is related to the statistic game data.
    ///</summary>
    ///<param name="player">
    ///Iplayer which player want the data to be shown
    ///</param>
    /// <returns>
    /// PlayerChessData of a player. 
    /// </returns>

    public PlayerChessData GetPlayerInfoAll(IPlayer player)
    {
        var infoPlayer = _players[player];
        return infoPlayer;
    }
    ///<summary>
    ///method to get colour of a player.
    ///</summary>
    ///<param name="player">
    ///Iplayer which player want the colour to be shown
    ///</param>
    /// <returns>
    /// Colour of a player. 
    /// </returns>

    public Colour GetPlayerColour(IPlayer player)
    {
        var colourPlayer = _players[player].PlayerColour;
        return colourPlayer;
    }
    ///<summary>
    ///method to get pieces and its location of a player.
    ///</summary>
    ///<param name="player">
    ///Iplayer which player want the pieces to be shown
    ///</param>
    /// <returns>
    /// Dictionary with piece as keys and Location as the value. Location has row and column data. Returns all of the pieces and their location that player has 
    /// </returns>

    public Dictionary<Piece, Location> GetPlayerPieces(IPlayer player)
    {
        Dictionary<Piece, Location> infoPlayerPieces = new();
        _players[player].PlayerPieces = infoPlayerPieces;
        return infoPlayerPieces;
    }
    ///<summary>
    ///method to get point of a player by calculating pieces which player has.
    ///</summary>
    ///<param name="player">
    ///Iplayer which player want the point to be shown
    ///</param>
    /// <returns>
    /// int total points of remaining pieces from a player
    /// </returns>

    public int GetPlayerMaterial(IPlayer player)
    {
        int totalPlayerMaterial = CalculatePlayerMaterial(player);
        _players[player].PlayerMaterial = totalPlayerMaterial;
        return totalPlayerMaterial;
    }
    ///<summary>
    ///method to get board current condition without parameter
    ///</summary>
    /// <returns>
    /// array of Piece from the board
    /// </returns>

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
    ///<summary>
    ///method to get Piece of piece from a location with one parameter
    ///</summary>
    ///<param name="location">
    ///Location which player wants to know the piece is
    ///</param>
    /// <returns>
    /// Piece from a location
    /// </returns>

    public Piece GetPieceOnLocation(Location location)
    {
        Piece getPiece = Board[location.Row, location.Column];
        return getPiece;
    }
    ///<summary>
    ///method to get Piece of piece from a location with two parameter
    ///</summary>
    ///<param name="row">
    ///int row of a position from board
    ///</param>
    ///<param name="col">
    ///int col of a position from board
    ///</param>
    /// <returns>
    /// Piece from int row and int col
    /// </returns>
    public Piece GetPieceOnLocation(int row, int col)
    {
        Piece getPiece = Board[row, col];
        return getPiece;
    }
    ///<summary>
    ///method to get Location of a piece from a player with two parameter
    ///</summary>
    ///<param name="pieceToFind">
    ///Piece which player want to search
    ///</param>
    ///<param name="player">
    ///Player which player want to retrieve
    ///</param>
    /// <returns>
    /// Location of a piece from Player
    /// </returns>

    public Location GetLocationOfPiece(Piece pieceToFind, IPlayer player)
    {
        int rowFound = 0;
        int colFound = 0;
        // bool isFound = false;
        Location pieceLocation = new(rowFound, colFound);

        foreach (var item in _players[player].PlayerPieces)
        {
            if (pieceToFind == item.Key)
            {
                pieceLocation.Row = item.Value.Row;
                pieceLocation.Column = item.Value.Column;
            }
        }
        return pieceLocation;
    }
    ///<summary>
    ///method to get checkmate status without parameter
    ///</summary>
    /// <returns>
    /// Checkmate checkmate status
    /// </returns>
    public CheckMate GetCheckMateStatus()
    {
        return CheckMate;
    }
    ///<summary>
    ///method to get game status without parameter
    ///</summary>
    /// <returns>
    /// GameStatus get the current game status
    /// </returns>

    public GameStatus GetGameStatus()
    {
        return GameStatus;
    }
    ///<summary>
    ///method to set player who goes first and second for the opponent turn without parameter
    ///</summary>
    /// <returns>
    /// void, because it just set palyer turn for the beginning of the game
    /// </returns>

    public void setPlayerTurn()
    {
        foreach (var item in _players)
        {
            if (item.Value.PlayerColour == Colour.WHITE)
            {
                NextTurn(item.Key);
            }
            else if (item.Value.PlayerColour == Colour.BLACK)
            {
                NextTurnOpponent(item.Key);
            }
        }
    }
    ///<summary>
    ///method to set player who is opponent from other player's turn
    ///</summary>
    /// <returns>
    /// bool if player is exist
    /// </returns>
    public bool NextTurnOpponent(IPlayer player)
    {
        if (OpponentTurn.Count == 0)
        {
            OpponentTurn.Enqueue(player);
        }
        else
        {
            OpponentTurn.Dequeue();
            OpponentTurn.Enqueue(player);
        }
        return true;
    }
    ///<summary>
    ///method to set player who goes first turn and changing turn to other player
    ///</summary>
    /// <returns>
    /// bool if player is exist and set turn to his player
    /// </returns>

    public bool NextTurn(IPlayer player)
    {
        if (PlayerTurn.Count == 0)
        {
            PlayerTurn.Enqueue(player);
        }
        else
        {
            PlayerTurn.Dequeue();
            PlayerTurn.Enqueue(player);
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