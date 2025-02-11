namespace Backend.Models
{
    public class Oseba
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public int Starost { get; set; }

        public Oseba()
        {
            Id = 0;
            Ime = string.Empty;
            Starost = 0;
        }

        public Oseba(string ime, int starost)
        {
            Ime = ime;
            Starost = starost;
        }
    }
}