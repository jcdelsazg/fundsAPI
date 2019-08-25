USE [funds]

Declare @Id int
Set @Id = 3

While @Id <= 10000
Begin 
   Insert Into Valuefund values (SYSDATETIME(),@Id, @Id)
   Print @Id
   Set @Id = @Id + 1
End

