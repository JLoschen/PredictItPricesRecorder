﻿using System;
using Topshelf;

namespace PredictItPriceRecorder
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x => 
            {
                x.Service<Heartbeat>(s =>
                {
                    s.ConstructUsing(heartBeat => new Heartbeat());
                    s.WhenStarted(heartBeat => heartBeat.Start());
                    s.WhenStopped(heartBeat => heartBeat.Stop());
                });

                x.RunAsLocalSystem();

                x.SetServiceName("PredictItPriceService");
                x.SetDisplayName("PredictIt Price Service");
                x.SetDescription("Calls PredictIt API and records their prices in a local DB");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}