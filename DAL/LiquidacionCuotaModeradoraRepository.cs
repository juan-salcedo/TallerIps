using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
namespace DAL
{
    public class LiquidacionCuotaModeradoraRepository
    {
        private string ruta = "Liquidacion.txt";
        public List<Liquidacion> liquidaciones;
        public LiquidacionCuotaModeradoraRepository()
        {
            liquidaciones = new List<Liquidacion>();
        }

        public void GuardarPaciente(List<Liquidacion> listaLiquidaciones)
        {
            File.Delete(ruta);
            FileStream file = new FileStream(ruta, FileMode.Append);
            StreamWriter escritor = new StreamWriter(file);
            foreach (var item in listaLiquidaciones)
            {
                escritor.WriteLine($"{item.Identificacion};{item.Nombre};{item.Salario};{item.TipoAfiliacion};" +
                    $"{item.ValorServicio};{item.NumeroLiquidacion};{item.CuotaModeradora}");
            }
            escritor.Close();
            file.Close();
        }

        public List<Liquidacion> leerArchivos()
        {
            liquidaciones = new List<Liquidacion>();
            string linea = string.Empty;
            FileStream sourceStream = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(sourceStream);
            while ((linea = reader.ReadLine()) != null)
            {
                Liquidacion liquidacion = new Liquidacion();
                char delimitador = ';';
                string[] Matriz = linea.Split(delimitador);
                liquidacion.Identificacion = Matriz[0];
                liquidacion.Nombre = Matriz[1];
                liquidacion.Salario = Convert.ToDouble(Matriz[2]);
                liquidacion.TipoAfiliacion = Matriz[3];
                liquidacion.ValorServicio = Convert.ToDouble(Matriz[4]);
                liquidacion.NumeroLiquidacion = Convert.ToInt32(Matriz[5]);
                liquidacion.CuotaModeradora = Convert.ToDouble(Matriz[6]);
                liquidaciones.Add(liquidacion);
            }
            reader.Close();
            sourceStream.Close();
            return liquidaciones;
        }

        public Liquidacion Buscar(string Identificacion)
        {
            liquidaciones = leerArchivos();
            foreach (var item in liquidaciones)
            {
                if (item.Identificacion.Equals(Identificacion))
                {
                    return item;
                }
            }
            return null;
        }

        public void Modificar(Liquidacion liquidacionVieja, Liquidacion liquidacionNueva)
        {
            liquidaciones.Remove(liquidacionVieja);
            liquidaciones.Add(liquidacionNueva);
            GuardarPaciente(liquidaciones);
        }

        public void Eliminar(Liquidacion liquidacion)
        {
            liquidaciones.Remove(liquidacion);
            GuardarPaciente(liquidaciones);
        }
    }
    }
