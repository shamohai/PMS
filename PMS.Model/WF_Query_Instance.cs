//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMS.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class WF_Query_Instance
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public System.DateTime SubTime { get; set; }
        public int StartedBy { get; set; }
        public short Level { get; set; }
        public string SubForm { get; set; }
        public int Status { get; set; }
        public int Result { get; set; }
        public Nullable<int> WF_TempID { get; set; }
        public System.Guid ApplicationId { get; set; }
    }
}
