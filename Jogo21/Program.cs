using System;
using System.Threading;

namespace Jogo21
{
    class Program
    {

        static Baralho Baralho = new Baralho();
        static Pessoa Pessoa = new Pessoa();
        static Computador Computador = new Computador();

        static void Main(string[] args)
        {
            Console.Title = "♠♥♣♦ Jogo 21";

            Console.WriteLine("------------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                   Atenção: regras do Jogo");
            Console.WriteLine("------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Você jogará contra o computador (dealer) com o objetivo de chegar o mais próximo possível de 21 pontos");
            Console.WriteLine("- Se passar de 21 pontos perde");
            Console.WriteLine("- Cada carta numerada tem o seu valor nominal");
            Console.WriteLine("- Os valetes, as damas e os reis (figuras), têm o valor de 10 pontos");
            Console.WriteLine("- O Ás vale 1 ponto, exceto no blackjack que ele vale 11 pontos");
            Console.WriteLine("- Blackjack é quando recebe um Ás + um 10 / uma figura no início, resultando 21 pontos");
            Console.WriteLine("- Se você ganhar receberá 1,5 vezes a sua aposta");
            Console.WriteLine("- Se perder o valor apostado vai todo para o dealer");

            Console.WriteLine("\nVamos começar...\n");

            while (Pessoa.Fichas > 0)
            {
                Jogo();
                Console.WriteLine("\nAperte qualquer tecla para próxima aposta...\n");
                Console.ReadKey(true);
            }
            Console.WriteLine("Você perdeu! Vejo você na próxima rodada...");
            Console.ReadLine();
        }

        static void Jogo()
        {
            Baralho.RestauraBaralho();

            Console.WriteLine("Cartas no baralho: {0}", Baralho.ContaCartasRestantes());
            Console.WriteLine("Suas fichas: {0}", Pessoa.Fichas);
            Console.WriteLine("Quantas fichas você gostaria de apostar? (1 - {0})", Pessoa.Fichas);
            string input = Console.ReadLine();
            Console.WriteLine("");

            int valorAposta;

            while (!Int32.TryParse(input, out valorAposta) || valorAposta < 1 || valorAposta > Pessoa.Fichas)
            {
                Console.WriteLine("Quantidade Insuficiente. Quantas fichas você gostaria de apostar? (1 - {0})", Pessoa.Fichas);
                input = Console.ReadLine();
            }

            Pessoa.Aposta(valorAposta);

            Pessoa.IniciaJogo(Baralho);
            Computador.IniciaJogo(Baralho);

            Pessoa.MostraCartas();
            Console.WriteLine("Total: {0}\n", Pessoa.CartasJogador[0].Valor + Pessoa.CartasJogador[1].Valor);

            if (Pessoa.VerificaBlackjack())
            {
                Console.WriteLine("Total: {0}", Pessoa.SomaPontuacao());
                Console.WriteLine("Uau, um BlackJack de primeira!");
                Computador.Perde(Pessoa.Ganha());
                return;
            }

            Console.WriteLine("[Computador]");
            Console.WriteLine("Carta 1: {0} de {1}", Computador.CartasJogador[0].Simbolo, Computador.CartasJogador[0].Naipe);
            Console.WriteLine("Carta 2: [Carta virada para baixo]");
            Console.WriteLine("");
            Console.WriteLine("Total: {0}\n", Computador.CartasJogador[0].Valor);

            do
            {
                Console.WriteLine("Escolha uma opção: [(E)Esperar (P)Pedir mais cartas]");
                ConsoleKeyInfo userOption = Console.ReadKey(true);
                Console.WriteLine("");

                while (userOption.Key != ConsoleKey.P && userOption.Key != ConsoleKey.E)
                {
                    Console.WriteLine("Tecla inválida. Escolha uma opção válida: [(E)Esperar (P)Pedir mais cartas]");
                    userOption = Console.ReadKey(true);
                }

                switch (userOption.Key)
                {
                    case ConsoleKey.P:
                        Pessoa.CartasJogador.Add(Baralho.TiraCarta());
                        Console.WriteLine("Você tirou {0} de {1}", Pessoa.CartasJogador[Pessoa.CartasJogador.Count - 1].Simbolo, Pessoa.CartasJogador[Pessoa.CartasJogador.Count - 1].Naipe);
                        int total = Pessoa.SomaPontuacao();
                        Console.WriteLine("Valor total das cartas é: {0}\n", total);
                        if (total > 21)
                        {
                            Console.Write("Você passou dos 21!\n");
                            Computador.Ganha(Pessoa.Perde());
                            return;
                        }
                        else if (total == 21)
                        {
                            Console.WriteLine("Bom Trabalho! Eu suponho que você queira esperar...\n");
                            Thread.Sleep(2000);
                            continue;
                        }
                        else
                        {
                            continue;
                        }

                    case ConsoleKey.E:

                        Computador.MostraCartas();
                        if (Computador.VerificaBlackjack())
                        {
                            Console.WriteLine("O Computador fez um BlackJack!");
                            Computador.Ganha(Pessoa.Perde());
                            return;
                        }
                        int total2 = Computador.SomaPontuacao();
                        while (total2 < 17)
                        {
                            Thread.Sleep(2000);
                            Computador.CartasJogador.Add(Baralho.TiraCarta());
                            total2 = Computador.SomaPontuacao();
                            Console.WriteLine("Carta {0}: {1} de {2}", Computador.CartasJogador.Count, Computador.CartasJogador[Computador.CartasJogador.Count - 1].Simbolo, Computador.CartasJogador[Computador.CartasJogador.Count - 1].Naipe);
                        }
                        Console.WriteLine("Valor total das cartas é: {0}\n", total2);

                        if (total2 > 21)
                        {
                            Console.WriteLine("O Computador passou de 21!");
                            Computador.Perde(Pessoa.Ganha());
                            return;
                        }
                        else
                        {
                            total = Pessoa.SomaPontuacao();
                            total2 = Computador.SomaPontuacao();

                            if (total2 > total)
                            {
                                Console.WriteLine("O Computador possui {0} e o você possui {1}, o Computador venceu!", Computador.Pontuacao, Pessoa.Pontuacao);
                                Computador.Ganha(Pessoa.Perde());
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Você possui {0} e o Computador possui {1}, você venceu!", Pessoa.Pontuacao, Computador.Pontuacao);
                                Computador.Perde(Pessoa.Ganha());
                                return;
                            }
                        }

                    default:
                        break;
                }

                Console.ReadLine();
            }
            while (true);
        }
    }
}