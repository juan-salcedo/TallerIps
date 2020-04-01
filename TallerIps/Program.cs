using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using BLL;

namespace TallerIps
{
    class Program
    {
        static Liquidacion liquidacion;
        static LiquidacionCuotaModeradoraSevice liquidacionCuotaModeradoraSevice;
        static List<Liquidacion> liquidaciones;
        static void Main(string[] args)
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("-----IPS Juan Salcedo -----");
                Console.WriteLine("1.Ingresar Datos");
                Console.WriteLine("2.Lista Pacientes");
                Console.WriteLine("3.Buscar Paciente");
                Console.WriteLine("4.Modificar Datos");
                Console.WriteLine("5.Eliminar Paciente");
                Console.WriteLine("6.Salir");
                opcion = Convert.ToInt32(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        VerificarDatos();
                        break;
                    case 2:
                        ListaPaciente();
                        break;
                    case 3:
                        BuscarPaciente();
                        break;
                    case 4:
                        VerificarIdentificacionModificar();
                        break;
                    case 5:
                        Eliminar();
                        break;
                }
            } while (opcion < 6);
        }

        static public void VerificarDatos()
        {
            string Identificacion;
            liquidacion = new Liquidacion();
            Console.Clear();
            Console.WriteLine("Ingrese la identificacion");
            Identificacion = Console.ReadLine();
            Console.WriteLine("Ingrese el tipo de afiliacion");
            liquidacion.TipoAfiliacion = Console.ReadLine().ToUpper();
            if (liquidacion.TipoAfiliacion.Equals("SUBSIDIADO"))
            {
                liquidacionCuotaModeradoraSevice = new SubsidiadoService();
            }
            else if (liquidacion.TipoAfiliacion.Equals("CONTRIBUTIVO"))
            {
                liquidacionCuotaModeradoraSevice = new ContributivoService();
            }
            if (liquidacionCuotaModeradoraSevice.Buscar(Identificacion) == null)
            {
                GuardarDatos(liquidacion, Identificacion);
            }
            else
            {
                Console.WriteLine("Paciente ya registrado");
                Console.ReadKey();
            }
        }

        public static void GuardarDatos(Liquidacion liquidacion, string Identificacion)
        {
            liquidaciones = liquidacionCuotaModeradoraSevice.Leer();
            liquidacion.Identificacion = Identificacion;
            Console.WriteLine("Introduzca su nombre");
            liquidacion.Nombre = Console.ReadLine();
            Console.WriteLine("Introduzca su salario");
            liquidacion.Salario = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Introduzca valor del servicio");
            liquidacion.ValorServicio = Convert.ToDouble(Console.ReadLine());
            liquidacion.NumeroLiquidacion = liquidaciones.Count() + 1;
            liquidacion.CuotaModeradora = liquidacionCuotaModeradoraSevice.CalcularCuotaModeradora(liquidacion);
            liquidacionCuotaModeradoraSevice.Guardar(liquidacion);
            Console.WriteLine("Paciente guardado correctamente");
            Console.ReadKey();


        }

        public static void ListaPaciente()
        {
            liquidacionCuotaModeradoraSevice = new SubsidiadoService();
            liquidaciones = liquidacionCuotaModeradoraSevice.Leer();
            Console.Clear();
            Console.WriteLine("Lista Paciente");
            foreach (var item in liquidaciones)
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine($"Identificacion: {item.Identificacion}");
                Console.WriteLine($"Nombre: {item.Nombre}");
                Console.WriteLine($"Salario: {item.Salario}");
                Console.WriteLine($"Tipo Afiliacion: {item.TipoAfiliacion}");
                Console.WriteLine($"Valor Servicio: {item.ValorServicio}");
                Console.WriteLine($"Numero Liquidacion: {item.NumeroLiquidacion}");
                Console.WriteLine($"Cuota Moderadora: {item.CuotaModeradora}");
                Console.WriteLine("---------------------------------------------------------------------");
            }
            Console.ReadKey();
        }

        public static void BuscarPaciente()
        {
            string Identificacion;
            liquidacionCuotaModeradoraSevice = new SubsidiadoService();
            liquidaciones = liquidacionCuotaModeradoraSevice.Leer();
            Console.Clear();
            Console.WriteLine("Ingrese identificacion del Paciente a buscar");
            Identificacion = Console.ReadLine();
            foreach (var item in liquidaciones)
            {
                if (item.Identificacion.Equals(Identificacion))
                {
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine($"Identificacion: {item.Identificacion}");
                    Console.WriteLine($"Nombre: {item.Nombre}");
                    Console.WriteLine($"Salario: {item.Salario}");
                    Console.WriteLine($"Tipo Afiliacion: {item.TipoAfiliacion}");
                    Console.WriteLine($"Valor Servicio: {item.ValorServicio}");
                    Console.WriteLine($"Numero Liquidacion: {item.NumeroLiquidacion}");
                    Console.WriteLine($"Cuota Moderadora: {item.CuotaModeradora}");
                    Console.WriteLine("---------------------------------------------------------------------");
                }

            }
            Console.ReadKey();
        }

        public static void VerificarIdentificacionModificar()
        {
            Console.Clear();
            liquidacion = new Liquidacion();
            liquidacionCuotaModeradoraSevice = new ContributivoService();
            string Identificacion;
            int opcion;
            Console.WriteLine("Ingrese la identificacion que desee buscar");
            Identificacion = Console.ReadLine();
            if (liquidacionCuotaModeradoraSevice.Buscar(Identificacion) != null)
            {
                liquidacion = liquidacionCuotaModeradoraSevice.Buscar(Identificacion);
                Liquidacion liquidacionVieja = liquidacion;
                do
                {
                    Console.Clear();
                    Console.WriteLine("¿Qué quiere modificar?");
                    Console.WriteLine("1.Nombre");
                    Console.WriteLine("2.Salario");
                    Console.WriteLine("3.Tipo Afiliación");
                    Console.WriteLine("4.Valor Servicio");
                    Console.WriteLine("5.Salir\n");
                    opcion = Convert.ToInt32(Console.ReadLine());
                    liquidacion = PedirDatosModificar(liquidacion, opcion);

                } while (opcion != 5);
                liquidacionCuotaModeradoraSevice.Modificar(liquidacionVieja, liquidacion);
            }
        }

        public static Liquidacion PedirDatosModificar(Liquidacion liquidacion, int opcion)
        {
            switch (opcion)
            {
                case 1:
                    Console.WriteLine("Ingrese el nombre");
                    liquidacion.Nombre = Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Ingrese el salario");
                    liquidacion.Salario = Convert.ToDouble(Console.ReadLine());
                    liquidacion.CuotaModeradora = CalcularCuota(liquidacion);
                    break;
                case 3:
                    Console.WriteLine("Ingrese tipo de afiliacion");
                    liquidacion.TipoAfiliacion = Console.ReadLine();
                    liquidacion.CuotaModeradora = CalcularCuota(liquidacion);
                    break;
                case 4:
                    Console.WriteLine("Ingrese el valor del servicio");
                    liquidacion.ValorServicio = Convert.ToDouble(Console.ReadLine());
                    liquidacion.CuotaModeradora = CalcularCuota(liquidacion);
                    break;
            }
            return liquidacion;
        }

        public static double CalcularCuota(Liquidacion liquidacion)
        {
            if (liquidacion.TipoAfiliacion.Equals("SUBSIDIADO"))
            {
                LiquidacionCuotaModeradoraSevice liquidacionCuotaModeradoraService = new SubsidiadoService();
                return liquidacionCuotaModeradoraService.CalcularCuotaModeradora(liquidacion);
            }
            else if (liquidacion.TipoAfiliacion.Equals("SUBSIDIADO"))
            {
                LiquidacionCuotaModeradoraSevice liquidacionCuotaModeradoraService = new ContributivoService();
                return liquidacionCuotaModeradoraService.CalcularCuotaModeradora(liquidacion);
            }
            return 0;
        }

        public static void Eliminar()
        {
            Console.Clear();
            liquidacion = new Liquidacion();
            Console.WriteLine("Ingrese la identificacion que desea eliminar");
            liquidacionCuotaModeradoraSevice = new SubsidiadoService();
            liquidacion = liquidacionCuotaModeradoraSevice.Buscar(Console.ReadLine());
            if (liquidacion != null)
            {
                liquidacionCuotaModeradoraSevice.Eliminar(liquidacion);
                Console.WriteLine("Paciente eliminado correctamente");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No se ha podido eliminar");
                Console.ReadKey();
            }
        }
    }
}
