using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Liquidacion : Paciente
    {
        public int NumeroLiquidacion { get; set; }
        public double CuotaModeradora { get; set; }
        public double ValorServicio { get; set; }

    }
}
