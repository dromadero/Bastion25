namespace AppLoggerApi.Model;

public class LogItem
{
    public int? Id { get; set; }

    public string BusinessId { get; set; } = default!;

    public string Message { get; set; } = default!;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

}
