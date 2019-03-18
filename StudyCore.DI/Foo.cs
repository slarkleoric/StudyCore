using System;

namespace StudyCore.DI
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public interface IQux { }

    public class Foo : IFoo
    {
        public IBar Bar { get; private set; }

        [Injection]
        public IBaz Baz { get; set; }

        public Foo() { }

        [Injection]
        public Foo(IBar bar)
        {
            this.Bar = bar;
        }
    }

    public class Bar : IBar { }
    public class Baz : IBaz
    {
        public IQux Qux { get; private set; }

        [Injection]
        public void Initialize(IQux qux)
        {
            this.Qux = qux;
        }
    }
    public class Qux : IQux { }
}
