begin
  SELECT concat('INSERT INTO [dbo].[PageContext]([IsSystem],[PageId],[Entity],[IdParameter],[CreatedDateTime],[Guid])
     VALUES(1
           ,(select [Id] from [Page] where [Guid] = ''' , [p].[Guid]  ,  ''')
           ,''', [pc].[Entity] ,'''
           ,''', [pc].[IdParameter] ,'''
           ,SYSDATETIME()
           ,''', NEWID() ,''')') [MigrateUp]
  FROM [dbo].[PageContext] [pc]
  join [Page] [p]
  on [p].[Id] = [pc].[PageId]
  where [pc].[IsSystem] = 0

  SELECT CONCAT('DELETE FROM [dbo].[PageContext] where [Guid] = ''', [Guid], '''') [MigrateDown] 
  from dbo.PageContext where IsSystem = 0

end

