namespace SimpleLdap.Interfaces
{
    public interface ILdapConfiguration
    {
        string ServerName { get; set; }
        int Port { get; set; }

        bool UseSsl { get; set; }
        int SslPort { get; set; }

        string BindUser { get; set; }
        string BindPassword { get; set; }
        
        ILdapProvider Provider { get; set; }
    }
}