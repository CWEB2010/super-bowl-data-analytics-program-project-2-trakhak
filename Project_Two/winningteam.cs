using System;

namespace Project_Two
{
    public class winningteam
    {
        public DateTime Sdate { get; set; }
        public string SB { get; set; }
        public int Attendance { get; set; }
        public string QBWinner { get; set; }
        public string CoachWinner { get; set; }
        public string Winner { get; set; }
        public int WinningPts { get; set; }
        public string QBLoser { get; set; }
        public string CoachLoser { get; set; }
        public string Loser { get; set; }
        public int LosingPts { get; set; }
        public string MVP { get; set; }
        public string Stadium { get; set; }
        public string City { get; set; }
        public string State { get; set; }


        public winningteam(string Sdate, string SB, string Attendance, string QBWinner, string CoachWinner, string Winner, string WinningPts, string QBLoser, string CoachLoser, string Loser, string LosingPts, string MVP, string Stadium, string City, string State)
        {
            this.Sdate = DateTime.Parse(s: Sdate);
            this.SB = SB;
            this.Attendance = int.Parse(Attendance);
            this.QBWinner = QBWinner;
            this.CoachWinner = CoachWinner;
            this.Winner = Winner;
            this.WinningPts = int.Parse(WinningPts);
            this.QBLoser = QBLoser;
            this.CoachLoser = CoachLoser;
            this.Loser = Loser;
            this.LosingPts = int.Parse(LosingPts);
            this.MVP = MVP;
            this.Stadium = Stadium;
            this.City = City;
            this.State = State;
        }

    }
}