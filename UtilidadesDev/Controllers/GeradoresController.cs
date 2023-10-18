using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using UtilidadesDev.Util;
using UtilidadesDev.ViewModels;

namespace UtilidadesDev.Controllers
{
    public class GeradoresController : Controller
    {

        #region QRCode

        [Route("/GerarQRCode")]
        [HttpGet]
        public IActionResult QRCode()
        {
            return View(new QRCodeViewModel());
        }

        [Route("/GerarQRCode")]
        [HttpPost]
        public IActionResult QRCode(QRCodeViewModel model)
        {
            try
            {
                return PartialView("/Views/Shared/Componentes/_Imagem.cshtml", GerarQRCode(model));
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult BaixarQRCode(int tamanho, string texto)
        {
            var bytes = GerarQRCode(new QRCodeViewModel { Tamanho = tamanho, Texto = texto });
            return File(bytes, "application/jpeg", System.Guid.NewGuid().ToString() + ".jpeg");
        }


        private byte[] GerarQRCode(QRCodeViewModel model)
        {
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(Geral.SemAcento(model.Texto), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(model.Tamanho.Value);

            return Geral.BitmapToBytes(qrCodeImage);
        }

        #endregion

        #region CPF

        [Route("/GerarCPF")]
        [HttpGet]
        public IActionResult CPF()
        {
            return View();
        }

        [Route("/GerarCPF")]
        [HttpPost]
        public IActionResult GerarCPF()
        {
            var cpf = GerarCpf();
            return Ok(new { cpfCMasc = Geral.FormatarCpf(cpf), cpfSMasc = cpf });
        }

        // Fonte: https://pt.stackoverflow.com/a/33685/26670
        private static String GerarCpf()
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            return semente;
        }

        #endregion

        #region Guid

        [Route("/GerarGuid")]
        [HttpGet]
        public IActionResult Guid()
        {
            return View();
        }

        #endregion

        #region LoremIpsum

        [Route("/GerarLoremipsum")]
        [HttpGet]
        public IActionResult LoremIpsum()
        {
            return View();
        }

        [Route("/GerarLoremipsum")]
        [HttpPost]
        public IActionResult LoremIpsum(LorenpsuViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                    model.Resultado = GerarLoremIpsum(model);

                return PartialView("/Views/Shared/Componentes/_Editor.cshtml", model.Resultado);
            }
            catch
            {
                return Json(new { status = false });
            }
        }

        private static string GerarLoremIpsum(LorenpsuViewModel model)
        {

            var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
            };

            var Rand = new Random();
            int numSentences = Rand.Next(model.MaxFrases - model.MinFrases)
                + model.MinFrases + 1;
            int numWords = Rand.Next(model.MaxPalavras - model.MinPalavras) + model.MinPalavras + 1;

            StringBuilder result = new();

            for (int p = 0; p < model.NumParagrafos; p++)
            {
                result.Append("<p>");
                for (int s = 0; s < numSentences; s++)
                {
                    for (int w = 0; w < numWords; w++)
                    {
                        if (w > 0) { result.Append(' '); }
                        result.Append(words[Rand.Next(words.Length)]);
                    }
                    result.Append(". ");
                }
                result.Append("</p>");
            }

            return result.ToString();
        }

        #endregion

        #region Imagem

        [Route("/GerarImagem")]
        [HttpGet]
        public IActionResult Imagem()
        {
            return View(GerarImagem(null));
        }

