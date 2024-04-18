﻿//using static Common.Utilities.Message;

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
    public Msg Message { get; set; } = new Msg() { Description = "", Id = "0" };

    public IEnumerable<Bases.BaseError>? Errors { get; set; }

    public Response<T> SetMessage(Msg mensaje)
    {
        this.Message = mensaje;
        return this;
    }
    public Response<T> SetMessage(MessageTypes messageType, string descripciónMensaje)
    {
        this.Message.SetMessage(messageType, descripciónMensaje);
        return this;
    }
    public Response<T> SetLogMessage(MessageTypes messageType, string description, Exception ex)
    {
        this.Ok = false;
        this.Message.SetLogMessage(messageType, description, ex);
        return this;
    }

}
