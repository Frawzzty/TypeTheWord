using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.Game
{
    public class WordGameResult
    {
        public WordGameResult() { }

        public int WordsCorrect { get; set; }
        public int WordsWrong { get; set; }
        public int TimeSettingSeconds { get; set; }

        public float StreakAmount { get; set; }
        public int TotalStreaks { get; set; }

        public float WPM 
            => MathF.Round((WordsCorrect * TimeSettingSeconds / 60f ),2); //Div by 60f for minute //Remove decimals?
        

    }
}
