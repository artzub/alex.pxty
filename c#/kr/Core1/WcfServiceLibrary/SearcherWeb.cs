using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;

namespace WcfServiceLibrary {
    public class SearcherWeb : ISearcher {
        private Searcher schr;
        private Searcher Schr {
            get {
                return schr;
            }
            set {                
                schr = value;
            }
        }

        public void Init(int step) {
            Initialized = false;
            Schr = new Searcher(step);
            Initialized = true;
        }

        private SearcherWeb() {
            Initialized = false;
        }

        private static SearcherWeb instance;
        public static SearcherWeb Instance {
            get {
                if (instance == null)
                    instance = new SearcherWeb();
                return instance;
            }            
        }

        public bool Initialized {
            get;
            private set;
        }

        public int AddItem(int item) {
            if (Initialized)
                return schr.AddItem(item);
            return -1;
        }

        public bool Fix() {
            return Initialized && schr.Fix();
        }

        public string ItemsToString() {
            var str = "Searcher was not initialized";
            if (Initialized)
                str = ToString();
            return str;
        }

        public int Step {
            get {
                return schr.Step;
            }
            set {
                schr.Step = value;
            }
        }

        public override string ToString() {
            return schr.ToString();
        }
    }
}