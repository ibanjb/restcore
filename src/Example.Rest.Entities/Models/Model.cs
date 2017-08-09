using Example.Rest.Entities.Interfaces;

namespace Example.Rest.Entities.Models
{
    public class Model : IModel
    {
        public int Id { get; set; }

        public string Description { get; set; }
    }
}
