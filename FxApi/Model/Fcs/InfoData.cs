namespace FxApi.Model.Fcs;

public record InfoData(
    string id,
    string @decimal,
    string symbol ,
    string period,
    string server_time,
    int credit_count 
    );
 