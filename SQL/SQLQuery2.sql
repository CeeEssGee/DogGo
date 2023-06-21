SELECT * FROM Dog
SELECT * FROM Neighborhood
SELECT * FROM Owner
SELECT * FROM Walker
SELECT * FROM Walks


SELECT [Owner].Id AS 'OwnerId', [Owner].Name AS 'OwnerName', [Address], Email, Phone, NeighborhoodId, Neighborhood.Name as 'NeighborhoodName'
                        FROM [Owner]
                        JOIN Neighborhood ON [Owner].Id = Neighborhood.Id

SELECT [Owner].Id AS 'OwnerId', [Owner].[Name] AS 'OwnerName', Email, Phone, NeighborhoodId, Neighborhood.Name as 'NeighborhoodName'
    FROM [Owner]
    JOIN Neighborhood ON [Owner].Id = Neighborhood.Id
    WHERE [Owner].Id = 1

    SELECT Walker.Id, Walker.[Name], Walker.ImageUrl, NeighborhoodId, Neighborhood.Name as 'NeighborhoodName'
                        FROM Walker
                        JOIN Neighborhood ON Walker.NeighborhoodId = Neighborhood.Id

SELECT [Owner].Id AS 'OwnerId', [Owner].Name AS 'OwnerName', [Address], Email, Phone, NeighborhoodId, Neighborhood.Name as 'NeighborhoodName'
    FROM [Owner]
    JOIN Dog ON [Owner].Id = Dog.OwnerId
    JOIN Neighborhood ON [Owner].NeighborhoodId = Neighborhood.Id

  SELECT [Owner].Id AS 'OwnerId', [Owner].Name AS 'OwnerName', [Address], Email, Phone, NeighborhoodId, Neighborhood.Name as 'NeighborhoodName'
                        FROM [Owner]
                        JOIN Neighborhood ON [Owner].Id = Neighborhood.Id
                        WHERE [Owner].Id =1

SELECT Walker.Id as 'WalkerId', Walker.[Name], Walker.ImageUrl, NeighborhoodId, Neighborhood.[Name] as 'NeighborhoodName'
                        FROM Walker
                        JOIN Neighborhood ON Walker.NeighborhoodId = Neighborhood.Id
                        WHERE Walker.Id = 1
