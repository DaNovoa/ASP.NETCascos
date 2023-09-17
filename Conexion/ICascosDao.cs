using Conexion.Models;

public interface ICascosDao
{
    void ActualizarCasco(Cascos casco);
    void EliminarCascoPorCedula(string cedulaCliente);
    void CalcularTotalVentas();
    void ListarCascos();
    void IngresarNuevoCasco(string talla, string marca, double precio, string comprador, string apellido, string cedula, int unidades);
}
