using System;
using System.Collections.Generic;
using System.Text;
using static Jogo21.Base;

namespace Jogo21
{
    class Baralho
    {
        private List<Carta> Cartas;

        public Baralho()
        {
            IniciaNovoBaralho();
        }

        public void IniciaNovoBaralho()
        {
            Cartas = new List<Carta>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Cartas.Add(new Carta() { Naipe = (Naipe)i, Simbolo = (Simbolo)j });

                    if (j <= 8)
                        Cartas[Cartas.Count - 1].Valor = j + 1;
                    else
                        Cartas[Cartas.Count - 1].Valor = 10;
                }
            }
        }
        public void Embaralhar()
        {
            Random r = new Random();
            int n = Cartas.Count;

            for (int i = 0; i < n; i++)
            {
                var j = r.Next(n);
                Carta Carta = Cartas[j];
                Cartas[j] = Cartas[n];
                Cartas[n] = Carta;
            }

        }
        public Carta TiraCarta()
        {
            if (Cartas.Count <= 0)
            {
                IniciaNovoBaralho();
                Embaralhar();
            }

            Carta cartaRetirada = Cartas[Cartas.Count - 1];
            Cartas.RemoveAt(Cartas.Count - 1);
            return cartaRetirada;
        }
        public int ContaCartasRestantes()
        {
            return Cartas.Count;
        }
        public void RestauraBaralho()
        {
            if (ContaCartasRestantes() < 20)
            {
                IniciaNovoBaralho();
                Embaralhar();
            }
        }

    }
}
