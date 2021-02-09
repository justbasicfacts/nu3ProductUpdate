using nu3ProductUpdate.Classes;
using nu3ProductUpdate.Data.Classes;
using nu3ProductUpdate.Models;
using System;

namespace nu3ProductUpdate.Tests
{
    public static class MockData
    {
        public static Product[] ProductsList
        {
            get
            {
                var productList = new Product[] {
                new Product {
                    Amount = 10,
                    Bodyhtml = "",
                    CreatedAt = DateTime.MinValue,
                    Handle = "test1", Id = 0,
                    Image = new Image{ Alt="alt", CreateDate= DateTime.MinValue, Height=1, Id=1, Position=0, ProductId=1, Src="", UpdatedAt= DateTime.MinValue, Width=1 },
                    Location = "BERLIN",
                    Producttype = "",
                    PublishedScope = "",
                    Tags = "", Title = "",
                    Vendor = "" }
                ,
                new Product {  Amount = 10,
                    Bodyhtml = "",
                    CreatedAt = DateTime.MinValue,
                    Handle = "test1", Id = 1,
                    Image = new Image{ Alt="alt", CreateDate= DateTime.MinValue, Height=1, Id=1, Position=0, ProductId=1, Src="", UpdatedAt= DateTime.MinValue, Width=1 },
                    Location = "BERLIN",
                    Producttype = "",
                    PublishedScope = "",
                    Tags = "", Title = "",
                    Vendor = ""} };

                return productList;
            }
        }

        public static CustomFileInfo[] FilesList
        {
            get
            {
                return new CustomFileInfo[] {
                    new CustomFileInfo { Id = "test", Chunks = 1, Filename = "test.xml", Length = 0, MimeType = "text/xml", UploadDate = DateTime.MinValue },
                    new CustomFileInfo { Id = "test2", Chunks = 1, Filename = "test2.xml", Length = 0, MimeType = "text/xml", UploadDate = DateTime.MinValue },
                    new CustomFileInfo { Id = "test3", Chunks = 1, Filename = "test3.xml", Length = 0, MimeType = "text/xml", UploadDate = DateTime.MinValue },
                };
            }
        }
    }
}