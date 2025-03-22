namespace Weather.Api.Responses;

public class ValidationFailureResponse
{
    public required IEnumerable<validationResponse> Errors { get; init; }

}


public class validationResponse
{
    public required string PropertyName { get; init; }
    public required string Message { get; init; }
}