using System;

namespace WebAPIToolkit.Dtos.Errors
{
    public class FieldErrorDto
    {

        public FieldErrorDto(string path, string message, string id)
        {
            this.Path = path;
            this.Message = message;
            this.Id = id;
            Level = LevelEnum.ERROR;
        }

        public string Path { get; set; }

        public string Message { get; set; }

        public string Id { get; set; }

        private LevelEnum Level { get; set; }
    }
}