﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.IBLL
{
    public interface IBaseDelBLL
    {
        bool PhysicsDel(List<int> list_ids);
    }
}
