using Seguranca.Controller;
using Seguranca.Controller.model;
using Seguranca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurancaComand
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int opcao = 0;
            do
            {
                opcao = MenuPrincipal();
                switch (opcao)
                {
                    case 0:
                        break;
                    case 1:
                        MenuModulos();

                            break;
                    default:
                        break;
                }
               

            } while (opcao != 0);
        } 

        private static int MenuPrincipal()
        {
            int opcaoSelecionada;

            Console.WriteLine("Menu");
            Console.WriteLine("Escolha a opcao");
            Console.WriteLine("1 - Modulos");
            Console.WriteLine("2 - Funcionalidades");
            Console.WriteLine("0 - Sair");
            if (int.TryParse(Console.ReadLine(), out opcaoSelecionada)){
                return opcaoSelecionada;
            }
            else
            {
                return -1;
            }
        }

        private static void MenuModulos()
        {
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
                OperacoesModulos(opcaoSelecionada); 
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

        private static void OperacoesModulos(int opcaoSelecionada)
        {
            switch (opcaoSelecionada)
            {
                case 1:
                    ListarModulos();
                    break;
                case 2:
                    CadastrarModulos();
                    break;
                case 3:
                    EditarModulos();
                    break;

                default:
                    break;
            }

        }

        private static void ListarModulos() {
            ModuloController moduloController = new ModuloController(BancoDeDados.Instance);
            List<Modulo> listaModulos;
            listaModulos = new List<Modulo>();
            listaModulos = moduloController.findAll();
            for (int pos = 0; pos < listaModulos.Count; pos++)
            {
                Console.WriteLine("ID: " + listaModulos[pos].ModuloCodigo.ToString() + " Descricao: " + listaModulos[pos].ModuloDescricao.ToString());
            }
            moduloController = null;
            listaModulos = null;
        }

        private static void CadastrarModulos()
        {
            Modulo modulo = new Modulo();
            ModuloController moduloController = new ModuloController(BancoDeDados.Instance);
            Console.Write("Digite o nome do Modulo: ");
            modulo.ModuloDescricao = Console.ReadLine();
            moduloController.Adicionar(modulo);
            ListarModulos();
            modulo = null;
            moduloController = null;
        }

        private static void EditarModulos()
        {
            int idBusca;
            string descricao;
            Console.WriteLine("Digite o ID do Modulo a ser Editado:");
            while (!int.TryParse(Console.ReadLine(), out idBusca))
            {
                Console.WriteLine("Digite um valor numerico!");
                Console.WriteLine("Digite o ID do Modulo a ser Editado:");
            }
            Modulo modulo = new Modulo();
            ModuloController moduloController = new ModuloController(BancoDeDados.Instance);
            modulo = moduloController.FindByPk(idBusca);
            Console.WriteLine("Digite a nova Descricao: ");
            descricao = Console.ReadLine().ToString();
            modulo.ModuloDescricao = descricao;
            moduloController.alterar(modulo);
            Console.WriteLine("Descricao Alterada");
            moduloController = null;
            modulo = null;
            ListarModulos();
        }
    }
}
