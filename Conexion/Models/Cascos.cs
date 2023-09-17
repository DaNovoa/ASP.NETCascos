using System;
using System.ComponentModel.DataAnnotations;

namespace Conexion.Models
{
    public class Cascos
    {
        public int IdCasco { get; set; }

        [Required(ErrorMessage = "El campo Talla es obligatorio.")]
        [Display(Name = "Talla del Casco")]
        public string Talla { get; set; } = "SinTalla"; // Asigna un valor predeterminado

        [Required(ErrorMessage = "El campo Marca es obligatorio.")]
        [Display(Name = "Marca del Casco")]
        public string Marca { get; set; } = "SinMarca"; // Asigna un valor predeterminado

        [Required(ErrorMessage = "El campo Comprador es obligatorio.")]
        [Display(Name = "Nombre del Comprador")]
        public string Comprador { get; set; } = "SinComprador"; // Asigna un valor predeterminado

        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        [Display(Name = "Apellido del Comprador")]
        public string Apellido { get; set; } = "SinApellido"; // Asigna un valor predeterminado

        [Required(ErrorMessage = "El campo Cedula es obligatorio.")]
        [RegularExpression(@"^\d{9,10}$", ErrorMessage = "La cédula debe contener 9 o 10 dígitos.")]
        [Display(Name = "Cédula del Comprador")]
        public string Cedula { get; set; } = "SinCedula"; // Asigna un valor predeterminado

        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        [Display(Name = "Precio del Casco")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "El campo Unidades es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Las unidades deben ser al menos 1.")]
        [Display(Name = "Unidades")]
        public int Unidades { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Compra")]
        public DateTime Fecha { get; set; }

        // Otras propiedades con anotaciones de validación...
    }
}
