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
            Menu menu = new Menu();
            int opcao = 0;
            do
            {

                opcao = menu.MenuPrincipal();// MenuPrincipal();
                switch (opcao)
                {
                    case 0:
                        break;
                    case 1:
                        menu.MenuModulo();
                        break;
                    case 2:
                        menu.MenuFuncionalidade();
                        break;
                    default:
                        Console.WriteLine("Digite uma opcao válida");
                        break;
                }
               

            } while (opcao != 0);
        }       
    }
}
