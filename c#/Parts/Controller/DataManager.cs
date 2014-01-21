﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db.DataAccess;
using Db.Domains;
using Db.Mapping;
using Db;
using System.Reflection;

namespace Controller {
    public class DataManager {
        private AlloyController alloyController;
        private TypeDepController typeDepController;
        private DepartamentController departamentController;
        private SurfaceController surfaceController;
        private StageController stageController;
        private PartController partController;

        private DataManager() {
            var conn = "User ID=PARTS;" +
                "Password=Zelda;" +
                    "Data Source=(" +
                    "DESCRIPTION=(" +
                    "ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))" +
                    "(CONNECT_DATA=(SERVER=DEDICATED)" +
                    "(SERVICE_NAME=XE)))";
            OracleConnection.Instance.Initialize(conn);

            OracleConnection.Instance.Open();
            Provider.Initialize(OracleConnection.Instance);
        }

        private static DataManager instance;
        public static DataManager Instance {
            get {
                if (instance == null)
                    instance = new DataManager();
                return instance;
            }
        }
        
        public AlloyController AlloyController {
            get {
                if (alloyController == null)
                    alloyController = new AlloyController();
                return alloyController;
            }
        }

        public TypeDepController TypeDepController {
            get {
                if (typeDepController == null)
                    typeDepController = new TypeDepController();
                return typeDepController;
            }
        }

        public DepartamentController DepartamentController {
            get {
                if (departamentController == null)
                    departamentController = new DepartamentController();
                return departamentController;
            }
        }

        public SurfaceController SurfaceController {
            get {
                if (surfaceController == null)
                    surfaceController = new SurfaceController();
                return surfaceController;
            }
        }

        public StageController StageController {
            get {
                if (stageController == null)
                    stageController = new StageController();
                return stageController;
            }
        }

        public PartController PartController {
            get {
                if (partController == null)
                    partController = new PartController();
                return partController;
            }
        }

        #region GetData

        public object GetAllByType(Type type) {
            object result = null;
			//return result;
            if (Types.Alloy.IsAssignableFrom(type)) {
                result = Alloies;
            }
            else if (Types.Surface.IsAssignableFrom(type)) {
                result = Surfaces;
            }
            else if (Types.TypeDep.IsAssignableFrom(type)) {
                result = TypeDeps;
            }
            else if (Types.Part.IsAssignableFrom(type)) {
                result = Parts;
            }
            else if (Types.Departament.IsAssignableFrom(type)) {
                result = Departaments;
            }
            else if (Types.Stage.IsAssignableFrom(type)) {
                result = Stages;
            }
            return result;
        }

        private HashSet<Alloy> alloies;
        public ICollection<Alloy> Alloies {
            get {
                if (alloies == null)
                    alloies = new HashSet<Alloy>(AlloyController.GetData());
                return alloies;
            }
        }

        private HashSet<Surface> surfaces;
        public ICollection<Surface> Surfaces {
            get {
                if (surfaces == null)
                    surfaces = new HashSet<Surface>(SurfaceController.GetData());
                return surfaces;
            }
        }

        private HashSet<TypeDep> typeDeps;
        public ICollection<TypeDep> TypeDeps {
            get {
                if (typeDeps == null)
                    typeDeps = new HashSet<TypeDep>(TypeDepController.GetData());
                return typeDeps;
            }        
        }

        private HashSet<Departament> departaments;
        public ICollection<Departament> Departaments {
            get {
                if (departaments == null)
                    departaments = new HashSet<Departament>(DepartamentController.GetData());
                return departaments;
            }
        }

        private HashSet<Part> parts;
        public ICollection<Part> Parts {
            get {
                if (parts == null)
                    parts = new HashSet<Part>(PartController.GetData());
                return parts;
            }
        }

        private HashSet<Stage> stages;
        public ICollection<Stage> Stages {
            get {
                if (stages == null)
                    stages = new HashSet<Stage>(StageController.GetData());
                return stages;
            }
        }

        #endregion

        #region Data Manipulation

        private object InvokeAction(IDomain item, string action) {
            if (item == null)
                return item;

            var type = item.GetType();
            object result = null;

            IController ctrl = GetContollerByType(type);

            if (ctrl != null) {

				var method = ctrl.GetType().GetMethod (action);

				result = method.Invoke(ctrl, new object[] {item});

                //result = ctrl.Save(item);
            }

            return result;
        }

        private IController GetContollerByType(Type type) {
            IController ctrl = null;

            if (Types.Alloy.IsAssignableFrom(type)) {
                ctrl = AlloyController;
            }
            else if (Types.Surface.IsAssignableFrom(type)) {
                ctrl = SurfaceController;
            }
            else if (Types.TypeDep.IsAssignableFrom(type)) {
                ctrl = TypeDepController;
            }
            else if (Types.Departament.IsAssignableFrom(type)) {
                ctrl = DepartamentController;
            }
            else if (Types.Part.IsAssignableFrom(type)) {
                ctrl = PartController;
            }
            else if (Types.Stage.IsAssignableFrom(type)) {
                ctrl = StageController;
            }
            return ctrl;
        } 

        public object Save(IDomain item) {
			var res = InvokeAction (item, "Save");
			if (item == null || res == null || res.Equals (0)) 
				return res;

			var type = item.GetType ();
			if (Types.Alloy.IsAssignableFrom (type))
				Alloies.Add (AlloyController.GetById (res));

            return res;
        }

        public object Update(IDomain item) {
            return InvokeAction(item, "Update");
        }

        public object Delete(IDomain item) {
            return InvokeAction(item, "Delete");
        }

        #region GetById

        public IDomain GetById(Type type, object id) {
            var ctor = GetContollerByType(type);
            return (IDomain)ctor.GetItemById(id);
        }

        public Alloy GetAlloyById(object id) {
            return (Alloy)GetById(Types.Alloy, id);
        }

        public Surface GetSurfaceById(object id) {
            return (Surface)GetById(Types.Surface, id);
        }

        public Part GetPartById(object id) {
            return (Part)GetById(Types.Part, id);
        }

        public Stage GetStageById(object id) {
            return (Stage)GetById(Types.Stage, id);
        }

        public TypeDep GetTypeDepById(object id) {
            return (TypeDep)GetById(Types.TypeDep, id);
        }

        public Departament GetDepartamentById(object id) {
            return (Departament)GetById(Types.Departament, id);
        }

        #endregion


        #region GetNewItem
        
        public object GetNewItem(Type type) {
            if (type == null)
                return null;

            var ctrl = GetContollerByType(type);

            return ctrl.GetNew();
        }

        public Alloy GetNewAlloy() {
            return (Alloy)GetNewItem(Types.Alloy);
        }

        public TypeDep GetNewTypeDep() {
            return (TypeDep)GetNewItem(Types.TypeDep);
        }

        public Surface GetNewSurface() {
            return (Surface)GetNewItem(Types.Surface);
        }

        public Departament GetNewDepartament() {
            return (Departament)GetNewItem(Types.Departament);
        }

        public Part GetNewPart() {
            return (Part)GetNewItem(Types.Part);
        }

        public Stage GetNewStage() {
            return (Stage)GetNewItem(Types.Stage);
        }

        #endregion

        #endregion


    }
}
