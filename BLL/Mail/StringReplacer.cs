using System.Text;
using System.Text.RegularExpressions;
using AutoMapper.Configuration.Annotations;
using DAL.Migrations;

namespace BLL.Mail
{
    public static class StringReplacer
    {
        public static StringBuilder ReplaceString(StringBuilder source, IDictionary<string, string> replaceValues)
        {
            StringBuilder result = new StringBuilder();
            result.Append(source);

            foreach (KeyValuePair<string, string> keyValuePair in replaceValues)
            {
                Regex regex = new Regex($"\\$\\{{ {keyValuePair.Key} \\}}");

                result.Replace($"\\$\\{{ {keyValuePair.Key} \\}}", keyValuePair.Value);
            }

            return result;
        }
    }
}