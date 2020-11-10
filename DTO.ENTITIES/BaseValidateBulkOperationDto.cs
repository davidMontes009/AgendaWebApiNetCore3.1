using API.SHARED;
using System;
using System.Collections.Generic;

namespace DTO.ENTITIES
{
    public abstract class BaseValidateBulkOperationDto : IValidateBulkOperation
    {

        public IList<ErrorViewModel> Validations { get; set; } = new List<ErrorViewModel>();

        public bool IsValid => this.Validations.Count == 0;

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
