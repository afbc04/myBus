using Controller;

public class API {

    public static ControllerManager? controller {get; private set;} = null;

    public static bool init() {

        try {

            ProgramHandler.start_logger();
            PacketTemplates.TemplateLoader.load_templates();
            API.controller = new();

            return true;

        }
        catch {
            return false;
        }

    }

}