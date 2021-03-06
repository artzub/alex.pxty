﻿using System;

namespace Db.Domains
{
    public class Stage : Domain/*, IDefaultEmpty<Stage>*/ {
        private void init (object idStagePrev = null, object idStageNext = null,
            object idDepartament = null, object idSurface = null, object idPart = null)
		{
			IdStagePrev = idStagePrev;
            IdStageNext = idStageNext;
            IdDepartament = idDepartament;
            IdPart = idPart;
            IdSurface = idSurface;
		}

        public Stage(object id)
            : base(id) {
            init();
        }

        public Stage(object id = null, object idStagePrev = null, object idStageNext = null,
            object idDepartament = null, object idSurface = null, object idPart = null) : base(id) {
            init(idStagePrev, idStageNext, idDepartament, idSurface, idPart);
        }

        public Stage(object id = null, Stage stagePrev = null, Stage stageNext = null,
            Departament departament = null, Surface surface = null, Part part = null)
            : base(id) {
            //init();
            StageNext = stageNext;
            StagePrev = stagePrev;
            Departament = departament;
            Surface = surface;
            Part = part;
            InitIds();
        }

        private void InitIds() {

            //TODO: по хорошему этот код должен быть прописан в свойствах.
            if (StageNext != null) {
                IdStageNext = StageNext.Id;
            }

            if (StagePrev != null) {
                IdStagePrev = StagePrev.Id;
            }

            if (Departament != null) {
                IdDepartament = Departament.Id;
            }

            if (Surface != null) {
                IdSurface = Surface.Id;
            }

            if (Part != null) {
                IdPart = Part.Id;
            }
        }

        public object IdStagePrev {
			get;
			set;
        }

        public object IdStageNext {
			get;
			set;
        }

        public object IdSurface {
			get;
			set;
        }

        public object IdDepartament {
			get;
			set;
        }

        public object IdPart {
			get;
			set;
        }

        public Stage StageNext {
			get;
			set;
        }

        public Stage StagePrev {
			get;
			set;
        }

        public Surface Surface {
			get;
			set;
        }

        public Departament Departament {
			get;
			set;
        }

        public Part Part {
			get;
			set;
        }

        public override void Update(IDomain obj) {
            var item = obj as Stage;

            if (item == null)
                return;

            base.Update(obj);

            init(item.IdStagePrev, item.IdStageNext, item.IdDepartament, item.IdSurface, item.IdPart);

            StageNext = item.StageNext;
            StagePrev = item.StagePrev;
            Part = item.Part;
            Surface = item.Surface;
            Departament = item.Departament;

            InitIds();
        }

        #region IDefaultEmpty<Stage> Members

        public static Stage Default {
            get {
                return new Stage(1, 1, 1, 1, 1, 1);
            }
        }

        public static Stage Empty {
            get {
                return new Stage(null, null, null, null, null, null);
            }
        }

        #endregion

		public override string ToString () {
			return string.Format ("{0} ({1})", Id, Departament, Part);
		}
    }
}
