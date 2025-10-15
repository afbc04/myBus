using System.Threading;
using PacketHandlers;
using Token;

namespace Controller {

    public class ControllerManager {

        public static ControllerManager controller = new ControllerManager(); 

        private ICountryCodeController _countryCodes;

        private readonly ReaderWriterLockSlim _lock;

        public ControllerManager() {
            this._lock = new();
            this._countryCodes = new CountryCodeController();
        }

        /// #################################
        ///           COUNTRY CODES
        /// #################################
        public SendingPacket createCountryCode(AccessToken? token, string id, string name) {
            using (new ControllerWriteLock(_lock))
                return this._countryCodes.createCountryCode(token,id,name);
        }

        public SendingPacket listCountryCode(AccessToken? token) {
            using (new ControllerReadLock(_lock))
                return this._countryCodes.listCountryCode(token);
        }

        public SendingPacket getCountryCode(AccessToken? token, string id) {
            using (new ControllerReadLock(_lock))
                return this._countryCodes.getCountryCode(token,id);
        }

        public SendingPacket deleteCountryCode(AccessToken? token, string id) {
            using (new ControllerWriteLock(_lock))
                return this._countryCodes.deleteCountryCode(token,id);
        }

        public SendingPacket updateCountryCode(AccessToken? token, string id, string name) {
            using (new ControllerWriteLock(_lock))
                return this._countryCodes.updateCountryCode(token,id,name);
        }

        public SendingPacket patchCountryCode(AccessToken? token, string id, string? name) {
            using (new ControllerWriteLock(_lock))
                return this._countryCodes.patchCountryCode(token,id,name);
        }


        

        /// #################################
        ///           CONTROLLER LOCKS
        /// #################################
        private class ControllerWriteLock : IDisposable {
            private readonly ReaderWriterLockSlim _lock;

            public ControllerWriteLock(ReaderWriterLockSlim _lock) {
                this._lock = _lock;
                this._lock.EnterWriteLock();
            }

            public void Dispose() {
                _lock.ExitWriteLock();
            }
        }

        private class ControllerReadLock : IDisposable {
            private readonly ReaderWriterLockSlim _lock;

            public ControllerReadLock(ReaderWriterLockSlim _lock) {
                this._lock = _lock;
                this._lock.EnterReadLock();
            }

            public void Dispose() {
                _lock.ExitReadLock();
            }
        }

    }

}

