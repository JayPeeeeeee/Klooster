using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klooster
{
    public class Woord
    {
        private Gedachte _gedachte;
        private Persoonlijkheid _begeestering;

        public Gedachte Gedachte { get { return _gedachte; } }
        public Persoonlijkheid Begeestering { get { return _begeestering; } }

        public Woord(Gedachte gedachte, Persoonlijkheid begeestering)
        {
            _gedachte = gedachte;
            _begeestering = begeestering;
        }

        public override string ToString()
        {
            return string.Format("{0}, uitgesproken met een {1} ondertoon", _gedachte.ToString(), _begeestering.ToString());
        }
    }
}
