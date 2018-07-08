namespace SimpleLdap.Interfaces
{
    public interface ILdapServerConfiguration<TAttribute>
    {
        ILdapProvider Provider { get; set; }
    }
}