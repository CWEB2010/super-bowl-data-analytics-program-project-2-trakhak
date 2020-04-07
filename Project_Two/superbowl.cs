using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Two
{
    class superbowl
    {
        //Date	SB	Attendance	QB  Winner	Coach Winner	Winner	Winning Pts	QB Loser	
        //Coach Loser	Loser	Losing Pts	MVP	Stadium	City	State

        public string Date { get; set; }
        public string SB { get; set; }
        public int Attendance { get; set; }
        public string QB_Winner { get; set; }
        public string Coach_Winner { get; set; }
        public string Team_Winner { get; set; }
        public int Winner_Pts { get; set; }
        public string QB_Loser { get; set; }
        public string Coach_Loser { get; set; }
        public string Team_Loser { get; set; }
        public string Loser_Pts { get; set; }
        public string MVP { get; set; }
        public string Stadium { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public superbowl(string date, string sb, int attendance, string QBWinner, string coachWinner, string teamWinner, int winnerPts, string QBLoser, string coachLoser, string teamLoser, string loserPts, string mvp, string stadium, string city, string state)
        {
            Date = date;
            SB = sb;
            Attendance = attendance;
            QB_Winner = QBWinner;
            Coach_Winner = coachWinner;
            Team_Winner = teamWinner;
            Winner_Pts = winnerPts;
            QB_Loser = QBLoser;
            Coach_Loser = coachLoser;
            Team_Loser = teamLoser;
            Loser_Pts = loserPts;
            MVP = mvp;
            Stadium = stadium;
            City = city;
            State = state;
        }

        public override string ToString()
        {
            return string.Format($"date: {Date}\nSB: {SB}\nAttendance: {Attendance}\nQuarterback Winner: {QB_Winner}\nCoach Winner: {Coach_Winner}\nTeam Winner: {Team_Winner}\nWinner Pts: {Winner_Pts}\nQuarterback Loser: {QB_Loser}\nCoach Loser: {Coach_Loser}\nTeam Loser: {Team_Loser}\nLoser Pts: {Loser_Pts}\nMVP: {MVP}\nStadium: {Stadium}\nCity: {City}\nState: {State}");
        }
    }
}
