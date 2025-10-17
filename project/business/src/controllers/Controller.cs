using System.Threading;
using PacketHandlers;
using Token;
using Nito.AsyncEx;
using Pages;

namespace Controller {

    public class ControllerManager {

        private readonly AsyncReaderWriterLock _lock;

        private CountryCodeController _country_codes;

        public ControllerManager() {
            this._lock = new();
            this._country_codes = new CountryCodeController();
        }

        /// #################################
        ///           COUNTRY CODES
        /// #################################
        public async Task<SendingPacket> create_country_code(AccessToken? token, string id, string name) {

            var controller_lock = await this._lock.WriterLockAsync();
            var country_code_manager_lock = await this._country_codes.Lock.WriterLockAsync();
            controller_lock.Dispose();

            try {

                var response = await this._country_codes.create(token,id,name);
                return response;
                
            } finally {
                country_code_manager_lock.Dispose();
            }
        
        }

        public async Task<SendingPacket> list_country_code(AccessToken? token, PageInput page) {

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

        public async Task<SendingPacket> update_country_code(AccessToken? token, string id, string? name) {

            var controller_lock = await this._lock.WriterLockAsync();
            var country_code_manager_lock = await this._country_codes.Lock.WriterLockAsync();
            controller_lock.Dispose();

            try {

                var response = await this._country_codes.update(token,id,name);
                return response;
                
            } finally {
                country_code_manager_lock.Dispose();
            }
        
        }

    }

}

