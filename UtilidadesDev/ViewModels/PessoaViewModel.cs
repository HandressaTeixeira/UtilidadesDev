using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtilidadesDev.ViewModels
{
    public class PessoaViewModel
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }

        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
