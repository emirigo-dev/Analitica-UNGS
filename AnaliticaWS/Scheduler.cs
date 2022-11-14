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
            s.WithIntervalInSeconds(20)
            //s.WithIntervalInHours(24)
            .OnEveryDay()
            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(20, 01))).Build();

            scheduler.ScheduleJob(job, trigger);
        }

        public static void StartAhorro()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<JobAhorro>().Build();

            ITrigger trigger = TriggerBuilder.Create().WithDailyTimeIntervalSchedule(s =>
            s.WithIntervalInSeconds(20)
            //s.WithIntervalInHours(24)
            .OnEveryDay()
            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(20, 01))).Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}