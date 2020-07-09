using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinPerformanceTest.DependencyServices
{
    public interface IWriteContactService
    {
        int WriteContact(Contact contact);
    }
}
