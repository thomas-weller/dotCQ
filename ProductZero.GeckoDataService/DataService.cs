namespace ProductZero.GeckoDataService
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.ServiceProcess;
    using System.Timers;

    using ProductZero.Data;
    using ProductZero.GeckoDataService.DataWriters;

    public class DataService : ServiceBase
    {
        private readonly Timer timer;

        private readonly List<IGeckoDataWriter> dataWriters;

        private readonly ZeroEntities entities = new ZeroEntities();


        public DataService()
        {
            this.ServiceName = "GeckoDataService";

            this.dataWriters = new List<IGeckoDataWriter>();

            timer = new Timer
                        {
                            Interval = Convert.ToInt32(ConfigurationManager.AppSettings["interval"])
                        };
            timer.Elapsed += TimerOnElapsed;
        }

        protected override void Dispose(bool disposing)
        {
            timer.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnStart(string[] args)
        {
            ListDataWriters();

            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
        }

        private void ListDataWriters()
        {
            //dataWriters.Add(new IpAddressesDataWriter());
            //dataWriters.Add(new CalculationDataWriter());
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs args)
        {
            foreach (IGeckoDataWriter writer in this.dataWriters)
            {
                writer.WriteData();
            }
        }
    }
}
