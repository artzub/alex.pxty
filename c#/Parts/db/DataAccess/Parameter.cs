using System.Data;

namespace db.DataAccess {
	public class Parameter {
		
		public Parameter(string name, object value, int size, ParameterDirection direction = ParameterDirection.Input) {
			Name = name;
			Value = value;
			Direction = direction;
			Size = size;
		}

		public Parameter(string name, object value, ParameterDirection direction = ParameterDirection.Input) {
			Name = name;
			Value = value;
			Direction = direction;
            Size = 0;
		}

        public int Size {
            get;
            private set;
        }

        public string Name {
            get;
            private set;
        }

        public object Value {
            get;
            private set;
        }

        public ParameterDirection Direction {
            get;
            private set;
        }
	}
}
