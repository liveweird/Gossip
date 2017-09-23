using System;

namespace Gossip.Contract
{
    public class ApiResult
    {
        public ResultType ResultType { get; set; }
        public string Result { get; set; }

        public static ApiResult Success()
        {
            return new ApiResult
            {
                ResultType = ResultType.Success
            };
        }

        public static ApiResult Fail(string customErrorMessage)
        {
            //TODO: save customErrorMessage in the logger

            return new ApiResult
            {
                ResultType = ResultType.Error,
                Result = customErrorMessage
            };
        }

        public static ApiResult Fail(Exception ex)
        {
            //TODO: save exeption in the logger

            return new ApiResult
            {
                ResultType = ResultType.Error,
                Result = "Internal Error."
            };
        }
    }
}