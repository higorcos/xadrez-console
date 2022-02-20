using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }
        public void executarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
        }
        public void realizarJogada(Posicao origem, Posicao destino)
        {
            executarMovimento(origem, destino);
            turno++;
            mudarJogador();

        }
        public void validarPosicaoDeOrigem(Posicao pos)
        { //verifica a existência de uma peça na posição para um futuro movimento
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição informada!!!");
            }
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça escolhida não é sua!!!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Essa peça não pode ser movimentada!!!");
            }
        }
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida");
            }
        }
        private void mudarJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux; 
        }
        public void colocarNovaPeca(Peca peca, char coluna, int linha)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }
        private void colocarPecas()
        {

            colocarNovaPeca(new Torre(tab, Cor.Branca), 'c', 1);
            colocarNovaPeca(new Torre(tab, Cor.Branca), 'c', 2);
            colocarNovaPeca(new Rei(tab, Cor.Branca), 'd', 1);
            colocarNovaPeca(new Torre(tab, Cor.Branca), 'd', 2);
            colocarNovaPeca(new Torre(tab, Cor.Branca), 'e', 1);
            colocarNovaPeca(new Torre(tab, Cor.Branca), 'e', 2);

            colocarNovaPeca(new Torre(tab, Cor.Preta), 'c', 8);
            colocarNovaPeca(new Torre(tab, Cor.Preta), 'c', 7);
            colocarNovaPeca(new Rei(tab, Cor.Preta), 'd', 8);
            colocarNovaPeca(new Torre(tab, Cor.Preta), 'd', 7);
            colocarNovaPeca(new Torre(tab, Cor.Preta), 'e', 8);
            colocarNovaPeca(new Torre(tab, Cor.Preta), 'e', 7);


        }
    }
}