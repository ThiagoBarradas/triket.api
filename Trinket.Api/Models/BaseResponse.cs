using Nancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trinket.Api.Models
{
    public class BaseResponse<T>
    {
        public bool IsSuccess { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public T SuccessBody { get; set; }

        public ErrorsResponse ErrorBody { get; set; }

        public void AddError(ErrorItemResponse error)
        {
            if (this.ErrorBody == null)
            {
                this.ErrorBody = new ErrorsResponse();
            }

            this.IsSuccess = false;
            this.ErrorBody.Errors.Add(error);
        }
    }
}
