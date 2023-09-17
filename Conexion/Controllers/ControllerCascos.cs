using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Conexion.Models;

namespace Conexion.Controllers
{
    public class CascosController : Controller
    {
        private string connectionString = "server=localhost;user=root;password=;database=cascosdb;";

        public IActionResult Index()
        {
            List<Cascos> cascos = new List<Cascos>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Cascos";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cascos casco = new Cascos
                        {
                            IdCasco = Convert.ToInt32(reader["IdCasco"]),
                            Talla = reader["Talla"].ToString(),
                            Marca = reader["Marca"].ToString(),
                            Comprador = reader["Comprador"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            Cedula = reader["Cedula"].ToString(),
                            Precio = Convert.ToDouble(reader["Precio"]),
                            Unidades = Convert.ToInt32(reader["Unidades"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"])
                        };

                        cascos.Add(casco);
                    }
                }
            }

            return View(cascos);
        }

        public IActionResult Details(int id)
        {
            Cascos casco = GetCascoById(id);
            if (casco == null)
            {
                return NotFound();
            }

            return View("Detalles", casco);
        }

        public IActionResult Create()
        {
            return View("Nueva");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cascos casco)
        {
            if (ModelState.IsValid)
            {
                InsertarNuevoCasco(casco);

                return RedirectToAction("Index");
            }
            return View(casco);
        }

        private ICascosDao _cascosDao;

        public CascosController(ICascosDao cascosDao)
        {
            _cascosDao = cascosDao;
        }


        public IActionResult Edit(int id)
        {
            Cascos casco = GetCascoById(id);
            if (casco == null)
            {
                return NotFound(); // Manejo de error si no se encuentra el casco
            }
            return View(casco); // Muestra la vista de edición con los detalles del casco
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdCasco,Talla,Marca,Comprador,Apellido,Cedula,Precio,Unidades,Fecha")] Cascos casco)
        {
            if (id != casco.IdCasco)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Llama al método ActualizarCasco de ICascosDao para actualizar el casco
                    _cascosDao.ActualizarCasco(casco);

                    TempData["Message"] = "Casco actualizado con éxito.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Registra cualquier excepción que ocurra para ayudar en la depuración
                    Console.WriteLine("Excepción al actualizar el casco: " + ex.Message);

                    ModelState.AddModelError(string.Empty, "Error al actualizar el casco.");
                    return View(casco);
                }
            }

            return View(casco);
        }


        public IActionResult Delete(int id)
        {
            Cascos casco = GetCascoById(id);
            if (casco == null)
            {
                return NotFound();
            }
            return View("Eliminar", casco);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            EliminarCasco(id);
            return RedirectToAction("Index");
        }

        private Cascos? GetCascoById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Cascos WHERE IdCasco = @IdCasco";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCasco", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader != null && reader.Read())
                        {
                            return new Cascos
                            {
                                IdCasco = Convert.ToInt32(reader["IdCasco"]),
                                Talla = reader["Talla"].ToString(),
                                Marca = reader["Marca"].ToString(),
                                Comprador = reader["Comprador"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Cedula = reader["Cedula"].ToString(),
                                Precio = Convert.ToDouble(reader["Precio"]),
                                Unidades = Convert.ToInt32(reader["Unidades"]),
                                Fecha = reader.GetDateTime("Fecha")
                            };
                        }
                    }
                }
            }
            return null; // Si no se encuentra ningún casco con el ID especificado, se devuelve null.
        }



        private void InsertarNuevoCasco(Cascos casco)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Cascos (Talla, Marca, Comprador, Apellido, Cedula, Precio, Unidades, Fecha) VALUES (@Talla, @Marca, @Comprador, @Apellido, @Cedula, @Precio, @Unidades, @Fecha)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Talla", casco.Talla);
                    command.Parameters.AddWithValue("@Marca", casco.Marca);
                    command.Parameters.AddWithValue("@Comprador", casco.Comprador);
                    command.Parameters.AddWithValue("@Apellido", casco.Apellido);
                    command.Parameters.AddWithValue("@Cedula", casco.Cedula);
                    command.Parameters.AddWithValue("@Precio", casco.Precio);
                    command.Parameters.AddWithValue("@Unidades", casco.Unidades);
                    command.Parameters.AddWithValue("@Fecha", casco.Fecha);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void ActualizarCasco(Cascos casco)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Cascos SET Talla = @Talla, Marca = @Marca, Comprador = @Comprador, Apellido = @Apellido, Cedula = @Cedula, Precio = @Precio, Unidades = @Unidades, Fecha = @Fecha WHERE IdCasco = @IdCasco";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdCasco", casco.IdCasco);
                        command.Parameters.AddWithValue("@Talla", casco.Talla);
                        command.Parameters.AddWithValue("@Marca", casco.Marca);
                        command.Parameters.AddWithValue("@Comprador", casco.Comprador);
                        command.Parameters.AddWithValue("@Apellido", casco.Apellido);
                        command.Parameters.AddWithValue("@Cedula", casco.Cedula);
                        command.Parameters.AddWithValue("@Precio", casco.Precio);
                        command.Parameters.AddWithValue("@Unidades", casco.Unidades);
                        command.Parameters.AddWithValue("@Fecha", casco.Fecha);
                        command.ExecuteNonQuery();
                    }
                }

                TempData["Message"] = "Casco actualizado con éxito.";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error al actualizar el casco: " + ex.Message;
            }
        }

        private void EliminarCasco(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Cascos WHERE IdCasco = @IdCasco";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCasco", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
