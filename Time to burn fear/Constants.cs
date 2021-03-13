using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time_to_burn_fear
{
    public static class Constants
    {
        public const string CHARS_FILE_NAME = "Chars.txt";
        public const string THING_FILE_NAME = "Thing.txt";
        public const string CUT_DRESS_NAME = "Снято";
        public const string HERO_FILE_NAME = "Heroes.txt";
        public const string NAME_BASE = "Time-to-burn-fear";
        public const string NAME_TABLE_DRESS = "dress";
        public static string[] dressColumnName
        { get; private set; } = { "name", "type_dress", "first_parameter", "second_parameter"};
        public static string serverName;
    }
}
