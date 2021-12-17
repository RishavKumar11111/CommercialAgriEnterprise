using System;
using System.Text;
using System.Web.Http.Filters;

namespace CommercialAgriEnterprise.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RandomnoAttribute : ActionFilterAttribute
    {
        private string randnum()
        {
            string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@_*";
            Random randomclass = new Random();
            StringBuilder s = new StringBuilder();
            char oneChar;
            for (int i = 0; i < 32; i++)
            {
                oneChar = input[randomclass.Next(0, input.Length)];
                s.Append(oneChar);
            }
            return s.ToString();
        }
    }
}