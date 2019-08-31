using System;
using System.Collections.Generic;
using System.Text;
using static Jogo21.Base;

namespace Jogo21
{
    class Jogador
    {
        public List<Carta> CartasJogador;
        public int Fichas;
        public int Pontuacao;

        public Jogador()
        {
            Fichas = 100;
            Pontuacao = 0;
        }
        public bool VerificaBlackjack()
        {
            int temDez = 0;
            foreach (Carta carta in CartasJogador)
            {
                if (carta.Valor == 10)
                {
                    temDez = 1;
                    break;
                }
            }
            if (temDez == 1)
            {
                foreach (Carta carta in CartasJogador)
                {
                    if (carta.Simbolo == Simbolo.As)
                    {
                        carta.Valor = 11;
                        return true;
                    }
                }
            }
            return false;
        }

        public int SomaPontuacao()
        {
            Pontuacao = 0;
            foreach (Carta carta in CartasJogador)
                Pontuacao += carta.Valor;
            return Pontuacao;
        }

        public void IniciaJogo(Baralho baralho)
        {
            CartasJogador = new List<Carta>();
            CartasJogador.Add(baralho.TiraCarta());
            CartasJogador.Add(baralho.TiraCarta());

        }

        public void MostraCartas()
        {
            int i = 1;
            foreach (Carta carta in CartasJogador)
            {
                Console.WriteLine("Carta {0} : {1} de {2}", i, carta.Simbolo, carta.Naipe);
                i++;
            }
        }

    }
}
