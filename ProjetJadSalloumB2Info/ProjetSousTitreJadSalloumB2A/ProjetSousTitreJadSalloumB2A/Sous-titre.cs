using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetSousTitreJadSalloumB2A
{
    class Sous_titre
    {
        public int subtValueCount = 1;
        public int totalShowTime = 0;
        public int subtCount = 0;
        public int totalByeTime = 0;
        public int totalTimePassed = 0; // on stockera ici le total de temps attendu entre chaque sous-titre
        public string Link;
        public Sous_titre(string link)
        {
            Link = link;
        }
    }
}
