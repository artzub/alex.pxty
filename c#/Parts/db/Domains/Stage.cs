using System;

namespace db.Domains
{
    public class Stage : Domain, IStage
    {
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
    }
}
