﻿namespace WcfServiceLibrary {
    public class SchrService : ISchrService {

        public string AddItem(int item) {
            var schrw = SearcherWeb.Instance;
            var res = -1;
            var str = "Value is not correct! ";
            if (schrw.Initialized)
                res = schrw.AddItem(item);

            if (res > -1)
                str = "";
            return str + GetState();
        }

        public string Fix() {
            var schrw = SearcherWeb.Instance;
            var str = "Fix was failed\n";
            if (schrw.Initialized && schrw.Fix())
                str = "";
            return str + GetState();
        }

        public string GetState() {
            return SearcherWeb.Instance.ItemsToString();
        }

        public string Init(int step) {
            SearcherWeb.Instance.Init(step);
            return GetState();
        }
    }
}
