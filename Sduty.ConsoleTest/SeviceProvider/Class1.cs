using System;
using System.Collections.Generic;
using System.Text;

namespace Sduty.ConsoleTest.SeviceProvider
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public interface IGux
    {
        IFoo Foo { get; }
        IBar Bar { get; }
        IBaz Baz { get; }
    }
    public class Foo : IFoo { }
    public class Bar : IBar { }
    public class Baz : IBaz { }
    public class Gux : IGux
    {
        public IFoo Foo { get; private set; }
        public IBar Bar { get; private set; }
        public IBaz Baz { get; private set; }

        public Gux(IFoo foo, IBar bar, IBaz baz)
        {
            this.Foo = foo;
            this.Bar = bar;
            this.Baz = baz;
        }
    }
}
