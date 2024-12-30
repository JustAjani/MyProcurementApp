using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace InputOutputOperation.PdfInterface
{
    public interface IPDF<T> where T : class
    {
        Task CreatePDF(T modelList, Page page);
    }
}
