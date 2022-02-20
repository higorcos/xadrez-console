using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] arg)
        {

            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();
                while (!partida.terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.imprimirPartida(partida);


                        Console.WriteLine("Origem do ataque:");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();

                        //validação
                        partida.validarPosicaoDeOrigem(origem);
                        //verificação das possibilidades
                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();
                        //movimento
                        Console.Clear();
                        Tela.imprimeTabuleiro(partida.tab, posicoesPossiveis);


                        Console.WriteLine("Destino do ataque:");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        //validação
                        partida.validarPosicaoDeDestino(origem,destino);
                        //aplica jogada
                        partida.realizarJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();

                    }
                }
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }



        }
    }
}