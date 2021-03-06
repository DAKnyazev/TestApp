﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CurrencyRateProvider.Common.Models.Report
{
    /// <summary>
    /// Месячный отчет о курсах валют
    /// </summary>
    public class MonthlyRateReport
    {
        public MonthlyRateReport(DateTime date)
        {
            WeeklyReports = new List<WeeklyRateReport>();
            Year = date.Year;
            MonthName = date.ToString("MMMM", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Год
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Название месяца
        /// </summary>
        public string MonthName { get; set; }

        /// <summary>
        /// Недельные отчеты о курсах валют
        /// </summary>
        public List<WeeklyRateReport> WeeklyReports { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("Year: ").Append(Year).Append(", month: ").AppendLine(MonthName);
            if (!WeeklyReports.Any())
            {
                builder.AppendLine("No data.");
                builder.AppendLine();

                return builder.ToString();
            }
            builder.AppendLine("Week periods:");
            foreach (var report in WeeklyReports)
            {
                builder.Append(report.StartDay);
                if (report.StartDay != report.EndDay)
                {
                    builder.Append("...").Append(report.EndDay);
                }
                builder.Append(": ");

                foreach (var statistic in report.RateStatistics)
                {
                    builder
                        .Append(statistic.Key)
                        .Append(" - max: ")
                        .Append(statistic.Value.MaxCost)
                        .Append(", min: ")
                        .Append(statistic.Value.MinCost)
                        .Append(", median: ")
                        .Append(statistic.Value.MedianCost)
                        .Append("; ");
                }

                builder.AppendLine();
            }
            builder.AppendLine();

            return builder.ToString();
        }
    }
}
