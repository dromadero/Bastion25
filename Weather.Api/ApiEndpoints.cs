namespace Weather.Api;

public static class ApiEndpoints
{
    private const string apiBase = "api";

    public static class Meteos
    {
        private const string Base = $"{apiBase}/meteos";

        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = $"{Base}";
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";

    }



}




