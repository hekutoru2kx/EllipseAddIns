﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedClassLibrary.Ellipse;

namespace PlaneacionFerrocarril.PlanHistory
{
    public class PlanHistoryActions
    {
        public static List<PlanHistoryItem> ReviewPlanHistory(EllipseFunctions ef, string startDate, string finishDate, string workGroup, string idConcepto)
        {
            var list = new List<PlanHistoryItem>();
            var sqlQuery = Queries.GetReviewPlanHistoryQuery(startDate, finishDate, workGroup, idConcepto);
            var dr = ef.GetQueryResult(sqlQuery);
            if (dr == null || dr.IsClosed)
                return list;
            while (dr.Read())
            {
                list.Add(new PlanHistoryItem(dr));
            }

            return list;
        }

        public static int LoadPlanHistoryItem(EllipseFunctions ef, PlanHistoryItem item)
        {
            var sqlQuery = Queries.GetUpdatePlanHistoryItemQuery(item.Fecha, item.Grupo, item.IdConcepto, item.Concepto, item.Valor1, item.Valor2);
            
            return ef.ExecuteQuery(sqlQuery.GetGeneratedSql());
        }
    }
}
