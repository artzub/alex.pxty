namespace Db.DataAccess {
	public interface IConnectSetting {		
		bool Save();
		object Load();
		void Apply();
		void Clear();
		string ToString();
	}
}
