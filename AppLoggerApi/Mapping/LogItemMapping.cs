using AppLoggerApi.Model;
using AppLoggerApi.Model.Dto;

namespace AppLoggerApi.Mapping;

public static class LogItemMapping
{


    public static LogItem MapToItem(this CreateLogItemRequestDto request)
    {
        return new LogItem
        {             
            Message = request.Message,
            BusinessId = request.BusinessId
 
        };
    }


    public static LogIemResponseDto MapToResponse(this LogItem item) => 
        new LogIemResponseDto(item.Id, item.BusinessId, item.Message, item.CreatedDate);


}