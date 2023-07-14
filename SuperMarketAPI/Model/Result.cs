namespace SuperMarketAPI.Model
{
    public class Result<T>
    {
        public string Status { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public T BaseResult { get; set; }


        public Result()
        {
            Status = "ERROR";
            Code = (int)CodeEnum.ERROR;
            Description = "";
        }

        public void SetResult(CodeEnum code, string description, T result)
        {
            Status = code == CodeEnum.SUCCESS ? "SUCCESS" : "ERROR";
            Code = (int)code;
            Description = description;
            BaseResult = result;
        }

    }

    public enum CodeEnum
    {
        SUCCESS = 10,
        ERROR = 40
    }
}
