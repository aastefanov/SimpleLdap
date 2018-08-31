using SimpleLdap.Interfaces;

namespace SimpleLdap
{
    public class LdapConfiguration : ILdapConfiguration
    {
        public string ServerName { get; set; }
        public int Port { get; set; }

        public bool UseSsl { get; set; }
        public int SslPort { get; set; }

        public string BindUser { get; set; }
        public string BindPassword { get; set; }

        public ILdapProvider Provider { get; set; }
    }
}