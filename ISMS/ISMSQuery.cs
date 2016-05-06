﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMS
{
    /// <summary>
    /// 发送短信查询接口
    /// </summary>
    public interface ISMSQuery
    {
        /// <summary>
        /// 根据传入的信息进行短信发送状态的查询
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        object QueryMsg(string result);
    }
}