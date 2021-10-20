namespace pneustoreAPI.Models
{
    public class Product
    {
        public int id { get; set; }
        public string nome { get; set; }
        public double preco { get; set; }
        public string imagemUrl { get; set; }
        public string imagemUrlMarca { get; set; }
        public string marca { get; set; }

    }
}