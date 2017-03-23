using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klooster
{
    [Serializable]
    public class Persoonlijkheid
    {        
        private int _iGoedheid;
        private int _iCreativiteit;
        private static readonly Random _random = new Random();

        public int Goedheid { get { return _iGoedheid; } }
        public int Creativiteit { get { return _iCreativiteit; } }

        public Persoonlijkheid()
        {
            _iGoedheid = _random.Next(0, 99);
            _iCreativiteit = _random.Next(0, 99);
        }

        public Persoonlijkheid(int iGoedheid, int iCreativiteit)
        {
            if ((iGoedheid >= 0 && iGoedheid <= 99) && (iCreativiteit >= 0 && iCreativiteit <= 99))
            {
                _iGoedheid = iGoedheid;
                _iCreativiteit = iCreativiteit;
            }
            else
            {
                throw new ArgumentException("Goedheid en creativiteit moeten tussen 0 en 99 zijn!");
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", TranslateGoedheid(), TranslateCreativiteit());
        }

        private string TranslateGoedheid()
        {
            string sGoedheid = string.Empty;
            if (_iGoedheid >= 0 && _iGoedheid <= 33)
            {
                sGoedheid = "evil";
            }
            else if (_iGoedheid >= 34 && _iGoedheid <= 66)
            {
                sGoedheid = "neutral";
            }
            else if (_iGoedheid >= 67 && _iGoedheid <= 99)
            {
                sGoedheid = "good";
            }
            return sGoedheid;
        }

        private string TranslateCreativiteit()
        {
            string sCreativiteit = string.Empty;
            if (_iCreativiteit >= 0 && _iCreativiteit <= 33)
            {
                sCreativiteit = "lawful";
            }
            else if (_iCreativiteit >= 34 && _iCreativiteit <= 66)
            {
                sCreativiteit = "neutral";
            }
            else if (_iCreativiteit >= 67 && _iCreativiteit <= 99)
            {
                sCreativiteit = "chaotic";
            }
            return sCreativiteit;
        }
    }
}
