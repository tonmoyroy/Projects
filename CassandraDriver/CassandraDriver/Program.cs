using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;

namespace Power2U
{
    class Program
    {
        static void Main(string[] args)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("ec2-52-209-210-139.eu-west-1.compute.amazonaws.com").Build();
            ISession session = cluster.Connect("eden");

            RowSet rows = session.Execute("select * from smhi_forecast");
            foreach (Row row in rows)
                Console.WriteLine("{0} {1}", row["ts"], row["t"]);
        }
    }
}
