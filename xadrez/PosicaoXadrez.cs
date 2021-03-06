using tabuleiro;

namespace xadrez
{
    class PosicaoXadrez
    {
        public char coluna { get; set; }
        public int linha { get; set; }
        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna= coluna;
            this.linha = linha;
        }
        public Posicao toPosicao()
        {
            Posicao toPos = new Posicao(8 - linha,coluna -'a');
            return toPos;
        }
        public override string ToString()
        {
            return $"{coluna}{linha}";
        }

    }
}