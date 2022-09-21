using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAzure_DemoApi.Bll.Entities
{
    public class ToDo
    {
        public int Id { get; init; }
        public string Title { get; set; }
        public bool Done { get; set; }

        //Création lors du retour de DB
        internal ToDo(int id, string title, bool done)
        {
            Id = id;
            Title = title;
            Done = done;
        }

        //Création depuis l'api
        public ToDo(string title)
        {
            Title = title;
        }
    }
}
