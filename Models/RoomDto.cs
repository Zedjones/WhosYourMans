using System.Collections.Generic;

namespace GuessWegmons.Models
{
    /// <summary>
    /// Contains data about a room.
    /// </summary>
    public class RoomDto
    {
        /// <summary>
        /// Whether or not player 1 is in the game.
        /// </summary>
        public bool Player1In { get; set; }

        /// <summary>
        /// Whether or not player 2 is in the game.
        /// </summary>
        public bool Player2In { get; set; }

        /// <summary>
        /// The name of the correct Pokemon associated for a player.
        /// </summary>
        public string RightAnswer { get; set; }

        /// <summary>
        /// List of the questions and their answers that have been in this room so far.
        /// </summary>
        public Stack<QuestionAnswer> questionsAndAnswers { get; set; }

        /// <summary>
        /// Room 'name'.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// If it is the current users turn.
        /// </summary>
        public bool MyTurn { get; set; }

        public bool GameOver { get; set; }

        /// <summary>
        /// Create a Room Dto object.
        /// </summary>
        /// <param name="fromRoom">Room data is from</param>
        /// <param name="playerId">Player Id</param>
        public RoomDto(Room fromRoom, int playerId)
        {
            Player1In = fromRoom.Player1Session != null;
            Player2In = fromRoom.Player2Session != null;

            // Set questions and answers, new if needed
            if (fromRoom.questionsAndAnswers is null)
            {
                questionsAndAnswers = new Stack<QuestionAnswer>();
            }
            else
            {
                questionsAndAnswers = fromRoom.questionsAndAnswers;
            }

            // Set turn and correct answer based on which player it is
            if (playerId == 1)
            {
                MyTurn = fromRoom.Turn % 2 == 1;
                RightAnswer = fromRoom.Player1Answer;
            }
            else
            {
                MyTurn = fromRoom.Turn % 2 == 0;
                RightAnswer = fromRoom.Player2Answer;
            }

            Name = fromRoom.Name;
            GameOver = fromRoom.GameOver;
        }
    }
}