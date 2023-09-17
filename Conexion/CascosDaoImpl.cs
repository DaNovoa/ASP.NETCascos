using Conexion.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class CascosDaoImpl : ICascosDao
{
    private MySqlConnection connection;
    private string connectionString = "server=localhost;user=root;password=;database=cascosdb;"; // Define tu cadena de conexión aquí

    public CascosDaoImpl(MySqlConnection connection)
    {
        this.connection = connection;
    }

    public void ActualizarCasco(Cascos casco)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Define la consulta SQL para actualizar el casco
                string query = "UPDATE Cascos SET Talla = @Talla, Marca = @Marca, Comprador = @Comprador, Apellido = @Apellido, Cedula = @Cedula, Precio = @Precio, Unidades = @Unidades, Fecha = @Fecha WHERE IdCasco = @IdCasco";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Asigna los parámetros con los valores del casco
                    command.Parameters.AddWithValue("@IdCasco", casco.IdCasco);
                    command.Parameters.AddWithValue("@Talla", casco.Talla);
                    command.Parameters.AddWithValue("@Marca", casco.Marca);
                    command.Parameters.AddWithValue("@Comprador", casco.Comprador);
                    command.Parameters.AddWithValue("@Apellido", casco.Apellido);
                    command.Parameters.AddWithValue("@Cedula", casco.Cedula);
                    command.Parameters.AddWithValue("@Precio", casco.Precio);
                    command.Parameters.AddWithValue("@Unidades", casco.Unidades);
                    command.Parameters.AddWithValue("@Fecha", casco.Fecha);

                    // Ejecuta la actualización
                    command.ExecuteNonQuery();
                }
            }

            // Establece un mensaje de éxito si es necesario
            // TempData["Message"] = "Casco actualizado con éxito.";
        }
        catch (Exception ex)
        {
            // Registra cualquier excepción que ocurra para ayudar en la depuración
            Console.WriteLine("Excepción al actualizar el casco: " + ex.Message);

            // Maneja el error como sea necesario
            // TempData["Message"] = "Error al actualizar el casco.";
        }
    }


    public void EliminarCascoPorCedula(string cedulaCliente)    
    {
        string deleteQuery = $"DELETE FROM cascos WHERE cedula = '{cedulaCliente}'";
        MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);

        int rowsDeleted = deleteCommand.ExecuteNonQuery();

        if (rowsDeleted > 0)
        {
            Console.WriteLine("Cascos eliminados exitosamente.");
        }
        else
        {
            Console.WriteLine("No se encontraron cascos para la cédula proporcionada.");
        }
    }

    public void CalcularTotalVentas()
    {
        string selectQuery = "SELECT precio, unidades FROM cascos";
        MySqlCommand command = new MySqlCommand(selectQuery, connection);

        using (MySqlDataReader reader = command.ExecuteReader())
        {
            double totalVentas = 0;
            while (reader.Read())
            {
                double precio = reader.GetDouble("precio");
                int unidades = reader.GetInt32("unidades");
                totalVentas += precio * unidades;
            }

            Console.WriteLine("El total de ventas es: " + totalVentas);
        }
    }

    public void ListarCascos()
    {
        string selectQuery = "SELECT idcasco, talla, marca, comprador, apellido, cedula, precio, unidades FROM cascos ORDER BY marca";
        MySqlCommand command = new MySqlCommand(selectQuery, connection);

        using (MySqlDataReader reader = command.ExecuteReader())
        {
            Console.WriteLine("Lista de cascos ordenados por marca:");
            while (reader.Read())
            {
                int idcasco = reader.GetInt32("idcasco");
                string talla = reader.GetString("talla");
                string marca = reader.GetString("marca");
                string comprador = reader.GetString("comprador");
                string apellido = reader.GetString("apellido");
                string cedula = reader.GetString("cedula");
                double precio = reader.GetDouble("precio");
                int unidad = reader.GetInt32("unidades");
                Console.WriteLine($"ID: {idcasco}, Talla: {talla}, Marca: {marca}, Comprador: {comprador} {apellido}, Cédula: {cedula}, Precio: {precio}, Unidades: {unidad}");
            }
        }
    }

    public void IngresarNuevoCasco(string nuevaTalla, string nuevaMarca, double nuevoPrecio, string nuevoComprador, string nuevoApellido, string nuevaCedula, int nuevasUnidades)
    {
        string insertQuery = $"INSERT INTO cascos (talla, marca, precio, comprador, apellido, cedula, unidades) VALUES ('{nuevaTalla}', '{nuevaMarca}', {nuevoPrecio}, '{nuevoComprador}', '{nuevoApellido}', '{nuevaCedula}', {nuevasUnidades})";

        MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
            
        int rowsInserted = insertCommand.ExecuteNonQuery();

        if (rowsInserted > 0)
        {
            Console.WriteLine("Casco ingresado exitosamente.");
        }
        else
        {
            Console.WriteLine("Error al ingresar el casco.");
        }
    }
}
