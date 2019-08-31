using System;
using System.Collections.Generic;
using System.Text;

namespace Jogo21
{
    class Pessoa : Jogador
    {
        static int ValorApostado;

        public bool Aposta(int valor)
        {
            if (valor < 1 || valor > this.Fichas)
            {
                return false;
            }

            ValorApostado = valor;
            return true;
        }

        public int Perde()
        {
            if (CartasJogador[0].Valor + CartasJogador[1].Valor != 21)
            {
                Fichas -= ValorApostado;
            }
            else
            {
                ValorApostado -= ValorApostado / 2;
                Fichas -= ValorApostado;
            }

            Console.WriteLine("Você perdeu {0} fichas", ValorApostado);
            return ValorApostado;
        }

        public int Ganha()
        {
            Console.WriteLine("Você ganhou! ({0} fichas)\n", ValorApostado + ValorApostado / 2);
            Fichas += ValorApostado + ValorApostado / 2;
            return ValorApostado + ValorApostado / 2;
        }

        public new void MostraCartas()
        {
            Console.WriteLine("[Você]");
            base.MostraCartas();
            Console.WriteLine("");
        }
    }
}
