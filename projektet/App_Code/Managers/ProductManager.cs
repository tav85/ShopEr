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
/// Summary description for ProductManager
/// </summary>
public class ProductManager
{
	public ProductManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public struct IncludeToFill
    {
        public bool Amount;
        public bool Description;
        public bool Discount;
        public bool Featured;
        public bool Id;
        public bool Images;
        public bool Name;
        public bool ShowOnPage;
        public bool Price;
    }

    public static ProductItem FillOne(DataRow r, IncludeToFill i)
    {
        
        ProductItem p = new ProductItem();
        if (i.Amount) p.Amount = (int)r["Amount"];
        if (i.Description) p.Description = (string)r["Description"];
        if (i.Discount) p.Discount = (int)r["Discount"];
        if (i.Featured) p.Featured = (bool)r["Featured"];
        if (i.Id) p.Id = (int)r["Id"];
        if (i.Images) p.Images = ProductImageManager.GetImagesForProduct(p.Id);
        if (i.Name) p.Name = (string)r["Name"];
        if (i.Price) p.Price = (int)r["Price"];
        if (i.ShowOnPage) p.ShowOnPage = (bool)r["ShowOnPage"];

        return p;
    }

    public static List<ProductItem> FillAll(DataTable table, IncludeToFill inc)
    {
        List<ProductItem> list = new List<ProductItem>();

        for (int i = 0; i < table.Rows.Count; i++)
        {
            list.Add(FillOne(table.Rows[i], inc));
        }

        return list;
    }

    public static ProductItem GetProductById(int id)
    {
        DbCommand comm = DataAccess.CreateCommand();
        comm.CommandType = CommandType.Text;

        comm.CommandText = "SELECT Amount, Description, Discount, Featured, Id, Name, Price, FROM Product WHERE Id = @Id";

        comm.CreateAndAddParameter("@Id", id, DbType.Int32);

        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        IncludeToFill inc;
        inc.Amount = true;
        inc.Description = true;
        inc.Discount = true;
        inc.Featured = false;
        inc.Id = false;
        inc.Images = true;
        inc.Name = true;
        inc.ShowOnPage = false;
        inc.Price = true;

        return FillOne(table.Rows[0], inc);
    }

    public static List<ProductItem> GetAllProducts()
    {
        DbCommand comm = DataAccess.CreateCommand();
        comm.CommandType = CommandType.Text;

        comm.CommandText = "SELECT * FROM Product";

        DataTable table = DataAccess.ExecuteSelectCommand(comm);

        IncludeToFill inc;
        inc.Amount = true;
        inc.Description = true;
        inc.Discount = true;
        inc.Featured = true;
        inc.Id = true;
        inc.Images = true;
        inc.Name = true;
        inc.ShowOnPage = true;
        inc.Price = true;

        return FillAll(table, inc);
    }


}
