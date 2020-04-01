using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;

namespace BLL
{
    public class SubsidiadoService : LiquidacionCuotaModeradoraSevice
    {
        public override double CalcularCuotaModeradora(Liquidacion liquidacion)
        {
            if ((liquidacion.ValorServicio * 0.05) > 200000)
            {
                return 200000;
            }

            return liquidacion.ValorServicio * 0.05;
        }
    }
}
