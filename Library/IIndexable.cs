using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library {
    public interface IIndexable {
        int[] indexes {
            get;
            set;
        }
    }
}
