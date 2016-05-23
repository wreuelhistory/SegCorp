using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurancaComand
{
    public class Menu
    {

        ModuloOperacoes moduloOperacoes;
        FuncionalidadeOperacoes funcionalidadeOperacoes;

        public Menu() { }
       
        public int MenuPrincipal()
        {
            int opcaoSelecionada;

            Console.WriteLine("Menu");
            Console.WriteLine("Escolha a opcao");
            Console.WriteLine("1 - Modulos");
            Console.WriteLine("2 - Funcionalidades");
            Console.WriteLine("0 - Sair");
            if (int.TryParse(Console.ReadLine(), out opcaoSelecionada))
            {
                return opcaoSelecionada;
            }
            else
            {
                return -1;
            }
        }

        public void MenuModulos()
        {
            moduloOperacoes = new ModuloOperacoes();
            int qtdMaximaOpcoes = 4;
            int opcaoSelecionada;
            do
            {
                Console.WriteLine("Menu Modulos");
                Console.WriteLine("Escolha a opcao");
                Console.WriteLine("1 - Listar Modulos");
                Console.WriteLine("2 - Cadastrar Modulos");
                Console.WriteLine("3 - Editar Modulos");
                Console.WriteLine("4 - Voltar");
                opcaoSelecionada = SelecionarOpcao(qtdMaximaOpcoes);
                moduloOperacoes.SelecionarOperacao(opcaoSelecionada);
            } while (opcaoSelecionada != qtdMaximaOpcoes);

        }

        public void MenuFuncionalidade()
        {
            funcionalidadeOperacoes = new FuncionalidadeOperacoes();
            int qtdMaximaOpcoes = 4;
            int opcaoSelecionada;
            do
            {
                Console.WriteLine("Menu Funcionalidades");
                Console.WriteLine("Escolha a opcao");
                Console.WriteLine("1 - Listar Todas Funcionalidades");
                Console.WriteLine("2 - Cadastrar Funcionalidade");
                Console.WriteLine("3 - Editar Funcionalidade");
                Console.WriteLine("4 - Voltar");
                opcaoSelecionada = SelecionarOpcao(qtdMaximaOpcoes);
                funcionalidadeOperacoes.SelecionarOperacao(opcaoSelecionada);
            } while (opcaoSelecionada != qtdMaximaOpcoes);

        }

        private static int SelecionarOpcao(int valorMaximo)
        {
            int opcao;
            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                if (opcao >= 0 && opcao <= valorMaximo)
                {
                    return opcao;
                }
                else
                {
                    Console.WriteLine("Escolha uma opcao Valida!");
                    return -1;
                }
            }
            else
            {
                Console.WriteLine("Escolha uma opcao Valida!");
                return -1;
            }
        }

     
    }
}
