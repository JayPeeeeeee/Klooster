using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klooster
{
    public class Gedachte
    {
        private int _iConcept;
        private Persoonlijkheid _persoonlijkheid;

        public int Concept { get { return _iConcept; } }
        public Persoonlijkheid Persoonlijkheid { get { return _persoonlijkheid; } }

        public Gedachte(int iConcept, Persoonlijkheid persoonlijkheid)
        {
            _iConcept = iConcept;
            _persoonlijkheid = persoonlijkheid;
        }

        public Gedachte(Woord verteldWoord, Persoonlijkheid persoonlijkheidPater)
        {
            int iGoedheid = (verteldWoord.Gedachte.Persoonlijkheid.Goedheid + verteldWoord.Begeestering.Goedheid + persoonlijkheidPater.Goedheid) / 3;
            int iCreativiteit = (verteldWoord.Gedachte.Persoonlijkheid.Creativiteit + verteldWoord.Begeestering.Creativiteit + persoonlijkheidPater.Creativiteit) / 3;
            _iConcept = verteldWoord.Gedachte._iConcept;
            _persoonlijkheid = new Persoonlijkheid(iGoedheid, iCreativiteit);
        }

        public Woord Verwoord(Persoonlijkheid begeestering)
        {
            return new Woord(new Gedachte(_iConcept, Cloner.DeepClone(_persoonlijkheid)), begeestering);
        }

        public override string ToString()
        {
            return string.Format("{0} gedachte over concept {1}", _persoonlijkheid.ToString(), _iConcept);
        }
    }
}
