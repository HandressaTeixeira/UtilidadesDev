using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtilidadesDev.Mock
{
    public static class Nomes
    {
        public static string NomeAleatorio(bool feminino)
        {
            if (feminino)
                return NomeFeminino();

            return NomeMasculino();
        }

        private static string NomeFeminino()
        {
            List<string> lista = new();

            lista.Add("Alice");
            lista.Add("Sophia");
            lista.Add("Helena");
            lista.Add("Valentina");
            lista.Add("Laura");
            lista.Add("Isabella");
            lista.Add("Manuela");
            lista.Add("Júlia");
            lista.Add("Heloísa");
            lista.Add("Luiza");
            lista.Add("Maria Luiza");
            lista.Add("Lorena");
            lista.Add("Lívia");
            lista.Add("Giovanna");
            lista.Add("Maria Eduarda");
            lista.Add("Beatriz");
            lista.Add("Maria Clara");
            lista.Add("Cecília");
            lista.Add("Eloá");
            lista.Add("Lara");
            lista.Add("Maria Júlia");
            lista.Add("Isadora");
            lista.Add("Mariana");
            lista.Add("Emanuelly");
            lista.Add("Ana Júlia");
            lista.Add("Ana Luiza");
            lista.Add("Ana Clara");
            lista.Add("Melissa");
            lista.Add("Yasmin");
            lista.Add("Maria Alice");
            lista.Add("Isabelly");
            lista.Add("Lavínia");
            lista.Add("Esther");
            lista.Add("Sarah");
            lista.Add("Elisa");
            lista.Add("Antonella");
            lista.Add("Rafaela");
            lista.Add("Maria Cecília");
            lista.Add("Liz");
            lista.Add("Marina");
            lista.Add("Nicole");
            lista.Add("Maitê");
            lista.Add("Isis");
            lista.Add("Alícia");
            lista.Add("Luna");
            lista.Add("Rebeca");
            lista.Add("Agatha");
            lista.Add("Letícia");
            lista.Add("Maria");
            lista.Add("Gabriela");
            lista.Add("Ana Laura");
            lista.Add("Catarina");
            lista.Add("Clara");
            lista.Add("Ana Beatriz");
            lista.Add("Vitória");
            lista.Add("Olívia");
            lista.Add("Maria Fernanda");
            lista.Add("Emilly");
            lista.Add("Maria Valentina");
            lista.Add("Milena");
            lista.Add("Maria Helena");
            lista.Add("Bianca");
            lista.Add("Larissa");
            lista.Add("Mirella");
            lista.Add("Maria Flor");
            lista.Add("Allana");
            lista.Add("Ana Sophia");
            lista.Add("Clarice");
            lista.Add("Pietra");
            lista.Add("Maria Vitória");
            lista.Add("Maya");
            lista.Add("Laís");
            lista.Add("Ayla");
            lista.Add("Ana Lívia");
            lista.Add("Eduarda");
            lista.Add("Mariah");
            lista.Add("Stella");
            lista.Add("Ana");
            lista.Add("Gabrielly");
            lista.Add("Sophie");
            lista.Add("Carolina");
            lista.Add("Maria Laura");
            lista.Add("Maria Heloísa");
            lista.Add("Maria Sophia");
            lista.Add("Fernanda");
            lista.Add("Malu");
            lista.Add("Analu");
            lista.Add("Amanda");
            lista.Add("Aurora");
            lista.Add("Maria Isis");
            lista.Add("Louise");
            lista.Add("Heloise");
            lista.Add("Ana Vitória");
            lista.Add("Ana Cecília");
            lista.Add("Ana Liz");
            lista.Add("Joana");
            lista.Add("Luana");
            lista.Add("Antônia");
            lista.Add("Isabel");
            lista.Add("Bruna");

            return lista.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }

        private static string NomeMasculino()
        {
            List<string> lista = new();

            lista.Add("Miguel");
            lista.Add("Arthur");
            lista.Add("Bernardo");
            lista.Add("Heitor");
            lista.Add("Davi");
            lista.Add("Lorenzo");
            lista.Add("Théo");
            lista.Add("Pedro");
            lista.Add("Gabriel");
            lista.Add("Enzo");
            lista.Add("Matheus");
            lista.Add("Lucas");
            lista.Add("Benjamin");
            lista.Add("Nicolas");
            lista.Add("Guilherme");
            lista.Add("Rafael");
            lista.Add("Joaquim");
            lista.Add("Samuel");
            lista.Add("Enzo Gabriel");
            lista.Add("João Miguel");
            lista.Add("Henrique");
            lista.Add("Gustavo");
            lista.Add("Murilo");
            lista.Add("Pedro Henrique");
            lista.Add("Pietro");
            lista.Add("Lucca");
            lista.Add("Felipe");
            lista.Add("João Pedro");
            lista.Add("Isaac");
            lista.Add("Benício");
            lista.Add("Daniel");
            lista.Add("Anthony");
            lista.Add("Leonardo");
            lista.Add("Davi Lucca");
            lista.Add("Bryan");
            lista.Add("Eduardo");
            lista.Add("João Lucas");
            lista.Add("Victor");
            lista.Add("João");
            lista.Add("Cauã");
            lista.Add("Antônio");
            lista.Add("Vicente");
            lista.Add("Caleb");
            lista.Add("Gael");
            lista.Add("Bento");
            lista.Add("Caio");
            lista.Add("Emanuel");
            lista.Add("Vinícius");
            lista.Add("João Guilherme");
            lista.Add("Davi Lucas");
            lista.Add("Noah");
            lista.Add("João Gabriel");
            lista.Add("João Victor");
            lista.Add("Luiz Miguel");
            lista.Add("Francisco");
            lista.Add("Kaique");
            lista.Add("Otávio");
            lista.Add("Augusto");
            lista.Add("Levi");
            lista.Add("Yuri");
            lista.Add("Enrico");
            lista.Add("Thiago");
            lista.Add("Ian");
            lista.Add("Victor Hugo");
            lista.Add("Thomas");
            lista.Add("Henry");
            lista.Add("Luiz Felipe");
            lista.Add("Ryan");
            lista.Add("Arthur Miguel");
            lista.Add("Davi Luiz");
            lista.Add("Nathan");
            lista.Add("Pedro Lucas");
            lista.Add("Davi Miguel");
            lista.Add("Raul");
            lista.Add("Pedro Miguel");
            lista.Add("Luiz Henrique");
            lista.Add("Luan");
            lista.Add("Erick");
            lista.Add("Martin");
            lista.Add("Bruno");
            lista.Add("Rodrigo");
            lista.Add("Luiz Gustavo");
            lista.Add("Arthur Gabriel");
            lista.Add("Breno");
            lista.Add("Kauê");
            lista.Add("Enzo Miguel");
            lista.Add("Fernando");
            lista.Add("Arthur Henrique");
            lista.Add("Luiz Otávio");
            lista.Add("Carlos Eduardo");
            lista.Add("Tomás");
            lista.Add("Lucas Gabriel");
            lista.Add("André");
            lista.Add("José");
            lista.Add("Yago");
            lista.Add("Danilo");
            lista.Add("Anthony Gabriel");
            lista.Add("Ruan");
            lista.Add("Miguel Henrique");
            lista.Add("Oliver");

            return lista.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }
    }
}
