namespace EventosAPI.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        //Crear relacion

        //BD donde se guarda el id del evento
        public int EventoId { get; set; }
        //Obtiene la informacion del evento relacionada al usuario
        public Evento evento { get; set; }

        //Relacionar otros de igual manera


    }
}
