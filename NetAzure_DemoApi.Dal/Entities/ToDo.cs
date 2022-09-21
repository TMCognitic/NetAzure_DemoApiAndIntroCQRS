namespace NetAzure_DemoApi.Dal.Entities
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }
        public DateTime Created { get; set; }
    }
}
