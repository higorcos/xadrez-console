using System;
using tabuleiro;
using xadrez;

namespace xadrez_console{
    class Program{
        static void Main(){
            
try{
            Tabuleiro tab = new Tabuleiro(8,8);

            tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0,0));
            tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1,9));
            tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(2,4));
            Tela.imprimeTabuleiro(tab);
}catch(TabuleiroException E){
    System.Console.WriteLine(E.Message);

}

           
        }
    }
}