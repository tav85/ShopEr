﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.Common;

/// <summary>
/// Summary description for ProductImageManager
/// </summary>
public class ProductImageManager
{
	public ProductImageManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public struct IncludeToFill
    {
        public bool Id;
        public bool ImageUrl;
    }

    public static ProductImageItem FillOne(DataRow r, IncludeToFill inc)
    {
        ProductImageItem p = new ProductImageItem();
        if (inc.Id) p.Id = (int)r["Id"];
        if (inc.ImageUrl) p.ImageURL = (string)r["ImageURL"];
        return p;
    }

    public static List<ProductImageItem> FillAll(DataTable table, IncludeToFill inc)
    {
        List<ProductImageItem> list = new List<ProductImageItem>();

        for (int i = 0; i < table.Rows.Count; i++)
        {
            list.Add(FillOne(table.Rows[i], inc));
        }

        return list;
    }


    public static List<ProductImageItem> GetImagesForProduct(int productId)
    {
        DbCommand comm = DataAccess.CreateCommand();
        comm.CommandType = CommandType.Text;

        comm.CommandText = "SELECT * FROM ProductImage WHERE ProductId = @ProductId";

        comm.CreateAndAddParameter("@ProductId", productId, DbType.Int32);

        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        //List<ProductImageItem> list = new List<ProductImageItem>();
        //ProductImageItem p;
        //for (int i = 0; i < table.Rows.Count; i++)
        //{
        //    p = new ProductImageItem();

        //    p.Id = (int)table.Rows[i]["Id"];
        //    p.ImageURL = (string)table.Rows[i]["ImageURL"];
        //    list.Add(p);

        //}

        IncludeToFill inc;
        inc.Id = true;
        inc.ImageUrl = true;

        return FillAll(table, inc);
    }
}
