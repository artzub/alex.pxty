﻿namespace Controller {
    public interface IController {
        object Delete(Db.IDomain item);
        System.Data.DataTable GetDataInTable(Db.DataAccess.Queries select = null);
		object Save(Db.IDomain item);
		object Update(Db.IDomain item);
        object GetNew();
        object GetItemById(object id);
        object AddItem(object item);
        void RemoveItem(object item);
    }
}
