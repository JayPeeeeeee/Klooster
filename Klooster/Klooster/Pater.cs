using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klooster
{
    public class Pater
    {
        private string _sNaam;
        private Persoonlijkheid _persoonlijkheid;
        private List<Gedachte> _liGedachtes;
        private Inspiratie _inspiratie;
        private const int _iMaxGedachtes = 20;
        private int _iVolgendeGedachte;
        private Denkwijze _denkwijze;

        public Pater(string sName, Inspiratie inspiratie)
        {
            _sNaam = sName;
            _persoonlijkheid = new Persoonlijkheid();
            _liGedachtes = new List<Gedachte>();
            _inspiratie = inspiratie;
            _iVolgendeGedachte = 0;
            _denkwijze = Denkwijze.Goedheidsgewijs;
        }

        public Pater(string sName, Persoonlijkheid persoonlijkheid, Inspiratie inspiratie)
        {
            _sNaam = sName;
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
                Gedachte gedachte = _inspiratie.InspireerMij(_persoonlijkheid);
                Console.WriteLine(string.Format("{0} bidt en komt tot een gedachte: {1}.", _sNaam, gedachte.ToString()));
                _liGedachtes.Add(gedachte);
            }
            else
            {
                Console.WriteLine(string.Format("{0} hoofd zit vol, gedacht niet opgeslagen. Hij gaat even nadenken.", _sNaam));
                if (!DenkNa())
                {
                    Console.WriteLine(string.Format("Hoofd van pater {0} zit vol en nadenken brengt geen soelaas.", _sNaam));
                    throw new Exception(string.Format("{0} zijn hoofd zit vol!", _sNaam));
                }
            }
        }

        public Woord Spreek()
        {
            Woord gesprokenWoord = null;
            if(_liGedachtes.Count > 0)
            {
                gesprokenWoord = _liGedachtes.ElementAt(_iVolgendeGedachte).Verwoord(Cloner.DeepClone(_persoonlijkheid));
                Console.WriteLine(string.Format("{0} spreekt over het volgende: {1}.", _sNaam, gesprokenWoord));
                if(++_iVolgendeGedachte > _liGedachtes.Count)
                {
                    _iVolgendeGedachte = 0;
                }
            }
            else
            {
                throw new Exception(string.Format("{0} heeft niets te zeggen.", _sNaam));
            }
            return gesprokenWoord;
        }

        public void Luister(Woord verteldWoord)
        {
            Console.WriteLine(string.Format("{0} luistert naar de volgende woorden: {1}.", _sNaam, verteldWoord));
            if(_liGedachtes.Count < _iMaxGedachtes)
            {
                Gedachte gedachte = new Gedachte(verteldWoord, _persoonlijkheid);
                Console.WriteLine(string.Format("{0} denkt er dit van: {1}", _sNaam, gedachte));
                _liGedachtes.Add(gedachte);
            }
            else
            {
                Console.WriteLine(string.Format("{0} hoofd zit vol, gedacht niet opgeslagen. Hij gaat even nadenken.", _sNaam));
                if (!DenkNa())
                {
                    Console.WriteLine(string.Format("Hoofd van pater {0} zit vol en nadenken brengt geen soelaas.", _sNaam));
                    throw new Exception(string.Format("{0} zijn hoofd zit vol!", _sNaam));
                }
            }
        }

        public bool DenkNa()
        {
            Console.WriteLine(string.Format("{0} gaat alles even overdenken: {1} gedachtes aan zijn hoofd momenteel.", _sNaam, _liGedachtes.Count));
            Console.WriteLine(string.Format("{0} heeft voor het nadenken deze persoonlijkheid: {1}.", _sNaam, _persoonlijkheid.ToString()));
            switch (_denkwijze)
            {
                case Denkwijze.Goedheidsgewijs:
                    OrdenGoedheidsgewijs();
                    break;
                case Denkwijze.Creativeitsgewijs:
                    OrdenCreativiteitsgewijs();
                    break;
                case Denkwijze.Gemiddeldegewijs:
                    OrdenGemiddeldegewijs();
                    break;
                default: break;
            }
            _denkwijze = (Denkwijze)((int)_denkwijze++ % 3);
            VeranderPersoonlijkheid();
            Console.WriteLine(string.Format("{0} heeft na het nadenken deze persoonlijkheid: {1}.", _sNaam, _persoonlijkheid.ToString()));
            Console.WriteLine(string.Format("{0} heeft de gedachten op een rijtje gezet: {1} resterende gedachten.", _sNaam, _liGedachtes.Count));
            return _liGedachtes.Count < 20 ? true : false;
        }

        public void VeranderPersoonlijkheid()
        {
            int iGemGoedheid = 0;
            int iGemCreativiteit = 0;
            foreach(Gedachte gedachte in _liGedachtes)
            {
                iGemGoedheid += gedachte.Persoonlijkheid.Goedheid;
                iGemCreativiteit += gedachte.Persoonlijkheid.Creativiteit;
            }
            iGemGoedheid /= _liGedachtes.Count;
            iGemCreativiteit /= _liGedachtes.Count;

            int iNieuweGoedheid = _persoonlijkheid.Goedheid + ((iGemGoedheid - _persoonlijkheid.Goedheid) / 10);
            int iNieuweCreativiteit = _persoonlijkheid.Creativiteit + ((iGemCreativiteit - _persoonlijkheid.Creativiteit) / 10);

            _persoonlijkheid = new Persoonlijkheid(iNieuweGoedheid, iNieuweCreativiteit);
        }

        private void OrdenGoedheidsgewijs()
        {
            List<Gedachte> liGeordendeGedachtes = new List<Gedachte>();
            foreach(int iConcept in _liGedachtes.Select(gedachte => gedachte.Concept).Distinct())
            {
                int iKleinsteVerschil = int.MaxValue;
                Gedachte besteGedachte = null;
                foreach(Gedachte gedachte in _liGedachtes.Where(gedachte => gedachte.Concept == iConcept).Select(gedachte => gedachte))
                {
                    int iVerschil = Math.Abs(gedachte.Persoonlijkheid.Goedheid - _persoonlijkheid.Goedheid);
                    if (iVerschil < iKleinsteVerschil)
                    {
                        //besteGedachte = Cloner.DeepClone(gedachte);
                        besteGedachte = gedachte;
                        iKleinsteVerschil = iVerschil;
                    }
                }
                liGeordendeGedachtes.Add(besteGedachte);
            }
            _liGedachtes = liGeordendeGedachtes;
        }

        private void OrdenCreativiteitsgewijs()
        {
            List<Gedachte> liGeordendeGedachtes = new List<Gedachte>();
            foreach(int iConcept in _liGedachtes.Select(gedachte => gedachte.Concept).Distinct())
            {
                int iKleinsteVerschil = int.MaxValue;
                Gedachte besteGedachte = null;
                foreach(Gedachte gedachte in _liGedachtes.Where(gedachte => gedachte.Concept == iConcept).Select(gedachte => gedachte))
                {
                    int iVerschil = Math.Abs(gedachte.Persoonlijkheid.Creativiteit - _persoonlijkheid.Creativiteit);
                    if(iVerschil < iKleinsteVerschil)
                    {
                        //besteGedachte = Cloner.DeepClone(gedachte);
                        besteGedachte = gedachte;
                        iKleinsteVerschil = iVerschil;
                    }
                }
                liGeordendeGedachtes.Add(besteGedachte);
            }
            _liGedachtes = liGeordendeGedachtes;
        }

        private void OrdenGemiddeldegewijs()
        {
            List<Gedachte> liGeordendeGedachtes = new List<Gedachte>();
            IEnumerable<int> iConcepten = _liGedachtes.Select(gedachte => gedachte.Concept).Distinct();
            foreach (int iConcept in iConcepten)
            {
                int iGoedheid = 0;
                int iCreativiteit = 0;
                IEnumerable<Gedachte> gedachten = _liGedachtes.Where(gedachte => gedachte.Concept == iConcept).Select(gedachte => gedachte);
                int iAantalGedachtes = gedachten.Count();
                foreach (Gedachte gedachte in gedachten)
                {
                    iGoedheid += gedachte.Persoonlijkheid.Goedheid;
                    iCreativiteit += gedachte.Persoonlijkheid.Creativiteit;
                }
                liGeordendeGedachtes.Add(new Gedachte(iConcept, new Persoonlijkheid(iGoedheid / iAantalGedachtes, iCreativiteit / iAantalGedachtes)));
            }
            _liGedachtes = liGeordendeGedachtes;
        }

        public override string ToString()
        {
            return string.Format("Pater {0}, {1}, {2} gedachten aan zijn hoofd.", _sNaam, _persoonlijkheid.ToString(), _liGedachtes.Count);
        }
    }
}
