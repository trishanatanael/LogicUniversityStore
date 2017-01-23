﻿using LogicUniversityStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LogicUniversityStore.Util;

namespace LogicUniversityStore.Dao
{
    public class RequisitionDao
    {
        public LogicUniStoreModel db = new LogicUniStoreModel();

        public List<Requisition> GetApprovedRequisitionList()
        {
            //return  db.Requisitions.Where(r => r.Status.Equals(RequisitionStatus.Approved.ToString())).ToList();
            return db.Requisitions.Where(r => r.Status.Equals(RequisitionStatus.Requested.ToString())).ToList();
        }

        public List<Requisition> GetRequisitionList()
        {
            return db.Requisitions.ToList();
        }

        public List<RequisitionItem> GetRequisitionItemList(int requisitionID)
        {
            Requisition req = db.Requisitions.Find(requisitionID);
            if (req != null)
            {
                return req.RequisitionItems.ToList();
            }
            return null;
        }

        public void Create(Requisition requistion)
        {
            db.Requisitions.Add(requistion);
            db.SaveChanges();
        }

        public bool Remove(int reqID)
        {
            Requisition requisition = db.Requisitions.Find(reqID);
            if (requisition != null)
            {
                db.Requisitions.Remove(requisition);
                return true;
            }
            return false;
        }
    }
}