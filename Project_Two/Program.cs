using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace Project_Two
{
    class Program
    {

        static void Main(string[] args)
        {
            List<winningteam> Team = new List<winningteam>();
            string primer = "";
            string textFile = "";
            string path = "";

            Console.WriteLine("Enter program by entering name or enter x to exit");

            primer = Convert.ToString(Console.ReadLine());//using continuous loop, x to exit loop

            if (primer != "x")
            {

                Console.WriteLine($"{primer} What file path do you want to read? (.csv)");
                textFile = @Console.ReadLine();
                Console.WriteLine(textFile);

                Console.WriteLine("\nWhat file path do you want it to be written in (.txt)");
                path = @Console.ReadLine();
                Console.WriteLine(path);

                try
                {
                    if (File.Exists(textFile))
                    {
                        using (StreamReader file = new StreamReader(textFile))

                        {
                            int counter = 0;
                            string ln;
                            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                            while ((ln = file.ReadLine()) != null)

                            {

                                string[] col = CSVParser.Split(ln);

                                {
                                  
                                    if (counter > 0)

                                    {
                                        winningteam team = new winningteam(col[0], col[1], col[2], col[3], col[4], col[5], col[6], col[7], col[8], col[9], col[10], col[11], col[12], col[13], col[14]);
                                        Team.Add(team);
                                        

                                    }
                                    counter++;
                                }
                            }
                            file.Close();

                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("error message: {0}", e.ToString());
                }
                //super bowl write winners

                using (StreamWriter file = File.CreateText(path))

                {

                    Console.WriteLine("\n\nSuper Bowl Winners\n");
                    file.WriteLine("\n\nSuper Bowl Winners\n");

                    file.WriteLine("Team name" + "\t\t" + "Year won" + "\t\t" + "Winning QB" + "\t\t" + "Winning Coach" + "\t\t" + "MVP" + "\t\t" + "Point Difference");
                    Console.WriteLine("Team name" + "\t\t" + "Year won" + "\t\t" + "Winning QB" + "\t\t" + "Winning Coach" + "\t\t" + "MVP" + "\t\t" + "Point Difference");


                    foreach (winningteam item in Team)
                    {

                        file.WriteLine(item.Winner + "\t\t" + item.Sdate.Year + "\t\t" + item.QBWinner + "\t\t" + item.CoachWinner + "\t\t" + item.MVP + "\t\t" + (item.WinningPts - item.LosingPts));
                        Console.WriteLine(item.Winner + "\t\t" + item.Sdate.Year + "\t\t" + item.QBWinner + "\t\t" + item.CoachWinner + "\t\t" + item.MVP + "\t\t" + (item.WinningPts - item.LosingPts));

                    }

                    file.Close();


                    //top 5
                    using (StreamWriter bfile = File.AppendText(path))

                    {
                        bfile.WriteLine("Attendance" + "\t\t" + "Year of Win" + "\t\t" + "Winning Team" + "\t\t" + "Losing Team" + "\t\t" + "City" + "\t\t" + "State" + "\t\t" + "Stadium");
                        Console.WriteLine("Attendance" + "\t\t" + "Year of Win" + "\t\t" + "Winning Team" + "\t\t" + "Losing Team" + "\t\t" + "City" + "\t\t" + "State" + "\t\t" + "Stadium");

                        bfile.WriteLine("\n\nTop 5 attended Super Bowls\n");
                        Console.WriteLine("\n\nTop 5 attended Super Bowls\n");


                        var qryTopFive = (from item in Team
                                          orderby item.Attendance descending
                                          select item).Take(5);

                        qryTopFive.ToList().ForEach(s => bfile.WriteLine(s.Attendance + "\t" + s.Sdate.Year + "\t" + s.Winner + "\t" + s.Loser + "\t" + s.City + "\t" + s.State + "\t" + s.Stadium));
                        qryTopFive.ToList().ForEach(s => Console.WriteLine(s.Attendance + "\t" + s.Sdate.Year + "\t" + s.Winner + "\t" + s.Loser + "\t" + s.City + "\t" + s.State + "\t" + s.Stadium));

                    }

                    file.Close();


                    //Generate a list of states that hosted the most super bowls
                    using (StreamWriter cfile = File.AppendText(path))

                    {

                        cfile.WriteLine("\n\nStatistics on States where SuperBowls were hosted\n");
                        Console.WriteLine("\n\n Statistics on States where SuperBowls were hosted\n");
                        var qryState = from stateRecord in Team 
                                       group stateRecord by new 
                                       {
                                           stateRecord.State 
                                       } into stateGroups 
                                       from cityGroups in
                                           (from city in stateGroups 
                                            orderby city.City, city.Sdate.Year descending, city.Stadium 
                                            group city by new { city.City }) 
                                       orderby stateGroups.Key.State 
                                       group cityGroups by new { stateGroups.Key.State }; //group cities and states


                        foreach (var outerGroup in qryState) 
                        {
                            Console.WriteLine($"{ outerGroup.Key.State }: ");
                            cfile.WriteLine($"{ outerGroup.Key.State }: ");
                            foreach (var detail in outerGroup)
                            {
                                Console.WriteLine($"\t{ detail.Key.City }");
                                cfile.WriteLine($"\t{ detail.Key.City }");

                                int v_count = 0;
                                foreach (var city in detail)

                                {
                                    Console.WriteLine($"\t\tIn {city.Sdate.Year} - Stadium: { city.Stadium} ");
                                    cfile.WriteLine($"\t\tIn {city.Sdate.Year} - Stadium: { city.Stadium}");
                                    v_count++;

                                }
                                Console.WriteLine($"\n\t\t{outerGroup.Key.State}'s Super Bowl count is { v_count }.");
                                cfile.WriteLine($"\n\t\t{outerGroup.Key.State}'s Super Bowl count is { v_count }.");
                            }
                        }


                        cfile.Close();



                        //show list of players that have more than one mvp

                        using (StreamWriter dfile = File.AppendText(path))
                        {
                            dfile.WriteLine("\n\nPlayers who have more than one MVP:");
                            Console.WriteLine("\n\nPlayers who have more than one MVP:");

                            var MVPCount = from m in Team
                                           group m by m.MVP into MVPGroup
                                           where MVPGroup.Count() >= 2
                                           orderby MVPGroup.Key
                                           select MVPGroup;

                            foreach (var m in MVPCount)
                            {
                                Console.WriteLine($"{ m.Key } has {  m.Count() } MVPs.");
                                dfile.WriteLine($"{ m.Key } has {  m.Count() } MVPs.");
                            }

                            dfile.Close();

                        }


                        //coaches who lost the most superbowls

                        using (StreamWriter efile = File.AppendText(path))

                        {

                            efile.WriteLine("\n\nCoach who has lost the most Super Bowls:");
                            Console.WriteLine("\n\nCoach who has lost the most Super Bowls:");

                            var CoachLoss = from cl in Team
                                            .GroupBy(cl => cl.CoachLoser)
                                            select new

                                            {
                                                cl.Key,
                                                Most = cl.Count()
                                            };


                            foreach (var cl in CoachLoss)
                            {
                                if (cl.Most == CoachLoss.Max(x => x.Most))
                                {
                                    Console.WriteLine($"{ cl.Key } lost { cl.Most } Super Bowls.");
                                    efile.WriteLine($"{ cl.Key } lost { cl.Most } Super Bowls.");
                                };
                            }

                            efile.Close();

                        }


                        //coaches who won most superbowls

                        using (StreamWriter ffile = File.AppendText(path))

                        {

                            ffile.WriteLine("\n\nCoach won the most Super Bowls:");
                            Console.WriteLine("\n\nCoach won the most Super Bowls:");

                            var CoachWin = from cl in Team
                                            .GroupBy(cl => cl.CoachWinner)
                                           select new

                                           {
                                               cl.Key,
                                               Most = cl.Count()
                                           };


                            foreach (var cl in CoachWin)
                            {
                                if (cl.Most == CoachWin.Max(x => x.Most))
                                {
                                    Console.WriteLine($"{ cl.Key } won { cl.Most } Super Bowls.");
                                    ffile.WriteLine($"{ cl.Key } won { cl.Most } Super Bowls.");
                                };
                            }

                            ffile.Close();

                        }
                        //team won the most super bowls
                        using (StreamWriter gfile = File.AppendText(path))

                        {

                            gfile.WriteLine("\n\nTeam won the most Super Bowls:");
                            Console.WriteLine("\n\nTeam won the most Super Bowls:");

                            var TeamWin = from cl in Team
                                            .GroupBy(cl => cl.Winner)
                                          select new

                                          {
                                              cl.Key,
                                              Most = cl.Count()
                                          };


                            foreach (var cl in TeamWin)
                            {
                                if (cl.Most == TeamWin.Max(x => x.Most))
                                {
                                    Console.WriteLine($"{ cl.Key } won { cl.Most } Super Bowls.");
                                    gfile.WriteLine($"{ cl.Key } won { cl.Most }Super Bowls.");
                                };
                            }

                            gfile.Close();

                        }
                        //team lost the most super bowls
                        using (StreamWriter hfile = File.AppendText(path))

                        {

                            hfile.WriteLine("\n\nTeam lost the most Super Bowls:");
                            Console.WriteLine("\n\nTeam lost the most Super Bowls:");

                            var TeamLoser = from cl in Team
                                            .GroupBy(cl => cl.Loser)
                                            select new

                                            {
                                                cl.Key,
                                                Most = cl.Count()
                                            };


                            foreach (var cl in TeamLoser)
                            {
                                if (cl.Most == TeamLoser.Max(x => x.Most))
                                {
                                    Console.WriteLine($"{ cl.Key } lost { cl.Most }Super Bowls.");
                                    hfile.WriteLine($"{ cl.Key } lost { cl.Most }Super Bowls.");
                                };
                            }

                            hfile.Close();

                        }

                        //point difference

                        using (StreamWriter ifile = File.AppendText(path))

                        {

                            ifile.WriteLine("\n\nSuper Bowl with the highest Point Difference:");
                            Console.WriteLine("\n\nSuper Bowl with the highest Point Difference:");

                            var PointDiff = from cl in Team
                                            select new

                                            {
                                                cl.Sdate.Year,
                                                cl.Winner,
                                                Most = Math.Abs(cl.WinningPts - cl.LosingPts)
                                            };

                            foreach (var cl in PointDiff)
                            {
                                if (cl.Most == PointDiff.Max(x => x.Most))
                                {
                                    Console.WriteLine($"The { cl.Year } Super Bowl, won by {cl.Winner}, had the biggest point difference of { cl.Most }.");
                                    ifile.WriteLine($"{ cl.Year } had the biggest point difference of { cl.Most }.");
                                };
                            }

                            ifile.Close();
                            //average superbowl attendance

                            using (StreamWriter jfile = File.AppendText(path))

                            {

                                jfile.WriteLine("\n\nAverage attendance of Super Bowls:");
                                Console.WriteLine("\n\nAverage attendance of Super Bowls");

                                var AverageAtt = (from cl in Team
                                                  select cl.Attendance).Average();


                                Console.WriteLine($"The average Super Bowl attendance of all time is {Math.Round(AverageAtt)}.");
                                jfile.WriteLine($"The average Super Bowl attendance of all time is {Math.Round(AverageAtt)}.");

                                jfile.Close();

                            }

                            primer = Convert.ToString(Console.ReadLine().ToUpper());

                        }
                    }
                }
            }
        }
    }
}






