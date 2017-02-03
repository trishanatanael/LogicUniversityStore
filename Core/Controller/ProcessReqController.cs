﻿using LogicUniversityStore.Dao;
using LogicUniversityStore.Model;
using LogicUniversityStore.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LogicUniversityStore.Controller
{
    public class ProcessReqController
    {
        public RequisitionDao RequisitionDao { get; set; }
        public RequisitionItemDao RequisitionItemDao { get; set; }
        public StockCardDao StockCardDao { get; set; }

        public Dictionary<int, int> lockedItemsCountForProcess; // will hold SupplierItemID and Count
        public Dictionary<Requisition, double> mainList;  // will hold requisition and unfullfilItemNumbers

        public ProcessReqController()
        {
            LogicUniStoreModel db = new LogicUniStoreModel();
            RequisitionDao = new RequisitionDao(db);
            StockCardDao = new StockCardDao(db);
            RequisitionItemDao = new RequisitionItemDao(db);
            lockedItemsCountForProcess = new Dictionary<int, int>();
            mainList = new Dictionary<Requisition, double>();
        }

        public Dictionary<Requisition, double> GetMainProcessReqList()
        {
            LogicUniStoreModel db = new LogicUniStoreModel();
            List<Requisition> rList = db.Requisitions.Where(r => r.Status == RequisitionStatus.Approved.ToString()).ToList();
            foreach (var requisition in rList)
            {

                if (mainList.ContainsKey(requisition))
                {
                    continue;
                }

                int? unfullfilledItem = 0;
                double percentage = 100;
                //  int countReqItem = requisition.RequisitionItems.Count();
                double progrssMeter = 0.0;


                foreach (RequisitionItem item in requisition.RequisitionItems)
                {
                    RequisitionItem rItem = RequisitionItemDao.db.RequisitionItems.Find(item.ReqItemID);

                    if (!lockedItemsCountForProcess.ContainsKey(rItem.SupplierItem.ItemID))
                    {
                        lockedItemsCountForProcess.Add(rItem.SupplierItem.ItemID, 0);
                    }
                    int actualQuantityInStock = (StockCardDao.GetProductCountInStock(rItem.SupplierItem.ItemID) - lockedItemsCountForProcess[rItem.SupplierItem.ItemID]);

                    if ((actualQuantityInStock - rItem.NeededQuantity) >= 0)
                    {
                        lockedItemsCountForProcess[rItem.SupplierItem.ItemID] += rItem.NeededQuantity.Value;
                        rItem.ApprovedQuantity = rItem.NeededQuantity;
                        progrssMeter += 1;
                    }
                    else
                    {
                        lockedItemsCountForProcess[rItem.SupplierItem.ItemID] += actualQuantityInStock;
                        rItem.ApprovedQuantity = actualQuantityInStock;
                        unfullfilledItem += 1;
                        progrssMeter += (double)rItem.ApprovedQuantity / (double)rItem.NeededQuantity;
                    }
                }
                db.SaveChanges();

                //if (unfullfilledItem.Value == 0)
                //{
                //    percentage = 100;
                //}
                //else
                //{
                //    double value = (requisition.RequisitionItems.Count - Convert.ToDouble(unfullfilledItem)) / requisition.RequisitionItems.Count;
                //    percentage = value * 100;
                //}

                //   mainList.Add(requisition, percentage);
                mainList.Add(requisition, (progrssMeter / requisition.RequisitionItems.Count) * 100);
            }

            return mainList;

        }


        internal Requisition GetRequisition(int requisitonID)
        {
            return RequisitionDao.Find(requisitonID);
           // throw new NotImplementedException();
        }

        public List<StockCard> GetAllStockCard()
        {
            return StockCardDao.GetAllInStock();
        }

        public List<RequisitionItem> GetRequisitionItems(int requisitionID)
        {
            return RequisitionDao.GetRequisitionItemList(requisitionID);
        }

        public Boolean InitialInsertToApprovedQuantity(int reqId)
        {
            List<RequisitionItem> items = this.GetRequisitionItems(reqId);
            foreach (RequisitionItem item in items)
            {

            }
            return true;
        }

        public int GetApprovedRequistionCount()
        {
            return RequisitionDao.GetApprovedRequisitionCount();
        }

        public List<RequisitionItem> GetOutstandingItems()
        {
            return new RequisitionItemDao().GetOutstandingItems();
        }

        public string GenerateRetreivalForm(Tuple<int, List<int>> retReq)
        {
            return new RequisitionDao().GenerateRetreivalForm(retReq);
        }
    }
}