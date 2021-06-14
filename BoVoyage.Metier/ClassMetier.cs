using BoVoyage.Donnees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage.Metier
{
    public class ClassMetier
    {
        private Repository repository = new Repository();
        private Droits droits = new Droits();
        public void Load()
        {
            droits.Load();
        }
    }
}
