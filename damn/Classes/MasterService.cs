using System.Collections.Generic;

namespace damn.Classes
{
    public class MasterService : IHasList<MasterService>
    {
        public readonly static List<MasterService> Links = new List<MasterService>(); 

        public MasterService() { }

        public MasterService(int id, int masterId, int serviceId)
        {
            Id = id;
            MasterID = masterId;
            ServiceID = serviceId;
        }

        public int Id { get; set; }
        public int MasterID { get; set; }
        public int ServiceID { get; set; }

        public List<MasterService> GetList()
        {
            return Links;
        }
    }
}
