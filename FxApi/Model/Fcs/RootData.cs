namespace FxApi.Model.Fcs;

public record RootData(
    bool status,
    int code,
    string msg,
    InfoData info
    );