        private static ImagemViewModel GerarImagem(ImagemViewModel model)
        {
            Bitmap imagem = new(700, 300);
            Graphics flagGraphics = Graphics.FromImage(imagem);


            var lista = ListaBrush();
            Brush brush1;
            Brush brush2;
            int tamCor1 = 3;
            int tamCor2 = 11;

            if (model == null)
            {
                model = new ImagemViewModel();
                //    Random randNum = new();
                //    model.tamCor1 = randNum.Next(2, 20);
                //    model.tamCor2 = randNum.Next(5, 20);
            }

            //tamCor1 = model.tamCor1;
            //tamCor2 = model.tamCor2;

            model.tamCor1 = tamCor1;
            model.tamCor2 = tamCor2;

            if (!string.IsNullOrWhiteSpace(model.brush1) && !string.IsNullOrWhiteSpace(model.brush2))
            {
                brush1 = lista.FirstOrDefault(x => JsonSerializer.Serialize(x.Clone()) == model.brush1);
                brush2 = lista.FirstOrDefault(x => JsonSerializer.Serialize(x.Clone()) == model.brush2);
            }
            else
            {
                brush1 = lista.OrderBy(x => System.Guid.NewGuid()).FirstOrDefault();
                brush2 = lista.OrderBy(x => System.Guid.NewGuid()).FirstOrDefault();
            }
            while (tamCor2 <= 700)
            {
                flagGraphics.FillRectangle(brush1, tamCor1, 0, 700, 100);
                flagGraphics.FillRectangle(brush2, tamCor2, 0, 700, 10);

                tamCor1 += 10;
                tamCor2 += 10;
            }


            tamCor1 = model.tamCor1;
            tamCor2 = model.tamCor2;


            while (tamCor2 <= 300)
            {
                flagGraphics.FillRectangle(brush1, 0, tamCor1, 700, 100);
                flagGraphics.FillRectangle(brush2, 0, tamCor2, 700, 10);

                tamCor1 += 10;
                tamCor2 += 10;
            }

            var tamFinalCor1 = tamCor1 - 10;
            var tamFinalCor2 = tamCor2 - 10;

            tamCor1 = model.tamCor1;
            tamCor2 = model.tamCor2;

            while (tamCor2 <= 700)
            {
                flagGraphics.FillRectangle(brush1, tamCor1, tamFinalCor1, 700, 100);
                flagGraphics.FillRectangle(brush2, tamCor2, tamFinalCor2, 700, 10);

                tamCor1 += 10;
                tamCor2 += 10;
            }


            //flagGraphics.DrawArc(new Pen(Color.Red, 3), 90, 235, 150, 50, 0, 360);
            var font = new Font("Rockwell Extra Bold", 10, FontStyle.Regular);
            flagGraphics.DrawString("www.utilidadesdev.com.br/GerarImagem", font, Brushes.DarkSlateBlue, 16, 16);
            model.imagem = Geral.BitmapToBytes(imagem);
            model.brush1 = JsonSerializer.Serialize(brush1.Clone());
            model.brush2 = JsonSerializer.Serialize(brush2.Clone());

            return model;
        }


        [HttpGet]
        public IActionResult BaixarImagem(ImagemViewModel model)
        {
            //var bytes = System.Convert.FromBase64String(String.Format("data:image/png;base64,{0}", Convert.ToBase64String(GerarImagem(model).imagem)));
            var bytes = GerarImagem(model).imagem;
            return File(bytes, "application/jpeg", System.Guid.NewGuid().ToString() + ".jpeg");
        }

