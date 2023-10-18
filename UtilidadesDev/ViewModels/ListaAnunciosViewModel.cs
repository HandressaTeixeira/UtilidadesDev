using System;
using System.Collections.Generic;
using System.Linq;
using UtilidadesDev.Mock;

namespace UtilidadesDev.ViewModels
{
    public class AnunciosSomenteLinkViewModel
    {
        /// <summary>
        /// Retorna lista aleatória de anuncios da Amazon por quantidade de anuncios
        /// </summary>
        /// <param name="qtdAnuncios">Quantidade de anuncios. 5 por padrão</param>
        /// <returns></returns>
        public List<string> ListaQuatroItensAmazon(int qtdAnuncios = 5)
        {
            return Anuncios.ListaQuatroItensAmazon().OrderBy(x => Guid.NewGuid()).Take(qtdAnuncios).ToList();
        }

        /// <summary>
        /// Retorna lista aleatória de anuncios da Yllix por quantidade de anuncios
        /// </summary>
        /// <param name="qtdAnuncios">Quantidade de anuncios. 1 por padrão</param>
        /// <returns></returns>
        public List<string> ListaItensYllix(int qtdAnuncios = 1)
        {
            return Anuncios.ListaItensYllix().OrderBy(x => Guid.NewGuid()).Take(qtdAnuncios).ToList();
        }
    }
}
