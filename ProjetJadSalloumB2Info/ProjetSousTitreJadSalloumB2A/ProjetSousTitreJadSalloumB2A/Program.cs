using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace ProjetSousTitreJadSalloumB2A
{

    class Program
    {
        public static async Task Main(string[] args)
        {
            // mettre son fichier srt ici
            string link = (Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Jad\Films\Tangled.srt");

            // J'ai ici essayé de lancer un fichier mp4 sans wpf en utilisant le lanceur Windows mais sans succès.
            // System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Jad\Films\Tangled.2010.1080p.BluRay.x264.YIFY.mp4");

            Srt srt = new Srt();
            Sous_titre st = new Sous_titre(link);
            Task.WaitAll(srt.FindSRTAsync(st));
        }
    }
}
