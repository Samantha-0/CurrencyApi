namespace Common.ResponseModel;

public class LatestResponse : FixerApiResponse
{
    public string Base { get; set; }
    public DateTime Date { get; set; }
    public dynamic Rates { get; set; }
    public string Timestamp { get; set; }
}