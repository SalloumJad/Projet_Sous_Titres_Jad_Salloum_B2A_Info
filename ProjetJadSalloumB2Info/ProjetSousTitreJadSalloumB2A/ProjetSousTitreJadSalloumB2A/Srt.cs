using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace ProjetSousTitreJadSalloumB2A
{
    class Srt
    {
        public async Task FindSRTAsync(Sous_titre st)
        {
            string result = "";

            using (StreamReader sr = new StreamReader(st.Link))
            {
                string print = "";
                DateTime startTime = DateTime.Now;
                result = sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    await countSeconds(startTime, sr);

                    if ((result == "") && (st.subtValueCount != 1))
                    {
                        if (st.totalTimePassed != st.totalShowTime) // si c'est l'heure de montrer le sous-titre
                        {
                            await Task.Delay(st.totalShowTime - st.totalTimePassed); // On attend que ce soit l'heure de montrer le sous-titre
                            st.totalTimePassed = st.totalShowTime;
                        }

                        if (st.totalTimePassed == st.totalShowTime)
                        {
                            if (print.Length >= 50) // Ajout de saut de ligne si l'affichage est trop long
                            {
                                int index = print.LastIndexOf(" ", 49);
                                print.Insert(index - 1,"\n");
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            Console.WriteLine(print);
                            await Task.Delay(st.totalByeTime - st.totalShowTime); // le temps de laisser le sous-titre affiché
                            Console.Clear();
                            st.totalTimePassed = st.totalByeTime;
                            st.subtValueCount = 1;
                            st.subtCount++;
                            print = "";
                        }

                    }
                    else if (st.subtValueCount == 1)
                    {
                        st.subtValueCount++;
                    }
                    else if (st.subtValueCount == 2)
                    {
                        st.subtValueCount++;
                        string[] subtTimes = result.Split(" --> "); // sépare la ligne entre le temps d'affichage et de faire disparaitre le sous-titre
                        string subtShowTime = subtTimes[0];
                        string subtByeTime = subtTimes[1];
                        subtShowTime = subtShowTime.Replace(":", ",");
                        subtByeTime = subtByeTime.Replace(":", ",");
                        string[] subtShowTimeSplit = subtShowTime.Split(','); // sépare les valeures individuelles pour les calculer en millisecondes pour le await
                        string[] subtByeTimeSplit = subtByeTime.Split(',');

                        st.totalShowTime = int.Parse(subtShowTimeSplit[0]) * 3600000 + int.Parse(subtShowTimeSplit[1]) * 60000 + int.Parse(subtShowTimeSplit[2]) * 1000 + int.Parse(subtShowTimeSplit[3]); // on convertit directement le temps en millisecondes.

                        st.totalByeTime = int.Parse(subtByeTimeSplit[0]) * 3600000 + int.Parse(subtByeTimeSplit[1]) * 60000 + int.Parse(subtByeTimeSplit[2]) * 1000 + int.Parse(subtByeTimeSplit[3]);
                    }
                    else if (st.subtValueCount >= 3)
                    {
                        st.subtValueCount++;
                        print += result;
                    }
                    result = sr.ReadLine();
                }
            }


            
        }

        // tentative de création d'un compteur de temps, mais il remplace les sous-titres à ce stade
        public async Task countSeconds(DateTime startTime, StreamReader sr) 
        {
            //while (!sr.EndOfStream)
            //{
            //    if (startTime.Millisecond == DateTime.Now.Millisecond))
            //    {
            //        Console.Clear();
            //        Console.WriteLine(DateTime.Now - startTime);
            //    }
            //}
        }
    }
}
