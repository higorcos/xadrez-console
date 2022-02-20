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
        public bool xeque { get; private set; }


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            xeque = false;
            colocarPecas();
        }
        public Peca executarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }
        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQteMovimentos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
            /* 
                        // #jogadaespecial roque pequeno
                        if (p is Rei && destino.coluna == origem.coluna + 2)
                        {
                            Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                            Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                            Peca T = tab.retirarPeca(destinoT);
                            T.decrementarQteMovimentos();
                            tab.colocarPeca(T, origemT);
                        }

                        // #jogadaespecial roque grande
                        if (p is Rei && destino.coluna == origem.coluna - 2)
                        {
                            Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                            Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                            Peca T = tab.retirarPeca(destinoT);
                            T.decrementarQteMovimentos();
                            tab.colocarPeca(T, origemT);
                        }

                        // #jogadaespecial en passant
                        if (p is Peao)
                        {
                            if (origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant)
                            {
                                Peca peao = tab.retirarPeca(destino);
                                Posicao posP;
                                if (p.cor == Cor.Branca)
                                {
                                    posP = new Posicao(3, destino.coluna);
                                }
                                else
                                {
                                    posP = new Posicao(4, destino.coluna);
                                }
                                tab.colocarPeca(peao, posP);
                            }
                        } */
        }

        public void realizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executarMovimento(origem, destino);
            if (emXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }
            if (emXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if (testeXequemate(adversaria(jogadorAtual))) { 
                terminada = true;
            }
            else {
                turno++;
                mudarJogador();
            }
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
            if (!tab.peca(origem).movimentoPossivel(destino))
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
        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }
        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }
        public bool emXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException($"Não tem rei {cor} !!");
            }
            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }
        public bool testeXequemate(Cor cor)
        {
            if (!emXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executarMovimento(origem, destino);
                            bool testeXeque = emXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
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