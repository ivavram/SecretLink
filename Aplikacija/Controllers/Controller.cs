namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{
    public Context Context { get; set; }

    public Controller(Context context)
    {
        Context = context;
    }
}
