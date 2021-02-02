using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Time_to_burn_fear
{
    public class DAO
    {
        public static void AddStringToFile(string addingString,string fileName)
        {
            StreamWriter streamWriter = new StreamWriter(fileName, true);
            streamWriter.WriteLine(addingString);
            streamWriter.Close();
        }
    }
}
