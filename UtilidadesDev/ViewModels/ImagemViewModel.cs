using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UtilidadesDev.ViewModels
{
    public class ImagemViewModel
    {
        [HiddenInput]
        public int tamCor1 { get; set; }

        [HiddenInput]
        public int tamCor2 { get; set; }

        [HiddenInput]
        public string brush1 { get; set; }

        [HiddenInput]
        public string brush2 { get; set; }

        [HiddenInput]
        public byte[] imagem { get; set; }
    }
}
