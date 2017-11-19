using System;
using System.Collections.Generic;
using System.Linq;
using Cassandra;

namespace Power2U
{
    class CassandraDriver
    {
        public CassandraDriver()
        {

        }

        public static void Main(string[] args)
        {
            String[] data = { "1", "2017-11-01", "2017-11-03" };
            ReadTempForecast(data);
        }

        public static List<Row> ReadTempForecast(params String[] parameters)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("ec2-52-209-210-139.eu-west-1.compute.amazonaws.com").Build();
            ISession session = cluster.Connect("eden");
            //Console.WriteLine("select ts,t from smhi_forecast2 where building_id =" + parameters[0] + " and ts>= '" + parameters[1] + " 00:00:00' and ts<='" + parameters[2] + " 23:59:59' allow filtering");
            RowSet rows = session.Execute("select building_id,ts,t from smhi_forecast2 where building_id =" + parameters[0] + " and ts>= '" + parameters[1] + " 00:00:00' and ts<='"+ parameters[2] + " 23:59:59' allow filtering");
            foreach(Row row in rows){

            }
            return rows.ToList();
        }

        public static List<Row> ReadLoadData(params String[] parameters)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("ec2-52-209-210-139.eu-west-1.compute.amazonaws.com").Build();
            ISession session = cluster.Connect("eden");

            RowSet rows = session.Execute("select ts,value from energy_consumption where id =" + parameters[0] + " and ts>= '" + parameters[1] + " 00:00:00' and ts<='" + parameters[2] + " 23:59:59' allow filtering");
            return rows.ToList();
        }

        public static List<Row> ReadHistoricalTempData(params String[] parameters)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("ec2-52-209-210-139.eu-west-1.compute.amazonaws.com").Build();
            ISession session = cluster.Connect("eden");

            RowSet rows = session.Execute("select date,hour,temperature from historical_temperature where building_id =" + parameters[0] + " and date>= '" + parameters[1] + "' and date<='" + parameters[2] + "' allow filtering");
            return rows.ToList();
        }


        public static List<Row> ReadSpotPriceData(params String[] parameters)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("ec2-52-209-210-139.eu-west-1.compute.amazonaws.com").Build();
            ISession session = cluster.Connect("eden");

            RowSet rows = session.Execute("select ts,price from nordpool_spot_price where ts>= '" + parameters[0] + " 00:00:00' and ts<='" + parameters[1] + " 23:59:59' allow filtering");
            return rows.ToList();
        }

        public static List<Row> ReadLoadForecastData(params String[] parameters)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("ec2-52-209-210-139.eu-west-1.compute.amazonaws.com").Build();
            ISession session = cluster.Connect("eden");

            RowSet rows = session.Execute("select * from (select ts,load_forecast from device_scheduling where ts>= '" + parameters[2] + " 00:00:00' and ts<='" + parameters[3] + " 23:59:59' allow filtering) ");
            return rows.ToList();
        }

        public static void SetScheduleData(params String[] parameters)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("ec2-52-209-210-139.eu-west-1.compute.amazonaws.com").Build();
            ISession session = cluster.Connect("eden");

            session.Execute("update device_scheduling set date='" + parameters[3] + "', load_forecast = " + parameters[4] + ",schedule_value = " + parameters[5] + " where building_id= " + parameters[0] + " and ts = '" + parameters[1] + "' and asset_id=" + parameters[2] + "");
            
        }
    }
}
