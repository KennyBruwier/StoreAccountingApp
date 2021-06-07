using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.GeneralClasses
{
    public class SourceTargetProps
    {
        public PropertyInfo sourceProp { get; set; }
        public PropertyInfo targetProp { get; set; }

        public SourceTargetProps(PropertyInfo sourceProp = null, PropertyInfo targetProp = null)
        {
            this.sourceProp = sourceProp;
            this.targetProp = targetProp;
        }
    }
}
