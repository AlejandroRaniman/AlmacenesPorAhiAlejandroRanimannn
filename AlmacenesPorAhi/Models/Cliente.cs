using System.ComponentModel.DataAnnotations;

namespace AlmacenesPorAhi.Models;

public class Cliente
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Rut { get; set; } = string.Empty;

    [Required]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public string ApellidoPaterno { get; set; } = string.Empty;

    [Required]
    public string ApellidoMaterno { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Telefono { get; set; } = string.Empty;

    [Required]
    public string Direccion { get; set; } = string.Empty;

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    [Required]
    public string Estado { get; set; } = "Activo"; // Activo o Inactivo
}