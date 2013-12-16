using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core {
    public enum TypeSearcherMessage {
        None,
        Init,
        Add,
        Fix
    }

    [Serializable]
    public class SearcherMessage {
        public TypeSearcherMessage Type {
            get;
            set;
        }

        public string Message {
            get;
            set;
        }

        public static SearcherMessage Parse(object obj) {
            SearcherMessage result = null;
            if (obj is SearcherMessage)
                result = obj as SearcherMessage;
            else if (obj is string) {
                var str = obj as string;
                result = new SearcherMessage();
                switch (str.Substring(0, 3)) {
                    case ":0:":
                        result.Type = TypeSearcherMessage.Init;
                        result.Message = str.Replace(":0:", "");
                        break;
                    case ":1:":
                        result.Type = TypeSearcherMessage.Add;
                        result.Message = str.Replace(":1:", "");
                        break;
                    case ":2:":
                        result.Type = TypeSearcherMessage.Fix;
                        result.Message = str.Replace(":2:", "");
                        break;
                    default:
                        result.Type = TypeSearcherMessage.None;
                        result.Message = str;
                        break;
                }
            }

            return result;
        }

        public override string ToString() {
            return string.Format("{0}: {1}", Type, Message);
        }

        public string ToString(bool toSend) {
            return toSend ? 
                Type == TypeSearcherMessage.None ? 
                    string.Format("{0}", Message) : 
                    string.Format(":{0}:{1}", (int)Type, Message) : 
                ToString();
        }
    }
}
