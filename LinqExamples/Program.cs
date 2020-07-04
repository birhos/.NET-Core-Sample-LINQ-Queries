using System;
using System.Linq;
using LinqExamples.Models;

namespace LinqExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            using (NorthwindContext db = new NorthwindContext())
            {
                // ************************** ************************** ************************** ************************** ************************** 
                // db deki product tablomdaki tüm verileri listeye ata
                //List<Products> q1 = db.Products.ToList();

                /* :: Çalışan Sorgu ::        
                      SELECT [p].[ProductID], [p].[CategoryID], [p].[Discontinued], [p].[ProductName], [p].[QuantityPerUnit], 
                             [p].[ReorderLevel], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock], [p].[UnitsOnOrder]
                      FROM[Products] AS[p]

                */

                // ************************** ************************** ************************** ************************** ************************** 
                // Ürünleri İsme Göre Sırala (ASC) ve Listeme Ata
                //List<Products> q2 = db.Products.OrderBy(p => p.ProductName).ToList();

                /* :: Çalışan Sorgu :: 
                    SELECT [p].[ProductID], [p].[CategoryID], [p].[Discontinued], [p].[ProductName], [p].[QuantityPerUnit], 
                           [p].[ReorderLevel], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock], [p].[UnitsOnOrder]
                    FROM [Products] AS [p]
                    ORDER BY [p].[ProductName]

                */

                //List<Products> q3 = db.Products.OrderByDescending(p => p.ProductName).ToList();

                /* :: Çalışan Sorgu :: 
                    SELECT [p].[ProductID], [p].[CategoryID], [p].[Discontinued], [p].[ProductName], [p].[QuantityPerUnit], 
                           [p].[ReorderLevel], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock], [p].[UnitsOnOrder]
                    FROM [Products] AS [p]
                    ORDER BY [p].[ProductName] DESC

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // Ürünleri Tersten Listele ve ilk 5 Ürünü Yakalayıp Listeme Ata
                //List<Products> q4 = db.Products.OrderByDescending(p => p.ProductName).Take(5).ToList();

                /* :: Çalışan Sorgu :: 
                    exec sp_executesql N'SELECT TOP(@__p_0) [p].[ProductID], [p].[CategoryID], [p].[Discontinued], [p].[ProductName], [p].[QuantityPerUnit], 
                                                            [p].[ReorderLevel], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock], [p].[UnitsOnOrder]
                    FROM [Products] AS [p]
                    ORDER BY [p].[ProductName] DESC',N'@__p_0 int',@__p_0=5

                */
                // ************************** ************************** ************************** ************************** ************************** 
                //id si 5 olan ürün yakala. Şartı Sağlayan İlk Değer Gelir Yada Sağlamazsa Veri Tipinin Varsayılan Değeri Gelir. Örneğin; NULL.
                //Products q5 = db.Products.FirstOrDefault(p => p.ProductId == 25);

                /* :: Çalışan Sorgu :: 
                    SELECT TOP(1) [p].[ProductID], [p].[CategoryID], [p].[Discontinued], [p].[ProductName], [p].[QuantityPerUnit], 
                                  [p].[ReorderLevel], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock], [p].[UnitsOnOrder]
                    FROM [Products] AS [p]
                    WHERE [p].[ProductID] = 5

                */
                // ************************** ************************** ************************** ************************** ************************** 
                //// id si 3 olan ürünün ADINI ver
                //var q6 = db.Products.FirstOrDefault(p => p.ProductId == 3)?.ProductName;

                /* :: Çalışan Sorgu :: 
                    SELECT TOP(1) [p].[ProductID], [p].[CategoryID], [p].[Discontinued], [p].[ProductName], [p].[QuantityPerUnit], 
                                  [p].[ReorderLevel], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock], [p].[UnitsOnOrder]
                    FROM [Products] AS [p]
                    WHERE [p].[ProductID] = 3

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // id si 3 olan ürünün ADINI ver (OPTİMİZE SORGU)
                //var q7 = db.Products.Where(x => x.ProductId == 3)
                //    .Select(x => new Products()
                //    {
                //        ProductName = x.ProductName
                //    }).ToList();

                /* :: Çalışan Sorgu :: 
                    SELECT[x].[ProductName]
                    FROM[Products] AS[x]
                    WHERE[x].[ProductID] = 3

                */

                //var q8 = db.Products.Where(x => x.ProductId == 3)
                //    .Select(x => new Products()
                //    {
                //        ProductName = x.ProductName,
                //        UnitPrice = x.UnitPrice
                //    }).ToList();

                /* :: Çalışan Sorgu :: 
                    SELECT[x].[ProductName], [x].[UnitPrice]
                    FROM[Products] AS[x]
                    WHERE[x].[ProductID] = 3

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // id si 3 olan ürünün ADINI ve SUpplier Adres ve Phone'unu GEtir. 
                //var q9 = db.Products.Where(x => x.ProductId == 3)
                //    .Select(x => new Products()
                //    {
                //        ProductName = x.ProductName,
                //        UnitPrice = x.UnitPrice,
                //        Supplier = new Suppliers()
                //        {
                //            Address = x.Supplier.Address,
                //            Phone = x.Supplier.Phone
                //        }

                //    }).ToList();

                /* :: Çalışan Sorgu :: (Product Tablosundaki SupplierID Alanı NULL Olabildiği İçin (Allow Nulls) Sorgu LEFT JOIN Şeklinde Yapıldı.)
                    SELECT [x].[ProductName], [x].[UnitPrice], [x.Supplier].[Address], [x.Supplier].[Phone]
                    FROM [Products] AS [x]
                    LEFT JOIN [Suppliers] AS [x.Supplier] ON [x].[SupplierID] = [x.Supplier].[SupplierID]
                    WHERE [x].[ProductID] = 3

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // Kategori ID si 3 ve Supplier ID si 2 olan ürünleri isme göre sırala ilk 5 imi al
                //List<Products> q10 = db.Products
                //    .Where(p => p.CategoryId == 3 && p.SupplierId == 2)
                //    .OrderBy(p => p.ProductName)
                //    .Take(5)
                //    .ToList();

                /* :: Çalışan Sorgu ::
                    exec sp_executesql N'SELECT TOP(@__p_0) [p].[ProductID], [p].[CategoryID], [p].[Discontinued], [p].[ProductName], [p].[QuantityPerUnit], 
                                                            [p].[ReorderLevel], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock], [p].[UnitsOnOrder]
                    FROM [Products] AS [p]
                    WHERE ([p].[CategoryID] = 3) AND ([p].[SupplierID] = 2)
                    ORDER BY [p].[ProductName]',N'@__p_0 int',@__p_0=5

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // productname abc geçen ürünlerin listesi
                //List<Products> q11 = db.Products.Where(p => p.ProductName.Contains("abc")).ToList();

                /* :: Çalışan Sorgu ::
                    SELECT [p].[ProductID], [p].[CategoryID], [p].[Discontinued], [p].[ProductName], [p].[QuantityPerUnit], 
                           [p].[ReorderLevel], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock], [p].[UnitsOnOrder]
                    FROM [Products] AS [p]
                    WHERE CHARINDEX(N'abc', [p].[ProductName]) > 0

                */

                // product name haydar ile BAŞLAYAN ürünlerin listesi
                //List<Products> q12 = db.Products.Where(p => p.ProductName.StartsWith("haydar")).ToList();

                /* :: Çalışan Sorgu ::
                    SELECT [p].[ProductID], [p].[CategoryID], [p].[Discontinued], [p].[ProductName], [p].[QuantityPerUnit], 
                           [p].[ReorderLevel], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock], [p].[UnitsOnOrder]
                    FROM [Products] AS [p]
                    WHERE [p].[ProductName] LIKE N'haydar' + N'%' AND (LEFT([p].[ProductName], LEN(N'haydar')) = N'haydar')

                */

                // productname haydar ile BİTEN ürünlerin listesi
                //List<Products> q13 = db.Products.Where(p => p.ProductName.EndsWith("haydar")).ToList();

                /* :: Çalışan Sorgu ::
                    SELECT [p].[ProductID], [p].[CategoryID], [p].[Discontinued], [p].[ProductName], [p].[QuantityPerUnit], [p].[ReorderLevel], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock], [p].[UnitsOnOrder]
                    FROM [Products] AS [p]
                    WHERE RIGHT([p].[ProductName], LEN(N'haydar')) = N'haydar'

                */
                // ************************** ************************** ************************** ************************** ************************** 
                //Kategori Var Mi
                //bool q14 = db.Categories.Any();

                /* :: Çalışan Sorgu ::
                    SELECT CASE
                        WHEN EXISTS (
                            SELECT 1
                            FROM [Categories] AS [c])
                        THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
                    END

                */
                // ************************** ************************** ************************** ************************** ************************** 
                //Kategori IDsi 5 olan kategori var mı_
                //bool q15 = db.Categories.Any(c => c.CategoryId == 5);

                /* :: Çalışan Sorgu ::
                    SELECT CASE
                        WHEN EXISTS (
                            SELECT 1
                            FROM [Categories] AS [c]
                            WHERE [c].[CategoryID] = 5)
                        THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
                    END

                */
                // ************************** ************************** ************************** ************************** ************************** 
                //İsminde 'ha' içeren ürün var mı? Küçük veya büyük harf farketmez
                //bool q16 = db.Products.Any(p => p.ProductName.Contains("ha"));

                /* :: Çalışan Sorgu ::
                    SELECT CASE
                        WHEN EXISTS (
                            SELECT 1
                            FROM [Products] AS [p]
                            WHERE CHARINDEX(N'ha', [p].[ProductName]) > 0)
                        THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
                    END

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // Ürün dizisine atar
                //Products[] q17 = db.Products.ToArray();

                /* :: Çalışan Sorgu ::
                    SELECT [p].[ProductID], [p].[CategoryID], [p].[Discontinued], [p].[ProductName], [p].[QuantityPerUnit], 
                           [p].[ReorderLevel], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock], [p].[UnitsOnOrder]
                    FROM [Products] AS [p]

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // ürün sayısı
                //int q18 = db.Products.Count();

                /* :: Çalışan Sorgu ::
                    SELECT COUNT(*)
                    FROM [Products] AS [p]

                */
                // ************************** ************************** ************************** ************************** ************************** 
                //product tablomdaki QuantityPerUnit sayısı(tabloda bu kolon eğer null ise toplama eklenmeyecektir)
                //int q19 = db.Products.Count(p => p.QuantityPerUnit != null);

                /* :: Çalışan Sorgu ::
                    SELECT COUNT(*)
                    FROM [Products] AS [p]
                    WHERE [p].[QuantityPerUnit] IS NOT NULL

                */
                // ************************** ************************** ************************** ************************** ************************** 
                //en pahalı ürünüm
                //decimal? q20 = db.Products.Max(a => a.UnitPrice);

                /* :: Çalışan Sorgu ::
                    SELECT MAX([a].[UnitPrice])
                    FROM [Products] AS [a]

                */
                // ************************** ************************** ************************** ************************** ************************** 
                //isme göre sıraladığında ilk 5 ürünü atla kalan ürünleri listele
                //List<Products> q21 = db.Products.OrderBy(p => p.ProductName).Skip(5).ToList();

                /* :: Çalışan Sorgu ::
                    exec sp_executesql N'SELECT [p].[ProductID], [p].[CategoryID], [p].[Discontinued], [p].[ProductName], [p].[QuantityPerUnit], 
                                                [p].[ReorderLevel], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock], [p].[UnitsOnOrder]
                    FROM [Products] AS [p]
                    ORDER BY [p].[ProductName]
                    OFFSET @__p_0 ROWS',N'@__p_0 int',@__p_0=5

                */
                // ************************** ************************** ************************** ************************** ************************** 
                //isme göre sıraladığında ilk 5 ürünü atla 10 ürünü listele
                //List<Products> q22 = db.Products.OrderBy(p => p.ProductName).Skip(5).Take(10).ToList();

                /* :: Çalışan Sorgu ::
                    exec sp_executesql N'SELECT [p].[ProductID], [p].[CategoryID], [p].[Discontinued], [p].[ProductName], [p].[QuantityPerUnit],
                                                [p].[ReorderLevel], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock], [p].[UnitsOnOrder]
                    FROM [Products] AS [p]
                    ORDER BY [p].[ProductName]
                    OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY',
                    N'@__p_0 int,@__p_1 int',
                    @__p_0=5,@__p_1=10

                */
                // ************************** ************************** ************************** ************************** ************************** 
                //order tablosundaki shipcountry kolonuna distinct uygular
                //List<string> q23 = db.Orders.Select(a => a.ShipCountry).Distinct().ToList();

                /* :: Çalışan Sorgu ::
                    SELECT DISTINCT [a].[ShipCountry]
                    FROM [Orders] AS [a]

                */
                // ************************** ************************** ************************** ************************** ************************** 
                //product tablosundaki primary key alanındaki değere eşdeğer product getirir(5 numaralı ProductID //değerini getirir)
                //Products q24 = db.Products.Find(5);

                /* :: Çalışan Sorgu ::
                    exec sp_executesql N'SELECT TOP(1) [e].[ProductID], [e].[CategoryID], [e].[Discontinued], [e].[ProductName], [e].[QuantityPerUnit], 
                                                       [e].[ReorderLevel], [e].[SupplierID], [e].[UnitPrice], [e].[UnitsInStock], [e].[UnitsOnOrder]
                    FROM [Products] AS [e]
                    WHERE [e].[ProductID] = @__get_Item_0',N'@__get_Item_0 int',@__get_Item_0=5

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // Her Bir Siparişin Id'sini, Sipariş Tarihini, Ship Name'ini, Siparişteki Ürünün Miktarı, Ürünün Birim Fiyatını ve Ürün İsmini Getiren Sorgu.
                //var q25 = from o in db.Orders
                //    join od in db.OrderDetails on o.OrderId equals od.OrderId
                //    join p in db.Products on od.ProductId equals p.ProductId
                //    select new Deneme
                //    {
                //        OrderId = o.OrderId,
                //        OrderDate = o.OrderDate,
                //        ShipName = o.ShipName,
                //        Quantity = od.Quantity,
                //        UnitPrice = od.UnitPrice,
                //        ProductName = p.ProductName
                //    };

                //foreach (var v in q25)
                //{
                //    Console.WriteLine(v.ProductName);
                //}

                /* :: Çalışan Sorgu ::
                    SELECT [o].[OrderID], [o].[OrderDate], [o].[ShipName], [od].[Quantity], [od].[UnitPrice], [p].[ProductName]
                    FROM [Orders] AS [o]
                    INNER JOIN [Order Details] AS [od] ON [o].[OrderID] = [od].[OrderID]
                    INNER JOIN [Products] AS [p] ON [od].[ProductID] = [p].[ProductID]

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // Her Bir Kategorinin İsmi ile Birlikte Ürünleri Listele.
                //var q26 = from c in db.Categories
                //          join p in db.Products on c equals p.Category
                //          select new Deneme { CategoryName = c.CategoryName, ProductName = p.ProductName };

                //foreach (var v in q26)
                //{
                //    Console.WriteLine(v.ProductName + ": " + v.CategoryName);
                //}

                /* :: Çalışan Sorgu ::
                    SELECT [c].[CategoryName] AS [CategoryName], [p].[ProductName] AS [ProductName]
                    FROM [Categories] AS [c]
                    INNER JOIN [Products] AS [p] ON [c].[CategoryID] =  (
														                    SELECT TOP(1) [subQuery0].[CategoryID]
														                    FROM [Categories] AS [subQuery0]
														                    WHERE [subQuery0].[CategoryID] = [p].[CategoryID]
													                    )

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // 50 yaşından büyük çalışanların adını, adresini, şehrini ve bölgesini getir.
                //var q27 = from e in db.Employees
                //          where e.BirthDate < DateTime.Today.AddYears(-50)
                //          select new Deneme { FirstName = e.FirstName, LastName = e.LastName, Address = e.Address, City = e.City, Region = e.Region };

                //foreach (var v in q27)
                //{
                //    Console.WriteLine(v.Address);
                //}

                /* :: Çalışan Sorgu ::
                    SELECT [E].[FirstName], [E].[LastName], [E].[Address], [E].[City], [E].[Region]
                    FROM [Employees] AS [E]
                    WHERE [E].[BirthDate] < DATEADD(year, -50, CONVERT(date, GETDATE()))

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // Brüksel'de yaşayan müşterilere 'Speedy Express' şirketi Tarafından gönderilen siparişler için firma ismini ve müşteri ismini getirir..
                //var q28 = from e in db.Employees
                //          join o in db.Orders on e.EmployeeId equals o.EmployeeId
                //          join c in db.Customers on o.CustomerId equals c.CustomerId
                //          join s in db.Shippers on o.ShipVia equals s.ShipperId
                //          where c.City == "Bruxelles"
                //          where s.CompanyName == "Speedy Express"
                //          select new Deneme { FirstName = e.FirstName, LastName = e.LastName, CompanyName = c.CompanyName };

                //foreach (var v in q28)
                //{
                //    Console.WriteLine($"First Name : {v.FirstName} | Last Name : {v.LastName} | Company Name : {v.CompanyName}" );
                //}

                /* :: Çalışan Sorgu ::
                    SELECT [e].[FirstName], [e].[LastName], [c].[CompanyName]
                    FROM [Employees] AS [e]
                    INNER JOIN [Orders] AS [o] ON [e].[EmployeeID] = [o].[EmployeeID]
                    INNER JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
                    INNER JOIN [Shippers] AS [s] ON [o].[ShipVia] = [s].[ShipperID]
                    WHERE ([c].[City] = N'Bruxelles') AND ([s].[CompanyName] = N'Speedy Express')

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // Gravad Lax veya Mishi Kobe Niku. Ürünlerden en az birini satan çalışanların unvanını ve adını verin
                //var q29 = (from e in db.Employees
                //           from o in db.Orders
                //           from d in db.OrderDetails
                //           join p in db.Products on d.ProductId equals p.ProductId
                //           where p.ProductName == "Gravad Lax" || p.ProductName == "Mishi Kobe Niku"
                //           select new Deneme { Title = e.Title, FirstName = e.FirstName, LastName = e.LastName }).Distinct();

                //foreach (var v in q29)
                //{
                //    Console.WriteLine($"First Name : {v.FirstName} | Last Name : {v.LastName} | Title : {v.Title}");
                //}

                /* :: Çalışan Sorgu ::
                    SELECT DISTINCT [e].[Title], [e].[FirstName], [e].[LastName]
                    FROM [Employees] AS [e]
                    CROSS JOIN [Orders] AS [o]
                    CROSS JOIN [Order Details] AS [d]
                    INNER JOIN [Products] AS [p] ON [d].[ProductID] = [p].[ProductID]
                    WHERE [p].[ProductName] IN (N'Gravad Lax', N'Mishi Kobe Niku')

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // 7'den fazla tedarikçinin ürünlerini satan çalışanların isimlerini verin.
                //var q30 = from e in db.Employees
                //          let nbSuppliers =
                //              (from o in e.Orders
                //               from d in o.OrderDetails
                //               join p in db.Products on d.ProductId equals p.ProductId
                //               select p.SupplierId).Distinct().Count()
                //          where nbSuppliers > 7
                //          select new Deneme  { FirstName = e.FirstName, LastName  = e.LastName };

                //foreach (var v in q30)
                //{
                //    Console.WriteLine($"First Name : {v.FirstName} | Last Name : {v.LastName}");
                //}

                /* :: Çalışan Sorgu ::
                    SELECT [e].[FirstName], [e].[LastName]
                    FROM [Employees] AS [e]
                    WHERE (
                        SELECT COUNT(*)
                        FROM (
                            SELECT DISTINCT [p].[SupplierID]
                            FROM [Orders] AS [o]
                            INNER JOIN [Order Details] AS [o.OrderDetails] ON [o].[OrderID] = [o.OrderDetails].[OrderID]
                            INNER JOIN [Products] AS [p] ON [o.OrderDetails].[ProductID] = [p].[ProductID]
                            WHERE [e].[EmployeeID] = [o].[EmployeeID]
                        ) AS [t]
                    ) > 7

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // Her Bir Çalışanın EmployeeId, Adı, Soyadı ve Toplam Satış Miktarını Getiren Sorgu.
                //var q31 = from E in db.Employees
                //          orderby E.EmployeeId
                //          select new Deneme()
                //          {
                //              EmployeeId = E.EmployeeId,
                //              FirstName = E.FirstName,
                //              LastName = E.LastName,
                //              TotalSales =
                //                  (from O in E.Orders
                //                   from D in O.OrderDetails
                //                   let LineTotal = System.Convert.ToDouble(D.UnitPrice) * D.Quantity * (1 - D.Discount) select LineTotal).Sum()
                //          };

                //foreach (var value in q31)
                //{
                //    Console.WriteLine($"EmployeeId : {value.EmployeeId} | FirstName : {value.FirstName} | LastName : {value.LastName} | TotalSales : {value.TotalSales}");
                //}

                /* :: Çalışan Sorgu ::
                    SELECT [E].[EmployeeID], [E].[FirstName], [E].[LastName], (
                        SELECT SUM((CONVERT(float, [O.OrderDetails].[UnitPrice]) * [O.OrderDetails].[Quantity]) * (CAST(1 AS real) - [O.OrderDetails].[Discount]))
                        FROM [Orders] AS [O]
                        INNER JOIN [Order Details] AS [O.OrderDetails] ON [O].[OrderID] = [O.OrderDetails].[OrderID]
                        WHERE [E].[EmployeeID] = [O].[EmployeeID]
                    ) AS [TotalSales]
                    FROM [Employees] AS [E]
                    ORDER BY [E].[EmployeeID]

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // Müşteri adına ve ürün adına, müşteri tarafından tek bir siparişle satın alınan bu ürünün miktarının, tüm müşteriler arasında tek bir siparişte satın alınan ortalama miktarının 5 katından fazla olduğu şekilde verin.

                //var list1 = (from od in db.OrderDetails
                //            join p in db.Products on od.ProductId equals p.ProductId
                //            select new OrderDetails
                //            {
                //                Quantity = od.Quantity
                //            }).ToList();

                //var ort1 = list1.Average(x => x.Quantity);

                //var q32 = from c in db.Customers
                //          from o in c.Orders
                //          from d in o.OrderDetails
                //          join p in db.Products on d.ProductId equals p.ProductId
                //          orderby c.CompanyName, p.ProductName
                //          where d.Quantity > 5 * ort1
                //          select new Deneme { CompanyName = c.CompanyName, ProductName = p.ProductName };

                //foreach (var v in q32)
                //{
                //    Console.WriteLine($"Company Name : {v.CompanyName} | Product Name : {v.ProductName}");
                //}

                /* :: Çalışan Sorgu ::
                    SELECT [od].[Quantity]
                    FROM [Order Details] AS [od]
                    INNER JOIN [Products] AS [p] ON [od].[ProductID] = [p].[ProductID]

                    exec sp_executesql N'SELECT [c].[CompanyName], [p].[ProductName]
                    FROM [Customers] AS [c]
                    INNER JOIN [Orders] AS [c.Orders] ON [c].[CustomerID] = [c.Orders].[CustomerID]
                    INNER JOIN [Order Details] AS [c.Orders.OrderDetails] ON [c.Orders].[OrderID] = [c.Orders.OrderDetails].[OrderID]
                    INNER JOIN [Products] AS [p] ON [c.Orders.OrderDetails].[ProductID] = [p].[ProductID]
                    WHERE [c.Orders.OrderDetails].[Quantity] > (5.0E0 * @__v1_0)
                    ORDER BY [c].[CompanyName], [p].[ProductName]',N'@__v1_0 smallint',@__v1_0=24
                */
                // ************************** ************************** ************************** ************************** ************************** 
                // Her Bir Çalışanın EmployeeId, Adı, Soyadı ve Sattığı Farklı Ürün Sayısını Getiren Sorgu.
                //var q32 = from E in db.Employees
                //          orderby E.EmployeeId
                //          select new Deneme
                //          {
                //              EmployeeId = E.EmployeeId,
                //              FirstName = E.FirstName,
                //              LastName = E.LastName,
                //              NbProds =
                //                  (from O in E.Orders
                //                   from D in O.OrderDetails
                //                   select D.ProductId).Distinct().Count()
                //          };

                //foreach (var value in q32)
                //{
                //    Console.WriteLine($"EmployeeId : {value.EmployeeId} | FirstName : {value.FirstName} | LastName : {value.LastName} | NbProds : {value.NbProds}");
                //}

                /* :: Çalışan Sorgu ::
                    SELECT [E].[EmployeeID], [E].[FirstName], [E].[LastName], (
                        SELECT COUNT(*)
                        FROM (
                            SELECT DISTINCT [O.OrderDetails].[ProductID]
                            FROM [Orders] AS [O]
                            INNER JOIN [Order Details] AS [O.OrderDetails] ON [O].[OrderID] = [O.OrderDetails].[OrderID]
                            WHERE [E].[EmployeeID] = [O].[EmployeeID]
                        ) AS [t]
                    ) AS [NbProds]
                    FROM [Employees] AS [E]
                    ORDER BY [E].[EmployeeID]

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // Her Bir Kategorinin Adı ve Her Kategorideki Ürünlerin Ortalama Fiyatını Getiren Sorgu.

                var q33 = from P in db.Products
                          join C in db.Categories on P.CategoryId equals C.CategoryId
                          group P by P.Category.CategoryName into categProds
                          select new Deneme
                          {
                              CategoryName = categProds.Key,
                              AvgPrice = categProds.Average(c => c.UnitPrice)
                          };

                foreach (var value in q33)
                {
                    Console.WriteLine($"CategoryName : {value.CategoryName} | AvgPrice : {value.AvgPrice}");
                }

                /* :: Çalışan Sorgu ::
                    SELECT [P.Category].[CategoryName], AVG([P].[UnitPrice]) AS [AvgPrice]
                    FROM [Products] AS [P]
                    LEFT JOIN [Categories] AS [P.Category] ON [P].[CategoryID] = [P.Category].[CategoryID]
                    INNER JOIN [Categories] AS [C] ON [P].[CategoryID] = [C].[CategoryID]
                    GROUP BY [P.Category].[CategoryName]

                */
                // ************************** ************************** ************************** ************************** ************************** 
                // Kimliğini 'LAZYK' olan firma ile tam olarak aynı ürünleri alan müşterilerin ismini verin

                //var q34 = from C in db.Customers
                //    where C.CustomerId != "LAZYK"
                //    let allProdsCustomer =
                //        from O in C.Orders
                //        from D in O.OrderDetails
                //        select D.ProductId
                //    let allProdsLazyk =
                //        from C1 in db.Customers
                //        where C1.CustomerId == "LAZYK"
                //        from O1 in C1.Orders
                //        from D1 in O1.OrderDetails
                //        select D1.ProductId
                //    where !allProdsLazyk.Except(allProdsCustomer).Any()
                //    where !allProdsCustomer.Except(allProdsLazyk).Any()
                //    select C.CompanyName;

            }
            Console.ReadLine();
        }
    }

    class Deneme
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string CompanyName { get; set; }

        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string ShipName { get; set; }
        public short Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Title { get; set; }
        public string Region { get; set; }

        public int EmployeeId { get; set; }
        public double TotalSales { get; set; }
        public int NbProds { get; set; }
        public decimal? AvgPrice { get; set; }

    }
}
