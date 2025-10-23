using PacketHandlers;

public class BusPassRequestWrapper {

    public string id {get;set;}
    public double? discount {get;set;}
    public long? localityLevel {get; set;}
    public long? duration {get; set;}
    public bool? isActive {get; set;}

    public BusPassRequestWrapper(string id, IDictionary<string,object> body) {
        this.id = id;
        this.discount = (double?) PacketUtils.get_value_double(body,"discount");
        this.localityLevel = (long?) PacketUtils.get_value(body,"localityLevel");
        this.duration = (long?) PacketUtils.get_value(body,"duration");
        this.isActive = (bool?) PacketUtils.get_value(body,"active");
    }

    public void auto_fill(BusPass bp) {
        this.id = this.id ?? bp.ID;
        this.discount =  this.discount ?? bp.discount;
        this.localityLevel = this.localityLevel ?? bp.locality_level;
        this.duration = this.duration ?? bp.duration_days;
        this.isActive = this.isActive ?? bp.is_active;
    }

    public BusPass convert() {
        return new BusPass(
            this.id.ToUpper(),
            this.discount ?? 0,
            Convert.ToInt16(this.localityLevel ?? 1),
            Convert.ToInt32(this.duration ?? 30),
            this.isActive ?? true
            );
    }

}