using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.BLL
{
    public class UtilsBLL
    {
        public static string BreakCamelCase(string CamelString)
        {
            string output = string.Empty;
            bool SpaceAdded = true;

            for (int i = 0; i < CamelString.Length; i++)
            {
                if (CamelString.Substring(i, 1) ==
                    CamelString.Substring(i, 1).ToLower())
                {
                    output += CamelString.Substring(i, 1);
                    SpaceAdded = false;
                }
                else
                {
                    if (!SpaceAdded)
                    {
                        output += " ";
                        output += CamelString.Substring(i, 1);
                        SpaceAdded = true;
                    }
                    else
                        output += CamelString.Substring(i, 1);
                }
            }
            return output;
        }
    }
}
