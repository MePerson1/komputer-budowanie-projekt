namespace KomputerBudowanieAPI.Models
{
    public class Fan
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ProducerCode { get; set; }
        private int Speed { get; set; }
        private int Voltatge { get; set; }
        private float Height { get; set; }
        private float Width { get; set; }
        private float Lenght { get; set; }
    }
}
