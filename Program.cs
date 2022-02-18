using System;
using tabuleiro;

namespace xadrez_console{
    class Program{
        static void Main(){
            
            Tabuleiro A = new Tabuleiro(8,8);

            System.Console.WriteLine(A);
        }
    }
}