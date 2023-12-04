using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class IDgenerator
    {
        private static Random random = new Random();
        private static HashSet<int> generatedIDs = new HashSet<int>();

        public static int GenerateUniqueRandomID()
        {
            int newID;
            do
            {
                newID = random.Next(10000, 99999);
            } while (!generatedIDs.Add(newID));

            return newID;
        }
    }
}
