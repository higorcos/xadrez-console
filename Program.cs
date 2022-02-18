using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main()
        {

            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();
                while (!partida.terminada)
                {
                    Console.Clear();
                    Tela.imprimeTabuleiro(partida.tab);
                    Console.WriteLine("Origem do ataque:");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                    Console.WriteLine("Destino do ataque:");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                    partida.executarMovimento(origem,destino);

                }
            }
            catch (TabuleiroException E)
            {
                System.Console.WriteLine(E.Message);
            }


        }
    }
}