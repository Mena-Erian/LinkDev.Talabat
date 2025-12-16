using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Errors
{
    public class ApiExceptionResponse(int statusCode, string? message = null, string? details = null) : ApiErrorResponse(statusCode, message)
    {
        public string? Details { get; set; } = details;
        public IEnumerable<string>? Errors { get; set; }
        public override string ToString()
        {
            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Serialize(this, serializerOptions);
        }

    }
}
