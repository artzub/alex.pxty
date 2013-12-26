using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using db.Domains;

namespace db.Mapping {
    public class DomainColumnsWrapper : BaseColumnsWrapper {
        public DomainColumnsWrapper(DataRow row) : base(row) {           
        }

        public object Id {
            get {
                return Row["ID"];
            }
        }
    }
}
