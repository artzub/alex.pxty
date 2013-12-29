using System;

namespace Db.Domains
{
    public class Stage : Domain, IStage/*, IDefaultEmpty<Stage>*/ {
        private void init (object idStagePrev = null, object idStageNext = null,
            object idDepartament = null, object idSurface = null, object idPart = null)
		{
			IdStagePrev = idStagePrev;
            IdStageNext = idStageNext;
            IdDepartament = idDepartament;
            IdPart = idPart;
            IdSurface = idSurface;
		}

        public Stage(object id = null, object idStagePrev = null, object idStageNext = null,
            object idDepartament = null, object idSurface = null, object idPart = null) : base(id) {
            init(idStagePrev, idStageNext, idDepartament, idSurface, idPart);
        }

        public Stage(object id = null, IStage stagePrev = null, IStage stageNext = null,
            IDepartament departament = null, ISurface surface = null, IPart part = null)
            : base(id) {
            //init();
            StageNext = stageNext;
            StagePrev = stagePrev;
            Departament = departament;
            Surface = surface;
            Part = part;

            //TODO: по хорошему этот код должен быть прописан в свойствах.
            if (StageNext != null)
                IdStageNext = StageNext.Id;

            if (StagePrev != null)
                IdStagePrev = StagePrev.Id;

            if (Departament != null)
                IdDepartament = Departament.Id;

            if (Surface != null)
                IdSurface = Surface.Id;

            if (Part != null)
                IdPart = Part.Id;

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

        public IStage StageNext {
			get;
			set;
        }

        public IStage StagePrev {
			get;
			set;
        }

        public ISurface Surface {
			get;
			set;
        }

        public IDepartament Departament {
			get;
			set;
        }

        public IPart Part {
			get;
			set;
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
    }
}
