namespace News.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Users
        {
            public const string GetAll = Base + "/users";
            public const string Get = Base + "/user/{id}";
            public const string Add = Base + "/users";
            public const string Delete = Base + "/user/{userName}";
            public const string Update = Base + "/user";
            public const string Change = Base + "/userData/{userName}";
        }
        
        
        public static class Friends
        {
            public const string GetPeople =  Base + "/friends";
            public const string GetAll = Base + "/friends/{id}";
            public const string Search = Base + "/friendsByBusiness/{business}";
            public const string Add = Base + "/friend";
            public const string Delete = Base + "/friend";
        }
        


        public static class Businesses
        {
            public const string GetAll = Base + "/business";
            public const string Get = Base + "/business/{id}";
            public const string Add = Base + "/business";
            public const string Delete = Base + "/business/{id}";
            public const string Update = Base + "/business/{id}";
        }

        public static class Roles
        {
            public const string GetAll = Base + "/roles";
            public const string Add = Base + "/roles";
            public const string Delete = Base + "/roles";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string Refresh = Base + "/identity/refresh";
        }
        
        public static class Tags
        {
            public const string GetAll = Base + "/tags";
            public const string Get = Base + "/tags/{tagName}";
            public const string Create = Base + "/tags";
            public const string Delete = Base + "/tags/{id}";
        }
    }
}