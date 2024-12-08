namespace Ordering.Processor.Models;

public class BackgroundTaskOptions
{
    public int GracePeriodTime { get; set; }
    public int PaymentCheckTime { get; set; }
    public int ProcessTime { get; set; }
    public int PackingTime { get; set; }
}