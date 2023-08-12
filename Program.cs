using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using NorthwindEfCore.Controllers;
using NorthwindEfCore.Models;

namespace NorthwindEfCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //install - package Microsoft.EntityFrameworkCore;
            //install - package Microsoft.EntityFrameworkCore.SqlServer
            //install - package Microsoft.EntityFrameworkCore.Tools
            // Scaffold - DbContext "Server=.\SQLEXPRESS ; Database=Northwind; Integrated Security=True; TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer - outputDir Models - f

            //NorthwindContext context = new NorthwindContext();
            //List<Customer>customerList = context.Customers.ToList();
            //foreach (Customer customer in customerList)
            //{
            //    Console.WriteLine(customer.CompanyName);
            //}

            //List<Category> categories = context.Categories.ToList();
            //foreach (Category category in categories)
            //{
            //    Console.WriteLine(category.CategoryId +" "+ category.CategoryName);
            //}

            //Console.WriteLine("Lütfen güncellemek istediğiniz kategorının Id sını giriniz");
            //int categoryId = int.Parse(Console.ReadLine());
            //Category asil = categories.FirstOrDefault(c => c.CategoryId == categoryId);
            //Console.WriteLine("Lütfen yeni katagori adını giriniz");
            //asil.CategoryName = Console.ReadLine();
            //CategoryController.Update(asil);



            //List<Category> categories = context.Categories.Where(c => c.Products.Count > 10).ToList();

            //Console.WriteLine($"10'dan fazla urun iceren kategoriler: ");

            //foreach (Category category in categories)
            //{
            //    Console.WriteLine($"{category.CategoryId}-{category.CategoryName}");
            //}




            //List<Category> categories = context.Categories.Where(c => c.Products.Count >10).ToList();

            //foreach (var category in categories)
            //{
            //    Console.WriteLine(category.CategoryName + "  " + category.Products.Count);

            //}





            //List<Product> products = context.Products.ToList();

            //foreach (var product in products)
            //{
            //    Console.WriteLine(product.QuantityPerUnit);
            //}


            //List<OrderDetail> orderDetails =context.OrderDetails.Where(o=>o.Quantity>10).ToList();

            //foreach (var orderDetail in orderDetails)
            //{
            //    List<Product> products = context.Products.ToList();
            //    Console.WriteLine( orderDetail.Quantity);
            //}

            //using (NorthwindContext context = new NorthwindContext())
            //{

            //    var categories = context.Categories
            //                    .Where(i => i.Products.Count() > 10)
            //                    .Select(i => new
            //                    {
            //                        i.CategoryName,
            //                        i.Products.Count,
            //                    }).ToList();

            //    foreach (var category in categories)
            //    {
            //        Console.WriteLine(category.CategoryName + "   " + category.Count);
            //    }

            //}




            //NorthwindContext context = new NorthwindContext();
            //List<Category> categories = context.Categories
            //                        .Where(i => i.Products.Count() > 10).ToList();

            //foreach (var category in categories)
            //{
            //    Console.WriteLine(category.CategoryName + "  " + category.Products.Count);
            //}



            NorthwindContext context = new NorthwindContext();

            // 1.soru => SELECT CategoryName , Description FROM Categories ORDER BY CategoryName

            var result = context.Categories.OrderBy(c => c.CategoryName).ToList();

            var result2 = context.Categories.Select(c => new { c.CategoryName, c.Description }).OrderBy(c => c.CategoryName);

            var result3 = context.Categories.Select(c => new Category { CategoryName = c.CategoryName + "Kategorisi", Description = c.Description }).OrderBy(c => c.CategoryName);

            // 3.soru SELECT * FROM Employees ORDER BY HireDate DESC

            var result4 = context.Employees.Select(e => new { e.FirstName, e.LastName, e.HireDate }).OrderBy(e => e.HireDate).OrderByDescending(e => e.HireDate);

            var result5 = context.Employees.Select(e => new { e.FirstName, e.LastName, e.HireDate }).OrderBy(e => e.HireDate).OrderByDescending(e => e.HireDate).Skip(2).Take(5);

            // 5. soru SELECT*FROM Suppliers Order By Country desc, CompanyName asc
            var result6 = context.Suppliers.OrderByDescending(s => s.Country).ThenBy(s => s.CompanyName);

            // 6.SORU SELECT*FROM Customer WHERE City='Buenos Aires'
            var result7 = context.Customers.Where(c => c.City == "Buenos aires").ToList();
            var result8 = context.Customers.Where(c => c.City.Contains("Buenos aires")).ToList();

            //7.soru SELECT*FROM Products WHERE UnitsInStock=0

            var result9 = context.Products.Where(c => c.UnitsInStock == 0).ToList();

            //Console.WriteLine(result9.Count);

            // 8.soru

            var result10 = context.Orders.Where(o => o.OrderDate == DateTime.Parse("1997-05-19")).ToList();

            //9 SELECT*FROM Employees Where Country !='USA'

            var result11 = context.Employees.Where(e => e.Country != "USA").ToList();

            // 11 SELECT * FROM Customer WHERE City LIKE 'A%' OR City LIKE 'B%'

            var result12 = context.Customers.Where(c => c.City.StartsWith("A") || c.City.StartsWith("B"));

            // 14 SELECT * FROM Customer WHERE Fax IS NOT NULL

            var result13 = context.Customers.Where(c => c.Fax != null).ToList();

            // 15 SELECT * FROM Employess WHERE ReportsTo is null

            var result14 = context.Employees.Where(c => c.ReportsTo == null).ToList();

            // 19.soru SELECT*FROM Products WHERE SupplierID IN (SELECT SupplierID FROM Suppliers WHERE CompanyName In('Tokya Traders','Exotic Liquids')

            var result15 = context.Products.Where(p => context.Suppliers
            .Where(s => s.CompanyName == "Exotic Liquids" || s.CompanyName == "Tokyo Traders")
                        .Select(s => s.SupplierId).Any(x => x == p.SupplierId)).ToList();

            // 33.soru SELECT*FROM Orders o JOIN Employees e ON e.EmployeeID=o.EmployeeID WHERE ShippedDate>RequıredDate
            // 

            //var result16 = context.Orders
            //    .Where(o => o.ShippedDate > o.RequiredDate)
            //    .Join(context.Employees, o => o.EmployeeId, e => e.EmployeeId,
            //    (order, employee) => new
            //    {
            //        order.OrderId,
            //        Name = employee.FirstName + " " + employee.LastName,
            //    }
            //    ).ToList();


            //foreach (var item in result16)
            //{
            //    Console.WriteLine(item.OrderId + "-" + item.Name);
            //}

            //Console.WriteLine("Toplam sonuç = " + result16.Count);

            //34.soru SELECT p.ProductName,SUM(od.Quantity) FROM Products p JOIN OderDetails od ON od.ProductID=p.ProductID
            //GROUP BY p.ProductName
            // HAVING SUM(o.Quantity) < 200

            //var result17 = context.Products.GroupJoin(context.OrderDetails, p => p.ProductId, od => od.ProductId,
            //    (product, orderDetails) => new
            //    {
            //        product.ProductName,
            //        TotalUnits = orderDetails.Sum(od => od.Quantity)
            //    }
            //    ).Where(x => x.TotalUnits < 200);

            //foreach (var item in result17)
            //{
            //    Console.WriteLine(item.ProductName + "-" + item.TotalUnits);
            //}

            // Ekrana tüm productların categoryname ve suppliernameleri ile getirelim


            ////Kendi Çözümüm
            //var result18 = context.Products.Join(context.Suppliers, p => p.SupplierId, s => s.SupplierId,
            //    (product, suplier) => new
            //    {
            //        product.Category.CategoryName,
            //        Name = suplier.CompanyName,
            //    }
            //    );
            //foreach (var item in result18)
            //{
            //    Console.WriteLine(item.CategoryName + " "+ item.Name);
            //}
            //Console.WriteLine(result18.Count());

            //// Kısa Yol

            //var result19=context.Products.Include(p=>p.Category).Include(p=>p.Supplier);

            //foreach (var item in result19)
            //{
            //    Console.WriteLine(item.Category.CategoryName+" "+item.Supplier.CompanyName);
            //}

           
            ///eski versiyonda ınclude tablonun içini doldurup cagırman gerekiyordu.
            
            var result22 = context.Products.Include(p=>p.Category).Include(p=>p.Supplier).Select(p => new
            {
                UrunAdi = p.ProductName,
                Kategory = p.Category.CategoryName,
                Tedarikci = p.Supplier.CompanyName

            });

            //Yeni versiyonda sistem kolaylık saglamış include ile doldurmadanda çagırabilirsin.

            var result20 = context.Products.Select(p => new
            {
                UrunAdi = p.ProductName,
                Kategory = p.Category.CategoryName,
                Tedarikci = p.Supplier.CompanyName

            });

            foreach (var item in result20)
            {
                Console.WriteLine(item.UrunAdi + "\t" + item.Kategory + "\t" + item.Tedarikci);
            }

            ////Hocanın esas istediği Çözüm
            // 2 joınlı çözüm

            var result21 = context.Products.Join(context.Categories, p => p.CategoryId, c => c.CategoryId, (product, Category) => new
            {
                product.ProductName,
                Category.CategoryName,
                product.SupplierId
            }).Join(context.Suppliers, pc => pc.SupplierId, s => s.SupplierId, (productCategory, supplier) => new
            {
               UrunAdi= productCategory.ProductName,
               Kategory= productCategory.CategoryName,
                Tedarikci= supplier.CompanyName
            });

            foreach (var item in result21)
            {
                Console.WriteLine(item.UrunAdi + "\t" + item.Kategory + "\t" + item.Tedarikci);
            }

        }
    }
}