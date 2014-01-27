using System;
using Db.Domains;

namespace Db {
    public static class Types {

        public static Type Domain {
            get {
                return typeof(Domain);
            }
        }

        public static Type Named {
            get {
                return typeof(INamed);
            }
        }

        public static Type Alloy {
            get {
                return typeof(Alloy);
            }
        }

        public static Type Surface {
            get {
                return typeof(Surface);
            }
        }

        public static Type TypeDep {
            get {
                return typeof(TypeDep);
            }
        }

        public static Type Departament {
            get {
                return typeof(Departament);
            }
        }

        public static Type Part {
            get {
                return typeof(Part);
            }
        }

        public static Type Stage {
            get {
                return typeof(Stage);
            }
        }
    }
}
