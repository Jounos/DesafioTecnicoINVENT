using System.Net.Http.Headers;

namespace DesafioInventBackend.Model.Entity
{
    public class HttpStatus<T>
    {
        public T? Body { get; set; }
        public int Status{ get; set; }
        public string? StatusText { get; set; }

        public string? url { get; set; }

        public HttpHeaders Headers {  get; set; }

    }
}
