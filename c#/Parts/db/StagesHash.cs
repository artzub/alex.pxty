using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Db.Domains;

namespace Db
{
	internal static class Hashes
	{
		public static readonly Dictionary<object, Stage> StagesHash = new Dictionary<object, Stage>();
		public static Stage GetStageById(object id) {
			Stage item;
		    StagesHash.TryGetValue(id, 
                out item);
			return item;
		}

        public static bool RemoveStageById(object id) {
            return Remove(GetStageById(id));
        }

        public static readonly Dictionary<object, Departament> DepartamentHash = new Dictionary<object, Departament>();
        public static Departament GetDepartamentById(object id) {
            Departament item;
            DepartamentHash.TryGetValue(id, 
                out item);
            return item;
        }

        public static bool RemoveDepartamentById(object id) {
            return Remove(GetDepartamentById(id));
        }

        public static readonly Dictionary<object, Part> PartHash = new Dictionary<object, Part>();
        public static Part GetPartById(object id) {
            Part item = null;
            PartHash.TryGetValue(id,
                out item);
            return item;
        }

        public static bool RemovePartById(object id) {
            return Remove(GetPartById(id));
        }

        public static readonly Dictionary<object, TypeDep> TypeDepHash = new Dictionary<object, TypeDep>();
        public static TypeDep GetTypeDepById(object id) {
            TypeDep item;
            TypeDepHash.TryGetValue(id,
                out item);
            return item;
        }

        public static bool RemoveTypeDepById(object id) {
            return Remove(GetTypeDepById(id));
        }

        public static readonly Dictionary<object, Surface> SurfaceHash = new Dictionary<object, Surface>();
        public static Surface GetSurfaceById(object id) {
            Surface item;
            SurfaceHash.TryGetValue(id,
                out item);
            return item;
        }

        public static bool RemoveSurfaceById(object id) {
            return Remove(GetSurfaceById(id));
        }

        public static readonly Dictionary<object, Alloy> AlloyHash = new Dictionary<object, Alloy>();
        public static Alloy GetAlloyById(object id) {
            Alloy item;
            AlloyHash.TryGetValue(id,
                out item);
            return item;
        }

	    public static bool RemoveAlloyById(object id) {
            return Remove(GetAlloyById(id));
	    }

	    public static bool Remove<T>(object id) {
	        bool res = false;

	        var type = typeof(T);
	        if (Types.Alloy.IsAssignableFrom(type)) {
	            res = RemoveAlloyById(id);
	        }
            else if (Types.Part.IsAssignableFrom(type)) {
                res = RemovePartById(id);
            }
            else if (Types.Surface.IsAssignableFrom(type)) {
                res = RemoveSurfaceById(id);
            }
            else if (Types.TypeDep.IsAssignableFrom(type)) {
                res = RemoveTypeDepById(id);
            }
            else if (Types.Departament.IsAssignableFrom(type)) {
                res = RemoveDepartamentById(id);
            }
            else if (Types.Stage.IsAssignableFrom(type)) {
                res = RemoveStageById(id);
            }
	        return res;
	    }

	    private static bool Remove(IDomain item) {
	        if (item == null)
	            return true;

	        var res = false;
	        var type = item.GetType();

	        if (Types.Alloy.IsAssignableFrom(type)) {
	            try {
                    var curItem = item as Alloy;
	                
                    res = (curItem != null);
                    if (res) {
                        res = !AlloyHash.ContainsKey(item.Id);

                        if (!res) {
                            res = !curItem.LazyValueCreated;
                            if (!res) {
                                foreach (var next in curItem.Parts.ToArray()) {
                                    res = Remove(next);
                                    if (!res)
                                        break;
                                }
                            }

                            res = res && AlloyHash.Remove(item.Id);
                        }
                    }
	            }
	            catch (Exception e) {
	                throw e;
	            }
	        }
            else if (Types.Part.IsAssignableFrom(type)) {
                try {
                    var curItem = item as Part;

                    res = (curItem != null);
                    if (res) {
                        res = !PartHash.ContainsKey(item.Id);

                        if (!res) {

                            if (curItem.Alloy != null
                                && curItem.Alloy.LazyValueCreated)
                                curItem.Alloy.Parts.Remove(curItem);

                            res = !curItem.LazyValueCreated;

                            if (!res) {
                                foreach (var next in curItem.Stages.ToArray()) {
                                    res = Remove(next);
                                    if (!res)
                                        break;
                                }
                            }

                            res = res && PartHash.Remove(item.Id);
                        }
                    }

                }
                catch (Exception e) {
                    throw e;
                }
            }
            else if (Types.Surface.IsAssignableFrom(type)) {
                try {
                    var curItem = item as Surface;

                    res = (curItem != null);
                    if (res) {
                        res = !SurfaceHash.ContainsKey(item.Id);

                        if (!res) {
                            res = !curItem.LazyValueCreated;

                            if (!res) {
                                foreach (var next in curItem.Stages.ToArray()) {
                                    res = Remove(next);
                                    if (!res)
                                        break;
                                }
                            }

                            res = res && SurfaceHash.Remove(item.Id);
                        }
                    }

                }
                catch (Exception e) {
                    throw e;
                }
            }
            else if (Types.TypeDep.IsAssignableFrom(type)) {
                try {
                    var curItem = item as TypeDep;

                    res = (curItem != null);
                    if (res) {
                        res = TypeDepHash.ContainsKey(item.Id);
                        if (!res) {
                            res = !curItem.LazyValueCreated;

                            if (!res) {
                                foreach (var next in curItem.Departaments.ToArray()) {
                                    res = Remove(next);
                                    if (!res)
                                        break;
                                }
                            }

                            res = res && TypeDepHash.Remove(item.Id);
                        }
                    }
                }
                catch (Exception e) {
                    throw e;
                }
            }
            else if (Types.Departament.IsAssignableFrom(type)) {
                try {
                    var curItem = item as Departament;

                    res = (curItem != null);
                    if (res) {
                        res = !DepartamentHash.ContainsKey(item.Id);
                        if (!res) {
                            if (curItem.TypeDep != null
                                && curItem.TypeDep.LazyValueCreated)
                                curItem.TypeDep.Departaments.Remove(curItem);

                            res = !curItem.LazyValueCreated;

                            if (!res) {
                                foreach (var next in curItem.Stages.ToArray()) {
                                    res = Remove(next);
                                    if (!res)
                                        break;
                                }
                            }

                            res = res && DepartamentHash.Remove(item.Id);
                        }
                    }
                }
                catch (Exception e) {
                    throw e;
                }
            }
            else if (Types.Stage.IsAssignableFrom(type)) {
                try {
                    var curItem = item as Stage;

                    res = (curItem != null);
                    if (res) {
                        res = !StagesHash.ContainsKey(item.Id);
                        if (!res) {
                            if (curItem.Part != null
                                && curItem.Part.LazyValueCreated)
                                curItem.Part.Stages.Remove(curItem);

                            if (curItem.Departament != null
                                && curItem.Departament.LazyValueCreated)
                                curItem.Departament.Stages.Remove(curItem);

                            if (curItem.Surface != null
                                && curItem.Surface.LazyValueCreated)
                                curItem.Surface.Stages.Remove(curItem);

                            if (curItem.StageNext.CompareTo(curItem) != 0)
                                res = Remove(curItem.StageNext);

                            res = res && StagesHash.Remove(item.Id);
                        }
                    }
                }
                catch (Exception e) {
                    throw e;
                }
            }

	        return res;
	    }
	}
}

