using System;
using System.Security.Cryptography;
using System.Text;

namespace Application
{
	public static class Constants
	{
		public static class UserTypes
        {
			public const string STUDENT = "Student";
			public const string LECTURER = "Lecturer";
			public const string REGISTRAR = "Registrar";
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }

        public static string Secret = "AMSC9d8534b48w951b9287d492b256x";
    }
}

