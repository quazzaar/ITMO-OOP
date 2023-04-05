namespace MessagingSystem.DataAccess;

public class Report
{
    public Report(string reportText)
    {
        ReportText = reportText;
        Id = Guid.NewGuid();
        ReportDate = DateTime.Now;
    }

    public Guid Id { get; set; }
    public string ReportText { get; set; }
    public DateTime ReportDate { get; set; }
}