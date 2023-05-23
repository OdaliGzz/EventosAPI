namespace EventosAPI.Entidades
{
    public class Evento
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public string Ubicacion { get; set; }

        public int Capacidad { get; set; }

        //Para cargar la lista de los usuarios registrados en el evento
        public List<Usuario> Usuarios { get; set; }

    }
}
