using System.Collections.Generic;

namespace Controller {
    public interface IController<T> : IController {        
        /*object Save(T item);
        object Update(T item);*/
        T GetNew(object id);
        T GetById(object id);
        T AddItem(T item);
        void RemoveItem(T item);
        IList<T> GetData(Db.DataAccess.Queries select = null);
        IList<T> Items {
            get;
        }
    }
}
