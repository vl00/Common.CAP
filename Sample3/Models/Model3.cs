using Common.CAP;

namespace WebAppl2.Models;

public class Model3 : ICapParams<Model1>
{
    public Model1 Data { get; set; }
    public string S { get; set; }
    public object Res { get; set; }

    object ICapParams.Data 
    { 
        get => Data;
        set => Data = (Model1)value;
    }
}
