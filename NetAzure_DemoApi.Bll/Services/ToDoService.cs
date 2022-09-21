using NetAzure_DemoApi.Bll.Entities;
using NetAzure_DemoApi.Bll.Mappers;
using DS = NetAzure_DemoApi.Dal.Services;

namespace NetAzure_DemoApi.Bll.Services
{
    public class ToDoService
    {
        private readonly DS.ToDoService _dalService;

        public ToDoService(DS.ToDoService dalService)
        {
            _dalService = dalService;
        }

        public IEnumerable<ToDo> Get()
        {
            return _dalService.Get().Select(td => td.ToBll());
        }

        public int Insert(ToDo entity)
        {
            return _dalService.Insert(entity.ToDal());
        }
    }
}
