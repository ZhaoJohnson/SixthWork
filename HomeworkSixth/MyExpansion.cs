using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSixth
{
    public static class MyExpansion
    {
        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static int GetEnumValue(this Enum e)
        {
            return Convert.ToInt32(e);
        }

        /// <summary>
        /// 获取枚举名称
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetEnumName(this Enum e)
        {
            return Enum.GetName(e.GetType(), e);
        }
    }
}