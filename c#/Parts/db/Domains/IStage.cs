using System;

namespace db.Domains
{
    public interface IStage : IDomain {


        object IdStagePrev { 
            get; 
            set; 
        }

        object IdStageNext {
            get;
            set;
        }

        object IdSurface {
            get;
            set;
        }

        object IdDepartament {
            get;
            set;
        }


        object IdPart {
            get;
            set;
        }
        

        IStage StageNext {
            get;
            set;
        }

        IStage StagePrev {
            get;
            set;
        }

        ISurface Surface {
            get;
            set;
        }

        IDepartament Departament {
            get;
            set;
        }

        IPart Part {
            get;
            set;
        }
    }
}

