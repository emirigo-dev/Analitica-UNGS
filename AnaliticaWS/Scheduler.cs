using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnaliticaWS
{
    public class Scheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<Job>().Build();

            ITrigger trigger = TriggerBuilder.Create().WithDailyTimeIntervalSchedule(s =>
            //s.WithIntervalInHours(24)
            s.WithIntervalInSeconds(60)
            .OnEveryDay()
            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(17, 53))).Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}