using Task12.Models;

namespace Task12.Services.Reports
{
    public interface IReportService
    {
        DailyReport GetDailyReport(DateOnly date);
        PeriodReport GetPeriodReport(DateOnly dateStart, DateOnly dateEnd);
    }
}