        private static List<Brush> ListaBrush()
        {
            List<Brush> lista = new();
            lista.Add(Brushes.AliceBlue);
            lista.Add(Brushes.PaleGoldenrod);
            lista.Add(Brushes.Orchid);
            lista.Add(Brushes.OrangeRed);
            lista.Add(Brushes.Orange);
            lista.Add(Brushes.OliveDrab);
            lista.Add(Brushes.Olive);
            lista.Add(Brushes.OldLace);
            lista.Add(Brushes.Navy);
            lista.Add(Brushes.NavajoWhite);
            lista.Add(Brushes.Moccasin);
            lista.Add(Brushes.MistyRose);
            lista.Add(Brushes.MintCream);
            lista.Add(Brushes.MidnightBlue);
            lista.Add(Brushes.MediumVioletRed);
            lista.Add(Brushes.MediumTurquoise);
            lista.Add(Brushes.MediumSpringGreen);
            lista.Add(Brushes.MediumSlateBlue);
            lista.Add(Brushes.LightSkyBlue);
            lista.Add(Brushes.LightSlateGray);
            lista.Add(Brushes.LightSteelBlue);
            lista.Add(Brushes.LightYellow);
            lista.Add(Brushes.Lime);
            lista.Add(Brushes.LimeGreen);
            lista.Add(Brushes.PaleGreen);
            lista.Add(Brushes.Linen);
            lista.Add(Brushes.Maroon);
            lista.Add(Brushes.MediumAquamarine);
            lista.Add(Brushes.MediumBlue);
            lista.Add(Brushes.MediumOrchid);
            lista.Add(Brushes.MediumPurple);
            lista.Add(Brushes.MediumSeaGreen);
            lista.Add(Brushes.Magenta);
            lista.Add(Brushes.PaleTurquoise);
            lista.Add(Brushes.PaleVioletRed);
            lista.Add(Brushes.PapayaWhip);
            lista.Add(Brushes.SlateGray);
            lista.Add(Brushes.Snow);
            lista.Add(Brushes.SpringGreen);
            lista.Add(Brushes.SteelBlue);
            lista.Add(Brushes.Tan);
            lista.Add(Brushes.Teal);
            lista.Add(Brushes.SlateBlue);
            lista.Add(Brushes.Thistle);
            lista.Add(Brushes.Transparent);
            lista.Add(Brushes.Turquoise);
            lista.Add(Brushes.Violet);
            lista.Add(Brushes.Wheat);
            lista.Add(Brushes.White);
            lista.Add(Brushes.WhiteSmoke);
            lista.Add(Brushes.Tomato);
            lista.Add(Brushes.LightSeaGreen);
            lista.Add(Brushes.SkyBlue);
            lista.Add(Brushes.Sienna);
            lista.Add(Brushes.PeachPuff);
            lista.Add(Brushes.Peru);
            lista.Add(Brushes.Pink);
            lista.Add(Brushes.Plum);
            lista.Add(Brushes.PowderBlue);
            lista.Add(Brushes.Purple);
            lista.Add(Brushes.Silver);
            lista.Add(Brushes.Red);
            lista.Add(Brushes.RoyalBlue);
            lista.Add(Brushes.SaddleBrown);
            lista.Add(Brushes.Salmon);
            lista.Add(Brushes.SandyBrown);
            lista.Add(Brushes.SeaGreen);
            lista.Add(Brushes.SeaShell);
            lista.Add(Brushes.RosyBrown);
            lista.Add(Brushes.Yellow);
            lista.Add(Brushes.LightSalmon);
            lista.Add(Brushes.LightGreen);
            lista.Add(Brushes.DarkRed);
            lista.Add(Brushes.DarkOrchid);
            lista.Add(Brushes.DarkOrange);
            lista.Add(Brushes.DarkOliveGreen);
            lista.Add(Brushes.DarkMagenta);
            lista.Add(Brushes.DarkKhaki);
            lista.Add(Brushes.DarkGreen);
            lista.Add(Brushes.DarkGray);
            lista.Add(Brushes.DarkGoldenrod);
            lista.Add(Brushes.DarkCyan);
            lista.Add(Brushes.DarkBlue);
            lista.Add(Brushes.Cyan);
            lista.Add(Brushes.Crimson);
            lista.Add(Brushes.Cornsilk);
            lista.Add(Brushes.CornflowerBlue);
            lista.Add(Brushes.Coral);
            lista.Add(Brushes.Chocolate);
            lista.Add(Brushes.AntiqueWhite);
            lista.Add(Brushes.Aqua);
            lista.Add(Brushes.Aquamarine);
            lista.Add(Brushes.Azure);
            lista.Add(Brushes.Beige);
            lista.Add(Brushes.Bisque);
            lista.Add(Brushes.DarkSalmon);
            lista.Add(Brushes.Black);
            lista.Add(Brushes.Blue);
            lista.Add(Brushes.BlueViolet);
            lista.Add(Brushes.Brown);
            lista.Add(Brushes.BurlyWood);
            lista.Add(Brushes.CadetBlue);
            lista.Add(Brushes.Chartreuse);
            lista.Add(Brushes.BlanchedAlmond);
            lista.Add(Brushes.DarkSeaGreen);
            lista.Add(Brushes.DarkSlateBlue);
            lista.Add(Brushes.DarkSlateGray);
            lista.Add(Brushes.HotPink);
            lista.Add(Brushes.IndianRed);
            lista.Add(Brushes.Indigo);
            lista.Add(Brushes.Ivory);
            lista.Add(Brushes.Khaki);
            lista.Add(Brushes.Lavender);
            lista.Add(Brushes.Honeydew);
            lista.Add(Brushes.LavenderBlush);
            lista.Add(Brushes.LemonChiffon);
            lista.Add(Brushes.LightBlue);
            lista.Add(Brushes.LightCoral);
            lista.Add(Brushes.LightCyan);
            lista.Add(Brushes.LightGoldenrodYellow);
            lista.Add(Brushes.LightGray);
            lista.Add(Brushes.LawnGreen);
            lista.Add(Brushes.LightPink);
            lista.Add(Brushes.GreenYellow);
            lista.Add(Brushes.Gray);
            lista.Add(Brushes.DarkTurquoise);
            lista.Add(Brushes.DarkViolet);
            lista.Add(Brushes.DeepPink);
            lista.Add(Brushes.DeepSkyBlue);
            lista.Add(Brushes.DimGray);
            lista.Add(Brushes.DodgerBlue);
            lista.Add(Brushes.Green);
            lista.Add(Brushes.Firebrick);
            lista.Add(Brushes.ForestGreen);
            lista.Add(Brushes.Fuchsia);
            lista.Add(Brushes.Gainsboro);
            lista.Add(Brushes.GhostWhite);
            lista.Add(Brushes.Gold);
            lista.Add(Brushes.Goldenrod);
            lista.Add(Brushes.FloralWhite);
            lista.Add(Brushes.YellowGreen);

            return lista;
        }
        #endregion

        #region Números Aleatórios

        [Route("/GerarNumerosAleatorios")]
        [HttpGet]
        public IActionResult NumerosAleatorios()
        {
            return View();
        }

