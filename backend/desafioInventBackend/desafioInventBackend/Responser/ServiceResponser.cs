namespace DesafioInventBackend.Responser
{
    public class ServiceResponser<T>
    {
        public T data { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public string error { get; set; }
        public List<string> ErrorMessages { get; set; } = null;

    }
}
