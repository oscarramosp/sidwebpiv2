using System;
using System.Collections.Generic;
using System.Text;

namespace BLLayer
{
    [Serializable]
    public class DTOResultado
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; }
        public object Objeto { get; set; }
    }
}