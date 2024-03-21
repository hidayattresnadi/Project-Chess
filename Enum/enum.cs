public enum CheckMate
{
    NONE,
    CHECK,
    CHECKMATE
}

public enum GameStatus
{
    INIT,
    START,
    IN_PROGRESS,
    END
}

public enum Colour
{
    NONE,
    WHITE,
    BLACK
}

public enum Type
{
    PAWN = 1,
    KNIGHT = 3,
    BISHOP = 3,
    ROOK = 5,
    QUEEN = 9,
    KING
}

public enum AiDifficulty
{
    EASY,
    MEDIUM,
    HARD,
    EXPERT
}


public enum ValidMove
{
    VALIDMOVE,
    INVALIDMOVE
}