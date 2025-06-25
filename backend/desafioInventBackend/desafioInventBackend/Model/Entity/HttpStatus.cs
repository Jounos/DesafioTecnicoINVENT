namespace DesafioInventBackend.Model.Entity
{
    public class HttpStatus<T>
    {
        public int Status{ get; set; }
        public string? Message { get; set; }
        public T? Body { get; set; }

    }
}
