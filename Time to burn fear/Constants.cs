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
        public const string NAME_TABLE_CONSTITUTION = "Constitution";
        public static string[] DressColumnName
        { get; private set; } = { "name", "type_dress", "first_parameter", "second_parameter"};
        public static string[] ConstitutionColumnName
        { get; private set; } = { "name", "race" };
        public static (string value, ValueType valueType)[] DressColumnNameType
        { get; private set; } = { ("name", ValueType.String), ("type_dress", ValueType.String), ("first_parameter", ValueType.Int),
            ("second_parameter", ValueType.Int) };
        
    }
}
