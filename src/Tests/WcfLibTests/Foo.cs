using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBrad.WcfLib;
using Wcf = System.ServiceModel;
namespace WcfLibTests
{
    [Wcf.ServiceContract]
    public interface IFoo
    {
        [Wcf.OperationContract]
        string GetName();

        [Wcf.OperationContract]
        void SetName(string name);

        [Wcf.OperationContract]
        int GetValue();

        [Wcf.OperationContract]
        void SetValue(int value);
    }

    public class Foo : IFoo
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string GetName()
        {
            return this.Name;
        }

        public int GetValue()
        {
            return this.Value;
        }

        public void SetName(string name)
        {
            this.Name = name;
        }

        public void SetValue(int value)
        {
            this.Value = value;
        }
    }
}
