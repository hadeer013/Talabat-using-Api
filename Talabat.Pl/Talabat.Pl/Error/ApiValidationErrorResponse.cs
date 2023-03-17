using System;
using System.Collections;
using System.Collections.Generic;

namespace Talabat.Pl.Error
{
    public class ApiValidationErrorResponse:ApiErrorResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {
        }

        public IEnumerable<string> Error { get; set; }
    }
}
