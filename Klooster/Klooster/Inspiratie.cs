using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klooster
{
    public class Inspiratie
    {
        private static int _iConcept;

        public Inspiratie()
        {
            _iConcept = 1;
        }

        public Gedachte InspireerMij(Persoonlijkheid persoonlijkheid)
        {
            Gedachte gedachte = new Gedachte(_iConcept, Cloner.DeepClone(persoonlijkheid));
            if(++_iConcept > 9)
            {
                _iConcept = 1;
            }
            return gedachte;
        }
    }
}
