using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;
namespace BLL
{
    public abstract class LiquidacionCuotaModeradoraSevice
    {
        Liquidacion liquidacion;
        LiquidacionCuotaModeradoraRepository liquidacionCuotaModeradoraRepository;
        List<Liquidacion> listaLiquidaciones;

        public LiquidacionCuotaModeradoraSevice()
        {
            liquidacionCuotaModeradoraRepository = new LiquidacionCuotaModeradoraRepository();
            Leer();
        }
        public abstract double CalcularCuotaModeradora(Liquidacion liquidacion);
        public Liquidacion Buscar(string Identificacion)
        {
            liquidacion = new Liquidacion();
            liquidacion = liquidacionCuotaModeradoraRepository.Buscar(Identificacion);
            if (liquidacion == null)
            {
                return null;
            }

            return liquidacion;

        }

        public List<Liquidacion> Leer()
        {
            listaLiquidaciones = liquidacionCuotaModeradoraRepository.leerArchivos();
            return listaLiquidaciones;
        }

        public void Guardar(Liquidacion liquidacion)
        {
            listaLiquidaciones.Add(liquidacion);
            liquidacionCuotaModeradoraRepository.GuardarPaciente(listaLiquidaciones);
        }

        public void Modificar(Liquidacion liquidacionVieja, Liquidacion liquidacionNueva)
        {
            liquidacionCuotaModeradoraRepository.Modificar(liquidacionVieja, liquidacionNueva);
        }

        public void Eliminar(Liquidacion liquidacion)
        {
            liquidacionCuotaModeradoraRepository.Eliminar(liquidacion);
        }
    }
}
