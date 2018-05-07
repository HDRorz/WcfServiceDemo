using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfExtensions.Extra
{
    public class TokenValidManager
    {
        /// <summary>
        /// 验证token是否合法，随便实现了一个
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsTokenValid(string token)
        {
            var random = new Random();

            var ranValue = random.Next(100);

            if (ranValue < 50)
            {
                return false;
            }

            return true;
        }
    }
}
