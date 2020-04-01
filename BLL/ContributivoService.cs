using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;

namespace BLL
{
    public class ContributivoService : LiquidacionCuotaModeradoraSevice
    {
        public override double CalcularCuotaModeradora(Liquidacion liquidacion)
        {
            if (liquidacion.Salario < (980000 * 2))
            {
                if ((liquidacion.ValorServicio * 0.15) > 250000)
                {
                    return 250000;
                }
                return liquidacion.ValorServicio * 0.15;
            }
            else if ((liquidacion.Salario > (980000 * 2) && (liquidacion.Salario < (980000 * 5))))
            {
                if ((liquidacion.ValorServicio * 0.2) > 900000)
                {
                    return 900000;
                }
                return liquidacion.ValorServicio * 0.2;
            }
            else
            {
                if ((liquidacion.ValorServicio * 0.25) > 1500000)
                {
                    return 1500000;
                }
                return liquidacion.ValorServicio * 0.25;
            }
        }
    }
}
