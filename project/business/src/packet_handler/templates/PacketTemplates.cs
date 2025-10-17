using System.Xml.Linq;
using Serilog;

namespace PacketTemplates {

    public class TemplateObject {

        public TemplateValidatorAuth? auth {get; set;}
        public TemplateValidatorBody? body {get; set;}

        public TemplateObject() {
            this.auth = null;
            this.body = null;
        }

    }

    public static class TemplateLoader {

        private static readonly string templates_directory = "src/packet_handler/templates/xmls";
        private static readonly Dictionary<string,TemplateObject> templates = new();

        public static void load_templates() {

            int templates_loaded = 0;
            int templated_failed = 0;

            foreach (var file in Directory.GetFiles(TemplateLoader.templates_directory, "*.xml")) {

                try {

                    var doc = XDocument.Load(file);
                    var templatesList = doc.Element("templates")?.Elements("template") ?? new List<XElement>();

                    foreach (var template in templatesList) {

                        string templateID = $"{Path.GetFileNameWithoutExtension(file)}/{template.Attribute("id")!.Value}";

                        var template_object = new TemplateObject();

                        template_object.auth = handle_auth(template);
                        template_object.body = handle_body(template);

                        templates[templateID] = template_object;
                        templates_loaded++;

                    }
                }
                catch {
                    Log.Warning($"Couldn't load \"{file}\" template");
                    templated_failed++;
                }

            }

            Log.Information($"Request templates were loaded ( {templates_loaded} loaded | {templated_failed} failed )");

        }

        private static TemplateValidatorAuth? handle_auth(XElement template) {

            var auth_element = template.Element("auth");

            if (auth_element == null) 
                return null;

            bool is_required = (auth_element.Attribute("required")?.Value ?? "FALSE").ToUpper() == "TRUE";

            return new TemplateValidatorAuth(is_required);

        }

        private static TemplateValidatorBody? handle_body(XElement template) {

            var body_element = template.Element("body");

            if (body_element == null)
                return null;

            bool is_required = (body_element.Attribute("required")?.Value ?? "FALSE").ToUpper() == "TRUE";
            var body = new TemplateValidatorBody(is_required);

            var inner_body = handle_inner_body(body_element);
            body.add_body(inner_body);

            return body;
        }

        private static Dictionary<string, TemplateValidatorField> handle_inner_body(XElement element) {

            var body = new Dictionary<string, TemplateValidatorField>();

            var items = element.Elements("i");
            foreach (var item in items) {

                var item_name = item.Attribute("name")?.Value;
                if (item_name == null)
                    continue;

                bool is_required = (item.Attribute("required")?.Value ?? "FALSE").ToUpper() == "TRUE";
                bool is_list = (item.Attribute("list")?.Value ?? "FALSE").ToUpper() == "TRUE";

                Type datatype = (item.Attribute("datatype")?.Value ?? "string").ToLower() switch {
                    "integer" => typeof(int),
                    "float" => typeof(float),
                    "bool" => typeof(bool),
                    _ => typeof(string)
                };

                body[item_name] = new TemplateValidatorItem(is_required, datatype, is_list);
            }

            var objects = element.Elements("o");
            foreach (var obj in objects) {

                var object_name = obj.Attribute("name")?.Value;
                if (object_name == null)
                    continue;

                bool is_required = (obj.Attribute("required")?.Value ?? "FALSE").ToUpper() == "TRUE";
                bool is_list = (obj.Attribute("list")?.Value ?? "FALSE").ToUpper() == "TRUE";

                var template_object = new TemplateValidatorObject(is_required, is_list);
                var body_inside_object = handle_inner_body(obj);
                template_object.add_child(body_inside_object);

                body[object_name] = template_object;
            }

            return body;
        }

        public static TemplateObject? get_template(string id) {
            return templates.ContainsKey(id) ? templates[id] : null;
        }
    }
}
