using System;
using Db.Domains;

namespace Db {
    public static class Types {
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
