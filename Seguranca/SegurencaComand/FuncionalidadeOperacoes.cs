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
    public class FuncionalidadeOperacoes
    {
        ModuloFuncionalidadeController moduloFuncionalidadeController;
        List<ModuloFuncionalidade> listaFuncionalidade;
        ModuloFuncionalidade moduloFuncionalidade;
        Modulo modulo;
        ModuloController moduloController;
        ModuloOperacoes moduloOperacoes;

        public FuncionalidadeOperacoes() { }

        public void SelecionarOperacao(int opcaoSelecionada)
        {
            switch (opcaoSelecionada)
            {
                case 1:
                    ListarFuncionalidades(null);
                    break;
                case 2:
                    CadastrarFuncionalidades();
                    break;
                case 3:
                    EditarFuncionalidade();
                    break;
                default:
                    break;
            }

        }

        public void ListarFuncionalidades(int? moduloCodigo)
        {
            int modCodigo;
            if (moduloCodigo == null)
            {
                
                Console.WriteLine("Digite ou ID do Módulo para listar suas Funcionalidades, ou digite 0 para todos: ");
                while (!int.TryParse(Console.ReadLine(), out modCodigo) || modCodigo < 0)
                {
                    Console.WriteLine("Digite um valor numerico!");
                    Console.WriteLine("Digite ou ID do Módulo para listar suas Funcionalidades, ou digite 0 para todos: ");
                }
                moduloCodigo = modCodigo;
                Console.WriteLine();
            }
  
            listaFuncionalidade =ListaBuscarFuncionalidades((int)moduloCodigo);
            if (listaFuncionalidade.Count == 0)
            {
                Console.WriteLine("O Módulo Escolhido não existe, ou não possui funcionalidades Cadastradas");
            }
            else
            {
                for (int pos = 0; pos < listaFuncionalidade.Count; pos++)
                {
                    Console.WriteLine("ID: " + listaFuncionalidade[pos].ModuloFuncionalidadeCodigo.ToString());
                    Console.WriteLine("Formulário: " + listaFuncionalidade[pos].FormDescricao.ToString());
                    Console.WriteLine("Funcionalidade: " + listaFuncionalidade[pos].FuncionalidadeDescricao.ToString());
                    Console.WriteLine("ID Módulo: " + listaFuncionalidade[pos].ModuloCodigo.ToString());
                    Console.WriteLine("Modulo: " + listaFuncionalidade[pos].Modulo.ModuloDescricao.ToString());
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }

        private List<ModuloFuncionalidade> ListaBuscarFuncionalidades(int moduloCodigo)
        {
            moduloFuncionalidadeController = new ModuloFuncionalidadeController(BancoDeDados.Instance);
            listaFuncionalidade = new List<ModuloFuncionalidade>();
            if (moduloCodigo == 0)
            {
                listaFuncionalidade = moduloFuncionalidadeController.FindAll();
            }
            else
            {
                listaFuncionalidade = moduloFuncionalidadeController.FindAll().Where(m => m.ModuloCodigo == moduloCodigo).ToList();
            }
            return listaFuncionalidade;
        }

        public void CadastrarFuncionalidades()
        {
            int moduloCodigo = 0;
            moduloFuncionalidade = new ModuloFuncionalidade();
            moduloFuncionalidadeController = new ModuloFuncionalidadeController(BancoDeDados.Instance);
            Console.Write("Digite o Descrição do Fórmulário: ");
            moduloFuncionalidade.FormDescricao = Console.ReadLine();
            Console.Write("Digite o Descrição da Funcionalidade: ");
            moduloFuncionalidade.FuncionalidadeDescricao = Console.ReadLine();
            Console.Write("Digite o código do Módulo a qual a funcionalidade irá fazer parte: ");
            while (!int.TryParse(Console.ReadLine(), out moduloCodigo))
            {
                Console.WriteLine("Digite um valor numerico!");
                Console.Write("Digite o código do Módulo a qual a funcionalidade irá fazer parte: ");
            }
                        
            moduloController = new ModuloController(BancoDeDados.Instance);
            moduloOperacoes = new ModuloOperacoes();
            modulo = moduloController.FindByPk(moduloCodigo);
            while (modulo == null)
            {
                Console.WriteLine("ID do modulo desejado não existe!");
                Console.WriteLine("Digite o ID de um módulo existente:");
                moduloCodigo =  moduloOperacoes.BuscarModulo();
                modulo = moduloController.FindByPk(moduloCodigo);
            }

            moduloFuncionalidade.ModuloCodigo = moduloCodigo;
            moduloFuncionalidadeController.Adicionar(moduloFuncionalidade);
            ListarFuncionalidades(moduloCodigo);
        
        }

        public void EditarFuncionalidade()
        {
            int idBusca, idNovoModulo;
            string descricao, nomeFormulario;
            Console.WriteLine("Digite o ID da Funcionalidade a ser Editada:");
            idBusca = BuscarFuncionalidade();
            moduloFuncionalidade = new ModuloFuncionalidade();
            moduloFuncionalidadeController = new ModuloFuncionalidadeController(BancoDeDados.Instance);
            moduloFuncionalidade = moduloFuncionalidadeController.FindByPk(idBusca);


            while (moduloFuncionalidade == null)
            {
                Console.WriteLine("Funcionalidade não existente para ID informado");
                idBusca = BuscarFuncionalidade();
                moduloFuncionalidade = moduloFuncionalidadeController.FindByPk(idBusca);
            }
            

            Console.WriteLine("Digite a nova Descricao: ");
            descricao = Console.ReadLine().ToString();
            moduloFuncionalidade.FuncionalidadeDescricao = descricao;

            Console.WriteLine("Digite o novo Formulario: ");
            nomeFormulario = Console.ReadLine().ToString();
            moduloFuncionalidade.FormDescricao = nomeFormulario;

            Console.Write("Digite o código do Módulo a qual a funcionalidade irá fazer parte: ");
            while (!int.TryParse(Console.ReadLine(), out idNovoModulo))
            {
                Console.WriteLine("Digite um valor numerico!");
                Console.Write("Digite o código do Módulo a qual a funcionalidade irá fazer parte: ");
            }

            moduloController = new ModuloController(BancoDeDados.Instance);
            moduloOperacoes = new ModuloOperacoes();
            modulo = moduloController.FindByPk(idNovoModulo);
            while (modulo == null)
            {
                Console.WriteLine("ID do modulo desejado não existe!");
                Console.WriteLine("Digite o ID de um módulo existente:");
                idNovoModulo = moduloOperacoes.BuscarModulo();
                modulo = moduloController.FindByPk(idNovoModulo);
            }

            moduloFuncionalidade.ModuloCodigo = idNovoModulo;
            moduloFuncionalidadeController.Alterar(moduloFuncionalidade);
            Console.WriteLine("Funcionalidade Alterada");
            ListarFuncionalidades(idNovoModulo);
        }

        private static int BuscarFuncionalidade()
        {
            int idBusca;
            while (!int.TryParse(Console.ReadLine(), out idBusca))
            {
                Console.WriteLine("Digite um valor numerico!");
                Console.WriteLine("Digite o ID da Funcionalidade a ser Editada:");
            }

            return idBusca;
        }
    }
}
