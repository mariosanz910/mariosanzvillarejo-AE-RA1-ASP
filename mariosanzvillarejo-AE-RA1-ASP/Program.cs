// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

namespace ra1 {
    using System;

    namespace BibliotecaDigital
    {
        // Clase abstracta (no se puede crear directamente, solo heredar) 
        abstract class Contenido
        {
            // Atributos (encapsulados) 
            private string titulo;
            private double duracion; // en minutos 

            // Propiedades 
            public string Titulo
            {
                get { return titulo; }
                set { titulo = value; }
            }

            public double Duracion
            {
                get { return duracion; }
                set
                {
                    // Aseguramos que la duración sea mayor o igual a 1 
                    if (value < 1)
                        duracion = 1;
                    else
                        duracion = value;
                }
            }

            // Constructor 
            public Contenido(string titulo, double duracion)
            {
                Titulo = titulo;
                Duracion = duracion;
            }

            // Métodos abstractos (deben implementarse en las clases hijas) 
            public abstract double ObtenerTasaCompresion();
            public abstract string Describir();
            public abstract void Reproducir();
        }
    }


    namespace BibliotecaDigital
    {
        // Clase abstracta que hereda de Contenido 
        abstract class ContenidoVisual : Contenido
        {
            // Atributo 
            private string resolucion;

            // Propiedad 
            public string Resolucion
            {
                get { return resolucion; }
                set { resolucion = value; }
            }

            // Constructor 
            public ContenidoVisual(string titulo, double duracion, string resolucion)
                : base(titulo, duracion)
            {
                Resolucion = resolucion;
            }

            // Método que calcula la tasa de compresión 
            public override double ObtenerTasaCompresion()
            {
                if (Resolucion == "4K")
                    return 0.6;
                else
                    return 0.9;
            }
        }
    }
    namespace BibliotecaDigital
    {
        // Clase abstracta que hereda de Contenido 
        abstract class ContenidoAuditivo : Contenido
        {
            // Atributo 
            private int bitrate;

            // Propiedad 
            public int Bitrate
            {
                get { return bitrate; }
                set { bitrate = value; }
            }

            // Constructor 
            public ContenidoAuditivo(string titulo, double duracion, int bitrate)
                : base(titulo, duracion)
            {
                Bitrate = bitrate;
            }

            // Método que calcula la tasa de compresión 
            public override double ObtenerTasaCompresion()
            {
                return 1.0 - (Bitrate / 500.0);
            }
        }
    } 
 
namespace BibliotecaDigital
    {
        // Hereda de ContenidoVisual 
        class Pelicula : ContenidoVisual
        {
            private string director;

            public string Director
            {
                get { return director; }
                set
                {
                    if (value.Length < 5)
                        director = "Desconocido";
                    else
                        director = value;
                }
            }

            public Pelicula(string titulo, double duracion, string resolucion, string director)
                : base(titulo, duracion, resolucion)
            {
                Director = director;
            }

            public override string Describir()
            {
                return $"PELÍCULA: {Titulo} ({Duracion} minutos)";
            }

            public override void Reproducir()
            {
                Console.WriteLine($"*** INICIO DE PELÍCULA: {Titulo} ***");
                Console.WriteLine("[INFO] Cargando componentes visuales…");
                Console.WriteLine($"[CRÉDITO] Director: {Director}");
                if (Duracion < 60)
                    Console.WriteLine("--> Es un cortometraje");
                Console.WriteLine("[INFO] Tasa de compresión reportada: " +
    ObtenerTasaCompresion());
                Console.WriteLine($"*** REPRODUCCIÓN FINALIZADA: {Titulo} ***\n");
            }
        }
    } 
 
namespace BibliotecaDigital
    {
        // Hereda de ContenidoAuditivo 
        class Cancion : ContenidoAuditivo
        {
            private string licencia;

            public string Licencia
            {
                get { return licencia; }
                set { licencia = value; }
            }

            public Cancion(string titulo, double duracion, int bitrate, string licencia)
                : base(titulo, duracion, bitrate)
            {
                Licencia = licencia;
            }

            public override string Describir()
            {
                return $"CANCIÓN: {Titulo} ({Duracion} minutos)";
            }

            public override void Reproducir()
            {
                Console.WriteLine($"*** INICIO DE CANCIÓN: {Titulo} ***");
                Console.WriteLine($"[INFO] Bitrate utilizado: {Bitrate} kbps");
                if (Licencia.ToLower() == "protegida")
                    Console.WriteLine("¡ADVERTENCIA! Pago de royalties aplicado");
                Console.WriteLine("[INFO] Tasa de compresión reportada: " +
    ObtenerTasaCompresion().ToString("0.00"));
                Console.WriteLine($"*** REPRODUCCIÓN FINALIZADA: {Titulo} ***\n");
            }
        }
    } 
 
namespace BibliotecaDigital
    {
        class Program
        {
            static void Main(string[] args)
            {
                // Catálogo precargado 
                List<Contenido> catalogo = new List<Contenido>()
            {
                new Pelicula("Interstellar", 169, "4K", "Christopher Nolan"),
                new Pelicula("Feast", 7, "480p", "Patrick Osborne"),
                new Cancion("Bohemian Rhapsody", 6, 320, "Protegida"),
                new Cancion("The House of the Rising Sun", 5, 128, "Libre"),
                new Pelicula("Matrix", 136, "1080p", "Lana Wachowski"),
                new Pelicula("Corto Pep", 10, "720p", "Pep"),
                new Cancion("Imagine", 4, 192, "Protegida"),
                new Cancion("Cancion Corta", 0.5, 256, "Libre")
            };

                int opcion = 0;
                do
                {
                    Console.WriteLine("=== BIBLIOTECA DIGITAL UNIFICADA ===");
                    Console.WriteLine("1. Ver catálogo");
                    Console.WriteLine("2. Reproducir todo el catálogo");
                    Console.WriteLine("3. Salir");
                    Console.Write("Elige una opción: ");
                    opcion = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();

                    if (opcion == 1)
                    {
                        foreach (var item in catalogo)
                        {
                            Console.WriteLine(item.Describir());
                        }
                        Console.WriteLine();
                    }
                    else if (opcion == 2)
                    {
                        foreach (var item in catalogo)
                        {
                            item.Reproducir();
                        }
                    }
                } while (opcion != 3);
                Console.WriteLine("Gracias por usar la biblioteca. ¡Hasta luego!");
            }
        }
    }
}