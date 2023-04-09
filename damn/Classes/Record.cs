using System;
using System.Collections.Generic;

namespace damn.Classes
{
    public class Record : IHasList<Record>
    {
        public readonly static List<Record> Records = new List<Record>();

        public int Id { get; set; }
        public int MasterID { get; set; }
        public int ClientID { get; set; }
        public int ServiceID { get; set; }
        public DateTime Date { get; set; }

        public Record() { }

        public Record(int id, int masterid, int clientid, int serviceid, DateTime date)
        {
            Id = id;
            MasterID = masterid;
            ClientID = clientid;
            ServiceID = serviceid;
            Date = date;
        }

        public List<Record> GetList()
        {
            return Records;
        }
    }
}
