using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Db {
    public interface IDefaultEmpty<T> {
        T Default {
            get;
        }

        T Empty {
            get;
        }
    }
}
