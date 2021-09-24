using System.Collections.Generic;

namespace TheGStore.Helpers
{
    public class ResponseResult<ResponseData>
    {
        public bool IsSuccessful => Errors.Count == 0 && StatusCode > 199 && StatusCode < 300;
        public int StatusCode { get; set; } 
        public List<string> Errors { get; set; } = new List<string>();
        public ResponseData Data { get; set; }
    }
}
