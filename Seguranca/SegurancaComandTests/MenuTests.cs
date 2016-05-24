using Microsoft.VisualStudio.TestTools.UnitTesting;
using SegurancaComand;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurancaComand.Tests
{
    
    [TestClass()]
    public class MenuTests
    {
        [TestMethod()]
        public void MenuPrincipalTest()
        {
            Menu menu = new Menu();
            int ret;
            Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject pobject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(menu);
            ret= menu.MenuPrincipal();
            var sr = new StringReader("1");
            Console.SetIn(sr);
            Console.WriteLine(ret);
            
        }
    }
}
