using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace CRM11.Utility
{
    public static class StringExtention
    {
        /// <summary>
        /// 扩展方法，比较字符串是否一致，忽略大小写
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static bool IsSame(this string str1,string str2)
        {
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
                return false;

            return str1.Equals(str2, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 获得MD5加密后的值
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string ToMD5(this string pwd)
        {
            //创键一个MD5的哈希算法实例
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

            ///计算获得pwd的哈希值
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(pwd));

            StringBuilder stringBuilder = new StringBuilder();

            
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 解密一个字符串
        /// </summary>
        /// <param name="decrystr"></param>
        /// <returns></returns>
        public static string ToDecryptString(this string decrystr)
        {
            FormsAuthenticationTicket formsAuthenticationTicket = FormsAuthentication.Decrypt(decrystr);
            return formsAuthenticationTicket.UserData;
        }

        /// <summary>
        /// 加密一个字符串
        /// </summary>
        /// <param name="encrystr"></param>
        /// <returns></returns>
        public static string ToEncryptString(this string encrystr)
        {
            FormsAuthenticationTicket formsAuthenticationTicket = new FormsAuthenticationTicket(1,"uid",DateTime.Now,DateTime.Now.AddMinutes(50),true,encrystr);
            return FormsAuthentication.Encrypt(formsAuthenticationTicket);

        }
    }
}
