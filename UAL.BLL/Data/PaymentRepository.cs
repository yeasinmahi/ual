using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAL.BLL.Models;
namespace UAL.BLL.Data
{
    public class PaymentRepository
    {
        public int SubmitPayment(Payment p)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            ual.Payments.Add(p);
            ual.SaveChanges();
            return 1;
        }
        public int getInvoiceNumber()
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            int x = ual.Payments.Select(m => m.PaymentID).ToList().Last();
            return x;
        }
        public List<Indent> getProformaInvoice(long[] list)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            List<Indent> li = ual.Indents.Where(i => list.Contains(i.IndentNo)).OrderBy(i => i.IndentNo).ToList();
            return li;
        }
    }
}
