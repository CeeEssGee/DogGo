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

SELECT Dog.Id as 'DogId', Dog.[Name] as 'DogName', Dog.Breed, Dog.OwnerId, [Owner].[Name] as 'OwnerName'
                        FROM Dog
                        JOIN [Owner] ON Dog.OwnerId = [Owner].Id

SELECT Dog.Id as 'DogId', Dog.[Name] as 'DogName', Dog.Breed, Dog.ImageUrl, Dog.Notes, Dog.OwnerId, [Owner].[Name] as 'OwnerName'
    FROM Dog
    JOIN [Owner] ON Dog.OwnerId = [Owner].Id
    WHERE Dog.Id = 1

    SELECT Id, Name, Breed, Notes, ImageUrl, OwnerId 
                FROM Dog
                WHERE OwnerId = 1

SELECT Id, Name FROM Neighborhood

