using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAL.BLL.Models;

namespace UAL.BLL.Data
{
    public class OrderRepository
    {
        public int CreateOrder(Order o)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            try
            {
                ual.Orders.Add(o);
                ual.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public Order getOrder(string o)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            Order or = ual.Orders.Where(m => m.OrderRef == o).ToList().SingleOrDefault();
            return or;
        }
        public List<Order> getOrderList()
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            List<Order> lor = ual.Orders.ToList();
            return lor;
        }
        public Sale getSalesItem(string woID)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            Sale s = ual.Sales.Where(m => m.WO == woID).ToList().FirstOrDefault();
            return s;
        }
        public List<Sale> getWorkOrderList()
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            List<Sale> lor = ual.Sales.ToList();
            return lor;
        }
        public int updateStatus(string wo)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            var update = ual.Sales.Where(m => m.WO == wo).ToList().FirstOrDefault();
            if (update != null)
            {
                update.Status = "Completed";
                ual.SaveChanges();
            }
            return 1;
        }
        public int updateOrder(Order o)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            var updatedOrder = ual.Orders.Where(m => m.OrderRef == o.OrderRef).SingleOrDefault();
            if (updatedOrder != null)
            {
                updatedOrder.OrderRefNo = o.OrderRefNo;
                updatedOrder.OrderQty = o.OrderQty;
                updatedOrder.FROMCompany = o.FROMCompany;
                updatedOrder.DeliveryPlace = o.DeliveryPlace;
                updatedOrder.DeliveryDate = o.DeliveryDate;
                updatedOrder.Date = o.Date;
                updatedOrder.CustomerName = o.CustomerName;
                updatedOrder.Attn = o.Attn;
                updatedOrder.AccessoryType = o.AccessoryType;
                updatedOrder.ReferenceNo = o.ReferenceNo;
                updatedOrder.Remark = o.Remark;
                updatedOrder.TOCompany = o.TOCompany;
                ual.SaveChanges();
                return 1;
            }
            return 0;

        }
        public List<Sale> getSalesList(string label, string month, string year)
        {
            string x = DateTime.Now.Month.ToString();
            
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            string y = ual.Sales.Select(s => s.Label.ToLower()).ToList().FirstOrDefault();
            List<Sale> ls = ual.Sales.Where(s => s.Label.ToLower().Contains(label.ToLower()) && s.WOIssueDate.Month.ToString().Contains(month) 
                && s.WOIssueDate.Year.ToString().Contains(year)).ToList();
            return ls;
        }
        public int createSales(Sale s, string[] size, string[] qty)
        {

            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            try
            {
                
                WOSize wos = new WOSize();
                for (int i = 0; i < size.Length; i++)
                {
                    switch (i)// this switch case
                    {
                        case 0: { wos.sn1 = size[i]; wos.s1 = int.Parse(qty[i]); break; }
                        case 1: { wos.sn2 = size[i]; wos.s2 = int.Parse(qty[i]); break; }
                        case 2: { wos.sn3 = size[i]; wos.s3 = int.Parse(qty[i]); break; }
                        case 3: { wos.sn4 = size[i]; wos.s4 = int.Parse(qty[i]); break; }
                        case 4: { wos.sn5 = size[i]; wos.s5 = int.Parse(qty[i]); break; }
                        case 5: { wos.sn6 = size[i]; wos.s6 = int.Parse(qty[i]); break; }
                        case 6: { wos.sn7 = size[i]; wos.s7 = int.Parse(qty[i]); break; }
                        case 7: { wos.sn8 = size[i]; wos.s8 = int.Parse(qty[i]); break; }
                        case 8: { wos.sn9 = size[i]; wos.s9 = int.Parse(qty[i]); break; }
                        case 9: { wos.sn10 = size[i]; wos.s10 = int.Parse(qty[i]); break; }
                        case 10: { wos.sn11 = size[i]; wos.s11 = int.Parse(qty[i]); break; }
                        case 11: { wos.sn12 = size[i]; wos.s12 = int.Parse(qty[i]); break; }
                        case 12: { wos.sn13 = size[i]; wos.s13 = int.Parse(qty[i]); break; }
                        case 13: { wos.sn14 = size[i]; wos.s14 = int.Parse(qty[i]); break; }
                        
                    }
                }
                if (wos.sn1 != null)
                {
                    
                    ual.WOSizes.Add(wos);
                    ual.SaveChanges();
                    s.IsSizewise = true;
                    
                }
                else
                {
                    s.IsSizewise = false;
                }
                s.WOS = ual.WOSizes.Select(m=>m.id).ToList().Last();
                ual.Sales.Add(s);
                ual.SaveChanges();
                return 1;
            }
            catch(Exception ex)
            {
                throw ex;
                
            }
            return 0;
            
        }
        public int hasSize(string order)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            int hasSize = ual.Sizes.Where(m => m.OrderRef == order).ToList().Count();
            return hasSize;
        }
        public int createSizewiseOrder(Size s)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            ual.Sizes.Add(s);
            ual.SaveChanges();
            return 1;
        }
        public int createPO(Breakdown bd)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            ual.Breakdowns.Add(bd);
            ual.SaveChanges();
            return 1;
        }
        public List<Breakdown> getBreakDown(string refN){
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            List<Breakdown> lbd = ual.Breakdowns.Where(m => m.ReferenceNo == refN).ToList();
            return lbd;
        }

        public int createIndent(string wo, string refN,long sil)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            Indent i = new Indent();
            i.ReferenceNo = refN;
            i.WorkOrder = wo;
            i.IndentNo = sil;
            i.Status = "Not Ready";
            ual.Indents.Add(i);
            ual.SaveChanges();
            return 1;
        }

        public long getSIL()
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            long sil = ual.Indents.Select(m => m.IndentNo).Distinct().Count()+1;
            return sil;
        }

        public List<Indent> getFollowUpList(long[] list)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            List<Indent> li = ual.Indents.Where(i => list.Contains(i.IndentNo)).OrderBy(i => i.IndentNo).ToList();
            return li;
        }
        public string generateWONumber(string dept)
        {
            string output = "";
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            int x = ual.Sales.Where(m => m.Label.Contains(dept)).Count();
            output = x.ToString();
            switch (dept.ToLower())
            {
                case "woven": { output = "WL-"+ output; break; }
                case "needle": { output = "NL" + output; break; }
                case "printed": { output = "PL" + output; break; }
                case "ribbon": { output = "RW" + output; break; }
                case "offset": { output = "OP" + output; break; }
            }
            return output;
        }
        public int CreatePurchaseOrder(PurchaseOrder po)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            po.Status = "Pending";
            ual.PurchaseOrders.Add(po);
            try
            {
                ual.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public List<PurchaseOrder> listOfPO()
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            List<PurchaseOrder> lopo = ual.PurchaseOrders.GroupBy(x => x.OrderRef).Select(x => x.FirstOrDefault()).ToList();
            //List<PurchaseOrder> lpo = new List<PurchaseOrder>();
            
            //foreach (string x in lopo) {
            //    PurchaseOrder y = new PurchaseOrder();
            //    y.OrderRef = x;
            //    lpo.Add(y);
            //}
            return lopo;
        }
        public int updatePOStatus(string OrdRefs)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            List<PurchaseOrder> pos = ual.PurchaseOrders.Where(m => m.OrderRef == OrdRefs).ToList();
            foreach (PurchaseOrder pox in pos)
            {
                if (pox != null)
                {
                    pox.Status = "Completed";
                    
                    
                }
            }
            try { ual.SaveChanges(); return 1; }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<PurchaseOrder> GetPurchaseOrder(string id)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            List<PurchaseOrder> pox = ual.PurchaseOrders.Where(m => m.OrderRef == id).ToList();
            return pox;
        }
        public PurchaseOrder GetPurchaseOrder(long id)
        {
            UnitedAccessoriesDBEntities ual = new UnitedAccessoriesDBEntities();
            PurchaseOrder pox = ual.PurchaseOrders.Where(m => m.ID == id).ToList().FirstOrDefault();
            return pox;
        }
    }
}
