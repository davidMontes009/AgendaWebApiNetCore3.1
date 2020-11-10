using System.Collections.Generic;

namespace API.SHARED
{
    public interface IValidate
    {
        IList<ErrorViewModel> Validations { get; set; }

        bool IsValid { get; }

        void Validate();
    }
}
