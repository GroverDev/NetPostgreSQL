using static Common.Utilities.Message;

namespace Common.Utilities;

public class Response<T>
{
    public Response()
    {
        this.Data = default(T);
        this.Message = new Msg();
        this.Ok = false;
        Errors = [];
    }
    public bool Ok { get; set; }
    public T? Data { get; set; }
    public Msg Message { get; set; } = new Msg() { Description = "", Id = "0"  };

    public IEnumerable<Bases.BaseError>? Errors { get; set; }
}
