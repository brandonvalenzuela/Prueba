class Program
{
    private static List<List<string>> read_csv_stream(Stream dataFile, string filename, bool ingnorar_primerlinea)
    {
        List<List<string>> datos = new List<List<string>>(); // Falta de parentesis para instanciar el objeto

        StreamReader sr = new StreamReader(dataFile);

        string sline;
        string concat = ""; /// Almacena las palabras
        string celda = ""; /// Almacena lo que hay entre comillas
        string[]/*<==*/ _values; // cambiar _values de una variable normal a un array
        int cont = 0; /// Contador para las lineas del archivo csv

        // bucle para recorrer el archivo csv
        while (!sr.EndOfStream)
        {
            cont++; // count+ no es una expresión /// contador de lineas
            sline = sr.ReadLine(); // Falta de punto y coma /// Leer la linea
            bool ban = false; /// basicamente es un indicador para saber si hay comillas
            int cont2 = 0; /// contador para las comillas
            concat = ""; // Reiniciar la cocatenacion en cada bucle

            foreach (var item in sline) // Declarar la variable con var
            {
                /// Comprobar si estamos en comillas
                if (item == '"') // = no es una comparacion en su lugar seria == // cambiar el 34 codigo ASII por el caracter explicito
                {
                    ban = true; //Tambien podria ser ban = !ban; pero es menos entendible
                    cont2++;
                }

                /// Si estamos en comillas entonces guardarla en celda 

                //Mejorando un poco mas la logica y optimizando
                if (ban && cont2 == 1) // & el comparador "y" lleva 2 ampersand && no uno
                {
                    celda += item;
                }
                /// si no es una comillas entonces guardarla en concat
                else if (!ban && cont2 == 0) // = no es una comparacion en su lugar seria ==
                {
                    concat += item; // $$ se sustituye por += para conectar strings
                }
                /// Si esta en una segunda comilla significa que las seccion de comillas se acabo
                else if (cont2 == 2) // = no es una comparacion en su lugar seria == // Era redundante tener 2 condiciones iguales
                {
                    /// Ahora todo lo que estaba temporalmente en celda se pasa a concat para procesarlas despues de eliminar la comillas
                    /// y se resetea tanto la celda como el contador de comillas para que esten preparados por si hay otra seccion
                    concat += celda.Replace(",", "");/*<==*/
                    celda = ""; // Falta de punto y coma para saltar la linea
                    cont2 = 0;
                }
                //if (cont2 == 2) // = no es una comparacion en su lugar seria ==
                //{
                //ban = false;/*<==*/ // Falta de punto y coma para saltar la linea
                //concat += item;
                //}

            }

            /// Pasar toda la linea de concat a sline sin las comillas
            sline = concat.Replace("\"", ""); // Metodo mal escrito Replac a Replace

            /// Como count representa la linea en la que estamos trabajando, si esta es la primera linea del archivo y la variable ingnorar_primerlinea
            /// se la pasamos como verdadero entonces con "continue" saltaremos todo el codigo faltante de ejecutarse dentro del ciclo while y asi continuar
            /// en la segunda linea, si pasamos la ingnorar_primerlinea en falso entonces el codigo continuara normal
            if (ingnorar_primerlinea && cont == 1) // Variable mal escrita ingnorar_primerline a ingnorar_primerlinea
                continue;

            /// Pasar la linea sin comillas divididas por cada comilla que tenga la linea a un array de strings
            _values = sline.Split(','); // Metodo mal escrito Spli a Split, cambiar _values de una variable normal a un array de strings para tener acceso al metodo Split

            /// Iniciamos una lista row que seran las filas del archivo, ya que el metodo retorna una lista de listas de strings
            List<string> row = new List<string>();

            /// Iteramos en cada elemento del array _values
            foreach (var str in _values)
            {
                /// Esto es para asegurase de que no tengamos conflictos con estos, comandos? que pasarian a representar las comas
                /// y lo mismo para los espacios vacios
                /// y ya una vez tratado el valor lo agregamos a la lista row que creamos
                /// y asi hasta terminar la iteracion
                string/*<==*/ val = str.Replace("&coma;", ","); // La variable val no estaba declarada // 
                val = val.Replace("&vacio;", "");
                row.Add(val);
                //val = null;
            }

            //row.Add(cont.ToString()); // ToString es un metodo y debe terminar con parentesis // Agregar contador de filas

            /// Agregamos la lista row procesada a nuestra lista de listas de strings que es el que retornaremos
            datos.Add(row);
            //row = null;
            //concat = "";
            //celda = "";
        }
        /// una vez que todo esto se realize con todas las lineas del archivo saldra del ciclo while y
        /// cerramos el StreamReader para optimizar recursos
        /// y regresamos la lista de listas de strings
        sr.Close();
        return datos; // Return debe estar fuera del bucle

    }

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Escribe el nombre del archivo csv \n > Para ver todos los archivos csv que hay escriba el comando \"list\"");
            var comand = Console.ReadLine();
            if (comand == "list")
            {
                Console.WriteLine("> Catalog_v2,\n> DemandPlan_v1,\n> Inventory_v2,\n> InventoryLot_v4,\n> Location_v3,\n> LocationGroup_v2,\n>" +
                    " Order_v3,\n> OrderAllocation_v2,\n> OrderLine_v4,\n> Organization_v3,\n> Product_v6,\n> Shipment_v4,\n> ShipmentLine_v3,\n> SupplyPlan_v2\n");

                Console.WriteLine("Escribe el nombre del archivo csv tal como esta");

                string filename = Console.ReadLine();

                string filePath = Path.Combine("csv", filename + ".csv");

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("El archivo no existe en la ruta especificada.");
                }

                try
                {
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        List<List<string>> result = read_csv_stream(fileStream, filename, true);

                        foreach (var row in result)
                        {
                            Console.WriteLine(string.Join(" ,", row));
                        }
                    }
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al leer el archivo CSV: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Comando no valido");
            }
        }
    }
}
