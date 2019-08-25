USE [funds]

Declare @Id int
Set @Id = 3

While @Id <= 10000
Begin 
   Insert Into funds values ('Fund - ' + CAST(@Id as nvarchar(10)),
              'Description Fund - ' + CAST(@Id as nvarchar(10)))
   Print @Id
   Set @Id = @Id + 1
End

