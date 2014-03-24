using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var oBLEmresa = new BLEmpresa();

            string codigo = oBLEmresa.grabarEmpresa("","EMPRESA 4");

            Console.WriteLine("Empresa ok: " + codigo);
        }
    }
}
