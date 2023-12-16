using System.Windows.Markup;

namespace Library.Mvvm;

public class ServiceProviderSource : MarkupExtension
{
    public static Func<Type, object> Resolver { get; set; }
    public Type Type { get; set; }
    
    public override object ProvideValue(IServiceProvider serviceProvider) => Resolver?.Invoke(Type);
}