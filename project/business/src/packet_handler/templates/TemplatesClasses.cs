namespace PacketTemplates {

    public abstract class TemplateValidatorField {
        public bool is_required { get; set; }
        public bool is_list { get; set; }
    }

    public class TemplateValidatorItem : TemplateValidatorField {

        public Type datatype { get; set; }

        public TemplateValidatorItem(bool is_required, Type datatype, bool is_list) {
            this.is_required = is_required;
            this.datatype = datatype;
            this.is_list = is_list;
        }
    }

    public class TemplateValidatorObject : TemplateValidatorField {

        public Dictionary<string, TemplateValidatorField> obj { get; set; }

        public TemplateValidatorObject(bool is_required, bool is_list) {
            this.is_required = is_required;
            this.is_list = is_list;
            this.obj = new();
        }

        public void add_child(Dictionary<string, TemplateValidatorField> obj) {
            this.obj = obj;
        }
    }

    public class TemplateValidatorBody {
        
        public bool is_required { get; set; }
        public Dictionary<string, TemplateValidatorField> body { get; set; }

        public TemplateValidatorBody(bool isRequired) {
            this.is_required = isRequired;
            this.body = new();
        }

        public void add_body(Dictionary<string, TemplateValidatorField> body) {
            this.body = body;
        }
    }

    public class TemplateValidatorAuth {
        
        public bool is_required { get; set; }

        public TemplateValidatorAuth(bool isRequired) {
            this.is_required = isRequired;
        }

    }

}
