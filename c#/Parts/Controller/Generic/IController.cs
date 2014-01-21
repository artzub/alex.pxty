using System.Collections.Generic;

namespace Controller {
    public interface IController<T> : IController {        
        object Save(T item);
        object Update(T item);
        T GetNew(object id);
        T GetById(object id);
        ICollection<T> GetData(Db.DataAccess.Queries select = null);
    }
}
