namespace QHRM_Practical_Test_App.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }= DateTime.Now;
    }
}
