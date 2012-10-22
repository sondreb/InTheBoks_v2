// Source: https://github.com/RickStrahl/AspNetWebApiArticle/blob/master/AspNetWebApi/Code/WebApi/Filters/ApiMessageError.cs

using System.Collections.Generic;

namespace InTheBoks.Web
{
    /// <summary>
    /// Class that represents an error returned to
    /// the client. Can be explicitly returned or
    /// as part of the UnhandledExceptionFilter
    /// </summary>
    public class ApiMessageError
    {
        public ApiMessageError()
            : this(null)
        {
        }

        public ApiMessageError(string errorMessage)
        {
            isCallbackError = true;
            errors = new List<string>();
            message = errorMessage;
        }

        public List<string> errors { get; set; }

        public bool isCallbackError { get; set; }

        public string message { get; set; }
    }
}