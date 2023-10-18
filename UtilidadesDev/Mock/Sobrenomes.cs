using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtilidadesDev.Mock
{
    public static class Sobrenomes
    {
        private static string NomeMeioAleatorio()
        {
            List<string> lista = new();


            return lista.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }

        private static string SobrenomeAleatorio()
        {
            List<string> lista = new();


            return lista.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }
    }
}
