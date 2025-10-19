using System.Threading;
using PacketHandlers;
using Token;
using Nito.AsyncEx;
using Pages;

namespace Controller {

    public class ControllerManager {

        private readonly AsyncReaderWriterLock _lock;

        private CountryCodeController _country_codes;
        private BusPassController _bus_passes;

        public ControllerManager() {
            this._lock = new();
            this._country_codes = new CountryCodeController();
            this._bus_passes = new BusPassController();
        }

        /// #################################
        ///           COUNTRY CODES
        /// #################################
        public async Task<SendingPacket> create_country_code(AccessToken? token, CountryCodeRequestWrapper request_wrapper) {

            var controller_lock = await this._lock.WriterLockAsync();
            var country_code_manager_lock = await this._country_codes.Lock.WriterLockAsync();
            controller_lock.Dispose();

            try {

                var response = await this._country_codes.create(token,request_wrapper);
                return response;
                
            } finally {
                country_code_manager_lock.Dispose();
            }
        
        }

        public async Task<SendingPacket> list_country_code(AccessToken? token, PageRequest page) {

            var controller_lock = await this._lock.ReaderLockAsync();
            var country_code_manager_lock = await this._country_codes.Lock.ReaderLockAsync();
            controller_lock.Dispose();

            try {

                var response = await this._country_codes.list(token,page);
                return response;
                
            } finally {
                country_code_manager_lock.Dispose();
            }
        
        }

        public async Task<SendingPacket> clear_country_code(AccessToken? token) {

            var controller_lock = await this._lock.WriterLockAsync();
            var country_code_manager_lock = await this._country_codes.Lock.WriterLockAsync();
            controller_lock.Dispose();

            try {

                var response = await this._country_codes.clear(token);
                return response;
                
            } finally {
                country_code_manager_lock.Dispose();
            }
        
        }

        public async Task<SendingPacket> get_country_code(AccessToken? token, string id) {

            var controller_lock = await this._lock.ReaderLockAsync();
            var country_code_manager_lock = await this._country_codes.Lock.ReaderLockAsync();
            controller_lock.Dispose();

            try {

                var response = await this._country_codes.get(token,id);
                return response;
                
            } finally {
                country_code_manager_lock.Dispose();
            }

        }

        public async Task<SendingPacket> delete_country_code(AccessToken? token, string id) {

            var controller_lock = await this._lock.WriterLockAsync();
            var country_code_manager_lock = await this._country_codes.Lock.WriterLockAsync();
            controller_lock.Dispose();

            try {

                var response = await this._country_codes.delete(token,id);
                return response;
                
            } finally {
                country_code_manager_lock.Dispose();
            }
        
        }

        public async Task<SendingPacket> update_country_code(AccessToken? token, string id, CountryCodeRequestWrapper request_wrapper) {

            var controller_lock = await this._lock.WriterLockAsync();
            var country_code_manager_lock = await this._country_codes.Lock.WriterLockAsync();
            controller_lock.Dispose();

            try {

                var response = await this._country_codes.update(token,id,request_wrapper);
                return response;
                
            } finally {
                country_code_manager_lock.Dispose();
            }
        
        }

        /// #################################
        ///           BUS PASSES
        /// #################################
        public async Task<SendingPacket> create_bus_pass(AccessToken? token, BusPassRequestWrapper request_wrapper) {

            var controller_lock = await this._lock.WriterLockAsync();
            var bus_pass_manager_lock = await this._bus_passes.Lock.WriterLockAsync();
            controller_lock.Dispose();

            try {

                var response = await this._bus_passes.create(token,request_wrapper);
                return response;
                
            } finally {
                bus_pass_manager_lock.Dispose();
            }
        
        }

        public async Task<SendingPacket> list_bus_pass(AccessToken? token, PageRequest page) {

            var controller_lock = await this._lock.ReaderLockAsync();
            var bus_pass_manager_lock = await this._bus_passes.Lock.ReaderLockAsync();
            controller_lock.Dispose();

            try {

                var response = await this._bus_passes.list(token,page);
                return response;
                
            } finally {
                bus_pass_manager_lock.Dispose();
            }
        
        }

        public async Task<SendingPacket> clear_bus_pass(AccessToken? token) {

            var controller_lock = await this._lock.WriterLockAsync();
            var bus_pass_manager_lock = await this._bus_passes.Lock.WriterLockAsync();
            controller_lock.Dispose();

            try {

                var response = await this._bus_passes.clear(token);
                return response;
                
            } finally {
                bus_pass_manager_lock.Dispose();
            }
        
        }

        public async Task<SendingPacket> get_bus_pass(AccessToken? token, string id) {

            var controller_lock = await this._lock.ReaderLockAsync();
            var bus_pass_manager_lock = await this._bus_passes.Lock.ReaderLockAsync();
            controller_lock.Dispose();

            try {

                var response = await this._bus_passes.get(token,id);
                return response;
                
            } finally {
                bus_pass_manager_lock.Dispose();
            }

        }

        public async Task<SendingPacket> delete_bus_pass(AccessToken? token, string id) {

            var controller_lock = await this._lock.WriterLockAsync();
            var bus_pass_manager_lock = await this._bus_passes.Lock.WriterLockAsync();
            controller_lock.Dispose();

            try {

                var response = await this._bus_passes.delete(token,id);
                return response;
                
            } finally {
                bus_pass_manager_lock.Dispose();
            }
        
        }

        public async Task<SendingPacket> update_bus_pass(AccessToken? token, string id, BusPassRequestWrapper request_wrapper) {

            var controller_lock = await this._lock.WriterLockAsync();
            var bus_pass_manager_lock = await this._bus_passes.Lock.WriterLockAsync();
            controller_lock.Dispose();

            try {

                var response = await this._bus_passes.update(token,id,request_wrapper);
                return response;
                
            } finally {
                bus_pass_manager_lock.Dispose();
            }
        
        }

    }

}

