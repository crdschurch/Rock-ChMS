/* List each Entity's Attributes' Values and which Entity ID it is associated with */
SELECT isnull([e].[Name], 'Global') [EntityName],
       [a].[Key] [AttributeKey],
       [a].[EntityTypeQualifierColumn],
       [a].[EntityTypeQualifierValue],
       isnull(cast([v].[EntityId] as nvarchar(100)), 'n/a') [Entity's Id Value],
       [v].[Id] [AttributeValueId],
       [a].[Guid] [AttributeGuid],
       [ft].[Guid] [FieldTypeGuid],
       [v].[Value] [Value],
       case e.Name
         when 'Rock.Model.Block' then b.Guid
         else null
       end 'Block.Guid'
  FROM [AttributeValue] [v]
  join [Attribute] [a] on [a].[Id] = [v].[AttributeId]
  left join [EntityType] [e] on [e].[Id] = [a].[EntityTypeId]
  join [FieldType] [ft] on [ft].[Id] = [a].[FieldTypeId]
  left join [Block] [b] on b.Id = [v].[EntityId]
order by [AttributeValueId], [EntityName], [AttributeKey]
  

