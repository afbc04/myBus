using PacketTemplates;

public class ProgramHandler {

    public static void load_templates() {

        PacketTemplates.TemplateLoader.load_templates();
        Console.WriteLine($"{TemplateLoader.templates_loaded} templates were loaded!");

    }

    public static async Task log_requests(HttpContext context, Func<Task> next) {

        var begin_time = DateTime.UtcNow;

        await next();

        var time = DateTime.UtcNow - begin_time;
        var method = context.Request.Method;
        var uri = context.Request.Path + context.Request.QueryString;
        var status = context.Response.StatusCode;
        var content_length = context.Response.ContentLength ?? 0;

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write($"[{DateTime.Now:dd/MMM/yyyy:HH:mm:ss}] ");

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"\u001b[1m{method} {uri} \u001b[0m");

        ConsoleColor status_color =
            status >= 200 && status < 300 ? ConsoleColor.Green :
            status >= 300 && status < 400 ? ConsoleColor.Cyan :
            status >= 400 && status < 500 ? ConsoleColor.Yellow :
            ConsoleColor.Red;

        Console.ForegroundColor = status_color;
        Console.Write($"{status} ");

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write($"{time.TotalMilliseconds:F1}ms {content_length}B");

        Console.WriteLine();
        Console.ResetColor();
    }

}