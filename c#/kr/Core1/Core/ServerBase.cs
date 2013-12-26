using System;
using System.Collections.Generic;
using System.Text;

namespace Core {
    public abstract class ServerBase {
        protected virtual string DoWork(SearcherMessage msg, ref Searcher schr) {
            var result = string.Empty;
            try {
                switch (msg.Type) {
                    //:0:2
                    case TypeSearcherMessage.Init:
                        schr = new Searcher(Convert.ToInt32(msg.Message));
                        if (schr == null)
                            result = "-1";
                        else
                            result = string.Format("Searcher made (step {0})", schr.Step);
                        break;
                    //:1:2,3,4,5
                    case TypeSearcherMessage.Add:
                        //make
                        if (schr == null) {
                            return "-1";
                        }

                        foreach (var item in msg.Message.Split(','))
                            if (!string.IsNullOrEmpty(item))
                                schr.AddItem(Convert.ToInt32(item));
                        result = string.Format("Seacher filled: {0}", schr);
                        break;
                    case TypeSearcherMessage.Fix:
                        //run
                        if (schr == null)                            
                            return "-1";

                        schr.Fix();
                        result = string.Format("Seacher fixed: {0}", schr);
                        break;
                }                
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
