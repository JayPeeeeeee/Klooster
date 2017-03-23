using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klooster
{
    public class Pater
    {
        private string _sName;
        private Persoonlijkheid _persoonlijkheid;
        private List<Gedachte> _liGedachtes;
        private Inspiratie _inspiratie;
        private const int _iMaxGedachtes = 20;
        private int _iVolgendeGedachte;
        private Denkwijze _denkwijze;

        public Pater(string sName, Inspiratie inspiratie)
        {
            _sName = sName;
            _persoonlijkheid = new Persoonlijkheid();
            _liGedachtes = new List<Gedachte>();
            _inspiratie = inspiratie;
            _iVolgendeGedachte = 0;
            _denkwijze = Denkwijze.Goedheidsgewijs;
        }

        public Pater(string sName, Persoonlijkheid persoonlijkheid, Inspiratie inspiratie)
        {
            _sName = sName;
            _persoonlijkheid = persoonlijkheid;
            _liGedachtes = new List<Gedachte>();
            _inspiratie = inspiratie;
            _iVolgendeGedachte = 0;
            _denkwijze = Denkwijze.Goedheidsgewijs;
        }

        public void Bid()
        {
            if (_liGedachtes.Count < _iMaxGedachtes)
            {
                _liGedachtes.Add(_inspiratie.InspireerMij(_persoonlijkheid));
            }
        }

        public Woord Spreek()
        {
            Woord gesprokenWoord = null;
            if(_liGedachtes.Count > 0)
            {
                gesprokenWoord = _liGedachtes.ElementAt(_iVolgendeGedachte).Verwoord(Cloner.DeepClone(_persoonlijkheid));
                if(++_iVolgendeGedachte > _liGedachtes.Count)
                {
                    _iVolgendeGedachte = 0;
                }
            }
            else
            {
                throw new Exception("Pater heeft niets te zeggen.");
            }
            return gesprokenWoord;
        }

        public void Luister(Woord verteldWoord)
        {
            if(_liGedachtes.Count < _iMaxGedachtes)
            {
                _liGedachtes.Add(new Gedachte(verteldWoord, _persoonlijkheid));
            }
            else
            {
                if (!DenkNa())
                {
                    throw new Exception("Hoofd zit vol");
                }
            }
        }

        private bool DenkNa()
        {
            switch (_denkwijze)
            {
                case Denkwijze.Goedheidsgewijs:
                    break;
                case Denkwijze.Creativeitsgewijs:
                    break;
                case Denkwijze.Gemiddeldegewijs:
                    break;
                default: break;
            }
            _denkwijze = (Denkwijze)((int)_denkwijze++ % 3);
            return _liGedachtes.Count < 20 ? true : false;
        }

        private void OrdenGoedheidsgewijs()
        {

        }

        private void OrdenCreativiteitsgewijs()
        {

        }

        private void OrdenGemiddeldegewijs()
        {

        }

        public override string ToString()
        {
            return string.Format("Pater {0}, {1}, {2} gedachten aan zijn hoofd", _sName, _persoonlijkheid.ToString(), _liGedachtes.Count);
        }
    }
}
