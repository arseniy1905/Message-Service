using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
namespace MessageService.Common.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Load the  <see cref="Type"/> from the base directory of the current domain using full type name as a <see cref="string"/>.
        /// (Includes assembly name) .
        /// This extension is mostly used to get the <see cref="Type"/> which is not referenced in the current project (dll,exe),
        /// but it's assembly file exists in domain base directory.
        /// The <see cref="string"/>  has to be in the following format:
        /// [namespace].[type], [assembly name], Version=[version], Culture=[culture], PublicKeyToken=[token]. 
        /// </summary>
        /// <param name="fullTypeName"></param>
        /// <returns><see cref="Type"/></returns>
        /// <exception cref="FormatException">The exception is thrown when the <see cref="string"/> type name is not in following format:
        /// [namespace].[type], [assembly name], Version=[version], Culture=[culture], PublicKeyToken=[token]. For more information please see:
        /// https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assemblyname?view=netframework-4.8
        /// See also <seealso cref="AssemblyName"/>
        /// </exception>
        /// <exception cref="FileNotFoundException"></exception>
        public static Type LoadTypeByName(this string fullTypeName)
        {
            var formatExceptionMessage = "String '{0}' is not in valid format. Please use the following format: [namespace].[type], [assembly name], Version=[version], Culture=[culture], PublicKeyToken=[token]. For more information: https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assemblyname?view=netframework-4.8";
            var fullTypeNameArray = fullTypeName.Split(",");
            if (fullTypeNameArray.Length < 5)
            {
                throw new FormatException(string.Format(formatExceptionMessage, fullTypeName));
            }
            var assemblyFullName = new StringBuilder(fullTypeNameArray[1].Trim())//name
                .Append(fullTypeNameArray[2])//version
                .Append(fullTypeNameArray[3])//culture
                .Append(fullTypeNameArray[4]).ToString();//token

            var assemblyFileName = string.Format("{0}.dll", fullTypeNameArray[1].Trim());
            var assemblyPath = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, string.Format("*{0}", assemblyFileName)).FirstOrDefault() ?? "";

            if (!File.Exists(assemblyPath))
            {
                throw new FileNotFoundException(string.Format("File '{0}' cannot be found. Assembly: '{1}'. ", assemblyFileName, assemblyFullName));
            }
            var assembly = Assembly.LoadFrom(assemblyPath);
            var type = assembly.GetType(fullTypeNameArray[0]);

            if (type == null)
            {
                throw new ArgumentException(string.Format("Type '{0}' cannot be found in Assembly: '{1}' ", fullTypeNameArray[0], assemblyFullName));
            }
            return type;
        }
    }
}
