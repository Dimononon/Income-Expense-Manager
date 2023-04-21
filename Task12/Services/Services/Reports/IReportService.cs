using Task12.Models;

namespace Task12.Services.Reports
{
    public interface IReportService
    {
        DailyReport GetDailyReport(DateOnly date, Guid userId);
        PeriodReport GetPeriodReport(DateOnly dateStart, DateOnly dateEnd, Guid userId);
    }
}