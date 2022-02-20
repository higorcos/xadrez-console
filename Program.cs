using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] arg)
        {
 
     try{
                PartidaDeXadrez partida = new PartidaDeXadrez();
                while (!partida.terminada){
                     Console.Clear();
                    Tela.imprimeTabuleiro(partida.tab);
                     
                   
                     Console.WriteLine("Origem do ataque:");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();

                    bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();
                    //movimento
                     Console.Clear();
                    Tela.imprimeTabuleiro(partida.tab,posicoesPossiveis);


                    Console.WriteLine("Destino do ataque:");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                    partida.executarMovimento(origem,destino);   
                     }
     }catch(TabuleiroException e){
         System.Console.WriteLine(e);
     }
           

 
        }
    }
}