namespace FxApi.Model.Fcs;

public record Info(
    string id,
    string @decimal,
    string symbol ,
    string period,
    string server_time,
    int credit_count 
    );
 