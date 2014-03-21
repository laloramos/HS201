using HS201_FinalAssignment.Data_Access;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace HS201.FinalAssignment.DependencyResolution
{
    public class NHibernateRegistry : Registry
    {
        public NHibernateRegistry()
        {
            For<ISessionFactory>()
                .Singleton()
                .Use(new NhConfiguration().CreateConfiguration().BuildSessionFactory());
            For<ISession>().HybridHttpOrThreadLocalScoped().Use(ctx => ctx.GetInstance<ISessionFactory>().OpenSession());
        }
    }
}