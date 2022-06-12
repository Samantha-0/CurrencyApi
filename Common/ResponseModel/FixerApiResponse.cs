namespace Common.ResponseModel;

public class FixerApiResponse
{
    public bool Success { get; set; }
    public FixerApiError? Error { get; set; }
}