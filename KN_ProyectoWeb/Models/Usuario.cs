namespace KN_ProyectoWeb.Models
{
    public class Usuario
    {
        public int ConsecutivoUsuario { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasenia { get; set; }
        public string NombrePerfil { get; set; }
        public string ContraseniaConfirmacion { get; set; }

    }
}