using System;
using System.Collections.Generic;
using System.Text;

namespace Jogo21
{
    class Computador : Jogador
    {
        public void Ganha(int valor)
        {
            Fichas += valor;
        }

        public void Perde(int valor)
        {
            Fichas -= valor;
        }
        public int MostraLucro()
        {
            return Fichas;
        }

        public new void MostraCartas()
        {
            Console.WriteLine("[Computador]");
            base.MostraCartas();
        }
    }
}
