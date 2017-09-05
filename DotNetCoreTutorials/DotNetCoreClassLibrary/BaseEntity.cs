using System;

namespace DotNetCoreClassLibrary
{
    public class BaseEntity
    {
        public static int Get(int number) => Newtonsoft.Json.JsonConvert.DeserializeObject<int>($"{1000 + number}");
    }
}