        [Route("/GerarNumerosAleatorios")]
        [HttpPost]
        public IActionResult NumerosAleatorios(NumerosAleatoriosViewModel model)
        {
            var randomico = new Random();
            List<string> listaCombinacoes = new();
            List<int> listaNumeros = new();
            for (var x = model.NumeroInicial; x <= model.NumeroFinal; x++)
                listaNumeros.Add(x);

            for (var x = 0; x < model.QuantidadeCombinacoes; x++)
            {
                List<int> numerosSortado = new();
                string combinacao = null;

                for (var y = 0; y < model.QuantidadeNumerosNaCombinacao; y++)
                {
                    var numeroSortado = listaNumeros.OrderBy(x => randomico.Next()).FirstOrDefault();

                    while (numerosSortado.Any(x => x == numeroSortado))
                    {
                        numeroSortado = listaNumeros.OrderBy(x => randomico.Next()).FirstOrDefault();
                    }
                    numerosSortado.Add(numeroSortado);

                }

                foreach (var numero in numerosSortado.OrderBy(x => x))
                    combinacao += numero + ", ";

                listaCombinacoes.Add(combinacao.Remove(combinacao.Length - 2, 2));
            }

            model.ListaCombinacoes = listaCombinacoes;

            return View(model);
        }
        #endregion

        #region Senha

        [Route("/GerarSenha")]
        [HttpGet]
        public IActionResult Senha()
        {
            return View();
        }


        [Route("/GerarSenha")]
        [HttpPost]
        public IActionResult Senha(SenhaViewModel model)
        {
            var randomico = new Random();
            List<string> listaCombinacoes = new();

            List<string> listaCaracteres = GerarListaCaracteres();

            List<string> caracteresSorteados = new();

            for (var x = 0; x < model.QuantidadeCaracteres; x++)
            {
                var caracterSorteado = listaCaracteres.OrderBy(x => randomico.Next()).FirstOrDefault();

                while (caracteresSorteados.Any(x => x.ToUpper() == caracterSorteado.ToUpper()))
                    caracterSorteado = listaCaracteres.OrderBy(x => randomico.Next()).FirstOrDefault();

                model.Resultado += caracterSorteado;

            }
            return View(model);
        }

        private List<string> GerarListaCaracteres()
        {
            List<string> listaCaracteres = new();
            for (var x = 0; x <= 9; x++)
                listaCaracteres.Add(x.ToString());

            listaCaracteres.Add("a");
            listaCaracteres.Add("b");
            listaCaracteres.Add("c");
            listaCaracteres.Add("d");
            listaCaracteres.Add("e");
            listaCaracteres.Add("f");
            listaCaracteres.Add("g");
            listaCaracteres.Add("h");
            listaCaracteres.Add("i");
            listaCaracteres.Add("j");
            listaCaracteres.Add("k");
            listaCaracteres.Add("l");
            listaCaracteres.Add("m");
            listaCaracteres.Add("n");
            listaCaracteres.Add("o");
            listaCaracteres.Add("p");
            listaCaracteres.Add("q");
            listaCaracteres.Add("r");
            listaCaracteres.Add("s");
            listaCaracteres.Add("t");
            listaCaracteres.Add("u");
            listaCaracteres.Add("v");
            listaCaracteres.Add("x");
            listaCaracteres.Add("z");
            listaCaracteres.Add("w");
            listaCaracteres.Add("y");

            listaCaracteres.Add("A");
            listaCaracteres.Add("B");
            listaCaracteres.Add("C");
            listaCaracteres.Add("D");
            listaCaracteres.Add("E");
            listaCaracteres.Add("F");
            listaCaracteres.Add("G");
            listaCaracteres.Add("H");
            listaCaracteres.Add("I");
            listaCaracteres.Add("J");
            listaCaracteres.Add("K");
            listaCaracteres.Add("L");
            listaCaracteres.Add("M");
            listaCaracteres.Add("N");
            listaCaracteres.Add("O");
            listaCaracteres.Add("P");
            listaCaracteres.Add("Q");
            listaCaracteres.Add("R");
            listaCaracteres.Add("S");
            listaCaracteres.Add("T");
            listaCaracteres.Add("U");
            listaCaracteres.Add("V");
            listaCaracteres.Add("X");
            listaCaracteres.Add("Z");
            listaCaracteres.Add("W");
            listaCaracteres.Add("Y");

            listaCaracteres.Add("@");
            listaCaracteres.Add("#");
            listaCaracteres.Add("$");
            listaCaracteres.Add("&");
            listaCaracteres.Add("*");
            listaCaracteres.Add("!");
            listaCaracteres.Add(".");
            listaCaracteres.Add("+");
            listaCaracteres.Add("?");

            return listaCaracteres;
        }
        #endregion
    }
}
